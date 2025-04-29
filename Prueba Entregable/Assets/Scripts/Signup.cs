using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Networking;
using System.Collections;

public class Signup : MonoBehaviour
{
    private UIDocument join;
    private Button botonB;
    private Button botonR;
    private TextField tfname;
    private TextField tfsecondname;
    private TextField tfemail;
    private TextField tfpassword;
    private TextField tfcountry;
    private TextField tfage;
    private Button tfhombre;
    private Button tfmujer;
    private Button tfotro;
    public struct DatosUsuario
    {
        public string nombre;
        public string apellido;
        public string correo;
        public string contrasena;
        public string pais;
        public string fecha_nacimiento; // cambiar a fecha de nacimiento
        public string genero;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        join = GetComponent<UIDocument>();
        var root = join.rootVisualElement;

        tfname = root.Q<TextField>("name");
        tfsecondname = root.Q<TextField>("secondname");
        tfemail = root.Q<TextField>("email");
        tfpassword = root.Q<TextField>("password");
        tfcountry = root.Q<TextField>("country");
        tfage = root.Q<TextField>("age");

        tfhombre = root.Q<Button>("hombre");
        tfmujer = root.Q<Button>("mujer");  
        tfotro = root.Q<Button>("otro"); 
        
        botonB = root.Q<Button>("Back");
        botonR = root.Q<Button>("Register");
        botonR.RegisterCallback<ClickEvent, String>(Jugar,"Login");
        botonB.RegisterCallback<ClickEvent, String>(Jugar,"Inicio11");
    }

    void Update()
    {
        
    }


    private void Jugar(ClickEvent evt, String nombreEscena)
    {
        if (nombreEscena == "Inicio11")
        {
            SceneManager.LoadSceneAsync("Inicio11");
        }
        else if (nombreEscena == "Login")
        {
            botonR.clicked += EnviarDatosLoginJSON;
            SceneManager.LoadSceneAsync("Login");
            
        }
    }

    
    private void EnviarDatosLoginJSON()
    {
        StartCoroutine(RegistrarUsuario());
    }
    private IEnumerator RegistrarUsuario()
    {
        string tfborraraqui = "hola"; // puse esto para que no de error, cambiar por tfhombre.value, tfmujer.value o tfotro.value según el botón seleccionado
        DatosUsuario datos;
        datos.nombre = tfname.value;
        datos.apellido = tfsecondname.value;
        datos.correo = tfemail.value;
        datos.contrasena = tfpassword.value;
        datos.pais = tfcountry.value;
        datos.fecha_nacimiento = tfage.value;
        datos.genero = tfborraraqui; // Cambiar a tfhombre.value, tfmujer.value o tfotro.value según el botón seleccionado

        string datosJSON = JsonUtility.ToJson(datos);
        UnityWebRequest request = UnityWebRequest.Post("http://98.80.206.204:8080/register", datosJSON, "application/JSON"); //Cambiar la URL por la del servidor

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            print("\nConexión exitosa.\n\n");
            SceneManager.LoadScene("login");
        }
        else
        {
            print("Error en la conexión: " + request.responseCode);
        }

    }
}

