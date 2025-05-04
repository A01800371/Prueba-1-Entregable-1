/*
 * Autor: Yael Michel García López A01750911
 * 
 * MenuJ: Controlador del menú principal del juego
 * Maneja la navegación a diferentes secciones:
 * - Ajustes (Settings)
 * - Juego principal (Mapa Pueblo)
 * - Créditos
 * - Política de privacidad
 * - Salida (Inicio11)
 */

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuJ : MonoBehaviour
{
    // Referencia al documento UI
    private UIDocument menu;
    
    // Botones del menú
    private Button botonE;  // Botón Exit (regresa a Inicio11)
    private Button botonS;  // Botón Settings (ajustes)
    private Button botonJ;  // Botón Juega (inicia juego)
    private Button botonC;  // Botón Credits
    private Button botonP;  // Botón Privacidad

    void Start()
    {
        // Obtiene referencia al componente UIDocument
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;

        // Asigna botones por sus nombres en el UXML
        botonS = root.Q<Button>("Settings");
        botonE = root.Q<Button>("Exit");
        botonJ = root.Q<Button>("Juega");
        botonC = root.Q<Button>("Credits");
        botonP = root.Q<Button>("Privacidad");

        // Asigna escenas a cada botón:
        // NOTA: Las escenas deben existir en Build Settings
        botonS.RegisterCallback<ClickEvent, String>(Jugar, "Settings");      // Escena de ajustes
        botonJ.RegisterCallback<ClickEvent, String>(Jugar, "Mapa Pueblo");  // Escena principal de juego
        botonE.RegisterCallback<ClickEvent, String>(Jugar, "Inicio11");     // Escena inicial
        botonC.RegisterCallback<ClickEvent, String>(Jugar, "Creditos");      // Escena de créditos
        botonP.RegisterCallback<ClickEvent, String>(Jugar, "Privacidad");    // Escena de política
    }

    /// <summary>
    /// Carga la escena especificada de manera asíncrona
    /// </summary>
    private void Jugar(ClickEvent evt, String nombreEscena)
    {
        SceneManager.LoadSceneAsync(nombreEscena);
    }

    // Método Update vacío (se puede eliminar si no se usa)
    void Update()
    {
        // Sin implementación actual
    }
}