using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    private UIDocument menu;
    private Button botonA;
    private Button botonB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;

        botonA = root.Q<Button>("signup");
        botonB = root.Q<Button>("login");

        botonA.RegisterCallback<ClickEvent, String>(Jugar,"Signup");
        botonB.RegisterCallback<ClickEvent, String>(Jugar,"Login");
    }

    private void Jugar(ClickEvent evt, String nombreEscena)
    {
        SceneManager.LoadSceneAsync(nombreEscena);
    }

    /* private void JugarB(ClickEvent evt)
    {
        print("Jugar B");
        SceneManager.LoadSceneAsync("EscenaMapa");
    } */

    // Update is called once per frame
    void Update()
    {
        
    }
}
