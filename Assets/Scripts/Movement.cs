using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000.0f;
    [SerializeField] float rotateThrust = 100.0f;
    [SerializeField] AudioClip engineThrust;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;


    Rigidbody mRigidbody;
    AudioSource mAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        mAudioSource = GetComponent<AudioSource>();
        mRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();

        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            StartRotatingLeft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRotatingRight();

        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        if (!mAudioSource.isPlaying)
        {
            mAudioSource.PlayOneShot(engineThrust);
        }
        // Whenever we call Play, the particle system restarts,
        // which is not what we want. 
        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
        ;
        mRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }

    void StopThrusting()
    {
        mainBoosterParticles.Stop();
        mAudioSource.Stop();
    }


    void StartRotatingLeft()
    {
        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();
        }
        ApplyRotation(-rotateThrust);
    }

    void StartRotatingRight()
    {
        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
        ApplyRotation(rotateThrust);
    }

    void StopRotating()
    {
        rightBoosterParticles.Stop();
        leftBoosterParticles.Stop();
    }

    void ApplyRotation(float thrust)
    {
        // Freeze/unfreeze solves obstacle edges bug
        mRigidbody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * thrust * Time.deltaTime);
        mRigidbody.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
