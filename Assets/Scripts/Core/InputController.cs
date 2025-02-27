using UnityEngine;

namespace Core
{
    public class InputController : MonoBehaviour
    {
        public delegate void MoveDelegate(Vector3 moveDirection);
        public static event MoveDelegate OnMove;

        public delegate void NotMovingDelegate();
        public static event NotMovingDelegate NotMove;

        public delegate void SpaceKeyDown();
        public static event SpaceKeyDown SpaceDown;
    
        public delegate void XKeyDown();
        public static event XKeyDown XDown;
    
        private Vector3 _keyboardInput;
        // Update is called once per frame
        private void Update()
        {
            _keyboardInput =  GetKeyboardInput();
        
            if(_keyboardInput != Vector3.zero)
                OnMove?.Invoke(_keyboardInput);
            else
                NotMove?.Invoke();

            if (SpaceKeyInputForSigns())
                SpaceDown?.Invoke();

            if (XKeyInputForAttack())
                XDown?.Invoke();
        
        }

        private Vector3 GetKeyboardInput()
        {
            _keyboardInput = Vector3.zero;
            _keyboardInput.x = Input.GetAxisRaw("Horizontal");
            _keyboardInput.y = Input.GetAxisRaw("Vertical");
            return _keyboardInput;
        }

        private bool SpaceKeyInputForSigns()
        {
            return (Input.GetKeyDown(KeyCode.Space));
        }

        private bool XKeyInputForAttack()
        {
            return (Input.GetKeyDown(KeyCode.X));
        }
    }
}
