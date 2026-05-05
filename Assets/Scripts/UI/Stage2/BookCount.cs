using TMPro;
using UnityEngine;

public class BookCount : MonoBehaviour
{
    public int bookCount = 0;
    public const int bookMax = 3;

    private TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
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
    }
}
