using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float forwardVelocity = 10f;
    [SerializeField] float sideVelocity = 10f;
    [SerializeField] float spinSpeed = 10f;

    Vector3 mousePos, objPos;
    float clampValue = 15f;
    bool canSpin = false;

    [SerializeField] Collision ground;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Spin();
    }

    private void FixedUpdate()
    {
        
    }

    private void Movement()
    {
        if (Input.GetMouseButton(0))
        {
            //float forVel = Input.GetAxis("Vertical");
            //float sideVel = Input.GetAxis("Horizontal");
            //Vector3 vel = new Vector3(sideVelocity * sideVel, 0, forwardVelocity * forVel);
            //rb.AddTorque(spinSpeed*Vector3.left, ForceMode.Force);

            Vector3 vel = new Vector3(0, 0, forwardVelocity);
            rb.AddForce(vel);

           
            OnMouseDrag();
            
        }
        //else
        //{
        //    rb.velocity = rb.velocity;
        //    rb.rotation = gameObject.transform.rotation;
        //    rb.AddTorque(Vector3.right, ForceMode.Force);
        //    rb.drag = 100;
        //}
    }

    private void OnMouseDrag()
    {
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        objPos = Camera.main.ScreenToWorldPoint(mousePos);
        objPos.x = Mathf.Clamp(objPos.x,-clampValue, clampValue);
        objPos.z = transform.position.z;
        objPos.y = transform.position.y;
        transform.position = objPos;
    }

    private void Spin()
    {
        if (canSpin)
        {
            if (Input.GetMouseButton(0))
            {
                rb.AddTorque(Vector3.left * spinSpeed, ForceMode.Force);
            }
            else
            {
                rb.drag = 5;
                rb.angularDrag = 10;
            }
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        canSpin = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        canSpin = false;
    }
}
