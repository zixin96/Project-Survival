using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody movementRigidbody;
    [SerializeField] float mainThrust = 1000.0f;
    [SerializeField] float rotateThrust = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        movementRigidbody = GetComponent<Rigidbody>();
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
            movementRigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
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
        movementRigidbody.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * thrust * Time.deltaTime);
        movementRigidbody.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
