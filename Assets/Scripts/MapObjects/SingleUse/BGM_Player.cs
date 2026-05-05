using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class TempBGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private float delayBetweenLoops = 3f;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        // Ensure standard looping is OFF so our script controls the timing
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;

        if (bgmClip != null)
        {
            StartCoroutine(PlayBGMWithDelay());
        }
        else
        {
            Debug.LogWarning("BGM Player: No clip assigned!");
        }
    }

    IEnumerator PlayBGMWithDelay()
    {
        while (true)
        {
            _audioSource.clip = bgmClip;
            _audioSource.Play();

            // Wait until the song reaches the end
            yield return new WaitWhile(() => _audioSource.isPlaying);

            // The specific 3-second gap you requested
            yield return new WaitForSeconds(delayBetweenLoops);
        }
    }
}