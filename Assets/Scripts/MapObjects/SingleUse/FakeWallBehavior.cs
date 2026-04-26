using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FakeWallBehavior : MonoBehaviour
{
    private SpriteRenderer sr;

    [Range(0f, 1f)]
    [SerializeField] private float transparentAlpha = 0.1f; //90% transparent

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        SetAlpha(transparentAlpha);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        SetAlpha(1f); //fully opaque
    }

    private void SetAlpha(float alpha)
    {
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;
    }
}