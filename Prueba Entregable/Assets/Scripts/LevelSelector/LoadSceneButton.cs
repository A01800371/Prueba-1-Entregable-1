/*‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾*\
| Script sencillo que permite la carga de un nivel cuando este es asignado a un      |
| botón, manda un mensaje de error e encarga de hacer cada apartado de nivel en el   |
| Selector de niveles, al igual que asigna funciones a los botones de navegación y   |
| los botones de los niveles, también carga escenas y permite un cambio de descripc- |
| ión según el nivel seleccionado.                                                   |
|                                                                                    |
| Autor: Daniel Díaz Romero                                                          |
\*_________________________________________________________________________________*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadScene()
    {
        if (Application.CanStreamedLevelBeLoaded(sceneToLoad))
        {
            if (ScoreManager.instance != null)
            {
            

            Destroy(ScoreManager.instance.gameObject);
            ScoreManager.instance = null; 
            }

            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Escena no encontrada o no está en el Build Settings: " + sceneToLoad);
        }
    }
}
