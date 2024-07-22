using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1MusicManager : MonoBehaviour
{
    public AudioClip Theme;
    private AudioSource audioSource;

    [SerializeField]
    float volume;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (Theme != null)
        {
            audioSource.clip = Theme;
            audioSource.volume = volume;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No se ha asignado un tema.");
        }
    }
}

