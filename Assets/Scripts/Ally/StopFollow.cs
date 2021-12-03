using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class StopFollow : MonoBehaviour
{
    public AIPath aiScript;

    void Start()
    {
        aiScript.canMove = false;
    }

    public void ToggleMove()
    {
        if (aiScript.canMove == true)
        {
            Debug.Log("Follow false");
            aiScript.canMove = false;
        }
        else if (aiScript.canMove == false) 
        {
            Debug.Log("Follow true");
            aiScript.canMove = true;
        }
    } 
}
