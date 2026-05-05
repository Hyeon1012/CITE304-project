using UnityEngine;

public class ElevatorTrap : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject interactionPrompt; // The child object with the text
    [SerializeField] private float descendSpeed = 2.0f;

    private GameObject playerObject;
    private bool isPlayerInRange = false;
    private bool isDescending = false;

    void Start()
    {
        // Ensure the prompt is hidden at the start
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    void Update()
    {
        // Only allow interaction if the player is there and we haven't started descending
        if (isPlayerInRange && !isDescending)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ActivateTrap();
            }
        }

        // If the trap is sprung, move the elevator down
        if (isDescending)
        {
            transform.Translate(Vector3.down * descendSpeed * Time.deltaTime);
        }
    }

    private void ActivateTrap()
    {
        isDescending = true;

        // Hide the prompt and the player
        if (interactionPrompt != null) interactionPrompt.SetActive(false);

        if (playerObject != null)
        {
            playerObject.SetActive(false);
            Debug.Log("ElevatorTrap: Player trapped. Descending...");
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

            // Clean up reference only if we aren't currently using it
            playerObject = null;
        }
    }
}