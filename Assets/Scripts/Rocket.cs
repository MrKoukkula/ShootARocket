using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rocketRigitBody;
    [SerializeField] float turningSpeed = 5f;
    AudioSource rocketThrustSound;
    // Start is called before the first frame update
    void Start()
    {
        rocketRigitBody = GetComponent<Rigidbody>();
        rocketThrustSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            rocketThrustSound.volume = 1.0f;
            rocketThrustSound.Play();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rocketRigitBody.AddRelativeForce(Vector3.up);
        } else
        {
            rocketThrustSound.volume = rocketThrustSound.volume - 0.1f;
            if (rocketThrustSound.volume == 0)
            {
                rocketThrustSound.Stop();
            }
        }

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    rocketThrustSound.volume = 0.0f;
        //    rocketThrustSound.Stop();
        //}

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * (Time.deltaTime * turningSpeed));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //print("Turning right");
            transform.Rotate(-Vector3.forward * (Time.deltaTime * turningSpeed));
        }
    }
}
