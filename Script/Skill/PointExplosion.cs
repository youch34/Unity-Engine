using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointExplosion : SkillSystem
{
    Vector3 Point;
    Vector3 point
    {
        set { Point = value; }
    }
    public SkillSystem ExplosionEffect;

    private void Update()
    {
        if (Vector3.Distance(transform.position, Point) < 0.1)
        {
            Instantiate(ExplosionEffect, transform.position, transform.rotation);
            DestroySelfObj();
        }
    }

}
