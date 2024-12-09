using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour
{
    public FirstPersonLook s1;
    public FirstPersonMovement s2;
    public Jump s3;
    public Crouch s4;

    public void deathS()
    {
        s1.enabled = false;
        s2.enabled = false;
        s3.enabled = false;
        s4.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }
}
