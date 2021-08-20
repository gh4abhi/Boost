﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Controller : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void Rotate()
    {
        Thrust();
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
            print(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
            print(-Vector3.forward);
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))      // We can thrust while rotating.                
        {
            rigidBody.AddRelativeForce(Vector3.up);
            print(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
