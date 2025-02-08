using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Princessa : MonoBehaviour
{
    [SerializeField] private GameObject canvasInteractive;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float timeVinner = 5f;
    
    private Rigidbody _rigidbody;
    private bool _isVinner = false;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !_isVinner)
        {
            canvasInteractive.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                _rigidbody.isKinematic = false;
                canvasInteractive.SetActive(false);
                _isVinner = true;
                Invoke(nameof(Vinner), timeVinner);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvasInteractive.SetActive(false);
    }

    public void Vinner()
    {
        gameManager.Vinner();
    }
}
