/*‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾*\
| Este script es un selector de animaciones para Unity. Carga animaciones según un  |
| ID.                                                                               |
|                                                                                   |
| Autor: Daniel Díaz Romero                                                         |
\*_________________________________________________________________________________*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimSelector : MonoBehaviour
{
    // Este método se llama al hacer clic en un botón de la interfaz de usuario.
    public void OpenAnim(int AnimationId)
    {
        string AnimationName = "Animation " + AnimationId;
        // Carga la escena de animación correspondiente al ID proporcionado.
        SceneManager.LoadScene(AnimationName);
    }
}