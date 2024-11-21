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
    public float attackDelay = 5.0f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private bool _facingRight = true;
    private bool _isPatrolling = true;
    private Vector2 initialPosition;
    private Transform attackArea;
    private Transform espada;
    private float lastAttackTime;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;

        attackArea = transform.Find("AttackArea");
        espada = transform.Find("espada");

        lastAttackTime = -attackDelay; // Aseguramos que el enemigo pueda atacar de inmediato al inicio.
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (Time.time - lastAttackTime >= attackDelay)  // Solo atacamos si ha pasado el tiempo de retraso
            {
                _rigidbody.velocity = Vector2.zero;
                _animator.SetTrigger("Attack");
                _animator.SetBool("Run", false);
                lastAttackTime = Time.time; // Actualizamos el tiempo del último ataque
            }
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
            ReturnToPatrolArea();  // El enemigo regresa a patrullar cuando el jugador se aleja
        }
    }

    private void MoveTowardsPlayer()
    {
        _animator.SetBool("Run", true);
        Vector2 direction = (player.position - transform.position).normalized;
        _rigidbody.velocity = new Vector2(direction.x * speed, _rigidbody.velocity.y);
    }

    private void ReturnToPatrolArea()
    {
        
        Vector2 direction = (initialPosition - (Vector2)transform.position).normalized;
        _rigidbody.velocity = new Vector2(direction.x * patrolSpeed, _rigidbody.velocity.y);

        // Cuando el enemigo llega de nuevo a su zona de patrullaje, comienza a patrullar.
        if (Vector2.Distance(transform.position, initialPosition) < 0.1f)
        {
            _animator.SetBool("Run", false); 
            _isPatrolling = true; 
        }
        else
        {
            _animator.SetBool("Run", true);  
        }
    }

    private void Patrol()
    {

        float distanceFromStart = Vector2.Distance(initialPosition, transform.position);

        if (distanceFromStart >= patrolRange)
        {
            Flip(); 
        }

        if (_isPatrolling)
        {
            float patrolDirection = _facingRight ? 1 : -1;
            _rigidbody.velocity = new Vector2(patrolDirection * patrolSpeed, _rigidbody.velocity.y);
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
