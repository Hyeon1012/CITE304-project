using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private GameObject _cannonballPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireCooldown = 3f;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _aimYOffset = 0.5f;
    [SerializeField] private float _chargeDuration = 1.35f;

    private Transform _playerTransform;
    private float _timer = 0f;
    private Renderer _renderer;
    private AudioSource _as;
    private bool _isChargingSoundPlayed = false;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerTransform = player.transform;
        }
        _renderer = GetComponent<Renderer>();
        _as = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_playerTransform == null) return;
        if (_renderer != null && !_renderer.isVisible)
        {
            if (_isChargingSoundPlayed && _as.isPlaying)
            {
                _as.Stop();
                _isChargingSoundPlayed = false;
                _timer = 0f;
            }
            return;
        }
        Vector2 targetPosition = (Vector2)_playerTransform.position + (Vector2)_playerTransform.up * _aimYOffset;
        Vector2 aimDirection = (targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        _timer += Time.deltaTime;
        if (_timer >= _fireCooldown - _chargeDuration && !_isChargingSoundPlayed)
        {
            if (_as != null) _as.Play();
            _isChargingSoundPlayed = true;
        }

        if (_timer >= _fireCooldown)
        {
            Fire(targetPosition);
            _timer = 0f;
            _isChargingSoundPlayed = false;
        }
    }

    void Fire(Vector2 targetPosition)
    {
        if (_cannonballPrefab == null || _firePoint == null) return;
        Vector2 direction = (targetPosition - (Vector2)_firePoint.position).normalized;
        GameObject cannonball = Instantiate(_cannonballPrefab, _firePoint.position, Quaternion.identity);
        Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * _projectileSpeed;
        }
    }
}