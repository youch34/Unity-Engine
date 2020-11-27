using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistaceCombat : Combat
{
    public GameObject posion;
    public ObjectPool bloodpool;
    ObjectPool ddpool;
    public SkillSystem basicAttack;

    protected override void Awake()
    {
        base.Awake();
        bloodpool = GameObject.Find("BloodPool").GetComponent<ObjectPool>();
        ddpool = GameObject.Find("DDPool").GetComponent<ObjectPool>();
    }
    public void BasicAttackSpawn()
    {
        Vector3 pos = transform.position + transform.forward;
        pos.y = 1.0f;
        SkillSystem ss = Instantiate(basicAttack, pos, transform.rotation);
        ss.GetComponent<Projectile>().direction = transform.forward;
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
        int ran = Random.Range(0, 100);
        if (ran < 20)
            Instantiate(posion, transform.position, transform.rotation);
    }
}
