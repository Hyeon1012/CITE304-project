using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private BookCount _bookCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _bookCount.bookObtained();
            Destroy(gameObject);
        }
    }
}
