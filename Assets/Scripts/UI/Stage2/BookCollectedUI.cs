using TMPro;
using UnityEngine;

public class BookCollectedUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Init(int bookMax, int bookCount)
    {
        text.text = bookCount + " / " + bookMax + " Collected";
    }
}
