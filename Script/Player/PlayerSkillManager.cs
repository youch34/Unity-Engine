using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    PlayerInput pInput;
    Animator animator;
    Combat combat;
    Move move;

    public struct SkillCoolTime 
    {
        public float CoolTime;
        public float CurTime;
    }

    public SkillSystem skill1;
    Vector3 skill1pos;
    public SkillCoolTime SCSkill1;
    public SkillSystem skill2;
    public SkillCoolTime SCSkill2;
    // Update is called once per frame
    private void Start()
    {
        pInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        combat = GetComponent<Combat>();
        move = GetComponent<Move>();
        InitSkills();
    }
    void Update()
    {
        CalcCooltime();
        if (combat.battack.Equals(true))
            return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (SCSkill1.CurTime > 0)
                return;
            SCSkill1.CurTime = SCSkill1.CoolTime;
            combat.battack = true;
            move.StopNav();
            skill1pos = pInput.GetMouseHit().point;
            transform.LookAt(pInput.GetMouseHit().point);
            animator.SetTrigger("Skill1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (SCSkill2.CurTime > 0)
                return;
            SCSkill2.CurTime = SCSkill2.CoolTime;
            combat.battack = true;
            move.StopNav();
            transform.LookAt(pInput.GetMouseHit().point);
            animator.SetTrigger("Skill2");
        }
    }

    public void Skill1Spawn()
    {
        Instantiate(skill1, skill1pos, transform.rotation);
    }

    public void Skill2Spawn()
    {
        Vector3 pos = transform.position + transform.forward;
        pos.y += 1.0f;
        SkillSystem ss = Instantiate(skill2, pos, transform.rotation);
        ss.GetComponent<Projectile>().direction = transform.forward;
    }

    void InitSkills() 
    {
        SCSkill1.CoolTime = 3.0f;
        SCSkill2.CoolTime = 1.5f;
    }
    void CalcCooltime() 
    {
        SCSkill1.CurTime -= Time.deltaTime;
        SCSkill2.CurTime -= Time.deltaTime;
    }

}
