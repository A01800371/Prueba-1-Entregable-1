using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public float tiempoLimite = 10f; // Tiempo que puede ser modificable
    public float tiempoRestante; // Tiempo restante en el temporizador
    public bool timerActivo = false; // Indica si el temporizador está activo o no

    public Text timerText; // Referencia al texto del temporizador en la UI
    public QuizManager quizManager; // Referencia al QuizManager para acceder a sus métodos
    [SerializeField] public LocalizedString timer; // Referencia a la cadena localizada para el temporizador

    private void Start()
    {
        ReiniciarTimer(); // Inicializa el temporizador al inicio
        
    }

    public void Update() 
    {
        if (!timerActivo || timerText == null || quizManager == null) return; // Verifica si el temporizador está activo y si las referencias son válidas

        tiempoRestante -= Time.deltaTime; // Resta el tiempo transcurrido desde la última actualización
        timerText.text = string.Format(timer.GetLocalizedString()+": " +Mathf.Ceil(tiempoRestante)); // Actualiza el texto del temporizador en la UI

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
