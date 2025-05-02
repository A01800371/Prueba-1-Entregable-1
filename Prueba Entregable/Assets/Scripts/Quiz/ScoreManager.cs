/*
 * ScoreManager.cs
 * Este script gestiona el puntaje del jugador, mostrando el número de preguntas correctas y el total de preguntas.
 */


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int preguntasCorrectas = 0; // numero de preguntas correctas
    public Text scoreText; 
    public QuizManager NumeroDePrguntas;
    public int quizID; // ID del quiz actual
    public List<RespuestaUsuario> historialUsuario = new List<RespuestaUsuario>();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // <- Clave para que sobreviva a los cambios de escena
        }
        else
        {
            Destroy(gameObject); // Para evitar duplicados si vuelves a la escena original
        }
    }


    public void Start()
    {   
        DontDestroyOnLoad(gameObject); // Aseguramos que el objeto no se destruya al cargar una nueva escena esto es importante mandar a la escena de feedback
        NumeroDePrguntas = GetComponent<QuizManager>(); // Obtenemos la referencia al QuizManager
        // obtener el nombre del examen y asignarlo
        string nombreEscena = SceneManager.GetActiveScene().name; // Obtener el nombre de la escena actual

        // Asignar ID según el nombre de la escena
        switch (nombreEscena)
        {
            case "Quiz 1":
                quizID = 1; 
                break;
            case "Quiz 2":
                quizID = 2;
                break;
            case "Quiz 3":
                quizID = 3;
                break;
            case "Quiz 4":
                quizID = 4;
                break;
            case "Quiz 5":
                quizID = 5;
                break;
            case "Quiz 6":
                quizID = 6;
                break;
            case "Quiz 7":
                quizID = 7;
                break;
            case "Quiz 8":
                quizID = 8;
                break;
            case "Quiz 9":
                quizID = 9;
                break;
            default:
                quizID = 0; // Por si hay una escena no reconocida
                break;
        }
        //Debug.Log("ID del examen: " + quizID); // Mensaje de depuración para verificar el ID del examen
        
    }

    public void Update()
    {
        
        scoreText.text = preguntasCorrectas + "/" + NumeroDePrguntas.NumeroDePreguntas ; // Actualiza el texto del puntaje en la UI
        // Si el jugador ha respondido todas las preguntas, se puede mostrar el resultado final

    }

    public void AgregarPunto()
    {
        preguntasCorrectas++; // Incrementa el puntaje por cada respuesta correcta
    }


}

