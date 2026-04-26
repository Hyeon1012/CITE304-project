using UnityEngine;

public class FlameGoalBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerStateManager player = collision.GetComponent<PlayerStateManager>();
        if (player == null) return;

        //only kill if the player does NOT have WaterBucket
        if (!player.HasItem(ItemType.WaterBucket))
        {
            player.KillPlayer();
        }

        else
        {
            //placeholder for a scene switching function
        }
    }
}