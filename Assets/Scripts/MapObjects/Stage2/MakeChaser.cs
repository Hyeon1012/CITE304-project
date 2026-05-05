using UnityEngine;

public class MakeChaser : MonoBehaviour
{
    public GameObject chaserPrefab;
    public Transform target;
    public Transform player;
    public Vector3 targetPosition;

    [SerializeField] private Vector3 offset = new Vector3(-40f, 0);

    public void makeChaser()
    {
        targetPosition = new Vector3(target.position.x, 0, 0) + offset;
        GameObject chaser = Instantiate(chaserPrefab, transform);
        chaser.transform.position = targetPosition;
        chaser.GetComponent<Chaser>().target = player;
        chaser.GetComponent<Chaser>().Init();
    }
}
