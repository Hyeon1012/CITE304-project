using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{
    [SerializeField] private Hazard spike;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        spike.MoveUp();
    }
}