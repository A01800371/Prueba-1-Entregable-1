using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class LoginScript : MonoBehaviour
{
    private TextField tfUsuario;
    private TextField tfContrasena;
    private Button botonEnviar;
    public struct DatosUsuario
    {
        public string usuario;
        public string contrasena;
    }

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        tfUsuario = root.Q<TextField>("Usuario");
        tfContrasena = root.Q<TextField>("Contrasena");
        botonEnviar = root.Q<Button>("BotonJoin");
        botonEnviar.clicked += EnviarDatosLoginJSON;

    }


    private void EnviarDatosLoginJSON()
    {
        StartCoroutine(SubirDatosJSON());
    }

    private IEnumerator SubirDatosJSON()
    {
        DatosUsuario datos;
        datos.usuario = tfUsuario.value;
        datos.contrasena = tfContrasena.value;

        string datosJSON = JsonUtility.ToJson(datos);

        UnityWebRequest request = UnityWebRequest.Post("http://98.80.206.204:8080/login", datosJSON, "application/JSON"); //Cambiar la URL por la del servidor

        yield return request.SendWebRequest();

        // Después de un rato
        if (request.result == UnityWebRequest.Result.Success)
        {
            print("\nConexión exitosa.\n\n");
            SceneManager.LoadScene("MenuJ");
        }
        else {
            print("Error en la conexión: " + request.responseCode);
        }

        request.Dispose();
    }
}
