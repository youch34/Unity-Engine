using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Ray ray;
    RaycastHit curHit;
    public Collider overlapcoll = null;

    private void Start()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray.origin, ray.direction,out curHit);
        overlapcoll = curHit.collider;
    }
    private void Update()
    {
    }

    public RaycastHit GetMouseHit() 
    {
        RaycastHit hit = new RaycastHit();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray.origin, ray.direction, out hit);
        return hit;
    }


    public bool MouseOverlap() 
    {
        Collider coll = GetMouseHit().collider;
        if (coll.Equals(null))
            return true;
        MaterialManager ocmm = overlapcoll.GetComponent<MaterialManager>();
        MaterialManager mm = null;
        if (coll.transform.TryGetComponent<MaterialManager>(out mm) && coll.CompareTag("Enemy"))
        {
            if (coll != overlapcoll)
            {
                if(ocmm)
                    ocmm.SetOutline(false);
                overlapcoll = coll;
                mm.SetOutline(true);
                Debug.Log("new Enemy");
                return false;
            }
            else
                return true;
        }
        if (ocmm)
            ocmm.SetOutline(false);
        overlapcoll = coll;
        return true;
    }
}
