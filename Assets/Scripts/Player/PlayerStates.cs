using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public enum PlayerState { Normal, Dead, PoweredUp }
    public PlayerState currentState = PlayerState.Normal;

    public void KillPlayer()
    {
        if (currentState == PlayerState.Dead) return;

        currentState = PlayerState.Dead;
        Debug.Log("Player has died!");

        Destroy(gameObject);
    }

    //no logic yet
    public void ChangeState(PlayerState newState)
    {
        currentState = newState;
    }
}