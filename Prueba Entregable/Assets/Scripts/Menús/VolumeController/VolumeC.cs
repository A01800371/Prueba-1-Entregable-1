/*‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾*\
| Este es un script se encarga de la configuración del volumen del audio en Unity.   |
| Permite modificar el volumen del mezclador de audio mediante el control de un      |
| slider. También asigna un valor por defecto, para que no se rompa el sistema de    |
| audio en caso de que este no se modifique.                                         |
|                                                                                    |
| Autores: Daniel Díaz Romero y Yael Michel García López A01750911                   |
\*_________________________________________________________________________________*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; // Necesario para el uso de UI, como sliders

public class VolumeC : MonoBehaviour
{
     public static VolumeC Instance { get; private set; }

    [Tooltip("Referencia al AudioMixer que controlará los niveles de volumen")]
    [SerializeField] private AudioMixer audioMixer;
    [Tooltip("Valor inicial del volumen al iniciar la escena (0.0001 a 1.0)")]
    [SerializeField, Range(0.0001f, 1f)] private float defaultVolume = 0.5f; // Volumen por defecto
    private float currentVolume;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Evita duplicados
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persiste entre escenas

        currentVolume = defaultVolume;
        ApplyVolume(currentVolume); // Aplica volumen inicial
    }

    public void VolumeControl(float slider)
    {
        currentVolume = slider;
        ApplyVolume(currentVolume);
    }

    public float GetCurrentVolume()
    {
        return currentVolume;
    }

    /// <summary>
    /// Ajusta el volumen del mezclador de audio basado en el valor de un control deslizante
    /// </summary>
    /// <param name="slider">Valor del control deslizante (rango típico 0.0001 a 1)</param>
    private void ApplyVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
    }
    
}
