using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Vector2 _boxSize = new Vector2(0.815f, 0.1f);
    [SerializeField] private LayerMask _groundLayer;

    private PlayerMovement _playerMovement;

    public bool isGrounded { get; private set; }
    public Collider2D currentPlatform { get; private set; }

    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        Collider2D collide = Physics2D.OverlapBox(transform.position, _boxSize, 0f, _groundLayer);
        
        if (collide != null)
        {
            isGrounded = true;
            currentPlatform = collide;
        }
        else isGrounded = false;
    }

    public Vector2 GetGroundVelocity()
    {
        if (!isGrounded || currentPlatform.gameObject.GetComponent<Rigidbody2D>() == null) return Vector2.zero;
        return currentPlatform.gameObject.GetComponent<Rigidbody2D>().linearVelocity;
    }
}
