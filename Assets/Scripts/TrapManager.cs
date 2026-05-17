using System.Collections;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    [SerializeField] private GameObject _hiddenCanonsGroup;
    [SerializeField] private GameObject _hiddenWallsGroup;
    [SerializeField] private float _survivalTime = 10f;
    private bool _isActivated = false;

    void Start()
    {
        if (_hiddenCanonsGroup != null) _hiddenCanonsGroup.SetActive(false);
        if (_hiddenWallsGroup != null) _hiddenWallsGroup.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isActivated)
        {
            _isActivated = true;
            if (_hiddenCanonsGroup != null) _hiddenCanonsGroup.SetActive(true);
            if (_hiddenWallsGroup != null) _hiddenWallsGroup.SetActive(true);
            StartCoroutine(SurvivalTimer());
        }
    }
    IEnumerator SurvivalTimer()
    {
        yield return new WaitForSeconds(_survivalTime);
        if (_hiddenWallsGroup != null)
        {
            _hiddenWallsGroup.SetActive(false);
        }
    }
}