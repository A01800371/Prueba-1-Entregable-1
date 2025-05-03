/*‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾*\
| Este script carga una escena al detectar una colisión con el collider del NPC.     |                                                 |
|                                                                                    |
| Autor: Daniel Díaz Romero                                                          |
\*_________________________________________________________________________________*/


using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaB : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Selector Blockchain");
        }
    }
}