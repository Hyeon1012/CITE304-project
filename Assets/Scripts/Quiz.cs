using UnityEngine;

public class Quiz : MonoBehaviour
{
    [SerializeField] private bool[] _answers; // Correct -> check, Incorrect -> not check
    [SerializeField] private Sprite[] _quizImages;
    [SerializeField] private Trap2 _trapManager;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _captcha;

    private int _currentIndex = 0;

    void Start()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateQuizImage();
    }

    public void SelectO()
    {
        CheckAnswer(true);
    }

    public void SelectX()
    {
        CheckAnswer(false);
    }

    private void CheckAnswer(bool playerChoice)
    {
        if (_currentIndex >= _answers.Length) return;

        if (playerChoice == _answers[_currentIndex])
        {
            Debug.Log($"정답! (현재 {_currentIndex + 1} / {_answers.Length})");

            _currentIndex++;

            if (_trapManager != null)
            {
                _trapManager.OnQuizCorrect();
            }

            UpdateQuizImage();
        }
        else
        {
            Debug.Log("Wrong");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerStateManager stateManager = player.GetComponent<PlayerStateManager>();
                if (stateManager != null)
                {
                    stateManager.KillPlayer();
                }
            }
        }
    }

    private void UpdateQuizImage()
    {
        if (_currentIndex < _answers.Length && _quizImages != null && _currentIndex < _quizImages.Length)
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.sprite = _quizImages[_currentIndex];
            }
        }
        else if (_currentIndex >= _answers.Length)
        {
            gameObject.SetActive(false);

            if (_captcha != null)
            {
                _captcha.SetActive(true);
            }
        }
    }
}