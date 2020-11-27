using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    RaycastHit Target;
    Move move = null;
    Camera camera = null;
    PlayerInput pInput = null;
    Combat combat = null;
    Inventory inven = null;
    PlayerUI ui = null;
    Vector3 camera_distance;

    private void Start()
    {
        camera = Camera.main;
        move = GetComponent<Move>();
        pInput = GetComponent<PlayerInput>();
        combat = GetComponent<Combat>();
        inven = GetComponent<Inventory>();
        ui = GetComponent<PlayerUI>();
        camera_distance = camera.transform.position - transform.position;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inven.GetItemCount("posion") > 0)
            {
                combat.hp += combat.MaxHp * 0.3f;
                inven.AddItem("posion", -1);
                ui.UpdateUI();
            }
        }
        if (Input.GetMouseButton(1))
        {
            if (combat.battack.Equals(true))
                return;
            transform.LookAt(pInput.GetMouseHit().point);
            move.StopNav();
            combat.BasicAttack();
        }
        if (Input.GetMouseButton(0))
        {
            if (combat.battack.Equals(true))
            {
                move.StopNav();
                return;
            }
            move.MoveToTarget(pInput.GetMouseHit().point);
        }
        MainCameraMove();
    }

    void MainCameraMove() 
    {
        camera.transform.position = transform.position + camera_distance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            string name = other.GetComponent<Item>().name;
            inven.AddItem(name, 1);
            ui.UpdateUI();
            Destroy(other.gameObject);
        }
    }
}
