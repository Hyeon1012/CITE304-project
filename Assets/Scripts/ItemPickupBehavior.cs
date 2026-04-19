using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemType itemType;

    [Header("Sprites")]
    [SerializeField] private Sprite acceleratorSprite;
    [SerializeField] private Sprite jumpBoostSprite;
    [SerializeField] private Sprite effectlessSprite;


    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        UpdateSprite();
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