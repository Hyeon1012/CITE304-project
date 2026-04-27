using UnityEngine;

public class CameraRoomTrigger : MonoBehaviour
{
    public Transform targetRoomPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RoomCameraController.Instance.MoveToRoom(targetRoomPoint);
        }
    }
}