using UnityEngine;

public class SignPopup2D : MonoBehaviour
{
    [Header("Follow Settings")]
    [Tooltip("How far offset from the player's center the popup should be.")]
    [SerializeField] private Vector3 popupOffset = new Vector3(0, 0, 0);

    private GameObject[] popups = new GameObject[6];
    private int currentIndex = -1;

    // Replaced playerIsInside with a state tracker for the sequence
    private bool isSequenceActive = false;
    private Transform playerTransform;

    void Start()
    {
        if (transform.childCount < 6)
        {
            Debug.LogError("SignPopup2D needs 6 children! You only have " + transform.childCount);
            return;
        }

        for (int i = 0; i < 6; i++)
        {
            popups[i] = transform.GetChild(i).gameObject;
            popups[i].SetActive(false);
        }
    }

    void Update()
    {
        // If the sequence hasn't started or has finished, ignore all input/follow logic
        if (!isSequenceActive) return;

        // --- FOLLOW PLAYER LOGIC ---
        if (currentIndex >= 0 && currentIndex < popups.Length && playerTransform != null)
        {
            popups[currentIndex].transform.position = playerTransform.position + popupOffset;
        }

        // Logic for screens 1, 2, and 3 (Indices 0, 1, 2)
        if (currentIndex >= 0 && currentIndex <= 2)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                ShowPopup(currentIndex + 1);
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                ShowPopup(4); // Jump to Child 5 (Index 4)
            }
        }
        // Logic for screens 4 and 5 (Indices 3 and 4)
        else if (currentIndex == 3 || currentIndex == 4)
        {
            if (AnyKeyExceptWASD())
            {
                ShowPopup(currentIndex + 1); // Go to the next screen
            }
        }
        // Logic for screen 6 (Index 5) - Dismiss the final screen
        else if (currentIndex == 5)
        {
            if (AnyKeyExceptWASD())
            {
                EndSequence();
            }
        }
    }

    private void ShowPopup(int index)
    {
        if (currentIndex >= 0 && currentIndex < popups.Length)
        {
            popups[currentIndex].SetActive(false);
        }

        currentIndex = index;
        popups[currentIndex].SetActive(true);
    }

    // New method to handle cleaning up once the player closes the final screen
    private void EndSequence()
    {
        if (currentIndex >= 0 && currentIndex < popups.Length)
        {
            popups[currentIndex].SetActive(false);
        }

        isSequenceActive = false;
        currentIndex = -1; // Reset so the player can read the sign again if they walk back to it
        playerTransform = null;
    }

    private bool AnyKeyExceptWASD()
    {
        if (Input.anyKeyDown)
        {
            bool isMovementKey =
                Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
                Input.GetKeyDown(KeyCode.Space);

            if (!isMovementKey)
            {
                return true;
            }
        }
        return false;
    }

    // --- 2D TRIGGER DETECTION ---
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only trigger if it's the player AND the sequence isn't already running
        if (other.CompareTag("Player") && !isSequenceActive)
        {
            isSequenceActive = true;
            playerTransform = other.transform;
            ShowPopup(0);
        }
    }

    // Notice that OnTriggerExit2D has been completely removed!
}