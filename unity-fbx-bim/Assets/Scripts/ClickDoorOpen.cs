using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDoorOpen : MonoBehaviour
{
    public Animator doorAnimator;

    private bool isOpen = false;
    private RaycastHit hit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {

            }
        }
    }
}
