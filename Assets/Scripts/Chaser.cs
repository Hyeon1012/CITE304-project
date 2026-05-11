using TMPro;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Chaser : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 4f;

    [SerializeField] private float _delay = 2.0f;
    [SerializeField] private float _fadeTime = 2.0f;
    [SerializeField] private float _destoryDistance = 80f;

    private Rigidbody2D rb;
    private AudioSource _audioSource;
    private float dirX;
    private bool start = false;
    private bool isDestroying = false;
    private float timer;

    public void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        dirX = Mathf.Sign(target.position.x - transform.position.x);
        timer = _delay;
        start = true;
    }

    void Update()
    {
        if (!start || isDestroying) return;

        if (timer <= 0)
        {
            if (!_audioSource.isPlaying) _audioSource.Play();

            rb.linearVelocityX = dirX * moveSpeed;

            if (target == null || ((transform.position.x - target.position.x) > _destoryDistance))
            {
                StartCoroutine(FadeOutAndDestroy());
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        isDestroying = true;
        float startVolume = _audioSource.volume;

        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= startVolume * Time.deltaTime / _fadeTime;
            yield return null;
        }

        _audioSource.Stop();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStateManager>().KillPlayer();
        }
    }
}