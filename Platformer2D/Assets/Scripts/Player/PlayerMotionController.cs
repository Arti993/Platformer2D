using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimationController))]
[RequireComponent(typeof(PlayerSoundController))]
public class PlayerMotionController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpforce;
   
    private bool _isGrounded;
    private float _direction;
    private PlayerAnimationController _animationController;
    private PlayerSoundController _soundController;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private PlayerInput _input;

    private void Start()
    {
        _input = new PlayerInput();
        _input.Enable();

        _animationController = GetComponent<PlayerAnimationController>();
        _soundController = GetComponent<PlayerSoundController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = _rigidbody.GetComponentInChildren<SpriteRenderer>();

        _input.Player.Jump.performed += ctx => OnJump();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        _direction = _input.Player.Move.ReadValue<float>();

        if (_isGrounded && _direction == 0)
            _animationController.PlayIdleAnimation();

        if (_direction != 0)
            Move(_direction);
    }

    private void Move(float direction)
    {
        if (_isGrounded && this != null)
            _animationController.PlayMoveAnimation();

        if (direction == 1)
            _spriteRenderer.flipX = false;
        else if (direction == -1)
            _spriteRenderer.flipX = true;

        Vector3 directionVector = new Vector3(direction, 0, 0);
        transform.position += directionVector * _moveSpeed * Time.deltaTime;
    }

    private void OnJump()
    {
        if (_isGrounded && this != null)
        {
            _rigidbody.AddForce(transform.up * _jumpforce, ForceMode2D.Impulse);
            _soundController.JumpSound.Play();
        }
    }

    private void CheckGround()
    {
        Collider2D[] collidersNearPlayerLegs = Physics2D.OverlapCircleAll(transform.position, 0.2f);

        _isGrounded = collidersNearPlayerLegs.Length > 1;

        if (_isGrounded == false)
            _animationController.PlayJumpAnimation();
    }

    public void JumpAfterAttack()
    {
        float forceReducingFactor = 1.2f;
        _rigidbody.AddForce(transform.up * _jumpforce / forceReducingFactor, ForceMode2D.Impulse);
    }

    public void DisableControl()
    {
        _input.Disable();
    }

    public void EnableControl()
    {
        _input.Enable();
    }
}
