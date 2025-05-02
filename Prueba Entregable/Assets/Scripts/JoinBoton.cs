// using System;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.Networking;
// using UnityEngine.SceneManagement;
// using UnityEngine.UIElements;

// public class JoinBoton : MonoBehaviour
// {
//     private UIDocument join;
//     private TextField tfUsuario;
//     private TextField tfContrasena;
   
//     private Button botonJ;
//     private Button botonE;
//     public struct DatosUsuario
//     {
//         public string usuario;
//         public string constrasena;
//     }
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void OnEnable()
//     {
//         join = GetComponent<UIDocument>();
//         var root = join.rootVisualElement;

//         botonJ = root.Q<Button>("BotonJoin");
//         botonE = root.Q<Button>("Exit");
//         tfUsuario = root.Q<TextField>("Usuario");
//         tfContrasena = root.Q<TextField>("Contrasena");
//         botonE.RegisterCallback<ClickEvent, String>(Jugar,"Inicio11");
//         botonJ.clicked+=EnviarDatosLoginJSON;
//     }

//     private void EnviarDatosLoginJSON()
//     {
//         StartCoroutine(SubirDatosJSON());
//     }
//     private IEnumerator SubirDatosJSON()
//     {
//         DatosUsuario datos;
//         datos.usuario = tfUsuario.value;
//         datos.constrasena = tfContrasena.value;

//         string datosJSON = JsonUtility.ToJson(datos);

//         UnityWebRequest request = UnityWebRequest.Post("http://35.169.93.195:8080", datosJSON, "/juego/login");

//         yield return request.SendWebRequest();

//         // Después de un rato
//         if (request.result == UnityWebRequest.Result.Success)
//         {
//             print("\nConexión exitosa.\n\n");
//             SceneManager.LoadScene("MenuJ");
//         }
//         else {
//             print("Error: " + request.responseCode);
//         }

//         request.Dispose();
//     }
//     private void Jugar(ClickEvent evt, String nombreEscena)
//     {
//         SceneManager.LoadSceneAsync(nombreEscena);
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }

// using System;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.Networking;
// using UnityEngine.SceneManagement;
// using UnityEngine.UIElements;

// public class JoinBoton : MonoBehaviour
// {
//     private UIDocument join;
//     private TextField tfUsuario;
//     private TextField tfContrasena;
   
//     private Button botonJ;
//     private Button botonE;

//     public struct DatosUsuario
//     {
//         public string usuario;
//         public string contrasena;
//     }

//     void OnEnable()
//     {
//         join = GetComponent<UIDocument>();
//         var root = join.rootVisualElement;

//         botonJ = root.Q<Button>("BotonJoin");
//         botonE = root.Q<Button>("Exit");
//         tfUsuario = root.Q<TextField>("Usuario");
//         tfContrasena = root.Q<TextField>("Contrasena");

//         botonE.RegisterCallback<ClickEvent, String>(Jugar, "Inicio11");
//         botonJ.clicked += EnviarDatosLoginJSON;
//     }

//     private void EnviarDatosLoginJSON()
//     {
//         StartCoroutine(SubirDatosJSON());
//     }

//     private IEnumerator SubirDatosJSON()
//     {
//         DatosUsuario datos;
//         datos.usuario = tfUsuario.value;
//         datos.contrasena = tfContrasena.value;

//         // Construir la URL con parámetros codificados
//         string url = $"http://35.169.93.195:8080/juego/login?correo={UnityWebRequest.EscapeURL(datos.usuario)}&contrasena={UnityWebRequest.EscapeURL(datos.contrasena)}";

//         UnityWebRequest request = UnityWebRequest.Get(url);

//         yield return request.SendWebRequest();

//         if (request.result == UnityWebRequest.Result.Success)
//         {
//             Debug.Log("\nConexión exitosa.\n\n");
//             SceneManager.LoadScene("MenuJ");
//         }
//         else
//         {
//             Debug.Log("Error: " + request.responseCode);
//         }

//         request.Dispose();
//     }

//     private void Jugar(ClickEvent evt, String nombreEscena)
//     {
//         SceneManager.LoadSceneAsync(nombreEscena);
//     }

//     void Update()
//     {
//         // No se necesita lógica por ahora
//     }
// }

// using System.Collections;
// using UnityEngine;
// using UnityEngine.Networking;
// using UnityEngine.SceneManagement;
// using UnityEngine.UIElements;

// public class JoinBoton : MonoBehaviour
// {
//     private UIDocument join;
//     private TextField tfUsuario;
//     private TextField tfContrasena;
//     private Button botonJ;
//     private Button botonE;
    
