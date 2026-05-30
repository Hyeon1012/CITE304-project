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
            player.KillPlayer("flameGoal");
        }

        else
        {
            GameManager.Instance.levelReached = GameManager.Instance.levelReached < 2 ? 2 : GameManager.Instance.levelReached;
            GameManager.Instance.sceneChanger.GoToLevelSelect();
            Debug.Log("Level Select called");
        }
    }
}