using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private BookCount _bookCount;
    public AudioClip obtainClip;
    public GameObject tempSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _bookCount.bookObtained();
            GameObject temp = Instantiate(tempSound, transform.parent);
            temp.GetComponent<TempSound>()?.Init(obtainClip);
            Destroy(gameObject);
        }
    }
}
