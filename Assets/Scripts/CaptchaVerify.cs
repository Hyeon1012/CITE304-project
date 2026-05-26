using UnityEngine;

public class CaptchaVerifyButton : MonoBehaviour
{
    [SerializeField] private Captcha _captcha;

    // 마우스로 VERIFY 버튼을 클릭하면 작동해! (BoxCollider2D 필수!)
    void OnMouseDown()
    {
        if (_captcha != null)
        {
            // 매니저한테 "지금까지 고른 걸로 채점해 줘!" 라고 데이터를 보내는 거야...
            _captcha.VerifyCaptcha();
        }
    }
}