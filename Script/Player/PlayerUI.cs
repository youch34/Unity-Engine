using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    Combat combat;
    PlayerSkillManager PSM;
    Inventory inven;

    public Slider Hpbar;
    public Image Posion;

    public Image skill1;
    public Image skill2;

    private void Start()
    {
        combat = GetComponent<Combat>();
        PSM = GetComponent<PlayerSkillManager>();
        inven = GetComponent<Inventory>();
        UpdateUI();
    }

    private void Update()
    {
        skill1.fillAmount = PSM.SCSkill1.CurTime / PSM.SCSkill1.CoolTime;
        skill2.fillAmount = PSM.SCSkill2.CurTime / PSM.SCSkill2.CoolTime;
    }

    public void UpdateUI() 
    {
        Hpbar.value = combat.hp / combat.MaxHp;
        Posion.GetComponentInChildren<Text>().text = inven.GetItemCount("posion").ToString();
    }
}
