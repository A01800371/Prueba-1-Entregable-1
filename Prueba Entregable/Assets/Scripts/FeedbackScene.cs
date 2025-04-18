using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FeedbackScene : MonoBehaviour
{
    public Text calificacionText;

    void Start()
    {
        GetCalificacion(); // Llamamos al método para obtener la calificación al iniciar
    }

    public void GetCalificacion()
    {
        int nota = 0;

        // Verificar si ScoreManager.instance existe y obtener la calificación
        if (ScoreManager.instance != null)
        {
            nota = ScoreManager.instance.preguntasCorrectas;
        }

        // Asignar la calificación al texto
        if (nota > 0)
        {
            calificacionText.text = nota.ToString() + "/10";
        }
        else
        {
            calificacionText.text = "0/10";
        }
    }

    public void VolverAlMenu()
    {
        if (ScoreManager.instance != null)
        {
            // Aqui mando los resultados a la base de datos (aun no implementado) 
            //string nombreJugador = nombreJugadorInput != null ? nombreJugadorInput.text : "Anonimo";
            //int puntuacion = ScoreManager.instance.preguntasCorrectas; // Este es el valor que queremos enviar

            //Debug.Log($"[DB] Enviando datos: Jugador={nombreJugador}, Puntuación={puntuacion}/10");


            // Destruimos el ScoreManager si existe esto es importante para evitar que el ScoreManager persista entre escenas
            Destroy(ScoreManager.instance.gameObject);
            ScoreManager.instance = null; 
        }

        SceneManager.LoadScene("MenuJ"); // Cambia "MenuJ" por el nombre exacto de tu escena de menú si es diferente
    }
}
