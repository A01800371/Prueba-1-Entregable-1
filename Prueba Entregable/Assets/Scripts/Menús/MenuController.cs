/*
 * Autor: Yael Michel García López A01750911
 * 
 * MenuController: Controlador para el menú principal
 * Maneja la navegación entre las pantallas de registro (Signup) y login
 */

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    // Referencia al documento de interfaz de usuario
    private UIDocument menu;
    
    // Botones de la interfaz
    private Button botonA;  // Botón para registro (Signup)
    private Button botonB;  // Botón para inicio de sesión (Login)

    // Método que se ejecuta al iniciar el script
    void Start()
    {
        // Obtiene el componente UIDocument adjunto al GameObject
        menu = GetComponent<UIDocument>();
        
        // Accede al elemento raíz del documento visual
        var root = menu.rootVisualElement;

        // Obtiene referencias a los botones por sus nombres en el UXML
        botonA = root.Q<Button>("signup");  // Botón de registro
        botonB = root.Q<Button>("login");   // Botón de inicio de sesión

        // Asigna eventos a los botones con parámetros de escena
        botonA.RegisterCallback<ClickEvent, String>(Jugar, "Signup");
        botonB.RegisterCallback<ClickEvent, String>(Jugar, "Login");
    }

    // Método para cargar escenas al hacer clic en los botones
    private void Jugar(ClickEvent evt, String nombreEscena)
    {
        // Carga la escena especificada de manera asíncrona
        SceneManager.LoadSceneAsync(nombreEscena);
    }

    // Método Update vacío (podría eliminarse si no se usa)
    void Update()
    {
        // Se deja vacío intencionalmente ya que no hay necesidad de actualización constante
    }
}