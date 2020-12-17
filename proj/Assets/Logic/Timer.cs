using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public delegate void ProcHandler();
    private float interval;
    private ProcHandler proc;
    private float last = 0;
    private long ticks;
    private bool first;

    internal bool started = true;

    public Timer(float interval, bool first, ProcHandler proc)
    {
        this.interval = interval;
        this.proc = proc;
        this.first = first;
    }
    public void Tick()
    {
        if (!started) return;
        if (ticks == 0)
            last = Time.time;
        if (ticks == 0 && first)
            proc();

        float delta = Time.time - last;
        if (delta >= interval)
        {
            proc();
            last = Time.time;
        }
        ticks++;
    }
    public void Reset()
    {
        ticks = 0;
        started = false;
    }
    public void Start()
    {
        started = true;
    }
}
