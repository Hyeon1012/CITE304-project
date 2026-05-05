using TMPro;
using UnityEngine;

public class BookCount : MonoBehaviour
{
    public int bookCount { get; private set; }
    public const int bookMax = 3;

    [SerializeField] private GameObject _door;

    private TextMeshPro _text;
    
    void Start()
    {
        bookCount = 0;
        _text = GetComponent<TextMeshPro>();
        updateText();
    }

    public void bookObtained()
    {
        bookCount++;
        updateText();
    }

    public void updateText()
    {
        _text.text = bookCount + " / " + bookMax;

        if (bookCount == bookMax)
        {
            _door.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
