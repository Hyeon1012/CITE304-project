using UnityEngine;

public class Cannonball : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5f;
    void Start()
    {
        Destroy(gameObject, _lifeTime);
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