//     [System.Serializable]
//     public struct DatosUsuario
//     {
//         public string correo;
//         public string contrasena;
//     }

//     void OnEnable()
//     {
//         join = GetComponent<UIDocument>();
//         var root = join.rootVisualElement;

//         botonJ = root.Q<Button>("BotonJoin");
//         botonE = root.Q<Button>("Exit");
//         tfUsuario = root.Q<TextField>("Usuario");
//         tfContrasena = root.Q<TextField>("Contrasena");
//         botonE.RegisterCallback<ClickEvent, string>(Jugar, "Inicio11");
//         botonJ.clicked += EnviarDatosLoginJSON;
//     }

//     private void EnviarDatosLoginJSON()
//     {
//         if(string.IsNullOrEmpty(tfUsuario.value) || string.IsNullOrEmpty(tfContrasena.value))
//         {
//             Debug.LogError("Por favor complete todos los campos");
//             return;
//         }
        
//         StartCoroutine(SubirDatosJSON());
//     }
    
//     private IEnumerator SubirDatosJSON()
//     {
//         DatosUsuario datos = new DatosUsuario
//         {
//             correo = tfUsuario.value,
//             contrasena = tfContrasena.value
//         };

//         string datosJSON = JsonUtility.ToJson(datos);
//         string url = "http://35.169.93.195:8080/juego/login";
        
//         // Opción 1: Enviar como parámetro GET
//         string urlCompleta = $"{url}?data={UnityWebRequest.EscapeURL(datosJSON)}";
        
//         // Opción 2: Alternativa enviando parámetros individuales (prueba esta si la opción 1 falla)
//         // string urlCompleta = $"{url}?correo={UnityWebRequest.EscapeURL(datos.correo)}&contrasena={UnityWebRequest.EscapeURL(datos.contrasena)}";
        
//         using (UnityWebRequest request = UnityWebRequest.Get(urlCompleta))
//         {
//             request.SetRequestHeader("Content-Type", "application/json");
//             request.SetRequestHeader("Accept", "application/json");
            
//             // Para debugging - muestra la URL que se está enviando
//             Debug.Log("URL enviada: " + urlCompleta);
//             Debug.Log("JSON enviado: " + datosJSON);

//             yield return request.SendWebRequest();

//             if (request.result == UnityWebRequest.Result.Success)
//             {
//                 Debug.Log("Respuesta del servidor: " + request.downloadHandler.text);
//                 SceneManager.LoadScene("MenuJ");
//             }
//             else
//             {
//                 Debug.LogError($"Error {request.responseCode}: {request.error}");
//                 Debug.LogError("Respuesta del servidor: " + request.downloadHandler.text);
//             }
//         }
//     }
    
//     private void Jugar(ClickEvent evt, string nombreEscena)
//     {
//         SceneManager.LoadScene(nombreEscena);
//     }
// }
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class JoinBoton : MonoBehaviour
{
    private UIDocument join;
    private TextField tfUsuario;
    private TextField tfContrasena;
    private Button botonJ;
    private Button botonE;
    
    void OnEnable()
    {
        join = GetComponent<UIDocument>();
        var root = join.rootVisualElement;

        botonJ = root.Q<Button>("BotonJoin");
        botonE = root.Q<Button>("Exit");
        tfUsuario = root.Q<TextField>("Usuario");
        tfContrasena = root.Q<TextField>("Contrasena");
        botonE.RegisterCallback<ClickEvent, string>(Jugar, "Inicio11");
        botonJ.clicked += EnviarDatosLoginJSON;
    }

    private void EnviarDatosLoginJSON()
    {
        if(string.IsNullOrEmpty(tfUsuario.value) || string.IsNullOrEmpty(tfContrasena.value))
        {
            Debug.LogError("Por favor complete todos los campos");
            return;
        }
        
        StartCoroutine(SubirDatosJSON());
    }
    
    private IEnumerator SubirDatosJSON()
    {
        // Codificar los valores individualmente
        string correo = UnityWebRequest.EscapeURL(tfUsuario.value);
        string contrasena = UnityWebRequest.EscapeURL(tfContrasena.value);
        
        // Construir URL con parámetros individuales
        string url = $"http://35.169.93.195:8080/juego/login?correo={correo}&contrasena={contrasena}";
        
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Accept", "application/json");
            
            Debug.Log("URL enviada: " + url);
            
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Respuesta del servidor: " + request.downloadHandler.text);
                SceneManager.LoadScene("MenuJ");
            }
            else
            {
                Debug.LogError($"Error {request.responseCode}: {request.error}");
                Debug.LogError("Respuesta del servidor: " + request.downloadHandler.text);
            }
        }
    }
    
    private void Jugar(ClickEvent evt, string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}