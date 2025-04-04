using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float BrakeManorotationSpeed;
    [SerializeField] float maxRotationSpeed;
    [SerializeField] float friction;
    [SerializeField] float brakeForce;
    [SerializeField] float frenoDeMano;
    private TrailRenderer trail;
    private Rigidbody rb;

    private bool accelerating;
    private bool decelerating;
    private bool frenoMano;

    private float rotationInput;
    private float rotationFrenoManoInput;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        movementWithInput();
    }

    void FixedUpdate()
    {
        MovementPhysics();
    }

    
    void movementWithInput()
    {
        rotationInput = 0;

        accelerating = Input.GetKey(KeyCode.W);

        decelerating = Input.GetKey(KeyCode.S);

        if (Input.GetKey(KeyCode.A))
        {
            rotationInput = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotationInput = 1;
        }
        frenoMano = Input.GetKey(KeyCode.Space);
        


    }

    // Controls the movement physics and applies friction
    void MovementPhysics()
    {
        rb.AddTorque(rotationInput * rotationSpeed * transform.up * Time.fixedDeltaTime);
       


        Vector3 forwardDirection = transform.forward; 

        Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity); 

        if (accelerating)
        {
            rb.linearVelocity += speed * forwardDirection * Time.fixedDeltaTime;
        }
        else
        {
            // Apply friction to gradually reduce speed
            rb.linearVelocity = new Vector3(
                rb.linearVelocity.x * (1 / (1 + friction * Time.fixedDeltaTime)),
                0,
                rb.linearVelocity.z * (1 / (1 + friction * Time.fixedDeltaTime))
            );
        }

        if (decelerating)
        {
            if (localVelocity.z > 0) 
            {
                Brake(); 
            }
            else 
            {
                rb.linearVelocity -= speed * forwardDirection * Time.fixedDeltaTime;
            }
        }
        if (frenoMano)
        {
            if (localVelocity.z > 0)
            {
                // aplico freno
                BrakeMano();

                // aplico giro adicional
                rb.AddTorque(rotationInput * BrakeManorotationSpeed * transform.up * Time.fixedDeltaTime);
                

            }
            else
            {
                rb.linearVelocity -= speed * forwardDirection * Time.fixedDeltaTime;
            }
        }

       
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
        
        if (rb.angularVelocity.magnitude > maxRotationSpeed)
        {
            rb.angularVelocity = rb.angularVelocity.normalized * maxRotationSpeed;
        }
    }

    void Brake()
    {
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x * (1 / (1 + brakeForce * Time.fixedDeltaTime)),
            0,
            rb.linearVelocity.z * (1 / (1 + brakeForce * Time.fixedDeltaTime))
        );
    }
    void BrakeMano()
    {
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x * (1 / (1 + frenoDeMano * Time.fixedDeltaTime)),
            0,
            rb.linearVelocity.z * (1 / (1 + frenoDeMano * Time.fixedDeltaTime))
        );


    }
}






