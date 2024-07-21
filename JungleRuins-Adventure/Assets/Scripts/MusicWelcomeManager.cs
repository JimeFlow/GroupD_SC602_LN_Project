using UnityEngine;

public class MusicWelcomeManager : MonoBehaviour
{
    public AudioClip Theme;
    private AudioSource audioSource;

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
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No se ha asignado un tema.");
        }
    }
}
