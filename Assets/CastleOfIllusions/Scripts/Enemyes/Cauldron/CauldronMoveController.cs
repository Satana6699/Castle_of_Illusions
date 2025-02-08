using UnityEngine;

public class CauldronMoveController : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;

    [SerializeField] private GameObject player;

    private float _moveX = 0f;
    private float _moveSpeed = 1f;
    private float _minDistanceToPlayer = 1f;
    private CharacterController _characterController;

    void Start()
    {
        if (gameSettings)
        {
            _moveSpeed = gameSettings.bossMoveSpeed;
            _minDistanceToPlayer = gameSettings.bossMinDistanceToPlayer;
        }
        
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Debug.Log("на земле " + _characterController.isGrounded);
        
        Move();
    }

    private void Move()
    {
        if (!_characterController.isGrounded || !player)
        {
            return;
        }
        
        var distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < _minDistanceToPlayer)
        {
            return;
        }
        
        var target = player.transform.position - transform.position;
        _moveX = target.normalized.x;
        var direction = new Vector3(_moveX, 0f, 0);
        _characterController.Move(direction * _moveSpeed * Time.deltaTime);
        
        AudioManager.Instance?.PlaySFXNoRepeat(AudioManager.Instance?.soundSettings.cauldronMovementSound);
    }
}