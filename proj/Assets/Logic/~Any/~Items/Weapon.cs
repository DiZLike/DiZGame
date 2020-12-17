using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public AudioClip Shoot;
    public AudioClip Hit;
    public GameObject Bullet;
    public GameObject HitEffect;
    //protected Timer reloadTimer;
    internal bool Reloaded = true;

    internal Transform bulletStart;

    public float Reload = 0.1f;
    // Start is called before the first frame update
    protected void Start()
    {

    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
    protected void FixedUpdate()
    {
        
    }
    protected void Proc()
    {
        Reloaded = true;
    }
    public void Fire()
    {
        Reloaded = false;
    }
}
