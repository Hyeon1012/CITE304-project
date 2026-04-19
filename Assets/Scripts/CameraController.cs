using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offsetX = 0f;

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = player.position.x + offsetX;
            transform.position = newPosition;
        }
    }
}