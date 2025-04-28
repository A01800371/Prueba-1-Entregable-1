//Dany comenta

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
            // Aqui mando los resultados a la base de datos (aun no implementado)
            

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
