using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _gravityScale = 2.0f;
    [SerializeField] private float _fallGravityScale = 2.5f;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Collider2D _playerCollider;
    private GroundChecker _groundChecker;
    private float _moveInput = 0;

    public event Action OnPlayerJumped;
    public event Action OnPlayerWalking;
    public event Action OnPlayerStopWalking;

    public float direction { get; private set; }

    void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _sr = gameObject.GetComponent<SpriteRenderer>();
        _playerCollider = gameObject.GetComponent<Collider2D>();
        _groundChecker = gameObject.GetComponent<GroundChecker>();
        direction = 1;
    }

    public void Init()
    {
        GameManager.Instance.inputManager.HorizontalKey += OnMoveInput;
        GameManager.Instance.inputManager.JumpKey += OnJumpInput;
        GameManager.Instance.inputManager.DownJumpKey += OnDownJumpInput;
    }

    private void OnMoveInput(float moveInput)
    {
        _moveInput = moveInput;
        if (_moveInput != 0 && _moveInput != direction) direction = _moveInput;
        if (_moveInput == 0) OnPlayerStopWalking.Invoke();
        else OnPlayerWalking.Invoke();
    }

    private void OnJumpInput()
    {
        if (_groundChecker.isGrounded) //&& _rb.linearVelocityY == 0)
        {
            OnPlayerJumped.Invoke();
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }

    private void OnDownJumpInput()
    {
        if (_groundChecker.isGrounded && _groundChecker.currentPlatform != null &&
            _groundChecker.currentPlatform.CompareTag("Platform"))
        {
            StartCoroutine(PassThroughPlatform(_groundChecker.currentPlatform));
        }
    }
    IEnumerator PassThroughPlatform(Collider2D platformCollider)
    {
        Physics2D.IgnoreCollision(_playerCollider, platformCollider, true);
        yield return new WaitUntil(() => _playerCollider.bounds.max.y < platformCollider.bounds.min.y);
        Physics2D.IgnoreCollision(_playerCollider, platformCollider, false);
    }

    void Update()
    {
        if (_moveInput == 0) OnPlayerStopWalking.Invoke();
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_moveInput * _speed, _rb.linearVelocityY) + _groundChecker.GetGroundVelocity();

        if (_moveInput < 0) _sr.flipX = true;
        else if (_moveInput > 0) _sr.flipX = false;

        if (_rb.linearVelocityY < 0) _rb.gravityScale = _fallGravityScale;
        else _rb.gravityScale = _gravityScale;
    }

    void OnDestroy()
    {
        if (GameManager.Instance.inputManager != null)
        {
            GameManager.Instance.inputManager.HorizontalKey -= OnMoveInput;
            GameManager.Instance.inputManager.JumpKey -= OnJumpInput;
            GameManager.Instance.inputManager.DownJumpKey -= OnDownJumpInput;
        }
    }
}
