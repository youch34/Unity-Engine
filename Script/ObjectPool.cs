using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    List<Transform> objs = new List<Transform>();
    public Transform Obj;
    public int ObjCount;
    private void Awake()
    {
        for (int i = 0; i < ObjCount; i++) 
        {
            Transform obj = Instantiate(Obj,transform);
            obj.gameObject.SetActive(false);
            objs.Add(obj);
        }
    }

    public Transform GetObject() 
    {
        foreach (Transform obj in objs) 
        {
            if (obj.gameObject.activeSelf.Equals(false))
            {
                return obj;
            }
        }
        return null;
    }
}
