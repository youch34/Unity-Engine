using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDamage : SkillSystem
{
    protected override void Start()
    {
        base.Start();
        ApplyDamage();
    }
}
