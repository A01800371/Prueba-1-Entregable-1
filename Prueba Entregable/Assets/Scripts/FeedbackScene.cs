using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FeedbackScene : MonoBehaviour
{
    public Text calificacionText;
    private string respuesta = ""; // Respuesta del servidor
    private int nota = 0; // calificación inicial del examen
    private int idSend = 0; // ID del examen
    private int idpregunta; // ID del jugador, puedes cambiarlo según tu lógica
    private int totalPreguntas = 0;// Cambia esto al número total de preguntas que tienes en tu quiz
    public GameObject contenedorRespuestas; // Panel que contiene las respuestas
    public GameObject prefabRespuesta; // Prefab UI para cada resultado
    public Sprite iconoCorrecto; // Icono para respuestas correctas
    public Sprite iconoIncorrecto; // Icono para respuestas incorrectas

    [System.Serializable]
    public struct ResultadoFinal //Estructura para almacenar los resultados de las preguntas
    {
        public int examen;
        public int calificacion;
        //public int id_jugador;

    }

    [System.Serializable]
    public struct preguntasCorrectas //Estructura para almacenar los resultados de las preguntas
    {
        public int pregunta;
        public int correcto;
        //public int id_jugador; //ID de pruebas
    }

    private List<ResultadoFinal> resultados = new List<ResultadoFinal>();
    private List<preguntasCorrectas> preguntas = new List<preguntasCorrectas>(); // Lista para almacenar las preguntas y respuestas

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
            idSend = ScoreManager.instance.quizID; // Obtenemos el ID del examen

        }
        //Debug.Log("Nota: " + nota); // Verifica si nota tiene un valor
        //Debug.Log("ID Examen: " + idSend); // Verifica si idSend tiene un valor

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
            idpregunta = respuesta.dbId; // Asignamos el ID de la pregunta a la variable idpregunta
            int correcta = respuesta.esCorrecta ? 1 : 0;

            // Agregamos la pregunta y su respuesta a la lista de resultados
            preguntasCorrectas pregunta = new preguntasCorrectas
            {
                pregunta = idpregunta,
                correcto = correcta,
                //id_jugador = 15 // Id de pruebas
            };
            
            preguntas.Add(pregunta);



            //Debug.Log("Pregunta ID: " + idpregunta + ", Correcta: " + correcta);
        }

    }

    [System.Serializable]
    public class ListaPreguntasCorrectas
    {
        public List<preguntasCorrectas> preguntas;
    }


    public void VolverAlMenu()
    {
        StartCoroutine(VolverDespuesDeEnviar());
    }
    private IEnumerator VolverDespuesDeEnviar()
    {
        yield return StartCoroutine(MandarRespuestas()); // Espera a que se termine de enviar el JSON
        yield return StartCoroutine(MandarResultados()); // Espera a que se termine de enviar el JSON

        OnDestroy(); // Destruye el ScoreManager de forma segura
        SceneManager.LoadScene("Mapa Pueblo"); // Cambia de escena solo después de terminar
    }

    private void OnDestroy()
    {
        // Asegúrate de que el ScoreManager se destruya al salir de la escena
        if (ScoreManager.instance != null)
        {
            Destroy(ScoreManager.instance.gameObject); // Destruye el objeto ScoreManager
            ScoreManager.instance = null;  // Asegúrate de que la referencia se elimine
        }
    }

    private IEnumerator MandarResultados()

    {


        resultados.Add(new ResultadoFinal
        {
            examen = idSend, // ID del examen
            calificacion = nota, // Calificación del examen
            //id_jugador = 15 // Id de pruebas
        });

        if (resultados.Count == 0)
        {
            Debug.LogError("La lista de resultados está vacía."); // Verifica si la lista de resultados está vacía
        }

        ResultadoFinal resultado = new ResultadoFinal
        {
            examen = idSend, // ID del examen
            calificacion = nota, // Calificación del examen
            //id_jugador = 15 // Id de pruebas
        };
        string resultadosJson = JsonUtility.ToJson(resultado); // Convertimos la lista de resultados a JSON
        //Debug.Log("JSON a enviar: " + resultadosJson); // Verifica el JSON en consola

        UnityWebRequest request = UnityWebRequest.Post("http://35.169.93.195:8080/juego/guardar-examen", resultadosJson, "application/json"); // URL del servidor

        yield return request.SendWebRequest(); // Espera a que se complete la solicitud

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Resultados enviados correctamente.");
            Debug.Log("Respuesta del servidor: " + request.downloadHandler.text); // Muestra la respuesta del servidor en consola
        }
        else
        {
            respuesta = "Error de conexion: " + request.responseCode;
            Debug.LogError(respuesta); // Mensaje de error en caso de fallo
        }
        request.Dispose(); // Liberar recursos del request

    }
    private IEnumerator MandarRespuestas() // Método para enviar las respuestas al servidor
    {
        foreach (var pregunta in preguntas)
        {
            string jsonPregunta = JsonUtility.ToJson(pregunta);
            //Debug.Log("Enviando pregunta JSON: " + jsonPregunta);

            UnityWebRequest request = UnityWebRequest.Post("http://35.169.93.195:8080/juego/guardar-pregunta", jsonPregunta, "application/json");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Pregunta enviada correctamente.");
                Debug.Log("Respuesta del servidor: " + request.downloadHandler.text);
            }
            else
            {
                string error = "Error al enviar pregunta ID " + pregunta.pregunta + " Codigo de error: " + request.responseCode;
                Debug.LogError(error);
            }

            request.Dispose(); // Liberar recursos después de cada envío
        }
    }

}
    