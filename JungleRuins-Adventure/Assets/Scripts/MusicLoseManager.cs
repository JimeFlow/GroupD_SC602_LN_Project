using UnityEngine;

public class MusicLoseManager : MonoBehaviour
{
    public AudioClip loseTheme;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (loseTheme != null)
        {
            audioSource.clip = loseTheme;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No se ha asignado un tema.");
        }
    }
}
