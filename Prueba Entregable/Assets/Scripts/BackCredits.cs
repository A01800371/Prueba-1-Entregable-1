// Yael Michel García López A01750911

using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Carga la escena Menu
    public void Creditos(){
        SceneManager.LoadScene("MenuJ");
    }
}
