using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public GameObject item;

    public void PickupItem()
    {
        //item.SetActive(false);
        Debug.Log("Item pickedup!");
    }

}
