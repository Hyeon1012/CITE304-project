using UnityEngine;

public class BouncingJunk : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private float rotationSpeed = 360f;

    private float speed;
    private float bounceForce;
    private Collider2D zoneCollider;

    private Rigidbody2D rb;
    private Collider2D myCollider;
    private bool isInitialized = false;
    private float birthTime;

    public void Setup(float moveSpeed, float force, Collider2D zone)
    {
        speed = moveSpeed;
        bounceForce = force;
        zoneCollider = zone;
        isInitialized = true;
        birthTime = Time.time;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!isInitialized) return;

        // 1. Maintain leftward velocity
        rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);

        // 2. Spin like a tumbleweed (Clockwise for leftward movement)
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);

        // 3. Despawn logic: Wait 0.1s before checking bounds to avoid instant death
        if (Time.time > birthTime + 0.1f)
        {
            if (zoneCollider != null && !myCollider.IsTouching(zoneCollider))
            {
                Debug.Log($"<color=red>Junk Despawned</color>: Left bounds of {zoneCollider.name}");
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Bounce logic
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.linearVelocity = new Vector2(-speed, bounceForce);
        }

        // Kill logic (Collision)
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerDeath(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kill logic (Trigger - in case your junk collider is a trigger)
        if (collision.CompareTag("Player"))
        {
            HandlePlayerDeath(collision.gameObject);
        }
    }

    private void HandlePlayerDeath(GameObject playerObj)
    {
        PlayerStateManager player = playerObj.GetComponent<PlayerStateManager>();
        if (player != null)
        {
            player.KillPlayer();
        }
    }
}