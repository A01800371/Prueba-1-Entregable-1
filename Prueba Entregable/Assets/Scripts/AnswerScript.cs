using UnityEngine;

/*

    * La función de este script es gestionar las respuestas del quiz.
    * Al hacer click en una respuesta, se llama al método 
    * Respuesta() que verifica si la respuesta es correcta o incorrecta.
    * Autores: 

*/

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
