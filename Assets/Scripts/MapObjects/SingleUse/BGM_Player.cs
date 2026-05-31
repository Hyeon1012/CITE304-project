using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class TempBGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private float delayBetweenLoops = 3f;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.clip = bgmClips[0];
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Stage1")
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = bgmClips[0];
                _audioSource.Play();
            }
        }
        else if (scene.name == "Stage3")
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = bgmClips[1];
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource?.Stop();
        }
    }
}