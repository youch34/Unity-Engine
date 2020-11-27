using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    public float move_speed;
    private Rigidbody rigidbody = null;
    private NavMeshAgent agent = null;
    private Animator animator = null;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = agent.speed * move_speed;
    }

    private void Update()
    {
        if (gameObject.activeSelf == false)
            return;
        if (agent.desiredVelocity.sqrMagnitude > 0)
            animator.SetBool("bMove", true);
        else
            animator.SetBool("bMove", false);
    }

    public void MoveToTarget(Vector3 target_pos) 
    {
        agent.SetDestination(target_pos);
    }

    public void Turn(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        Vector3 dirXZ = new Vector3(dir.x, 0f, dir.z);
        Quaternion targetRot = Quaternion.LookRotation(dirXZ);
        rigidbody.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 550.0f * Time.deltaTime);
    }

    public void StopNav()
    {
        agent.SetDestination(transform.position);
    }
}
