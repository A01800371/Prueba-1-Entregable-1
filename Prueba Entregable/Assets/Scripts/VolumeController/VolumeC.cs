using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeC : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private AudioMixer audioMixer;

    public void VolumeControl (float slider)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(slider) * 20);
    }
}
