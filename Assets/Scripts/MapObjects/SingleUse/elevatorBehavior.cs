using UnityEngine;
using System.Collections;

public class ElevatorTrap : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private float descendSpeed = 2.0f;

    [Header("Trap Settings")]
    [SerializeField] private float timeUntilDeath = 3.0f;

    private GameObject playerObject;
    private bool isPlayerInRange = false;
    private bool isDescending = false;

    void Start()
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && !isDescending)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ActivateTrap();
            }
        }

        if (isDescending)
        {
            transform.Translate(Vector3.down * descendSpeed * Time.deltaTime);
        }
    }

    private void ActivateTrap()
    {
        isDescending = true;

        if (interactionPrompt != null) interactionPrompt.SetActive(false);

        if (playerObject != null)
        {
            // Note: If KillPlayer() needs to play animations or sounds on the player, 
            // you might want to remove this SetActive(false) and let KillPlayer() handle hiding the player instead!
            playerObject.SetActive(false);
            Debug.Log("ElevatorTrap: Player trapped. Descending...");
        }

        // Pass the trapped player into the coroutine
        StartCoroutine(TriggerDeathSequence(playerObject));
    }

    private IEnumerator TriggerDeathSequence(GameObject trappedPlayer)
    {
        yield return new WaitForSeconds(timeUntilDeath);

        Debug.Log("ElevatorTrap: 3 seconds have passed! Executing HandlePlayerDeath...");

        // Call your custom death function
        if (trappedPlayer != null)
        {
            HandlePlayerDeath(trappedPlayer);
        }
    }

    // Your custom player killing function
    private void HandlePlayerDeath(GameObject playerObj)
    {
        PlayerStateManager player = playerObj.GetComponent<PlayerStateManager>();
        if (player != null)
        {
            player.KillPlayer();
        }
        else
        {
            Debug.LogWarning("ElevatorTrap: Could not find PlayerStateManager on the trapped object!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDescending)
        {
            isPlayerInRange = true;
            playerObject = collision.gameObject;

            if (interactionPrompt != null)
                interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDescending)
        {
            isPlayerInRange = false;

            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);

            playerObject = null;
        }
    }
}