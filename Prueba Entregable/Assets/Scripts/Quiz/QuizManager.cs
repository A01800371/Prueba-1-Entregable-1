/*
 * QuizManager.cs
 * Este script gestiona las preguntas y respuestas del quiz, maneja la lógica de las respuestas correctas e incorrectas,
 * y controla la animación del personaje y el globo de diálogo.
 * 
 * Autores: Erick Owen, Brian Axel Velarde.
  */

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

    public Animator personajeAnimator; // Referencia al Animator del personaje
    public Animator GloboAnimator; // Referencia globo de diálogo


    
    


    private void Start()
    {
        timer = GetComponent<TimerManager>(); 
        score = GetComponent<ScoreManager>();
        feedbackManager = GetComponent<FeedbackManager>();
        NumeroDePreguntas = QnA.Count; // Guardamos el número total de preguntas para usarlo 
        ActivarAnimacionHablar(); // Activamos la animación de hablar al inicio
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
                options[i].transform.GetChild(0).GetComponent<Text>().text = preguntaActual.Respuestas[i].GetLocalizedString(); // Mostramos la respuesta en el botón


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

    // Método para generar una nueva pregunta
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
        questionText.text = preguntaActual.Pregunta.GetLocalizedString(); // Mostramos la pregunta

        EstableceRespuestas(); // Establecemos las respuestas según el tipo de pregunta
        Invoke("DesbloquearBotones", 1f); // Desbloqueamos los botones de respuesta para la nueva pregunta
    }

    // Métodos para verificar la respuesta seleccionada de acuero a si fue correcta o incorrecta o si el tiempo se agotó
    public void correct()
    {
        timer.ReiniciarTimer(); // Reiniciamos el timer
        BloquearBotones(); // Bloqueamos los botones de respuesta para evitar múltiples clics

        //Animaciones 
        ActivarAnimacionHablar(); 
        globoAnimacion(); 

        ScoreManager.instance.historialUsuario.Add(new RespuestaUsuario(QnA[currentQuestion], true));
        
        QnA.RemoveAt(currentQuestion); // Quitamos la pregunta respondida
        score.AgregarPunto(); // Sumamos un punto
        feedbackManager.RegistrarRespuesta(true); // Retroalimentación
        Invoke("GeneraPregunta", 0.15f);// Generamos la siguiente pregunta
    }

    public void incorrect()
    {
        timer.ReiniciarTimer(); // Reiniciamos el timer
        BloquearBotones(); // Bloqueamos los botones de respuesta para evitar múltiples clics

        //Animaciones 
        ActivarAnimacionHablar(); 
        globoAnimacion(); 

        ScoreManager.instance.historialUsuario.Add(new RespuestaUsuario(QnA[currentQuestion], false));


        QnA.RemoveAt(currentQuestion); // Quitamos la pregunta respondida
        feedbackManager.RegistrarRespuesta(false); // Retroalimentación
        Invoke("GeneraPregunta", 0.5f);// Generamos la siguiente pregunta
    }

    internal void TiempoAgotado()
    {
        timer.ReiniciarTimer(); // Reiniciamos el timer
        
        //Animaciones 
        ActivarAnimacionHablar();
        globoAnimacion(); 

        ScoreManager.instance.historialUsuario.Add(new RespuestaUsuario(QnA[currentQuestion], false));
        
        QnA.RemoveAt(currentQuestion); // Quitamos la pregunta
        feedbackManager.RegistrarRespuesta(false); // Retroalimentación de tiempo agotado
        Invoke("GeneraPregunta", 0.5f);// Generamos la siguiente pregunta
    }
    //=======================================================

    // Método para regresar al menú
    // Se asegura de que el ScoreManager se destruya y no persista entre escenas
    public void RegresarAlMenu()
    {
        if (ScoreManager.instance != null)
        {
            
            Destroy(ScoreManager.instance.gameObject); // Destruimos el ScoreManager para evitar que persista entre escenas
            // Aseguramos que la referencia a ScoreManager sea nula
            ScoreManager.instance = null; 
        }
        SceneManager.LoadScene("MenuJ"); // Regresamos al menú
    }


    //Funciones de animación
    private void ActivarAnimacionHablar() // Activamos la animación de hablar
    {
        personajeAnimator.SetBool("IsTalk", true);
        Invoke(nameof(DesactivarAnimacionHablar), 1f); // Desactivamos después de 1 segundo
    }
    private void DesactivarAnimacionHablar() // Desactivamos la animación de hablar
    {
        if (personajeAnimator != null)
    {
        personajeAnimator.SetBool("IsTalk", false);
    }
    }

    private void globoAnimacion() // Activamos la animación del globo de diálogo
    {
        GloboAnimator.SetBool("IsGlobe", true);
        Invoke(nameof(DesactivarAnimacionGlobo), 0.2f); // Desactivamos después de 0.1 segundos
    }

    private void DesactivarAnimacionGlobo() // Desactivamos la animación del globo de diálogo
    {
        if (personajeAnimator != null)
    {
        GloboAnimator.SetBool("IsGlobe", false);
    }
        
    }
    //=======================================================
    // metodo para bloquear los botones de respuesta
    void BloquearBotones()
    {
        foreach (GameObject boton in options)
        {
            boton.GetComponent<Button>().interactable = false;
        }
    }

    void DesbloquearBotones()
    {
        foreach (GameObject boton in options)
        {
            boton.GetComponent<Button>().interactable = true;
        }
    }
}