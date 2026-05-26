using UnityEngine;

public class CaptchaButton : MonoBehaviour
{
    [SerializeField] private int _buttonIndex;
    [SerializeField] private Captcha _captcha;
    [SerializeField] private GameObject _checkmark;

    private bool _isSelected = false;

    void Start()
    {
        if (_checkmark != null) _checkmark.SetActive(false);
    }

    void OnMouseDown()
    {
        _isSelected = !_isSelected;

        if (_checkmark != null) _checkmark.SetActive(_isSelected);

        if (_captcha != null)
        {
            _captcha.ToggleSelection(_buttonIndex, _isSelected);
        }
    }
}