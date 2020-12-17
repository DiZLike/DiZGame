using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Any
{
    protected Rigidbody rb;
    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
