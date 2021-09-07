using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000.0f;
    [SerializeField] float rotateThrust = 100.0f;
    [SerializeField] AudioClip engineThrust;

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
            if(!mAudioSource.isPlaying)
            {
                mAudioSource.PlayOneShot(engineThrust);
            }
            mRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
        else
        {
            mAudioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateThrust);
        }
    }
    void ApplyRotation(float thrust)
    {
        // Freeze/unfreeze solves obstacle edges bug
        mRigidbody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * thrust * Time.deltaTime);
        mRigidbody.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
