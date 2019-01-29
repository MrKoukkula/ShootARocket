using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rocketRigitBody;
    [SerializeField] float turningSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rocketRigitBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        //if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.S))
        //{
        //    print("space and right pressed");
        //}
        //else if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.A))
        //{
        //    print("space and left pressed");
        //}
        if (Input.GetKey(KeyCode.Space))
        {
            //print("Thrusting");
            rocketRigitBody.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.A))
        {
            //print("Turning left");
            transform.Rotate(Vector3.forward * (Time.deltaTime * turningSpeed));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //print("Turning right");
            transform.Rotate(-Vector3.forward * (Time.deltaTime * turningSpeed));
        }
    }
}
