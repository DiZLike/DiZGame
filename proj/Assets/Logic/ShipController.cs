using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipController : MonoBehaviour
{
    public float LookSpeed;
    private float Speed = GSpace.shipSpeed;
    private float acceleration = GSpace.shipAcceleration;

    private Rigidbody rb;
    private Camera cam;
    private NavMeshAgent agent;

    internal Vector3 pTarget;
    internal float currentSpeed;

    private bool auto;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        if (auto)
            MoveAuto();
        else
            ControllerCheck();
    }
    void Update()
    {
        
    }
    void ControllerCheck()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        float zAcceleration = Input.GetAxis("Acceleration");
        float yRot = Input.GetAxisRaw("Mouse X");
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 rotation = new Vector3(-xRot, yRot, 0f) * LookSpeed;
        //Vector3 camRotation = new Vector3(xRot, 0f, 0f) * LookSpeed;

        Vector3 movHor = transform.right * xMov;
        Vector3 movVer = transform.forward * zMov;
        Vector3 velocity = new Vector3();
        if (zAcceleration == 0)
        {
            velocity = (movHor + movVer).normalized * Speed;
            currentSpeed = Speed;
        }
        else
        {
            velocity = (movHor + movVer).normalized * acceleration;
            currentSpeed = acceleration;
        }
        Move(velocity);
        Look(rotation);
    }
    private void Move(Vector3 velocity)
    {
        if (velocity == Vector3.zero)
        {
            currentSpeed = 0f;
            return;
        }
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }
    void Look(Vector3 rot)
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rot * Time.deltaTime));
        //cam.transform.Rotate(-camRot * Time.deltaTime);
    }
    void MoveAuto()
    {
        transform.position = Vector3.MoveTowards(transform.position, pTarget, (acceleration * 100 * Time.deltaTime));
        LookAtXZ(transform, pTarget, LookSpeed * Time.deltaTime);
        currentSpeed = acceleration * 100;
    }
    void LookAtXZ(Transform transform, Vector3 point, float speed)
    {
        var direction = (point - transform.position).normalized;
        //direction.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), speed);
    }
    public void Auto(Vector3 target)
    {
        auto = !auto;
        if (!auto) return;
        pTarget = target;
    }
}
