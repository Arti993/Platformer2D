using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerSoundEffector))]
public class Player : Entity
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpforce;
    [SerializeField] private int _health;
    [SerializeField] private PlayerSoundEffector _playerSoundEffector;

    private const string Condition = "state";
    private bool _isGrounded;
    private float _direction;
    private int _collectedCoins;
    private PlayerInput _input;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public event UnityAction<int> CollectedCoinsCountChanged;
    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;

    private States State
    {
        get { return (States)_animator.GetInteger(Condition); }
        set { _animator.SetInteger(Condition, (int)value); }
    }

    private void Start()
    {
        _input = new PlayerInput();
        _input.Enable();

        HealthChanged?.Invoke(_health);

        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = _rigidbody.GetComponentInChildren<SpriteRenderer>();
        _playerSoundEffector = GetComponent<PlayerSoundEffector>();

        _collectedCoins = 0;
        CollectedCoinsCountChanged?.Invoke(_collectedCoins);


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
            State = States.idle;

        if (_direction != 0)
            Move(_direction);
    }

    private void Move(float direction)
    {
        if (_isGrounded)
            State = States.move;

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
            _playerSoundEffector.JumpSound.Play();
        }
    }

    private void CheckGround()
    {
        Collider2D[] collidersNearPlayerLegs = Physics2D.OverlapCircleAll(transform.position, 0.2f);

        _isGrounded = collidersNearPlayerLegs.Length > 1;

        if (_isGrounded == false)
            State = States.jump;
    }

    public bool IsAboveEnemyHead()
    {
        bool isPlayerAboveEnemyHead = false;

        Collider2D[] collidersNearPlayerLegs = Physics2D.OverlapCircleAll(transform.position, 0.15f);

        foreach (Collider2D collider in collidersNearPlayerLegs)
        {
            if (collider.gameObject.TryGetComponent(out Enemy enemy))
                isPlayerAboveEnemyHead = true;

        }

        return isPlayerAboveEnemyHead;
    }

    public void JumpAfterAttack()
    {
        float forceReducingFactor = 1.2f;
        _rigidbody.AddForce(transform.up * _jumpforce / forceReducingFactor, ForceMode2D.Impulse);
        _playerSoundEffector.AttackSound.Play();
    }

    public void TakeCoin()
    {
        _collectedCoins++;
        _playerSoundEffector.TakeCoinSound.Play();
        CollectedCoinsCountChanged?.Invoke(_collectedCoins);
    }

    public override void GetDamage()
    {
        _health--;
        _playerSoundEffector.GetDamageSound.Play();
        HealthChanged?.Invoke(_health);

        if (_health < 1)
            Die();
        else
            StartCoroutine(OnHurt(_spriteRenderer));
    }

    public override void Die()
    {
        Died?.Invoke();
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 0f);
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

public enum States
{
    idle,
    move,
    jump
}
