using UnityEngine;

public class QuizButton : MonoBehaviour
{
    [SerializeField] private bool _isOButton;
    [SerializeField] private Quiz _quizManager;

    void OnMouseDown()
    {
        if (_quizManager != null)
        {
            if (_isOButton)
            {
                _quizManager.SelectO();
            }
            else
            {
                _quizManager.SelectX();
            }
        }
    }
}