using UnityEngine;

namespace Player
{
    public class PlayerHit : MonoBehaviour
    {
        public delegate void OnHit();
        public static event OnHit HitPot;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Pot"))
            {
                HitPot?.Invoke();
            }
        }
    }
}
