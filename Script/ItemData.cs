using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public Item[] items;
    private static ItemData instance = null;
    public static ItemData Instance 
    {
        get 
        {
            if (instance == null)
                return null;
            else
                return instance; 
        }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public Item GetItemInfoByName(string name)
    {
        for (int i = 0; i < items.Length; i++) 
        {
            if (items[i].name == name)
                return items[i];
        }
        return null;
    }
}
