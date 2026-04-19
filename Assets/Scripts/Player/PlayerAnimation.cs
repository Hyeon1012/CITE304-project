using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();

        _playerMovement.OnPlayerJumped += Jump;
        _playerMovement.OnPlayerWalking += Walking;
        _playerMovement.OnPlayerStopWalking += StopWalking;
    }

    private void Walking()
    {
        _animator.SetBool("IsWalking", true);
    }

    private void StopWalking()
    {
        _animator.SetBool("IsWalking", false);
    }

    private void Jump()
    {
        _animator.SetTrigger("Jump");
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
    }
}
