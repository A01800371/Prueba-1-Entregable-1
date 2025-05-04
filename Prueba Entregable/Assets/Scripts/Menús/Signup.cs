/*
 * Autor: Yael Michel García López A01750911
 * 
 * Signup: Controlador para el formulario de registro de usuarios
 * 
 * Funcionalidades:
 * - Validación de campos del formulario
 * - Configuración de dropdowns para país y género
 * - Envío seguro de datos al servidor con timeout
 * - Manejo de respuestas del servidor
 */

using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Signup : MonoBehaviour
{
    // Elementos UI
    private UIDocument join;
    private Button botonB;          // Botón para regresar
    private Button botonR;          // Botón de registro
    private TextField tfname;       // Campo para nombre
    private TextField tfsecondname; // Campo para apellido
    private TextField tfemail;      // Campo para email
    private TextField tfpassword;   // Campo para contraseña
    private TextField tfage;        // Campo para fecha nacimiento
    private DropdownField genderDrop; // Dropdown para género
    private DropdownField countryDrop; // Dropdown para país

    // Configuración
    private const float REQUEST_TIMEOUT = 5f; // Tiempo máximo para esperar respuesta del servidor

    void OnEnable()
    {
        // Obtener referencias UI
        join = GetComponent<UIDocument>();
        var root = join.rootVisualElement;

        // Asignar elementos por nombre
        tfname = root.Q<TextField>("name");
        tfsecondname = root.Q<TextField>("secondname");
        tfemail = root.Q<TextField>("email");
        tfpassword = root.Q<TextField>("password");
        countryDrop = root.Q<DropdownField>("country");
        tfage = root.Q<TextField>("age");
        botonB = root.Q<Button>("Back");
        botonR = root.Q<Button>("Register");
        genderDrop = root.Q<DropdownField>("gender");

        // Configurar dropdowns
        ConfigCountry();
        ConfigGender();

        // Asignar eventos
        botonR.clicked += OnBotonRClicked;
        botonB.clicked += OnBotonBClicked;
    }

    private void OnDisable()
    {
        // Limpiar eventos al desactivar
        botonR.clicked -= OnBotonRClicked;
        botonB.clicked -= OnBotonBClicked;
    }

    /// <summary>
    /// Configura las opciones del dropdown de género
    /// </summary>
    private void ConfigGender()
    {
        genderDrop.choices = new List<string> { "H", "M", "N" };
        genderDrop.value = "N";
    }

    /// <summary>
    /// Configura las opciones del dropdown de país
    /// </summary>
    private void ConfigCountry()
    {
        countryDrop.choices = new List<string>
        {
            "---", "ARG", "CAN", "CHL", "COL", 
            "ESP", "USA", "MEX", "PER"
        };
        countryDrop.value = "Selecciona tu país";
    }

    /// <summary>
    /// Maneja el clic en el botón de regresar
    /// </summary>
    private void OnBotonBClicked()
    {
        SceneManager.LoadScene("Login");
    }

    /// <summary>
    /// Maneja el clic en el botón de registro
    /// </summary>
    private void OnBotonRClicked()
    {
        if (!ValidateForm()) return;
        StartCoroutine(RegisterUserWithTimeout());
    }

    /// <summary>
    /// Valida todos los campos del formulario
    /// </summary>
    /// <returns>True si todos los campos son válidos</returns>
    private bool ValidateForm()
    {
        // Validación de campos requeridos
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

        // Validación de selección de país
        if (countryDrop.value == "Selecciona tu país")
        {
            Debug.LogError("Por favor selecciona un país válido");
            return false;
        }

        // Validación de formato de fecha (YYYY-MM-DD)
        if (!Regex.IsMatch(tfage.value, @"^\d{4}-\d{2}-\d{2}$"))
        {
            Debug.LogError("Formato de fecha incorrecto. Debe ser YYYY-MM-DD");
            return false;
        }

        // Validación de formato de email
        if (!Regex.IsMatch(tfemail.value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            Debug.LogError("Formato de email inválido. Debe ser ejemplo@dominio.com");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Envía los datos de registro al servidor con timeout
    /// </summary>
    private IEnumerator RegisterUserWithTimeout()
    {
        // Preparar datos del formulario
        WWWForm formData = new WWWForm();
        formData.AddField("nombre", tfname.value);
        formData.AddField("apellido", tfsecondname.value);
        formData.AddField("fecha_nacimiento", tfage.value);
        formData.AddField("correo", tfemail.value);
        formData.AddField("contrasena", tfpassword.value);
        formData.AddField("pais", countryDrop.value);
        formData.AddField("genero", genderDrop.value);

        // Configurar la solicitud HTTP POST
        using (UnityWebRequest request = UnityWebRequest.Post("http://35.169.93.195:8080/registrar", formData))
        {
            request.timeout = (int)REQUEST_TIMEOUT;
            float startTime = Time.time;
            
            // Enviar solicitud
            var asyncOperation = request.SendWebRequest();
            
            // Esperar respuesta con timeout
            while (!asyncOperation.isDone)
            {
                if (Time.time - startTime >= REQUEST_TIMEOUT)
                {
                    request.Abort();
                    yield break;
                }
                yield return null;
            }

            // Procesar respuesta
            if (request.result == UnityWebRequest.Result.Success)
            {
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