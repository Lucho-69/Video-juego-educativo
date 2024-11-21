using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float patrolRange = 6f;
    public float detectRange = 3f;
    public float attackRange = 1.5f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private bool _facingRight = true;
    private bool _isPatrolling = true;
    private Vector2 initialPosition;
    private Transform attackArea;
    private Transform espada;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;

        attackArea = transform.Find("AttackArea");
        espada = transform.Find("espada");
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            _rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger("Attack");
            _animator.SetBool("Run", false);
        }
        else if (distanceToPlayer <= detectRange)
        {
            speed = chaseSpeed;
            LookAtPlayer();
            MoveTowardsPlayer();
        }
        else
        {
            _animator.SetBool("Run", true);
            Patrol();
        }
    }

    private void MoveTowardsPlayer()
    {
        _animator.SetBool("Run", true);
        Vector2 direction = (player.position - transform.position).normalized;
        _rigidbody.velocity = new Vector2(direction.x * speed, _rigidbody.velocity.y);
    }

    private void Patrol()
    {
        speed = patrolSpeed;
        float distanceFromStart = Vector2.Distance(initialPosition, transform.position);

        if (distanceFromStart >= patrolRange)
        {
            Flip();
        }

        if (_isPatrolling)
        {
            float patrolDirection = _facingRight ? 1 : -1;
            _rigidbody.velocity = new Vector2(patrolDirection * speed, _rigidbody.velocity.y);
        }
    }

    private void LookAtPlayer()
    {
        if ((player.position.x < transform.position.x && _facingRight) || (player.position.x > transform.position.x && !_facingRight))
        {
            Flip();
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(initialPosition, patrolRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
