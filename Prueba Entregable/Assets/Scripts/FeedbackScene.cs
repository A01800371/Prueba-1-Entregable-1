using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FeedbackScene : MonoBehaviour
{
    public Text calificacionText;
    private int nota = 0;
    private int totalPreguntas = 0;// Cambia esto al número total de preguntas que tienes en tu quiz
    public GameObject contenedorRespuestas; // Panel que contiene las respuestas
    public GameObject prefabRespuesta; // Prefab UI para cada resultado
    public Sprite iconoCorrecto; // Icono para respuestas correctas
    public Sprite iconoIncorrecto; // Icono para respuestas incorrectas

    public struct ResultadoPregunta //Estructura para almacenar los resultados de las preguntas
    {
        public string pregunta;
        public bool fueCorrecta;
        // public string usuario; // como identificamos al usuario y el Quiz?
    }

    private List<ResultadoPregunta> resultados = new List<ResultadoPregunta>();

    void Start()
    {
        GetCalificacion(); // Llamamos al método para obtener la calificación al iniciar
    }

    public void GetCalificacion()
    {
        

        // Verificar si ScoreManager.instance existe y obtener la calificación
        if (ScoreManager.instance != null)
        {
            nota = ScoreManager.instance.preguntasCorrectas;
            totalPreguntas = ScoreManager.instance.NumeroDePrguntas.NumeroDePreguntas; // Obtenemos el número total de preguntas

        }

        // Asignar la calificación al texto

        calificacionText.text = nota.ToString() + "/" + totalPreguntas.ToString(); // Actualiza el texto con la calificación

        foreach (var respuesta in ScoreManager.instance.historialUsuario)
        {   
            // Instanciamos una nueva fila en el contenedor
            GameObject nuevaFila = Instantiate(prefabRespuesta, contenedorRespuestas.transform);

            Text preguntaText = nuevaFila.transform.Find("PreguntaText").GetComponent<Text>();
            Image icono = nuevaFila.transform.Find("IconoResultado").GetComponent<Image>();

            // Asignamos el texto de la pregunta
            preguntaText.text = respuesta.pregunta.Pregunta.GetLocalizedString();
            
            // Establecemos el icono dependiendo de si la respuesta fue correcta o incorrecta
            icono.sprite = respuesta.esCorrecta ? iconoCorrecto : iconoIncorrecto;

            resultados.Add(new ResultadoPregunta {
            pregunta = respuesta.pregunta.Pregunta.GetLocalizedString(),
            fueCorrecta = respuesta.esCorrecta
            // agregar el usuario y como saber que quiz es
            });
        }

    }

    public void VolverAlMenu()
    {
        Debug.Log(nota); // Mensaje de depuración
        EnviarDatosLoginJSON(); // Llamamos a la función para enviar los resultados antes de volver al menú
        OnDestroy(); // Llamamos a OnDestroy para asegurarnos de que el ScoreManager se destruya antes de volver al menú
        SceneManager.LoadScene("Mapa Pueblo 1"); // Regresamos a la escena del menú
    }

    private void OnDestroy()
    {
        // Asegúrate de que el ScoreManager se destruya al salir de la escena
        if (ScoreManager.instance != null)
        {
            Destroy(ScoreManager.instance.gameObject);
            ScoreManager.instance = null; 
        }
    }

    private void EnviarDatosLoginJSON()
    {
        StartCoroutine(MandarResultados());
    }

    private IEnumerator MandarResultados()
    {
        string resultadosJson = JsonUtility.ToJson(resultados); // Convertimos la lista de resultados a JSON
        Debug.Log(resultadosJson); // Mensaje de depuración para verificar el JSON

        UnityWebRequest request = UnityWebRequest.Post("http://98.80.206.204:8080/???", resultadosJson, "application/json"); //Necesitamos la URL del servidor para enviar los resultados
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success) // Verificamos si la solicitud fue exitosa
        {
            Debug.Log("Resultados enviados correctamente.");
        }
        else
        {
            Debug.LogError("Error al enviar resultados: " + request.responseCode);
        }
    }
}
