using TMPro;
using UnityEngine;

namespace Core
{
    public class RoomMove : MonoBehaviour
    {
        [SerializeField] private Vector2 _cameraChange;
        [SerializeField] private Vector3 _playerChange;
        [SerializeField] private bool _needText;
        [SerializeField] private string _placename;
        [SerializeField] private GameObject _text;
        [SerializeField] private TextMeshProUGUI _placeText;

        private CameraMovement _cam;
        private void Start()
        {
            _cam = Camera.main.GetComponent<CameraMovement>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _cam._minPos += _cameraChange;
                _cam._maxPos += _cameraChange;
                other.transform.position += _playerChange;
                if (_needText)
                {
                    _text.SetActive(true);
                    _placeText.text = _placename;
                    Invoke(nameof(PlaceNameDeActive),4f);
                }
            }
        }

        private void PlaceNameDeActive()
        {
            _text.SetActive(false);
        }
    }
}
