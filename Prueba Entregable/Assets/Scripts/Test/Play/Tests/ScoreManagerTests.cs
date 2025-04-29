using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

[TestFixture]
public class ScoreManagerTests
{
    private GameObject scoreManagerObject;
    private ScoreManager scoreManager;
    private Text scoreText;
    private QuizManager quizManager;

    [SetUp]
    public void Setup()
    {
        // Crear el objeto principal
        scoreManagerObject = new GameObject("ScoreManager");

        // Añadir el componente ScoreManager
        scoreManager = scoreManagerObject.AddComponent<ScoreManager>();

        // Crear el componente Text simulado
        scoreText = new GameObject("ScoreText").AddComponent<Text>();
        scoreManager.scoreText = scoreText;

        // Crear un QuizManager simulado
        quizManager = scoreManagerObject.AddComponent<QuizManager>();
        scoreManager.NumeroDePrguntas = quizManager;

        // Configurar un número fijo de preguntas
        quizManager.NumeroDePreguntas = 10;
    }

    [Test]
    public void Test_AgregarPunto_DeberiaIncrementarPreguntasCorrectas()
    {
        int preguntasAntes = scoreManager.preguntasCorrectas;

        // Llamar al método que suma puntos
        scoreManager.AgregarPunto();

        // Verificar que aumentó en 1
        Assert.AreEqual(preguntasAntes + 1, scoreManager.preguntasCorrectas);
    }

    [Test]
    public void Test_ScoreText_DeberiaActualizarCorrectamente()
    {
        // Simula que tiene 3 respuestas correctas
        scoreManager.preguntasCorrectas = 3;

        // Llama a Update manualmente
        scoreManager.Update();

        // Verifica que el texto sea correcto
        Assert.AreEqual("3/10", scoreManager.scoreText.text);
    }

    [Test]
    public void Test_Awake_DeberiaAsignarInstanciaSingleton()
    {
        // Simula el llamado a Awake
        scoreManager.Awake();

        // Verificar que la instancia sea él mismo
        Assert.AreSame(scoreManager, ScoreManager.instance);
    }

    [TearDown]
    public void Teardown()
    {
        // Limpiar después de cada prueba
        Object.DestroyImmediate(scoreManagerObject);
    }
}
