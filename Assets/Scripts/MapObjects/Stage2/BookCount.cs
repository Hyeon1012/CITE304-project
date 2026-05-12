using System.Collections;
using TMPro;
using UnityEngine;

public class BookCount : MonoBehaviour
{
    public int bookCount { get; private set; }
    public const int bookMax = 3;

    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _bookCollectedUI;
    [SerializeField] private float waitTime = 3f;

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
        StartCoroutine(PopUp());
        updateText();
    }

    IEnumerator PopUp()
    {
        _bookCollectedUI.SetActive(true);
        _bookCollectedUI.GetComponent<BookCollectedUI>().Init(bookMax, bookCount);
        yield return new WaitForSeconds(waitTime);
        _bookCollectedUI.SetActive(false);
        if (bookCount == bookMax)
        {
            _door.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void updateText()
    {
        _text.text = bookCount + " / " + bookMax;
    }
}
