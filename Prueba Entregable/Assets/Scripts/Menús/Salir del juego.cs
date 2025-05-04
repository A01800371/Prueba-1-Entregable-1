/*
 * Autor: Yael Michel García López A01750911
 * 
 * Salir_del_juego: Controlador para la opción de salida/retorno al menú
 * 
 * Funcionalidad:
 * - Proporciona un botón para regresar al menú principal (MenuJ)
 * - Utiliza SceneManager para cargar la escena de manera asíncrona
 */

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Salir_del_juego : MonoBehaviour
{
    // Referencia al documento de interfaz de usuario
    private UIDocument menu;
    
    // Botón para regresar al menú
    private Button botonR;  // Botón "Regresar"

    /// <summary>
    /// Configuración inicial del controlador UI
    /// </summary>
    void Start()
    {
        // Obtiene la referencia al UIDocument
        menu = GetComponent<UIDocument>();
        
        // Accede al elemento raíz del documento visual
        var root = menu.rootVisualElement;

        // Asigna el botón por su nombre en el UXML
        botonR = root.Q<Button>("BotonR");
        
        // Registra el evento click para cargar la escena MenuJ
        botonR.RegisterCallback<ClickEvent, String>(Jugar, "MenuJ");
    }

    /// <summary>
    /// Maneja el evento de clic para cargar una escena
    /// </summary>
    /// <param name="evt">Evento de clic</param>
    /// <param name="nombreEscena">Nombre de la escena a cargar</param>
    private void Jugar(ClickEvent evt, String nombreEscena)
    {
        // Carga la escena especificada de manera asíncrona
        SceneManager.LoadSceneAsync(nombreEscena);
    }

    // Método Update (se mantiene por si se requiere funcionalidad futura)
    void Update()
    {
        // Actualmente no se requiere lógica de actualización
    }
}