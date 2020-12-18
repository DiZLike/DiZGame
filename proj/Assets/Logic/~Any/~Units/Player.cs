using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private Camera cam;
    private bool grounded;

    public GameObject weapon;

    private AudioSource wS;
    private Timer fireTimer;
    private Timer bulletTimer;
    private Weapon weaObj;
    private Animator animWea;
    private GameObject bullet;
    private LineRenderer laser;
    public LayerMask WeaponLayerMask;

    void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    new void Start()
    {
        base.Start();
        LockCursor();
        cam = GetComponentInChildren<Camera>();

        var s = GetComponentsInChildren<AudioSource>();
        foreach (var item in s)
        {
            if (item.name == "Wpos")
            {
                wS = item;
                animWea = item.GetComponent<Animator>();
            }
        }
        weaObj = weapon.GetComponent<WeaT>();
        weaObj.bulletStart = GameObject.Find("StartBullet").transform;
        laser = GetComponentInChildren<LineRenderer>();
    }
    
    new void Update()
    {
        base.Update();
    }
    new void FixedUpdate()
    {
        base.FixedUpdate();
        ControllerCheck();
        if (fireTimer != null)
            fireTimer.Tick();
        if (bulletTimer != null)
            bulletTimer.Tick();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!grounded)
            grounded = CheckObjTag(collision, "Platform");
    }
    private void OnCollisionExit(Collision collision)
    {
        if (grounded)
            grounded = !CheckObjTag(collision, "Platform");
    }
    private bool CheckObjTag(Collision col, string tag)
    {
        if (col.gameObject.tag == tag)
            return true;
        return false;
    }

    void ControllerCheck()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        float yRot = Input.GetAxisRaw("Mouse X");
        float xRot = Input.GetAxisRaw("Mouse Y");
        float jump = Input.GetAxisRaw("Jump");
        float fire = Input.GetAxisRaw("Fire1");

        Vector3 movHor = transform.right * xMov;
        Vector3 movVer = transform.forward * zMov;
        Vector3 velocity = (movHor + movVer).normalized * Speed;

        Vector3 rotation = new Vector3(0f, yRot, 0f) * LookSpeed;
        Vector3 camRotation = new Vector3(xRot, 0f, 0f) * LookSpeed;

        Move(velocity);
        if (velocity != Vector3.zero)
            animWea.SetBool("Move", true);

        if (velocity == Vector3.zero)
            animWea.SetBool("Move", false);
        if (grounded)
            animWea.SetBool("Jump", false);

        Look(rotation, camRotation);
        Jump(jump);
        Fire(fire);
    }
    void Look(Vector3 rot, Vector3 camRot)
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rot * Time.deltaTime));
        cam.transform.Rotate(-camRot * Time.deltaTime);
    }
    void Jump(float jump)
    {
        if (jump <= 0 || !grounded) return;
        rb.AddForce(new Vector3(0, 1, 0) * 2, ForceMode.Impulse);
        animWea.SetBool("Jump", true);
    }
    void Fire(float fire)
    {
        if (fire == 0) return;
        if (!weaObj.Reloaded) return;
        fireTimer = new Timer(weaObj.Reload, false, FireProc);

        wS.clip = weaObj.Shoot;
        wS.Play();
        CreateLaser();
        weaObj.Reloaded = false;

        animWea.SetBool("Fire", true);
        animWea.SetBool("Jump", false);
    }
    void CreateLaser()
    {
        bulletTimer = new Timer(0.05f, false, BulletProc);
        laser.enabled = true;
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        laser.SetPosition(0, weaObj.bulletStart.position);
        if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, 100, WeaponLayerMask))
        {
            laser.SetPosition(1, hit.point);
            var ha = hit.collider.gameObject.GetComponentsInChildren<AudioSource>();
            foreach (var item in ha)
            {
                if (item.name == "AudioHit")
                {
                    item.clip = weaObj.Hit;
                    item.Play();
                }
            }
            
            var h = Instantiate(weaObj.HitEffect, hit.point, cam.transform.rotation);
        }
        else
        {
            laser.SetPosition(1, cam.transform.forward * 5000);
        }
    }
    void FireProc()
    {
        weaObj.Reloaded = true;
        animWea.SetBool("Fire", false);
    }
    void BulletProc()
    {
        laser.enabled = false;
        bulletTimer.Reset();
    }

}