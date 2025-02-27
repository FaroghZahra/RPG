using System.Collections;
using Enemy;
using UnityEngine;

namespace Player
{
    public class KnockBackEnemy : MonoBehaviour
    {
        public delegate void SetStateStagger();
        public static event SetStateStagger Stagger;
        
        public delegate void SetStateIdle();
        public static event SetStateIdle Idle;
        
        public delegate void TakeDamageDelegate(float damage);
        public static event TakeDamageDelegate TakeDamage;
        
        [SerializeField] private float _thrust;
        [SerializeField] private float _knockTime;
        [SerializeField] private float _damage;

        private Rigidbody2D _enemyRb;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                _enemyRb = other.GetComponent<Rigidbody2D>();
                if (_enemyRb != null)
                {
                    Stagger?.Invoke();
                    Vector2 difference = _enemyRb.transform.position - transform.position;
                    difference = difference.normalized * _thrust;
                    _enemyRb.AddForce(difference, ForceMode2D.Impulse);
                    StartCoroutine(KnockCo(_enemyRb));
                }
            }
        }

        private IEnumerator KnockCo(Rigidbody2D _enemyRb)
        {
            if (_enemyRb != null)
            {
                yield return new WaitForSeconds(_knockTime);
                _enemyRb.velocity = Vector2.zero;
                Idle?.Invoke();
                TakeDamage?.Invoke(_damage);
            }
        }
    }
}
