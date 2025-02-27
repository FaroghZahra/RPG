using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public class Log : IEnemyProperties
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _chaseRadius;
        [SerializeField] private float _attackRadius;
       // [SerializeField] private Transform _homePosition;

        private Rigidbody2D _rb;
        private Animator _anim;

        private readonly string WAKEUP_ANIMATION = "WakeUp";
        private readonly string MOVE_X_ANIMATION = "MoveX";
        private readonly string MOVE_Y_ANIMATION = "MoveY";
        private void Start()
        {
            _health = _maxHealth.InitialValue;
            currentState = EnemyState.idle;
            _target = GameObject.FindWithTag("Player").transform;
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            KnockBackEnemy.Stagger += SetStateStagger;
            KnockBackEnemy.Idle += SetStateIdle;
            KnockBackEnemy.TakeDamage += TakeDamage;
        }

        private void FixedUpdate()
        {
            CheckDistance();
        }

        private void CheckDistance()
        {
            if (Vector3.Distance(_target.position, transform.position) <= _chaseRadius && Vector3.Distance(_target.position, transform.position) >= _attackRadius && (currentState == EnemyState.idle || currentState == EnemyState.walk ) && currentState != EnemyState.stagger)
            {
                Vector3 temp = (Vector3.MoveTowards(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime));
                ChangeAnim(temp - transform.position);
                _rb.MovePosition(temp);
                ChangeState(EnemyState.walk);
                _anim.SetBool(WAKEUP_ANIMATION, true);
            }
            else if(Vector3.Distance(_target.position, transform.position) > _chaseRadius)
            {
                _anim.SetBool(WAKEUP_ANIMATION, false);
            }
        }
        private void SetStateStagger()
        {
            currentState = EnemyState.stagger;
        }
        private void SetStateIdle()
        {
            currentState = EnemyState.idle;
        }

        private void ChangeAnim(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    SetAnimFloat(Vector2.right);
                }else if (direction.x < 0)
                {
                    SetAnimFloat(Vector2.left);
                }
            }else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                if (direction.y > 0)
                {
                    SetAnimFloat(Vector2.up);
                }else if (direction.y  < 0)
                {
                    SetAnimFloat(Vector2.down);
                }
            }
        }

        private void SetAnimFloat(Vector2 setVector)
        {
            _anim.SetFloat(MOVE_X_ANIMATION, setVector.x);
            _anim.SetFloat(MOVE_Y_ANIMATION, setVector.y);
            
        }
        private void ChangeState(EnemyState newState)
        {
            if (currentState != newState)
                currentState = newState;
        }

        private void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Enemy collided");
            }
        }
    }
}
