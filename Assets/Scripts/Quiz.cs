using UnityEngine;

public class Quiz : MonoBehaviour
{
    [SerializeField] private Trap2 _trapManager;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (_trapManager != null)
        {
            _trapManager.OnQuizCorrect();
            Destroy(gameObject);
        }
    }
}
