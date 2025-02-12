using UnityEngine;

namespace CastleOfIllusions.Scripts
{
    public class Spike : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        
        [Header("Animator Settings")]
        [SerializeField] private Animator animator;

        [Header("Spike Activation Settings")]
        [SerializeField] private float timeActivateSpike = 0.3f;

        private void Start()
        {
            if (gameSettings)
            {
                timeActivateSpike = gameSettings.timeActivateSpike;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Invoke(nameof(ActivateSpike), timeActivateSpike);
            }
        }

        private void ActivateSpike()
        {
            AudioManager.Instance?.PlaySFX(AudioManager.Instance?.soundSettings.spikesSound, 
                transform.position);
            animator.SetTrigger("Spike");
        }
    }
}
