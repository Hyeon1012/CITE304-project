using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemType itemType;

    [Header("Sprites")]
    [SerializeField] private Sprite acceleratorSprite;
    [SerializeField] private Sprite jumpBoostSprite;
    [SerializeField] private Sprite effectlessSprite;

    [Header("Movement")]
    [SerializeField] private float moveUpDistance = 1f;
    [SerializeField] private float moveUpSpeed = 3f;

    private SpriteRenderer _sr;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingUp = false;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        startPos = transform.position;
        targetPos = startPos + Vector3.up * moveUpDistance;

        UpdateSprite();
    }

    private void Update()
    {
        if (!movingUp) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            moveUpSpeed * Time.deltaTime
        );
    }

    public void MoveUp()
    {
        if (movingUp) return;

        movingUp = true;
    }

    private void UpdateSprite()
    {
        switch (itemType)
        {
            case ItemType.Accelerator:
                _sr.sprite = acceleratorSprite;
                break;

            case ItemType.JumpBoost:
                _sr.sprite = jumpBoostSprite;
                break;

            default:
                _sr.sprite = effectlessSprite;
                break;
        }
    }

    private void OnValidate()
    {
        _sr = GetComponent<SpriteRenderer>();
        if (_sr != null)
            UpdateSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerStateManager player = collision.GetComponent<PlayerStateManager>();
        if (player == null) return;

        player.ApplyItem(itemType);

        Destroy(gameObject);
    }
}