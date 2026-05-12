using UnityEngine;

public class HideObject : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private bool isHiding = false;
    private GameObject playerObject;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if ((isPlayerInRange || isHiding) && Input.GetKeyDown(KeyCode.S))
        {
            if (isHiding)
            {
                playerObject.SetActive(true);
                isHiding = false;
            }
            else
            {
                isHiding = true;
                _audioSource.Play();
                playerObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;

            if (!isHiding)
            {
                playerObject = null;
            }
        }
    }

    public void Pause()
    {
        _audioSource.Pause();
    }

    public void Resume()
    {
        _audioSource.UnPause();
    }
}