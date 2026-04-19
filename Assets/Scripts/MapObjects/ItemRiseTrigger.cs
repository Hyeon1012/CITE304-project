using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    [SerializeField] private ItemPickup item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        item.MoveUp();
    }
}