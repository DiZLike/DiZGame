using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkLamp : MonoBehaviour
{
    public float Interval;
    public float IntensityMax;
    public float IntensityMin;
    public float Step;
    private Light lamp;
    private Timer tim;
    private bool min = true;
    void Start()
    {
        lamp = GetComponentInChildren<Light>();
        tim = new Timer(Interval, true, Tick);
    }

    
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        tim.Tick();
    }
    void Tick()
    {
        if (lamp.intensity <= IntensityMax && min)
        {
            lamp.intensity -= Step;
            if (lamp.intensity <= IntensityMin) min = !min;
        }
        else if (lamp.intensity >= IntensityMin && !min)
        {
            lamp.intensity += Step;
            if (lamp.intensity >= IntensityMax) min = !min;
        }
        else if (lamp.intensity < IntensityMin)
            lamp.intensity = IntensityMin;
        else if (lamp.intensity > IntensityMax)
            lamp.intensity = IntensityMax;




    }
}
