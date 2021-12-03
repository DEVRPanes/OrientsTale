using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationExit : MonoBehaviour
{
    public Button ExitButton;

    public void ExitFunction()
    {
        Debug.Log("Application Exit!");
        Application.Quit();
    }
}
