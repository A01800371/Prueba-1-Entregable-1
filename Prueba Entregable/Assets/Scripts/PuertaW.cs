using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaW : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("1°Selector Web 3");
        }
    }
}

