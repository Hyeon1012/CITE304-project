using UnityEngine;

public class HiddenBlock : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D solidCollider;
    private BoxCollider2D triggerCollider;

    [SerializeField] private float minUpwardVelocity = 0.1f;

    private bool activated = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get both colliders
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach (var col in colliders)
        {
            if (col.isTrigger) triggerCollider = col;
            else solidCollider = col;
        }

        // Start invisible and non-solid
        spriteRenderer.enabled = false;
        solidCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated) return;
        if (!collision.CompareTag("Player")) return;

        Rigidbody2D rb = collision.attachedRigidbody;
        if (rb == null) return;

        // 1. Must be moving upward
        if (rb.linearVelocity.y <= minUpwardVelocity) return;

        // 2. Check if player's HEAD is below the block
        if (collision.bounds.max.y > transform.position.y) return;

        ActivateBlock(rb);
    }

    private void ActivateBlock(Rigidbody2D rb)
    {
        activated = true;

        // Enable visuals + solid collision
        spriteRenderer.enabled = true;
        solidCollider.enabled = true;

        // Disable trigger so it doesn't fire again
        triggerCollider.enabled = false;

        // Stop upward motion (the "bonk")
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

        // Slight push down to prevent clipping
        rb.position += Vector2.down * 0.02f;

        Debug.Log("Hidden Block Revealed!");
    }
}