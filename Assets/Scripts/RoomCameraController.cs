using UnityEngine;

public class RoomCameraController : MonoBehaviour
{
    public static RoomCameraController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void MoveToRoom(Transform roomPoint)
    {
        Vector3 newPos = roomPoint.position;
        newPos.z = transform.position.z; // 카메라 z 유지
        transform.position = newPos;
    }
}