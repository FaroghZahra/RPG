using Player;
using UnityEngine;

namespace Interactables
{
    public class Pot : MonoBehaviour
    {
        private Animator _anim;
        private readonly string SMASH_ANIMATION = "Smash";
        private void Start()
        {
            _anim = GetComponent<Animator>();
            PlayerHit.HitPot += Smash;
        }
        private void Smash()
        {
            _anim.SetBool(SMASH_ANIMATION, true);
            Destroy(gameObject, 0.8f);
        }
    }
}
