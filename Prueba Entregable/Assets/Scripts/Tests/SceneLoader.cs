using UnityEngine.SceneManagement;

public class SceneLoader : ISceneLoader
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
