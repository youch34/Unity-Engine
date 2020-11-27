using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState 
{
    Idle,
    Follow,
    Combat
}

public class EnemyController : MonoBehaviour
{
    Move move = null;
    Combat combat = null;

    Transform player;
    Vector3 SpawnPos;
    public EnemyState State;
    public float DetectRange;
    public float AttackRange;
    private void Awake()
    {
        move = GetComponent<Move>();
        combat = GetComponent<Combat>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        State = EnemyState.Idle;
    }

    public void Activate()
    {
        SpawnPos = transform.position;
        gameObject.SetActive(true);
        combat.hp = combat.MaxHp;
        combat.bDeath = false;
        combat.bDamaged = false;
        State = EnemyState.Idle;
        GetComponent<BoxCollider>().enabled = true;
        Rigidbody rg = GetComponent<Rigidbody>();
        rg.WakeUp();
        StartCoroutine("EnemyBehavior", 0.5f);
    }
 

    IEnumerator EnemyBehavior(float updateTime) 
    {
        yield return new WaitForSeconds(updateTime);
        if (combat.bDeath == true)
            StopCoroutine("EnemyBehavior");
        CalcDistance();
        if (combat.battack.Equals(true) || combat.bDamaged.Equals(true))
        {
            StartCoroutine("EnemyBehavior", updateTime);
            yield break;
        }
        switch (State)
        {
            case EnemyState.Idle:
                if (Detect().Equals(true))
                    State = EnemyState.Follow;
                break;
            case EnemyState.Follow:
                Following();
                break;
            case EnemyState.Combat:
                Combat();
                break;
        }
        StartCoroutine("EnemyBehavior", updateTime);
    }

    bool Detect() 
    {
        move.MoveToTarget(SpawnPos);
        if (CalcDistance() > DetectRange)
            return false;
        else
            return true;
    }

    void Following() 
    {
        if (CalcDistance() <= AttackRange)
        {
            State = EnemyState.Combat;
            return;
        }else if (CalcDistance() > DetectRange)
        {
            State = EnemyState.Idle;
            return;
        }
        move.MoveToTarget(player.position);
    }

    void Combat() 
    {
        if (CalcDistance() > AttackRange)
        { 
            State = EnemyState.Follow;
            return;
        }
        if (combat.battack == true)
            return;
        transform.LookAt(player.position);
        combat.battack = true;
        move.StopNav();
        combat.BasicAttack();
    }

    float CalcDistance() 
    {
        Vector2 XZpos = new Vector2(transform.position.x, transform.position.z);
        Vector2 targetXZ = new Vector2(player.position.x, player.position.z);
        float dist = Vector2.Distance(XZpos, targetXZ);
        return dist;
    }
}
