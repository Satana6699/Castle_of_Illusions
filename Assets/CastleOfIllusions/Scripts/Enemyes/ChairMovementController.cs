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
        
            if (_moveInput > 0 && !_facingRight)
            {
                Rotate(90);
                _facingRight = true;
            }
            else if (_moveInput < 0 && _facingRight)
            {
                Rotate(-90);
                _facingRight = false;
            }
        }
    
        private void Rotate(float angle)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}
