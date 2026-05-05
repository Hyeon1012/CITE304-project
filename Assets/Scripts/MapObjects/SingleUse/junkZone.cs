using UnityEngine;
using System.Collections;

public class JunkZone : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject junkPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float heightOffsetA = -5f; // Relative to zone center
    [SerializeField] private float heightOffsetB = 5f;  // Relative to zone center

    [Header("Junk Physics")]
    [SerializeField] private float junkSpeed = 5f;
    [SerializeField] private float junkBounce = 50f;

    private BoxCollider2D zoneCollider;
    private bool playerInside = false;
    private bool useHeightA = true;
    private Coroutine spawnRoutine;

    private void Awake()
    {
        zoneCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerInside)
        {
            playerInside = true;
            spawnRoutine = StartCoroutine(SpawnJunk());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            if (spawnRoutine != null) StopCoroutine(spawnRoutine);
        }
    }

    IEnumerator SpawnJunk()
    {
        while (playerInside)
        {
            // X is the right edge, slightly nudged inward
            float spawnX = zoneCollider.bounds.max.x - 4f;

            // Y is now relative to the Zone's center position
            float chosenOffset = useHeightA ? heightOffsetA : heightOffsetB;
            float spawnY = transform.position.y + chosenOffset;

            Vector3 spawnPos = new Vector3(spawnX, spawnY, transform.position.z);

            GameObject newJunk = Instantiate(junkPrefab, spawnPos, Quaternion.identity);
            Debug.Log($"<color=green>Junk Spawned</color> at {spawnPos}. Offset used: {chosenOffset}");

            if (newJunk.TryGetComponent(out BouncingJunk junkScript))
            {
                junkScript.Setup(junkSpeed, junkBounce, zoneCollider);
            }

            useHeightA = !useHeightA;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}