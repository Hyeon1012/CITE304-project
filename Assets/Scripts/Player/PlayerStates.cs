using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public enum PlayerState { Normal, Dead }
    public PlayerState currentState = PlayerState.Normal;

    public ItemType currentItem = ItemType.None;

    public void KillPlayer()
    {
        if (currentState == PlayerState.Dead) return;

        currentState = PlayerState.Dead;
        Debug.Log("Player has died!");

        Destroy(gameObject);
    }

    public void ChangeState(PlayerState newState)
    {
        currentState = newState;
    }

    public void ApplyItem(ItemType item)
    {
        currentItem = item;
        Debug.Log("Picked up item: " + item);
    }

    // --- ITEM EFFECT QUERIES ---

    public bool HasItem(ItemType queryItem = ItemType.None)
    {
        return currentItem == queryItem;
    }



    public float GetJumpMultiplier()
    {
        if (currentItem == ItemType.JumpBoost)
            return 2f;

        return 1f;
    }
}