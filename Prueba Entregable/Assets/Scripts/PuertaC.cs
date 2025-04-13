using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaC : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("3°Selector Cryptocurrency");
        }
    }
}