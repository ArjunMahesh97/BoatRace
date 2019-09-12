using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float forwardVelocity = 10f;
    [SerializeField] float sideVelocity = 10f;
    [SerializeField] float spinSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void FixedUpdate()
    {
        if (rb.transform.position.y > 0)
        {
            rb.useGravity = true;
            rb.drag = 1;
        }
        else
        {
            rb.useGravity=false;
            rb.drag = 10;
        }
    }

    private void Movement()
    {
        if (Input.GetKey("w"))
        {
            float forVel = Input.GetAxis("Vertical");
            float sideVel = Input.GetAxis("Horizontal");
            Vector3 vel = new Vector3(sideVelocity * sideVel, 0, forwardVelocity * forVel);
            rb.velocity = vel;
            rb.AddTorque(spinSpeed * Vector3.right);
        }
        else
        {
            rb.velocity = rb.velocity;
            rb.rotation = gameObject.transform.rotation;
            rb.AddTorque(spinSpeed * Vector3.left);
        }
    }
}
