using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ClockTimer targetClock;

    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";

    private bool hasTriggered = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the thing that stepped on us is the player
        if (collision.gameObject.CompareTag(playerTag) && !hasTriggered)
        {
            TriggerClock();
        }
    }

    // Alternatively, use this if your collider is set to "Is Trigger"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && !hasTriggered)
        {
            TriggerClock();
        }
    }

    private void TriggerClock()
    {
        if (targetClock != null)
        {
            hasTriggered = true;
            targetClock.StartTimer();
            Debug.Log("Platform stepped on. Signaling the ClockTimer.");
        }
        else
        {
            Debug.LogWarning("TriggerPlatform: No ClockTimer assigned!");
        }
    }
}