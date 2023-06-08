using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Enemy : Entity
{
    [SerializeField] private int _health;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxWalkInOneWayTime;

    private float _currentWalkInOneWayTime;
    private Vector3 _direction;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private void Start()
    {
        _direction = transform.right;
        _currentWalkInOneWayTime = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = _rigidbody.GetComponentInChildren<SpriteRenderer>();

        if (_spriteRenderer.flipX)
            _direction = -transform.right;
        else
            _direction = transform.right;
    }

    private void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
          
            if (player.IsAboveEnemyHead())
            {
                player.Attack();

                GetDamage();
            }
            else
                player.GetDamage();
        }
    }

    private void Move()
    {
        _currentWalkInOneWayTime += Time.deltaTime;

        if (_currentWalkInOneWayTime > _maxWalkInOneWayTime)
        {
            _direction *= -1f;
            _currentWalkInOneWayTime = 0;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, _moveSpeed * Time.deltaTime);
    }
    public override void GetDamage()
    {
        _health--;

        StartCoroutine(OnHurt(_spriteRenderer));

        if (_health < 1)
            Die();
    }

    public override void Die()
    {
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EnemyDestroyer enemyDestroyer))
            Destroy(this.gameObject);
    }
}
