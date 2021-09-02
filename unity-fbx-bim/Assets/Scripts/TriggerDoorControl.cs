using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorControl : MonoBehaviour
{
    [SerializeField]
    private Animator myDoor = null;


    private void OnTriggerEnter(Collider other)
    {
        Debug.LogFormat("OnTriggerEnter called by {0}", other.tag);
        if (other.CompareTag("Player"))
        {
            myDoor.Play("DoorOpen", 0, 0.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.Play("DoorClose", 0, 0.0f);
        }
    }

}
