using UnityEngine;

public class Firebar : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = -100f;
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStateManager>().KillPlayer();
        }
    }
}