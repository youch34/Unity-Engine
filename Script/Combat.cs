using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Combat : MonoBehaviour
{

    public delegate void OnEndAttackDele();
    public static OnEndAttackDele OnEndAttackEvent;

    protected Animator animator;
    public int basicAttackNum;
    public float AttackPow;
    private bool bAttack;
    public bool battack { get { return bAttack; } set { bAttack = value; } }
    public bool bDamaged = false;
    public bool bDeath = false;

    public float MaxHp;
    private float CurHp;
    public float hp { get { return CurHp; } set { CurHp = value; } }

    virtual protected void Awake()
    {
        animator = GetComponent<Animator>();
        CurHp = MaxHp;
    }

    public void OnStartAttack() 
    {
    }

    public void OnEndAttack() 
    {
        StartCoroutine(SetbAttack(0.3f,false));
    }
    public void BasicAttack() 
    {
        int rn = Random.Range(0, basicAttackNum);
        animator.SetFloat("BasicAttackNum", rn);
        animator.SetTrigger("BasicAttack");
        bAttack = true;
    }
  
    virtual public void Damaged(float damage) 
    {
        CurHp -= damage;
        if (CurHp <= 0)
            Death();
    }

    public IEnumerator SetbAttack(float time, bool val) 
    {
        yield return new WaitForSeconds(time);
        bAttack = val;
    }

    virtual protected void Death() 
    {
        bDeath = true;
        animator.SetTrigger("bDeath");
        StopAllCoroutines();
        GetComponent<BoxCollider>().enabled = false;
        Rigidbody rg = GetComponent<Rigidbody>();
        rg.Sleep();
        Invoke("SetActiveFalse", 5.0f);
    }

    void SetActiveFalse()
    {
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }
}
