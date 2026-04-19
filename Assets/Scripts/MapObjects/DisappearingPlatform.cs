using UnityEngine;

public class DisappearingPlatform2D : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 2.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}