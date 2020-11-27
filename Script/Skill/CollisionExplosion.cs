using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionExplosion : SkillSystem
{
    public SkillSystem ExplosionEffect;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(ExplosionEffect,transform.position, transform.rotation);
        DestroySelfObj();
    }
}
