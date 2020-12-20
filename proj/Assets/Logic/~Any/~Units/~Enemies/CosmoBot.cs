using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CosmoBot : Enemy
{
    public LayerMask RayMask;
    public float RayDidtanse = 200;

    BoxCollider sight;
    bool playerInSight = false;
    GameObject player;
    NavMeshAgent agent;
    Animator anim;

    new void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();

    }

	// Update is called once per frame
	new void Update()
    {
        base.Update();
        var tmp = CheckRayCast();
        if (tmp.collider != null)
        {
            player = tmp.collider.gameObject;
        }

        if (playerInSight)
        {
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = player.transform.position.z;
            Vector3 target = new Vector3(x, 0, z);
            transform.LookAt(target);

            float dist = Vector3.Distance(transform.position, target);
            if (dist > 2)
            {
                SetAnimMove();
                agent.enabled = true;
                agent.SetDestination(target);
            }
            else
            {
                SetAnimIdle();
                agent.enabled = false;
            }
        }
    }
    void SetAnimMove()
    {
        anim.SetBool("idle", false);
        anim.SetBool("move", true);
    }
    void SetAnimIdle()
    {
        anim.SetBool("idle", true);
        anim.SetBool("move", false);
    }
    private RaycastHit CheckRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, RayDidtanse, RayMask))
        {
            playerInSight = true;
        }
        else
        {
            //playerInSight = false;
        }
        return hit;
    }
}
