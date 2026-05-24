using UnityEngine;

public class Cannonball : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5f;
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {
        if (_rb != null && _rb.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(_rb.linearVelocity.y, _rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStateManager player = other.GetComponent<PlayerStateManager>();
            if (player != null)
            {
                player.KillPlayer();
            }

            Destroy(gameObject);

            Debug.Log("canon hit");
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}