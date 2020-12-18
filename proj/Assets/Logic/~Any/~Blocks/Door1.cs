using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : Block
{
    private Timer timer;
    private bool started;
    private float startPos;
    private float end;

    private Transform door;
    private AudioSource sound;

    public DoorPossition Move;
    public AudioClip Open;
    public AudioClip Close;

    new void Start()
    {
        base.Start();
        var v = GetComponentsInChildren<Transform>();
        sound = GetComponentInChildren<AudioSource>();

        foreach (var item in v)
        {
            if (item.tag == "Door")
            {
                door = item;
                break;
            }
        }

        if (Move == DoorPossition.Z)
            startPos = door.position.z;
        else startPos = door.position.x;


        end = startPos - 0.8f;
        timer = new Timer(0.01f, true, Proc);
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        timer.Tick();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "CosmoBot")
        {
            started = true;
            sound.clip = Open;
            sound.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "CosmoBot")
        {
            started = false;
            sound.clip = Close;
            sound.Play();
        }
    }

    private void Proc()
    {
        float p = 0;
        if (Move == DoorPossition.Z)
            p = door.position.z;
        else
            p = door.position.x;
        if (started && p >= end)
        {
            float x = door.position.x;
            float y = door.position.y;
            float z = door.position.z;

            Vector3 np = new Vector3();
            if (Move == DoorPossition.Z)
                np = new Vector3(x, y, z - 0.03f);
            else
                np = new Vector3(x - 0.03f, y, z);
            door.position = np;

        }
        else if (!started && p < startPos)
        {
            float x = door.position.x;
            float y = door.position.y;
            float z = door.position.z;

            Vector3 np = new Vector3();
            if (Move == DoorPossition.Z)
                np = new Vector3(x, y, z + 0.03f);
            else
                np = new Vector3(x + 0.03f, y, z);
            door.position = np;
        }
    }

}