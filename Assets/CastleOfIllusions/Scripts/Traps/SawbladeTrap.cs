using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class SawbladeTrap : MonoBehaviour
{
    [Header("Saw Blade Positions")]
    [SerializeField] private GameObject rightPositionSawBlade;
    [SerializeField] private GameObject leftPositionSawBlade;

    [Header("Saw Blade Settings")]
    [SerializeField] private GameObject sawBlade;

    enum SawBladeState
    {
        OneSawBlade,
        TwoSawBlade,
    }
    
    [SerializeField] private SawBladeState sawBladeState = SawBladeState.OneSawBlade;
    private List<GameObject> _sawBladeObjects = new List<GameObject>();

    void Start()
    {
        InitializeSawBlade();
    }

    void Update()
    {
        foreach (GameObject obj in _sawBladeObjects)
        {
            SawbladeObject sawBladeObject = obj.GetComponent<SawbladeObject>();
            if (sawBladeObject is not null)
            {
                if (Mathf.Approximately(sawBladeObject.MoveInput, -1) &&
                    sawBladeObject.transform.position.x <= leftPositionSawBlade.transform.position.x)
                {
                    sawBladeObject.MoveInput = 1;
                }
                else if (Mathf.Approximately(sawBladeObject.MoveInput, 1) &&
                         sawBladeObject.transform.position.x >= rightPositionSawBlade.transform.position.x)
                {
                    sawBladeObject.MoveInput = -1;
                }
            }
        }
    }

    private void InitializeSawBlade()
    {
        if (sawBladeState == SawBladeState.OneSawBlade)
        {
            _sawBladeObjects.Add(Instantiate(sawBlade, rightPositionSawBlade.transform.position, Quaternion.identity));
        }
        else if (sawBladeState == SawBladeState.TwoSawBlade)
        {
            _sawBladeObjects.Add(Instantiate(sawBlade, rightPositionSawBlade.transform.position, Quaternion.identity));
            _sawBladeObjects.Add(Instantiate(sawBlade, leftPositionSawBlade.transform.position, Quaternion.identity));
            
        }
    }
    
}
