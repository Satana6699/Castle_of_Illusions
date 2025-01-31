using UnityEngine;

namespace CastleOfIllusions.Scripts
{
    public class ChairMovementController : MonoBehaviour
    {
        [SerializeField] private float leftPredelPositionForX;
        [SerializeField] private float righrPredelPositionForY;
        [SerializeField] private float moveSpeed = 5f;
    
        private bool _facingRight = true;
        private Rigidbody _rigidbody;
        private float _moveInput = 1;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if (_moveInput == -1 && transform.position.x <= leftPredelPositionForX)
            {
                _moveInput = 1;
            }
            else if (_moveInput == 1 && transform.position.x >= righrPredelPositionForY)
            {
                _moveInput = -1;
            }
        
            Vector3 newPosition = _rigidbody.position + Vector3.right * _moveInput * moveSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(newPosition);
        
            Rotate();
        }
    
        private void Rotate()
        {
            if (_moveInput > 0 && !_facingRight)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                _facingRight = true;
            }
            else if (_moveInput < 0 && _facingRight)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                _facingRight = false;
            }
        }
    }
}
