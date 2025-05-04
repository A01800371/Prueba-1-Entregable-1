/*
 * Autor: Yael Michel García López A01750911
 * 
 * JoinBoton: Controla la interfaz de login del juego
 * Gestiona la autenticación de usuarios mediante una API REST
 */

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class JoinBoton : MonoBehaviour
{
    // Referencia al documento UXML
    private UIDocument join;
    
    // Campos de texto para credenciales
    private TextField tfUsuario;    // Campo para ingresar usuario/correo
    private TextField tfContrasena; // Campo para ingresar contraseña
    
    // Botones de interacción
    private Button botonJ;          // Botón para iniciar sesión
    private Button botonE;          // Botón para salir/regresar

    // Método que se ejecuta cuando el componente se habilita
    void OnEnable()
    {
        // Obtiene el componente UIDocument adjunto
        join = GetComponent<UIDocument>();
        
        // Accede al elemento raíz del documento visual
        var root = join.rootVisualElement;

        // Obtiene referencias a los elementos UI por sus nombres
        botonJ = root.Q<Button>("BotonJoin");     // Botón de ingreso
        botonE = root.Q<Button>("Exit");          // Botón de salida
        tfUsuario = root.Q<TextField>("Usuario"); // Campo de usuario
        tfContrasena = root.Q<TextField>("Contrasena"); // Campo de contraseña

        // Asigna eventos a los botones
        botonE.RegisterCallback<ClickEvent, string>(Jugar, "Inicio11"); // Evento para botón Exit
        botonJ.clicked += EnviarDatosLoginJSON; // Evento para botón Join
    }

    // Valida los campos y envía los datos de login
    private void EnviarDatosLoginJSON()
    {
        // Verifica que los campos no estén vacíos
        if(string.IsNullOrEmpty(tfUsuario.value) || string.IsNullOrEmpty(tfContrasena.value))
        {
            Debug.LogError("Por favor complete todos los campos");
            return;
        }
        
        // Inicia la corrutina para enviar datos al servidor
        StartCoroutine(SubirDatosJSON());
    }
    
    // Envía los datos de login al servidor mediante una petición HTTP GET
    private IEnumerator SubirDatosJSON()
    {
        // Codifica los valores para URL
        string correo = UnityWebRequest.EscapeURL(tfUsuario.value);
        string contrasena = UnityWebRequest.EscapeURL(tfContrasena.value);
        
        // Construye la URL con los parámetros
        string url = $"http://35.169.93.195:8080/juego/login?correo={correo}&contrasena={contrasena}";
        
        // Crea y configura la petición web
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Establece el header para aceptar JSON
            request.SetRequestHeader("Accept", "application/json");
            
            // Log para depuración
            Debug.Log("URL enviada: " + url);
            
            // Envía la petición y espera la respuesta
            yield return request.SendWebRequest();

            // Procesa la respuesta del servidor
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Respuesta del servidor: " + request.downloadHandler.text);

                // Guardar la cookie de sesión
                string cookies = request.GetResponseHeader("Set-Cookie");
                if (!string.IsNullOrEmpty(cookies))
                {
                    PlayerPrefs.SetString("cookies", cookies.Split(';')[0]); // Guarda solo la cookie (sin flags extra)
                    Debug.Log("Cookie guardada: " + PlayerPrefs.GetString("cookies"));
                }

                // Cargar la siguiente escena
                SceneManager.LoadScene("MenuJ");
            }
            else
            {
                // Muestra errores si la petición falla
                Debug.LogError($"Error {request.responseCode}: {request.error}");
                Debug.LogError("Respuesta del servidor: " + request.downloadHandler.text);
            }
        }
    }
    
    // Método para cargar una escena (usado por el botón Exit)
    private void Jugar(ClickEvent evt, string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
