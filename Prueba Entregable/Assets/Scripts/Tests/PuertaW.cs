using UnityEngine;

public class PuertaW : MonoBehaviour
{
    public ISceneLoader sceneLoader = new SceneLoader(); // Por defecto

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sceneLoader.LoadScene("Selector Web 3");
        }
    }
}
