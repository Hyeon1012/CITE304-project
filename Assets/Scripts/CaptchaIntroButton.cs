using UnityEngine;

public class CaptchaIntroButton : MonoBehaviour
{
    [SerializeField] private GameObject _introWindow;
    [SerializeField] private GameObject _mainCaptchaBoard;

    void OnMouseDown()
    {
        if (_introWindow != null)
        {
            _introWindow.SetActive(false);
        }
        if (_mainCaptchaBoard != null)
        {
            _mainCaptchaBoard.SetActive(true);
        }

    }
}