using UnityEngine;

public class FlashLight : MonoBehaviour
{
    private Vector3 _mousePosition;
    private Vector2 _direction;
    private float _angle;

    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = _mousePosition - transform.position;

        _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, _angle - 90f);
    }
}
