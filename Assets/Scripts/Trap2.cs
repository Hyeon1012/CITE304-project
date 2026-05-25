using UnityEngine;

public class Trap2 : MonoBehaviour
{
    [SerializeField] private Transform _leftFirebar;
    [SerializeField] private Transform _rightFirebar;
    [SerializeField] private GameObject _quizSquare;
    [SerializeField] private GameObject _coreItem;

    [SerializeField] private float _closeSpeed = 2f;
    [SerializeField] private float _pushBackDistance = 3f;
    [SerializeField] private int _totalQuizzes = 3;

    private int _correctCount = 0;
    private bool _isActivated = false;

    void Start()
    {
        if (_leftFirebar != null) _leftFirebar.gameObject.SetActive(false);
        if (_rightFirebar != null) _rightFirebar.gameObject.SetActive(false);
        if (_coreItem != null) _coreItem.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isActivated)
        {
            _isActivated = true;
            if (_leftFirebar != null) _leftFirebar.gameObject.SetActive(true);
            if (_rightFirebar != null) _rightFirebar.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (_isActivated)
        {
            if (_leftFirebar != null)
                _leftFirebar.Translate(Vector3.right * _closeSpeed * Time.deltaTime, Space.World);

            if (_rightFirebar != null)
                _rightFirebar.Translate(Vector3.left * _closeSpeed * Time.deltaTime, Space.World);
        }
    }

    public void OnQuizCorrect()
    {
        if (!_isActivated) return;

        _correctCount++;
        Debug.Log($"현재 정답 수: {_correctCount} / {_totalQuizzes}");

        if (_correctCount >= _totalQuizzes)
        {
            ClearTrap();
        }
        else
        {
            if (_leftFirebar != null)
                _leftFirebar.position += Vector3.left * _pushBackDistance;

            if (_rightFirebar != null)
                _rightFirebar.position += Vector3.right * _pushBackDistance;
        }
    }

    private void ClearTrap()
    {
        _isActivated = false;

        if (_leftFirebar != null) _leftFirebar.gameObject.SetActive(false);
        if (_rightFirebar != null) _rightFirebar.gameObject.SetActive(false);
        if (_quizSquare != null) _quizSquare.SetActive(false);
        if (_coreItem != null) _coreItem.SetActive(true);
    }
}