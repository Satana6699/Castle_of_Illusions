using UnityEngine;

public class EnemyBattleController : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}