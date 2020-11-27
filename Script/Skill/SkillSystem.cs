using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    protected Combat combat = null;
    protected Animator animator = null;
    public float Damage;
    public float Cooltime;
    public float LifeTime;
    public float Radius;


    protected virtual void Start()
    {
        combat = GetComponent<Combat>();
        animator = GetComponent<Animator>();
        StartCoroutine("ReserveDestroy", LifeTime);
    }

    public void DestroySelfObj()
    {
        Destroy(gameObject);
    }

    public void ApplyDamage()
    {
        int layer = gameObject.layer;
        layer = layer == 13 ? 12 : 11;
        Collider[] collider = Physics.OverlapSphere(transform.position, Radius, 1 << layer);
        for (int i = 0; i < collider.Length; i++)
        {
            Combat cc;
            if (collider[i].TryGetComponent<Combat>(out cc))
                cc.Damaged(Damage);
        }
    }

    public IEnumerator ReserveDestroy(float time)
    {
        if (time <= 0)
            StopCoroutine("ReserveDestroy");

        yield return new WaitForSeconds(time);
        DestroySelfObj();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, Radius);
    }
}


