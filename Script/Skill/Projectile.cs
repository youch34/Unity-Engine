using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 Direction;
    public float speed;
    public Vector3 direction 
    {
        get { return Direction; }
        set { Direction = value; }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
