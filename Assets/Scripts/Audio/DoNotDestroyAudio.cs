using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyAudio : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("MenuMusic");
        if (musicObj.Length > 1)
        {
            Debug.Log("Found more than 1 audio source");
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
