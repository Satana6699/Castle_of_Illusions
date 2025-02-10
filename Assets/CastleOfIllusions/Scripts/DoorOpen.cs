using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private float openRotateYStop = -100f;
    [SerializeField] private float rotateYForTime = -1f;
    
    private BoxCollider _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private IEnumerator OpenDoorAnimation()
    {
        float openDoor = 0f;
        
        while (Math.Abs(openDoor) <= Math.Abs(openRotateYStop))
        {
            openDoor += rotateYForTime;
            door.transform.localRotation = Quaternion.Euler(0f, openDoor, 0f);
            yield return new WaitForSeconds(0.01f);
        }
        
        _boxCollider.enabled = false;
    }

    public void OpenDoor()
    {
        StartCoroutine(OpenDoorAnimation());
    }
}
