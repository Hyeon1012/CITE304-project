using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Chaser : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 4f;

    [SerializeField] private float _delay = 2.0f;

    private Rigidbody2D rb;
    private float dirX;
    private bool start = false;
    private float timer;

    public void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        dirX = Mathf.Sign(target.position.x - transform.position.x);
        timer = _delay;
        start = true;
    }

    void Update()
    {
        if (start)
        {
            if (timer <= 0)
            {
                rb.linearVelocityX = dirX * moveSpeed;
                if (start && (target == null || transform.position.x - target.position.x > 100f))
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStateManager>().KillPlayer();
        }
    }
}