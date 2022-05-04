using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Animator doorAnimator;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            doorAnimator.Play("OpenDoor");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " has entered!!");
        if (other.CompareTag("Player"))
        {
            
            doorAnimator.Play("OpenDoor");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER EXITED");
            doorAnimator.Play("CloseDoor");
        }
    }

}
