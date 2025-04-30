/*
 * ScoreManager.cs
 * Este script gestiona el puntaje del jugador, mostrando el número de preguntas correctas y el total de preguntas.
 */


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int preguntasCorrectas = 0; // numero de preguntas correctas
    public Text scoreText; 
    public QuizManager NumeroDePrguntas;
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
    }

    public void Update()
    {
        
        scoreText.text = preguntasCorrectas + "/" + NumeroDePrguntas.NumeroDePreguntas ; // Actualiza el texto del puntaje en la UI
        // Si el jugador ha respondido todas las preguntas, se puede mostrar el resultado final

    }

    public void AgregarPunto()
    {
        preguntasCorrectas++; // Incrementa el puntaje por cada respuesta correcta
        // Aquí puedes agregar lógica adicional, como reproducir un sonido o mostrar una animación de éxito 
    }


}

