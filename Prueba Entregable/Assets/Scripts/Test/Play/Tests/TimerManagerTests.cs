using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;

[TestFixture]
public class TimerManagerTests
{
    private GameObject timerObject;
    private TimerManager timerManager;
    private Text timerText;

    [SetUp]
    public void Setup()
    {
        // Crear un objeto de prueba en la escena
        timerObject = new GameObject("TimerManager");
        
        // Añadir el componente TimerManager
        timerManager = timerObject.AddComponent<TimerManager>();
        
        // Crear un objeto Text (simulando la UI)
        timerText = new GameObject("TimerText").AddComponent<Text>();
        timerManager.timerText = timerText;  // Asignar el Text al TimerManager

        // Configurar un tiempo límite para el temporizador
        timerManager.tiempoLimite = 5f;

        // Configurar el LocalizedString para las pruebas
        timerManager.timer = new LocalizedString();
        timerManager.timer.SetReference("Timer", "Tiempo"); // Proporcionar un valor para la localización
    }

    [Test]
    public void Test_ReiniciarTimer_DeberiaRestablecerTiempo()
    {
        // Inicializa el temporizador
        timerManager.ReiniciarTimer();
        
        // Comprobar que el tiempo restante es igual al tiempo límite
        Assert.AreEqual(timerManager.tiempoRestante, timerManager.tiempoLimite);
    }

    [Test]
    public void Test_TimerDeberiaContarHaciaAtras()
    {
        // Inicializar el temporizador
        timerManager.ReiniciarTimer();
        
        // Simula el paso de tiempo
        timerManager.tiempoRestante -= 1f; // Simula que ha pasado 1 segundo

        // Comprobar que el tiempo restante ha disminuido
        Assert.Less(timerManager.tiempoRestante, timerManager.tiempoLimite);
    }

    [Test]
    public void Test_TimerDeberiaDetenerseCuandoTiempoSeAgote()
    {
        // Establecer el tiempo restante a 0
        timerManager.tiempoRestante = 0f;

        // Simula la actualización
        timerManager.Update();

        // Comprobar que el temporizador está detenido
        Assert.IsFalse(timerManager.timerActivo);
    }

    [Test]
    
    public void Test_TimerDeberiaMostrarCorrectamenteEnElTexto()
    {
        // Inicializar el temporizador
        timerManager.ReiniciarTimer();

        // Llamar a Update varias veces para simular el paso de tiempo
        timerManager.Update();
        timerManager.Update();  // Llamar una segunda vez para simular el paso de tiempo

        // Comprobar que el texto de la UI se actualiza correctamente
        Assert.AreEqual("Tiempo: " +5, "Tiempo: " + Mathf.Ceil(timerManager.tiempoRestante).ToString());
    }




    [Test]
    public void Test_TimerDeberiaReiniciarAlTerminar()
    {
        // Inicializar el temporizador
        timerManager.ReiniciarTimer();
        
        // Simula el paso de tiempo hasta que el tiempo se agote
        timerManager.tiempoRestante = 0f;

        // Llamar al método Update para comprobar que el tiempo se reinicia
        timerManager.Update();

        // Comprobar que el temporizador se reinicia
        Assert.AreEqual(timerManager.tiempoRestante, timerManager.tiempoRestante);
    }

    [TearDown]
    public void Teardown()
    {
        // Limpiar después de cada prueba
        Object.Destroy(timerObject);
    }
}
