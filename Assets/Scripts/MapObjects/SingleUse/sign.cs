using UnityEngine;

public class SignPopup2D : MonoBehaviour
{
    private GameObject[] popups = new GameObject[6];
    private int currentIndex = -1;
    private bool playerIsInside = false;

    void Start()
    {
        // Check if we have enough children
        if (transform.childCount < 6)
        {
            Debug.LogError("SignPopup2D needs 6 children! You only have " + transform.childCount);
            return;
        }

        // Initialize: Find all 6 children and hide them immediately
        for (int i = 0; i < 6; i++)
        {
            popups[i] = transform.GetChild(i).gameObject;
            popups[i].SetActive(false);
        }
    }

    void Update()
    {
        // Only run logic if the player is standing in the trigger
        if (!playerIsInside) return;

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
        // Logic for screen 4 (Index 3)
        else if (currentIndex == 3)
        {
            if (AnyKeyExceptWASD())
            {
                ShowPopup(4); // Go to Child 5
            }
        }
        // Logic for screen 5 (Index 4)
        else if (currentIndex == 4)
        {
            if (AnyKeyExceptWASD())
            {
                ShowPopup(5); // Go to Child 6
            }
        }
        // Screen 6 (Index 5) stays visible once reached
    }

    private void ShowPopup(int index)
    {
        // Hide the current popup if one is active
        if (currentIndex >= 0 && currentIndex < popups.Length)
        {
            popups[currentIndex].SetActive(false);
        }

        // Update index and show the new one
        currentIndex = index;
        popups[currentIndex].SetActive(true);
    }

    private bool AnyKeyExceptWASD()
    {
        if (Input.anyKeyDown)
        {
            // Exclude WASD, Arrow Keys, and Space
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
        if (other.CompareTag("Player"))
        {
            playerIsInside = true;
            // Only show the first child if the sequence hasn't started yet
            if (currentIndex == -1)
            {
                ShowPopup(0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
        }
    }
}