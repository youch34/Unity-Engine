using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : Combat
{
    private BoxCollider basicAttackBox = null;
    protected override void Awake()
    {
        base.Awake();
        basicAttackBox = transform.FindAllChildByName("BasicAttackBox").GetComponent<BoxCollider>();
        if (basicAttackBox != null)
            basicAttackBox.enabled = false;
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

    public override void Damaged(float damage)
    {
        base.Damaged(damage);
        PlayerUI ui = GetComponent<PlayerUI>();
        if (ui != null)
            ui.UpdateUI();
    }
}
