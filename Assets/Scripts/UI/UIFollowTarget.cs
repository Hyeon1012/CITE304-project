using UnityEngine;

public class UIFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = Vector3.zero;

    private Camera mainCamera;
    private RectTransform _rectTransform;

    void Start()
    {
        mainCamera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        if (_target == null) return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(_target.position + _offset);

        _rectTransform.position = screenPos;
    }
}
