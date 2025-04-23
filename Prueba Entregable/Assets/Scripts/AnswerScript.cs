using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false; // Indica si la respuesta es correcta o no

    public QuizManager quizManager;

    public void Respuesta() // Metodo que se llama al hacer click en la respuesta 
    {
        if (isCorrect)
        {
            //Debug.Log("Respuesta Correcta"); // Mensaje de depuración
            quizManager.correct();
        }
        else
        {
            //Debug.Log("Respuesta Incorrecta"); // Mensaje de depuración
            quizManager.incorrect();
        }
    }
}
