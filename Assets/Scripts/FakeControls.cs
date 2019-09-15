using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FakeControls : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float forwardVelocity = 10f;
    [SerializeField] float sideVelocity = 10f;

    Vector3 mousePos, objPos;
    float clampValue = 20f;
    float deltaX, deltaXfake;

    List<Touch> fakeTouches;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        fakeTouches = InputHelper.GetTouches();
        TouchMovement();

    }

    private void TouchMovement()
    {
        if (Input.touchCount > 0 || fakeTouches.Count > 0)
        {
            //Debug.Log(fakeTouches.Count);
            Vector3 vel = new Vector3(0, 0, forwardVelocity);
            rb.AddForce(vel);
            //TouchSpin();

            FakeXMove();
            XMove();

        }
    }

    private void XMove()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
        //Debug.Log("aa");
        switch (touch.phase)
        {
            case TouchPhase.Began:
                deltaX = touchPos.x - transform.position.x;
                break;

            case TouchPhase.Moved:
                Vector3 finalPos = new Vector3(0, 0, 0);
                if (transform.position.x - (touchPos.x - deltaX) > 0.2f)
                {
                    finalPos.x = Mathf.Clamp(touchPos.x - deltaX, -clampValue, clampValue);
                }
                else
                {
                    finalPos.x = transform.position.x;
                }
                finalPos.z = transform.position.z;
                finalPos.y = transform.position.y;
                rb.MovePosition(finalPos);
                break;
        }
    }

    private void FakeXMove()
    {
        foreach (Touch fakeTouch in fakeTouches)
        {
            //Debug.Log("aa");
            Vector3 touchPosFake = fakeTouch.position;
            //Debug.Log(touchPosFake.x);
            switch (fakeTouch.phase)
            {
                case TouchPhase.Began:
                    deltaXfake = touchPosFake.x - transform.position.x;
                    break;

                case TouchPhase.Moved:
                    Vector3 finalPosfake = new Vector3(0, 0, 0);
                    finalPosfake.x = Mathf.Clamp(touchPosFake.x - deltaXfake, -clampValue, clampValue);
                    finalPosfake.z = transform.position.z;
                    finalPosfake.y = transform.position.y;
                    //Debug.Log(finalPosfake);
                    rb.MovePosition(finalPosfake);
                    break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("aa");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
