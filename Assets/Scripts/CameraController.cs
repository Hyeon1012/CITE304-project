using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offsetX = 0f;
    [SerializeField] private float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (player != null)
        {
            float targetX = player.position.x + offsetX;
            float smoothedX = Mathf.Lerp(transform.position.x, targetX, smoothSpeed * Time.deltaTime);
            transform.position = new Vector3(smoothedX, transform.position.y, transform.position.z);
        }
    }
}