using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // continually growing over time
        // note here we don't do == 0 because of precision issue
        float cycles = period <= Mathf.Epsilon ? 0.0f : Time.time / period; 

        // tau is a constant value of 6.283
        const float tau = Mathf.PI * 2; 

        // going from -1 to 1
        float rawSinWave = Mathf.Sin(cycles * tau);

        // going from 0 to 1
        movementFactor = (rawSinWave + 1.0f) / 2.0f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
