using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerSoundController))]
public class Player : Entity
{

    [SerializeField] private int _health;
    
    private PlayerMotionController _motionController;
    private PlayerSoundController _soundController;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private int _collectedCoins;

    public event UnityAction<int> CollectedCoinsCountChanged;
    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = _rigidbody.GetComponentInChildren<SpriteRenderer>();
        _soundController = GetComponent<PlayerSoundController>();
        _motionController = GetComponent<PlayerMotionController>();

        _collectedCoins = 0;
        CollectedCoinsCountChanged?.Invoke(_collectedCoins);
        HealthChanged?.Invoke(_health);
    }
    
    public void TakeCoin()
    {
        _collectedCoins++;

        _soundController.TakeCoinSound.Play();

        CollectedCoinsCountChanged?.Invoke(_collectedCoins);
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

    public void Attack()
    {
        _soundController.AttackSound.Play();
        _motionController.JumpAfterAttack();
    }

    public override void GetDamage()
    {
        _health--;
        _soundController.GetDamageSound.Play();
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
        _motionController.DisableControl();
    }

    public void EnableControl()
    {
        _motionController.EnableControl();
    }
}


