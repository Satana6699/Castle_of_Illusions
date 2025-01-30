using UnityEngine;

namespace CastleOfIllusions.Scripts
{
    [RequireComponent(typeof(PlayerController))]
    public class StunEffect : MonoBehaviour
    {
        [Header("Stunned Stats")]
        [SerializeField] private float stunDuration = 0.5f;
        private bool _isStunned  = false;
        private float _timerStun = 0f;
    
        private PlayerController _playerController;
    
        void Start()
        {
            _playerController = GetComponent<PlayerController>();
        }

        void Update()
        {
        
            _timerStun += Time.deltaTime;
        
            if (_timerStun >= stunDuration)
            {
                _timerStun = 0f;
                _isStunned = false;
                _playerController.enabled = true;
            }
        }
    
        public void Stunned()
        {
            _playerController.enabled = false;
            _isStunned = true;
        }

        public bool CheckStunned() => _isStunned;
    }
}
