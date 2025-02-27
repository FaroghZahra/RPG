using UnityEngine;

namespace Enemy
{
    public class IEnemyProperties : MonoBehaviour
    {
        protected enum EnemyState
        {
            idle,
            walk,
            attack,
            stagger 
        }

        [SerializeField] protected EnemyState currentState;
        [SerializeField] protected string _enemyName;
        [SerializeField] protected int _baseAttack;
        [SerializeField] protected float _moveSpeed;
        [SerializeField] protected FloatValue _maxHealth;
        protected float _health;
    }
}
