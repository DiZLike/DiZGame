using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Any
{
    public float Speed;
    public float LookSpeed;

    public AudioClip Step1;
    public AudioClip Step2;

    protected int curStep;

    protected Rigidbody rb;
    protected AudioSource stepSound;

    private Timer stepTimer;

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        var sounds = GetComponentsInChildren<AudioSource>();
        foreach (var item in sounds)
        {
            if (item.name == "AudioStep")
                stepSound = item;
        }

        if (Step1 != null)
            stepSound.clip = Step1;

        stepTimer = new Timer(0.5f, true, StepProc);
    }

    protected void Update()
    {
        
    }
    protected void FixedUpdate()
    {
        
    }

    public void Move(Vector3 velocity)
    {
        if (velocity == Vector3.zero)
            return;
        stepTimer.Tick();
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }
    private void StepProc()
    {
        //if (stepSound.clip == Step1)
        //    stepSound.clip = Step2;
        //else
        //    stepSound.clip = Step1;
        stepSound.Play();
    }

}
