using UnityEngine;

public class MouseLight : MonoBehaviour
{
    private Vector3 _mousePosition;
    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(_mousePosition.x, _mousePosition.y);
    }
}