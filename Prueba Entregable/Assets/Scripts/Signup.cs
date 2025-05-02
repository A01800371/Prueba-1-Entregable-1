using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public class Signup : MonoBehaviour
{
    private UIDocument join;
    private Button botonB;
    private Button botonR;
    private TextField tfname;
    private TextField tfsecondname;
    private TextField tfemail;
    private TextField tfpassword;
    private TextField tfage;
    private DropdownField genderDrop;
    private DropdownField countryDrop;
    private const float REQUEST_TIMEOUT = 5f;
    // public struct DatosUsuario
    // {
    //     public string nombre;
    //     public string apellido;
    //     public string correo;
    //     public string contrasena;
    //     public string pais;
    //     public string fecha_nacimiento; // cambiar a fecha de nacimiento
    //     public string genero;
    // }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        join = GetComponent<UIDocument>();
        var root = join.rootVisualElement;

        tfname = root.Q<TextField>("name");
        tfsecondname = root.Q<TextField>("secondname");
        tfemail = root.Q<TextField>("email");
        tfpassword = root.Q<TextField>("password");
        countryDrop = root.Q<DropdownField>("country");
        tfage = root.Q<TextField>("age");
        botonB = root.Q<Button>("Back");
        botonR = root.Q<Button>("Register");
        genderDrop = root.Q<DropdownField>("gender");
        // botonR.RegisterCallback<ClickEvent, String>(Jugar,"Login");
        // botonB.RegisterCallback<ClickEvent, String>(Jugar,"Inicio11");

        ConfigCountry();
        ConfigGender();

        botonR.clicked+=OnBotonRClicked;
        botonB.clicked+=OnBotonBClicked;
        
    }
     private void OnDisable()
    {
        
        botonR.clicked+=OnBotonRClicked;
        botonB.clicked+=OnBotonBClicked;
    }
    private void ConfigGender()
    {
        genderDrop.choices = new List<string>
        {
            "H",
            "M",
            "N"
        };
        genderDrop.value = "Otro"; 
    }
    private void ConfigCountry()
    {
        countryDrop.choices = new List<string>
        {
            "---", // Placeholder
            "ARG", // Argentina
            "CAN", // Canada
            "CHL", // Chile
            "COL", // Colombia
            "ESP", // España
            "USA", // Estados Unidos
            "MEX", // México
            "PER"  // Perú
        };
        countryDrop.value = "Selecciona tu país";
    }
    private void OnBotonBClicked()
    {
        SceneManager.LoadScene("Login");
    }
     private void OnBotonRClicked()
    {
        if (!ValidateForm())
            return;

        StartCoroutine(RegisterUserWithTimeout());
    }
private bool ValidateForm()
    {
        // Validar campos vacíos
        if (string.IsNullOrEmpty(tfname.value))
        {
            Debug.LogError("Por favor ingresa tu nombre");
            return false;
        }

        if (string.IsNullOrEmpty(tfsecondname.value))
        {
            Debug.LogError("Por favor ingresa tu apellido");
            return false;
        }

        if (string.IsNullOrEmpty(tfage.value))
        {
            Debug.LogError("Por favor ingresa tu fecha de nacimiento");
            return false;
        }

        if (string.IsNullOrEmpty(tfemail.value))
        {
            Debug.LogError("Por favor ingresa tu correo electrónico");
            return false;
        }

        if (string.IsNullOrEmpty(tfpassword.value))
        {
            Debug.LogError("Por favor ingresa una contraseña");
            return false;
        }

        // Validar selección de país
        if (countryDrop.value == "Selecciona tu país")
        {
            Debug.LogError("Por favor selecciona un país válido");
            return false;
        }

        // Validar formato de fecha (YYYY-MM-DD)
        if (!Regex.IsMatch(tfage.value, @"^\d{4}-\d{2}-\d{2}$"))
        {
            Debug.LogError("Formato de fecha incorrecto. Debe ser YYYY-MM-DD");
            return false;
        }

        // Validar formato de email
        if (!Regex.IsMatch(tfemail.value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            Debug.LogError("Formato de email inválido. Debe ser ejemplo@dominio.com");
            return false;
        }

        return true;
    }



    private IEnumerator RegisterUserWithTimeout()
    {
        // Mostrar estado de carga
        // ShowAlert("Conectando con el servidor...", false, true);

        // Preparar datos del formulario
        WWWForm formData = new WWWForm();
        formData.AddField("nombre", tfname.value);
        formData.AddField("apellido", tfsecondname.value);
        formData.AddField("fecha_nacimiento", tfage.value);
        formData.AddField("correo", tfemail.value);
        formData.AddField("contrasena", tfpassword.value);
        formData.AddField("pais", countryDrop.value);
        formData.AddField("genero", genderDrop.value);
        // Configurar la solicitud
        using (UnityWebRequest request = UnityWebRequest.Post("http://35.169.93.195:8080/registrar", formData))
        {
            request.timeout = (int)REQUEST_TIMEOUT;
            
            // Iniciar temporizador
            float startTime = Time.time;
            bool requestCompleted = false;
            
            // Enviar la solicitud
            var asyncOperation = request.SendWebRequest();
            
            // Esperar con timeout
            while (!asyncOperation.isDone)
            {
                if (Time.time - startTime >= REQUEST_TIMEOUT)
                {
                    request.Abort();
                    // ShowAlert("Error de conexión: Tiempo de espera agotado. Por favor intenta nuevamente");
                    yield break;
                }
                yield return null;
            }
            // Procesar respuesta
            if (request.result == UnityWebRequest.Result.Success)
            {
                // ShowAlert("¡Registro exitoso! Redirigiendo...", true);
                yield return new WaitForSeconds(0.5f);
                SceneManager.LoadScene("Login");
            }
            else
            {
                Debug.LogError($"Error {request.responseCode}: {request.error}");
                Debug.LogError("Respuesta del servidor: " + request.downloadHandler.text);
            }
        }
    }

    
}

