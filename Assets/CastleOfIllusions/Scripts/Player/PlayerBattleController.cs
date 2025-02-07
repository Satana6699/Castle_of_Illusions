using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBattleController : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;
    
    [Header("Attack Settings")]
    private float _speedAttack = 1f;
    private float _timerAttack = 0f;

    [Header("Sword Settings")]
    [SerializeField] private Animator animator;


    void Start()
    {
        if (gameSettings is not null)
        {
            _speedAttack = gameSettings.playerSpeedAttck;
        }
        
        animator ??= GetComponent<Animator>();

        _timerAttack = _speedAttack;
    }
    
    void Update()
    {
        _timerAttack += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && _timerAttack >= _speedAttack)
        {
            Attack();
            _timerAttack = 0f;
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
