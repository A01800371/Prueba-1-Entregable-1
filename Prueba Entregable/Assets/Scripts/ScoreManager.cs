//Aun esta en progreso no agregar hasta terminar

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int preguntasCorrectas = 0; // numero de preguntas correctas
    public Text scoreText; 
    public QuizManager NumeroDePrguntas;

    private void Awake()
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
        DontDestroyOnLoad(gameObject);
        NumeroDePrguntas = GetComponent<QuizManager>();
    }

    void Update()
    {
        
        scoreText.text = preguntasCorrectas + "/" + NumeroDePrguntas.NumeroDePreguntas ;

    }

    public void AgregarPunto()
    {
        preguntasCorrectas++;
    }


}

