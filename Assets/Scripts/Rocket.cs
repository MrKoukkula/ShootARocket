using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rocketRigitBody;
    [SerializeField] float turningSpeed = 5f;
    [SerializeField] float thrustSpeed = 10f;
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
        Thrust();
        Rotate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "friendly")
        {
            Debug.Log("ok");
        } else if (collision.gameObject.tag == "goal")
        {
            Debug.Log("You win!");
        } else
        {
            Debug.Log("You died");
        }
    }

    private void Thrust()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rocketThrustSound.volume = 1.0f;
            rocketThrustSound.Play();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rocketRigitBody.AddRelativeForce(Vector3.up * (Time.deltaTime * thrustSpeed));
        }
        else
        {
            rocketThrustSound.volume = rocketThrustSound.volume - 0.1f;
            if (rocketThrustSound.volume == 0)
            {
                rocketThrustSound.Stop();
            }
        }
    }

    private void Rotate()
    {

        rocketRigitBody.freezeRotation = true; // stop environment physics rotation

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * (Time.deltaTime * turningSpeed));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //print("Turning right");
            transform.Rotate(-Vector3.forward * (Time.deltaTime * turningSpeed));
        }

        rocketRigitBody.freezeRotation = false; // continue environment physics rotation
    }
}
