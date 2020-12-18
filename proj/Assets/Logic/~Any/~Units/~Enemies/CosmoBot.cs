using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmoBot : Enemy
{

    BoxCollider sight;
    bool playerInSight = false;
    GameObject player;

    new void Start()
    {
        base.Start();

        var comps = GetComponentsInChildren<BoxCollider>();
        foreach (var item in comps)
        {
            if (item.name == "Sight")
                sight = item;
        }
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (playerInSight)
        {
            float x = player.transform.position.x;
            float z = player.transform.position.z;
            transform.LookAt(new Vector3(x, 0, z));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;
        player = other.gameObject;
        playerInSight = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;
        Debug.Log(2);
    }
}
