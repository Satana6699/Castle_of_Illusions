using UnityEngine;

namespace CastleOfIllusions.Scripts
{
    public class Spike : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float timeActivateSpike = 0.3f;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Invoke(nameof(ActivateSpike), timeActivateSpike);
            }
        }

        private void ActivateSpike()
        {
            animator.SetTrigger("Spike");
        }
    }
}
