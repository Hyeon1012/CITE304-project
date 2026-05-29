using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _gravityScale = 2.0f;
    [SerializeField] private float _fallGravityScale = 2.5f;
    [SerializeField] private float _shiftRate = 0.3f;
    [SerializeField] private bool _shiftTurnOn = true;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Collider2D _playerCollider;
    private GroundChecker _groundChecker;
    private float _moveInput = 0f;

    private float _fallDistance = 0f;
    private bool _shift = false;

    public event Action OnPlayerJumped;
    public event Action OnPlayerWalking;
    public event Action OnPlayerStopWalking;

    public float direction { get; private set; }

    private PlayerStateManager _stateManager;
    private PlayerNoiseMaker _noiseMaker;
    private PlayerSound _sound;
    private float _currentSpeed;
    private float _gravityMultiplier = 1f;

    public float gravityMultiplier
    {
        get { return _gravityMultiplier; }
        set
        {
            if (_gravityMultiplier != value)
            {
                _gravityMultiplier = value;
                float targetAngle = _gravityMultiplier < 0 ? 180f : 0f;
                StartCoroutine(SmoothRotate(targetAngle, 0.5f));
            }
        }
    }

    void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _sr = gameObject.GetComponent<SpriteRenderer>();
        _playerCollider = gameObject.GetComponent<Collider2D>();
        _groundChecker = gameObject.GetComponent<GroundChecker>();
        _stateManager = gameObject.GetComponent<PlayerStateManager>();
        
        _noiseMaker = gameObject.GetComponent<PlayerNoiseMaker>();
        _sound = gameObject.GetComponent<PlayerSound>();

        direction = 1;
        _currentSpeed = _speed;
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
        if (_moveInput == 0)
        {
            OnPlayerStopWalking?.Invoke();
            _sound?.StopMovingSound();
        }
        else
        {
            OnPlayerWalking?.Invoke();
        }
    }

    private void OnJumpInput()
    {
        if (_groundChecker.isGrounded)
        {

            if(_shift)
            {
                _sound?.WeakJumpSound();
            }
            else
            {
                _sound?.JumpSound();
            }

            OnPlayerJumped?.Invoke();

            float jumpMultiplier = _stateManager != null ? _stateManager.GetJumpMultiplier() : 1f;
            float shiftMultiplier = (_shiftTurnOn && GameManager.Instance.inputManager.ShiftKey) ? shiftMultiplier = Mathf.Sqrt(_shiftRate) : 1f;
            _rb.AddForce(Vector2.up * gravityMultiplier * _jumpPower * jumpMultiplier * shiftMultiplier, ForceMode2D.Impulse);
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
        if (GameManager.Instance.IsPaused) return;

        if (_moveInput == 0) OnPlayerStopWalking?.Invoke();

        _shift = _shiftTurnOn && GameManager.Instance.inputManager.ShiftKey;
        float shiftMultiplier = _shift ? shiftMultiplier = _shiftRate : 1f;

        // --- ACCELERATOR LOGIC ---
        if (_stateManager != null && _stateManager.HasItem(ItemType.Accelerator))
        {
            if (_moveInput != 0)
            {
                _currentSpeed += Time.deltaTime * _speed; // accelerate
                _currentSpeed = Mathf.Clamp(_currentSpeed, _speed, _speed * 3f);
            }
            else
            {
                _currentSpeed = _speed; // reset instantly when stopping
            }
        }
        else
        {
            _currentSpeed = _speed * shiftMultiplier;
        }

        if (_moveInput != 0)
        {
            if (_noiseMaker != null)
            {
                _noiseMaker.MakeWalkNoise(_shift, _shiftRate);
            }

            if (_groundChecker.isGrounded)
            {
                _sound?.PlayMovingSound(_shift);
            }
            else
            {
                _sound?.StopMovingSound();
            }
        }
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_moveInput * _currentSpeed, _rb.linearVelocityY)
                             + _groundChecker.GetGroundVelocity();
        bool isInverted = gravityMultiplier < 0;

        if (isInverted)
        {
            if (_moveInput < 0) _sr.flipX = false;
            else if (_moveInput > 0) _sr.flipX = true;
        }
        else
        {
            if (_moveInput < 0) _sr.flipX = true;
            else if (_moveInput > 0) _sr.flipX = false;
        }

        if (_rb.linearVelocityY * gravityMultiplier < 0)
        {
            _fallDistance -= (_rb.linearVelocityY * gravityMultiplier) * 0.02f;
            _rb.gravityScale = _fallGravityScale * gravityMultiplier;
        }
        else
        {
            if (_noiseMaker != null && _fallDistance >= 0.1f)
            {
                _noiseMaker.MakeLandingNoise(_fallDistance);
                _sound?.FallSound();
            }
            _fallDistance = 0;
            _rb.gravityScale = _gravityScale * gravityMultiplier;
        }
    }

    private IEnumerator SmoothRotate(float targetZ, float duration)
    {
        float startZ = transform.rotation.eulerAngles.z;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float z = Mathf.LerpAngle(startZ, targetZ, time / duration);
            transform.rotation = Quaternion.Euler(0, 0, z);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, targetZ);
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