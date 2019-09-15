using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float forwardVelocity = 10000f;
    [SerializeField] float sideVelocity = 15f;

    Vector3 mousePos, objPos;
    float clampValue = 45f;
    float deltaX;
    //bool canSpin = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        //Debug.Log(deltaX);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TouchMovement();
        
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (Input.touchCount > 0)
        {
            XMove();
        }
    }

    private void TouchMovement()
    {
        if (Input.touchCount > 0)
        {
            //Debug.Log(Input.touchCount);
            Vector3 vel = new Vector3(0, 0, forwardVelocity);
            //Debug.Log(vel);
            rb.AddForce(vel);
          

            //XMove();
        }
    }

    private void XMove()
    {
        Touch touch = Input.touches[0];
        Vector3 touchPos = touch.position;
        //Debug.Log(touchPos);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                deltaX = touchPos.x - transform.position.x;
                break;

            case TouchPhase.Moved:
                Vector3 finalPos = new Vector3(0, 0, 0);
                /*if (Mathf.Abs(transform.position.x - (touchPos.x - deltaX)) > 0.2f)
                {
                    finalPos.x = Mathf.Clamp(touchPos.x - deltaX, -clampValue, clampValue);
                }
                else
                {
                    finalPos.x = transform.position.x;
                }*/
                finalPos.x = Mathf.Clamp(touchPos.x - deltaX, -clampValue, clampValue);
                finalPos.z = transform.position.z;
                finalPos.y = transform.position.y;
                rb.MovePosition(finalPos);
                break;

                /*if (!canSpin)
                {
                    float diff = transform.position.x - (touchPos.x - deltaX);
                    //Debug.Log(diff);
                    if (Mathf.Abs(diff) > 180f)
                    {
                        if (diff > 0f)
                            transform.localRotation = Quaternion.Euler(new Vector3(0, -30f, 0));
                        else
                            transform.localRotation = Quaternion.Euler(new Vector3(0, 30f, 0));
                    }
                    else if (Mathf.Abs(diff) < 180f)
                    {
                        transform.localRotation = Quaternion.Euler(new Vector3(0, Mathf.Clamp(-diff / 3, -clampValue, clampValue), 0));
                    }
                    break;
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, 0, transform.localRotation.z));
                    break;
                }*/

                /*if (!canSpin)
                {
                    float diff = transform.position.x - (touchPos.x - deltaX);
                    Debug.Log(diff);
                    if (Mathf.Abs(diff) > 180f)
                    {
                        if (diff > 0f)
                        {
                            rb.AddForce(new Vector3(sideVelocity, 0, 0));
                        }
                        else
                        {
                            rb.AddForce(new Vector3(-sideVelocity, 0, 0));
                        }
                    }
                    else if (Mathf.Abs(diff) < 180f)
                    {
                        if (diff > 0f)
                        {
                            rb.AddForce(new Vector3(sideVelocity * diff / 18f, 0f, 0f));
                        }
                        else
                        {
                            rb.AddForce(new Vector3(-sideVelocity * diff / 18f, 0f, 0f));
                        }
                    }
                    break;
                }
                break;*/
                

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
