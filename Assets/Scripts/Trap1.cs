using System.Collections;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    [SerializeField] private GameObject _hiddenCanonsGroup;
    [SerializeField] private GameObject _appearWall; // firebar (5)
    [SerializeField] private GameObject _blockingWall; // firebar (6)
    [SerializeField] private float _survivalTime = 10f;
    private bool _isActivated = false;

    void Start()
    {
        if (_hiddenCanonsGroup != null) _hiddenCanonsGroup.SetActive(false);
        if (_appearWall != null) _appearWall.SetActive(false);
        if (_blockingWall != null) _blockingWall.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isActivated)
        {
            _isActivated = true;
            if (_hiddenCanonsGroup != null) _hiddenCanonsGroup.SetActive(true);
            if(_appearWall != null) _appearWall.SetActive(true);
            StartCoroutine(SurvivalTimer());
        }
    }
    IEnumerator SurvivalTimer()
    {
        yield return new WaitForSeconds(_survivalTime);
        if (_hiddenCanonsGroup != null) _hiddenCanonsGroup.SetActive(false);
        if (_appearWall != null) _appearWall.SetActive(false);
        if (_blockingWall != null) _blockingWall.SetActive(false);
    }
}