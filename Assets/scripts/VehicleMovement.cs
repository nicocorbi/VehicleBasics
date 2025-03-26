using UnityEngine;

public class VehicleMovement3D : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 5f;
    public float friction = 2f;
    public float turnSpeed = 50f;

    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(Vector3.up * turnSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(Vector3.down * turnSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void ApplyMovement()
    {
        Vector3 inputMovement = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) inputMovement -= transform.up;
        if (Input.GetKey(KeyCode.S)) inputMovement += transform.up;

        inputMovement = inputMovement.normalized;

        if (inputMovement.magnitude > 0)
        {
            velocity += inputMovement * acceleration * Time.fixedDeltaTime;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        else
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, friction * Time.fixedDeltaTime);
        }

        rb.linearVelocity = velocity;
    }
}



