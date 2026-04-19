using UnityEngine;

public class Hazard : MonoBehaviour
{
    [Header("Spike Movement")]
    [SerializeField] private float riseHeight = 2f;
    [SerializeField] private float riseSpeed = 5f;

    [Header("Return Settings")]
    [SerializeField] private bool returnToStart = false;
    [SerializeField] private float returnDelay = 0f;

    private Vector3 startPos;
    private Vector3 targetPos;

    private bool activated = false;
    private bool returning = false;
    private float delayTimer = 0f;

    private void Awake()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.up * riseHeight;
    }

    private void Update()
    {
        if (!activated) return;

        //rising phase
        if (!returning)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                riseSpeed * Time.deltaTime
            );

            //reached top
            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                if (returnToStart)
                {
                    delayTimer += Time.deltaTime;

                    if (delayTimer >= returnDelay)
                    {
                        returning = true;
                    }
                }
            }
        }
        //returning phase
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                startPos,
                riseSpeed * Time.deltaTime
            );
        }
    }

    public void MoveUp()
    {
        if (activated) return;

        activated = true;
        delayTimer = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerStateManager player = collision.GetComponent<PlayerStateManager>();

        if (player != null)
        {
            player.KillPlayer();
        }
    }
}