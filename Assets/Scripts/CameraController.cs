using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offsetX = 0f;
    [SerializeField] private float maxY = 50f;
    [SerializeField] private float minY = -4f;
    [SerializeField] private float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (player != null)
        {
            float targetX = player.position.x + offsetX;
            float targetY = player.position.y;

            if (targetY > maxY)
            {
                targetY = maxY;
            }
            else if (targetY < minY)
            {
                targetY = minY;
            }

            float smoothedX = Mathf.Lerp(transform.position.x, targetX, smoothSpeed * Time.deltaTime);
            float smoothedY = Mathf.Lerp(transform.position.y, targetY, smoothSpeed * Time.deltaTime);


            transform.position = new Vector3(smoothedX, smoothedY, transform.position.z);
        }
    }
}