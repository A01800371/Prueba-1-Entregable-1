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
    [SerializeField] private AudioMixer audioMixer; // Referencia al mezclador de audio que será controlado

    /// <summary>
    /// Ajusta el volumen del mezclador de audio basado en el valor de un control deslizante
    /// </summary>
    /// <param name="slider">Valor del control deslizante (rango típico 0.0001 a 1)</param>
    public void VolumeControl(float slider)
    {
        /* 
         * Conversión del valor lineal del slider a escala logarítmica (dB):
         * - Mathf.Log10(slider) convierte el valor lineal a logarítmico
         * - Se multiplica por 20 para convertir a decibeles (escala estándar de audio)
         * - Valores por debajo de 0.0001 podrían producir -infinito (silencio total)
         */
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(slider) * 20);
    }
}