using UnityEngine;

namespace Core
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _smoothing;
    
        public Vector2 _minPos;
        public Vector2 _maxPos;

        // Update is called once per frame
        private void LateUpdate()
        {
            if (transform.position != _target.position)
            {
                Vector3 targetPosition = new Vector3(_target.position.x, _target.position.y, transform.position.z);
                targetPosition.x = Mathf.Clamp(targetPosition.x, _minPos.x, _maxPos.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, _minPos.y, _maxPos.y);
                transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothing);
            }
        }
    }
}
