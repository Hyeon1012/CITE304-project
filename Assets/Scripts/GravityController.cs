using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] private float flipInterval = 5f;
    private float timer = 0f;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        if (playerMovement == null) return;
        timer += Time.deltaTime;
        if (timer >= flipInterval)
        {
            playerMovement.gravityMultiplier *= -1f;
            timer = 0f;
        }
    }
}