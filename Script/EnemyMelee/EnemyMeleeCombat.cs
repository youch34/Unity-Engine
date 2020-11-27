using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeCombat : Combat
{
    public GameObject posion;
    ObjectPool bloodpool;
    ObjectPool ddpool;
    private BoxCollider basicAttackBox = null;

    protected override void Awake()
    {
        base.Awake();
        basicAttackBox = transform.FindAllChildByName("BasicAttackBox").GetComponent<BoxCollider>();
        if (basicAttackBox != null)
            basicAttackBox.enabled = false;
        bloodpool = GameObject.Find("BloodPool").GetComponent<ObjectPool>();
        ddpool = GameObject.Find("DDPool").GetComponent<ObjectPool>();
    }
    public void BasicAttackSpawn()
    {
        if (basicAttackBox.Equals(null))
            return;
        Vector3 boxSize = basicAttackBox.size;
        Collider[] colliders = Physics.OverlapBox(basicAttackBox.transform.position, boxSize);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag(gameObject.tag))
                continue;
            Combat combat = null;
            collider.TryGetComponent<Combat>(out combat);
            if (combat != null)
                combat.Damaged(AttackPow);
        }
    }
    override public void Damaged(float damage) 
    {
        base.Damaged(damage);
        GetComponent<Move>().StopNav();
        bDamaged = true;
        OnEndAttack();
        animator.SetTrigger("bDamage");
        HitBlood hb = null;
        hb = bloodpool.GetObject().GetComponent<HitBlood>();
        if (hb != null)
        {
            hb.gameObject.SetActive(true);
            Vector3 pos = transform.position;
            pos.y = 1.2f;
            hb.transform.position = pos;
        }
        DisplayDamage dd = null;
        dd = ddpool.GetObject().GetComponent<DisplayDamage>();
        if (dd != null)
        {
            dd.gameObject.SetActive(true);
            Vector3 pos = transform.position;
            pos.y += 2;
            dd.damage = damage;
            dd.SetPos(pos);
        }
    }

    public void EndDamaged() 
    {
        bDamaged = false;
    }

    protected override void Death()
    {
        base.Death();
        int ran = Random.Range(0,100);
        if (ran < 20)
            Instantiate(posion,transform.position,transform.rotation);
    }
}
