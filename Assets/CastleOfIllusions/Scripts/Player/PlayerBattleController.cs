using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBattleController : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float speedAtack = 10f;
    private float _timerAtack = 0f;

    [FormerlySerializedAs("swordAnimator")]
    [Header("Sword Settings")]
    [SerializeField] private Animator animator;


    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        _timerAtack = speedAtack;
    }
    
    void Update()
    {
        _timerAtack += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && _timerAtack >= speedAtack)
        {
            Atack();
            _timerAtack = 0f;
        }
    }

    private void Atack()
    {
        animator.SetTrigger("Attack");
    }
}
