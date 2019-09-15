using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpin : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float spinSpeed = 10f;
    [SerializeField] float dragVal = 3f;
    [SerializeField] float angDragVal = 10f;

    bool canSpin = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TouchSpin();
    }

    private void TouchSpin()
    {
        if (canSpin)
        {
            rb.drag = dragVal;

            if (Input.touchCount > 0)
            {
                rb.AddTorque(Vector3.left * spinSpeed, ForceMode.Force);
            }
            else
            {
                rb.angularDrag = angDragVal;
            }
        }
        else
        {
            rb.drag = 0.1f;
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
