using UnityEngine;

public class FakeHideBox2 : MonoBehaviour
{
    public GameObject Book;
    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.S))
        {
            if (Book != null) Book.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
