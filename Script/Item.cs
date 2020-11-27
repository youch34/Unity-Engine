using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string name;
    public Sprite img;

    private void Start()
    {
        transform.tag = "Item";
    }
}
