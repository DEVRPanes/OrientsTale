using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickJoyStick : MonoBehaviour
{
    private Image JRing;
    private Image JHandle;
    GameObject theHandle;
    Vector3 origin;

    private void originColor()
    {
        JRing.color = new Color(JRing.color.r, JRing.color.g, JRing.color.b, 0.5f);
        JHandle.color = new Color(JHandle.color.r, JHandle.color.g, JHandle.color.b, 0.5f);
    }

    void Start()
    {
        theHandle = GameObject.Find("Handle");
        origin = theHandle.transform.position;

        // Joystick Ring
        JRing = GetComponent<Image>();

        // Joystick Handle
        JHandle = theHandle.GetComponent<Image>();

        originColor();
    }

    void Update()
    {
        if(origin != theHandle.transform.position)
        {
            JRing.color = new Color(JRing.color.r, JRing.color.g, JRing.color.b, 1f);
            JHandle.color = new Color(JHandle.color.r, JHandle.color.g, JHandle.color.b, 1f);
        }
        
        if(origin == theHandle.transform.position)
        {
            originColor();
        }

      

    }
}
