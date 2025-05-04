/*‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾*\
| Este es un script se encarga de la configuración del volumen del audio en Unity.   |
| Permite modificar el volumen del mezclador de audio mediante el control de un      |
| slider. También asigna un valor por defecto, para que no se rompa el sistema de    |
| audio en caso de que este no se modifique.                                         |
|                                                                                    |
| Autores: Daniel Díaz Romero y Yael Michel García López A01750911                   |
\*_________________________________________________________________________________*/

/*
 * Autor: Yael Michel García López A01750911
 * 
 * Controlador de volumen para ajustar el nivel de audio en Unity.
 * Este script permite modificar el volumen del mezclador de audio mediante un control deslizante (slider),
 * aplicando una conversión logarítmica para una curva de volumen más natural.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeC : MonoBehaviour
{
    [Tooltip("Referencia al AudioMixer que controlará los niveles de volumen")]
    [SerializeField] private AudioMixer audioMixer;

    [Tooltip("Valor inicial del volumen al iniciar la escena (0.0001 a 1.0)")]
    [Range(0.0001f, 1f)]
    [SerializeField] private float defaultVolume = 0.5f; // Volumen por defecto

    private void Start()
    {
        ApplyVolume(defaultVolume); // Aplica volumen inicial
    }

    public void VolumeControl(float slider)
    {
        ApplyVolume(slider);
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
