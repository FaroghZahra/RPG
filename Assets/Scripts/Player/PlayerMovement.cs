using System;
using Core;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        //Serialized Fields
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _hitBox;
        [SerializeField] private FloatValue _currentHealth;
        
        //Components
        private Rigidbody2D _rb;
        private Animator _anim;

        //private fields strings
        private readonly string IDLE_ANIMATION_X = "MoveX";
        private readonly string IDLE_ANIMATION_Y = "MoveY";
        private readonly string MOVING_ANIMATION = "Moving";
        private readonly string ATTACKING_ANIMATION = "Attacking";
    
        private enum PlayerState
        {
            walk,
            attack
        }
        [SerializeField]private PlayerState _currentState;
        private void Start()
        {
            _currentState = PlayerState.walk;

            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            InputController.OnMove += UpdateAnimationAndMovePlayer;
            InputController.NotMove += NotMoving;
            InputController.XDown += UpdateAnimationAndAttack;
        }

        private void UpdateAnimationAndMovePlayer(Vector3 change)
        {
            if (_currentState == PlayerState.walk)
            {
                change.Normalize();
                _rb.MovePosition(transform.position + _speed * Time.deltaTime * change );
        
                _anim.SetFloat(IDLE_ANIMATION_X, change.x);
                _anim.SetFloat(IDLE_ANIMATION_Y, change.y);
                _anim.SetBool(MOVING_ANIMATION, true); 
            }
        }
    
        private void UpdateAnimationAndAttack()
        {
            if (_currentState != PlayerState.attack)
            {
                _anim.SetBool(ATTACKING_ANIMATION, true);
                _hitBox.SetActive(true);
                _currentState = PlayerState.attack;
                Invoke(nameof(NotAttacking), 0.1f);
            }
        }
        private void NotMoving()
        {
            _anim.SetBool(MOVING_ANIMATION, false);
        }

        private void NotAttacking()
        {
            _anim.SetBool(ATTACKING_ANIMATION, false);
            _hitBox.SetActive(false);
            Invoke(nameof(ResetState), 0.3f);
        }

        private void ResetState()
        {
            _currentState = PlayerState.walk;
        }

        
    }
}
