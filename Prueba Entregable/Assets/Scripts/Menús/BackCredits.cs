/*
 * Autor: Yael Michel García López A01750911
 * 
 * Script para manejar el regreso al menú principal desde los créditos
 * Proporciona una función pública que puede ser vinculada a un botón UI
 * para cargar la escena del menú principal.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class BackCredits : MonoBehaviour
{
    /// <summary>
    /// Carga la escena del menú principal cuando se llama
    /// </summary>
    public void ReturnToMenu()
    {
        // Nota: Es buena práctica usar constantes o un sistema de gestión de escenas
        // en lugar de strings literales para evitar errores de escritura
        SceneManager.LoadScene("MenuJ");
    }

    // Eliminados los métodos Start y Update vacíos ya que no son necesarios en este script
    // (Unity genera advertencias cuando existen métodos vacíos innecesarios)
}