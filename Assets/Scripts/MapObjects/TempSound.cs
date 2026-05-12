using UnityEngine;

public class TempSound : MonoBehaviour
{
    AudioSource _audioSource;

    public void Init(AudioClip clip)
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = clip;
        _audioSource.Play();
        Destroy(gameObject, clip.length);
    }
}
