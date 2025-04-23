using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public float tiempoLimite = 10f; // Tiempo que puede ser modificable
    private float tiempoRestante; // Tiempo restante en el temporizador
    private bool timerActivo = false; // Indica si el temporizador está activo o no

    public Text timerText; // Referencia al texto del temporizador en la UI
    public QuizManager quizManager; // Referencia al QuizManager para acceder a sus métodos

    private void Start()
    {
        ReiniciarTimer(); // Inicializa el temporizador al inicio
    }

    private void Update() 
    {
        if (!timerActivo || timerText == null || quizManager == null) return; // Verifica si el temporizador está activo y si las referencias son válidas

        tiempoRestante -= Time.deltaTime; // Resta el tiempo transcurrido desde la última actualización
        timerText.text = "Tiempo: " + Mathf.Ceil(tiempoRestante); // Actualiza el texto del temporizador en la UI

        if (tiempoRestante <= 0) // Si el tiempo se ha agotado
        {
            timerActivo = false; // Detiene el temporizador
            //Debug.Log("Tiempo agotado. Pasando a la siguiente pregunta."); // Mensaje de depuración
            quizManager.TiempoAgotado(); // Llama al método TiempoAgotado en el QuizManager
            ReiniciarTimer(); // Reinicia el temporizador para la siguiente pregunta
        }
    }

    public void ReiniciarTimer() // Reinicia el temporizador
    {
        tiempoRestante = tiempoLimite; // Restablece el tiempo restante al tiempo límite
        timerActivo = true; // Activa el temporizador
    }

    public void DetenerTimer() // Detiene el temporizador
    {
        timerActivo = false;
    }
}
