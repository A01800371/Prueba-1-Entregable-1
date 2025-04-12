using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<PreguntaRespuesta> QnA; // Lista de preguntas y respuestas
    public GameObject[] options; // Los botones de respuesta
    public int currentQuestion; // Índice de la pregunta actual
    public Text questionText; // Texto que muestra la pregunta
    private TimerManager timer; // Referencia al Timer
    private ScoreManager score; // Referencia al Score
    private FeedbackManager feedbackManager; // Referencia al Feedback
    public int NumeroDePreguntas; // Total de preguntas

    private void Start()
    {
        timer = GetComponent<TimerManager>();
        score = GetComponent<ScoreManager>();
        feedbackManager = GetComponent<FeedbackManager>();
        NumeroDePreguntas = QnA.Count;
        GeneraPregunta(); // Genera la primera pregunta
    }

    void EstableceRespuestas()
    {
        PreguntaRespuesta preguntaActual = QnA[currentQuestion];
        
        // Aseguramos que se muestran los botones correctos según el tipo de pregunta
        for (int i = 0; i < options.Length; i++)
        {
            // Desactivamos todas las opciones inicialmente
            options[i].SetActive(false);

            // Si la pregunta tiene menos de 4 opciones, solo mostramos las que hay
            if (i < preguntaActual.Respuestas.Length)
            {
                options[i].SetActive(true); // Hacemos visibles los botones necesarios
                options[i].transform.GetChild(0).GetComponent<Text>().text = preguntaActual.Respuestas[i];

                // Verificamos si la respuesta es correcta
                if (preguntaActual.RespuestaCorrecta == i + 1)
                {
                    options[i].GetComponent<AnswerScript>().isCorrect = true;
                }
                else
                {
                    options[i].GetComponent<AnswerScript>().isCorrect = false;
                }
            }
        }
    }

    void GeneraPregunta()
    {
        if (QnA.Count == 0)
        {
            // Si no hay preguntas restantes, cargamos la escena de feedback
            SceneManager.LoadScene("EscenaFeedback");
            return;
        }

        currentQuestion = Random.Range(0, QnA.Count); // Seleccionamos una pregunta al azar

        PreguntaRespuesta preguntaActual = QnA[currentQuestion];
        questionText.text = preguntaActual.Pregunta; // Mostramos la pregunta

        EstableceRespuestas(); // Establecemos las respuestas según el tipo de pregunta
    }

    public void correct()
    {
        timer.ReiniciarTimer(); // Reiniciamos el timer
        QnA.RemoveAt(currentQuestion); // Quitamos la pregunta respondida
        score.AgregarPunto(); // Sumamos un punto
        feedbackManager.RegistrarRespuesta(true); // Retroalimentación
        GeneraPregunta(); // Generamos la siguiente pregunta
    }

    public void incorrect()
    {
        timer.ReiniciarTimer(); // Reiniciamos el timer
        QnA.RemoveAt(currentQuestion); // Quitamos la pregunta respondida
        feedbackManager.RegistrarRespuesta(false); // Retroalimentación
        GeneraPregunta(); // Generamos la siguiente pregunta
    }

    internal void TiempoAgotado()
    {
        QnA.RemoveAt(currentQuestion); // Quitamos la pregunta
        feedbackManager.RegistrarRespuesta(false); // Retroalimentación de tiempo agotado
        GeneraPregunta(); // Generamos la siguiente pregunta
    }

    public void RegresarAlMenu()
    {
        if (ScoreManager.instance != null)
        {
            
            Destroy(ScoreManager.instance.gameObject);
            ScoreManager.instance = null; 
        }
        SceneManager.LoadScene("MenuJ"); // Regresamos al menú
    }
}
