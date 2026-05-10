using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip jumpSound;
    public AudioClip weakJumpSound;
    public AudioClip fallSound;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Pause()
    {
        _audioSource.Pause();
    }

    public void Resume()
    {
        _audioSource.UnPause();
    }

    public void PlayMovingSound(bool shift)
    {
        if (shift)
        {
            if (!_audioSource.isPlaying || (_audioSource.isPlaying && _audioSource.clip != walkSound))
            {
                _audioSource.clip = walkSound;
                _audioSource.Play();
            }
        }
        else
        {
            if (!_audioSource.isPlaying || (_audioSource.isPlaying && _audioSource.clip != runSound))
            {
                _audioSource.clip = runSound;
                _audioSource.Play();
            }
        }
    }

    public void StopMovingSound()
    {
        _audioSource.Stop();
    }

    public void JumpSound()
    {
        _audioSource.PlayOneShot(jumpSound);
    }

    public void WeakJumpSound()
    {
        _audioSource.PlayOneShot(weakJumpSound);
    }

    public void FallSound()
    {
        _audioSource.PlayOneShot(fallSound);
    }
}
