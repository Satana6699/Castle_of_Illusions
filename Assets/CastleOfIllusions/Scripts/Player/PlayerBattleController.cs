using UnityEngine;

public class PlayerBattleController : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float speedAtack = 10f;
    private float _timerAtack = 0f;

    [Header("Sword Settings")]
    [SerializeField] private GameObject sword;
    [SerializeField] private Animator swordAnimator;


    void Start()
    {
        if (swordAnimator == null)
        {
            swordAnimator = sword.GetComponent<Animator>();
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
        swordAnimator.SetTrigger("Atack");
    }
}
