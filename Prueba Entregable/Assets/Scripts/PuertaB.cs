using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaB : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("2°Selector Blockchain");
        }
    }
}