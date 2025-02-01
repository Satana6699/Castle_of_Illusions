using UnityEngine;

namespace CastleOfIllusions.Scripts
{
    public class PlayerBattleController : MonoBehaviour
    {
        [SerializeField] private float speedAtack = 10f;
    
        [SerializeField] private GameObject sword;
        [SerializeField] private Animator swordAnimator;

        private float _timerAtack = 0f;

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
}
