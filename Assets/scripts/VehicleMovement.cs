using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public TextMeshProUGUI mensajeFin;

    public TrailRenderer trail;
    private Rigidbody rb;

    private bool accelerating;
    private bool decelerating;
    private bool frenoMano;

    private float rotationInput;

    private bool juegoTerminado = false;
    private bool puedeReiniciar = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (juegoTerminado)
        {
            if (puedeReiniciar && Input.GetKeyDown(KeyCode.Space))
            {
                ReiniciarJuego();
            }
            return;
        }

        movementWithInput();
    }

    void FixedUpdate()
    {
        if (juegoTerminado) return;

        MovementPhysics();
    }

    void movementWithInput()
    {
        rotationInput = 0;

        accelerating = Input.GetKey(KeyCode.W);
        decelerating = Input.GetKey(KeyCode.S);

        if (Input.GetKey(KeyCode.A)) rotationInput = -1;
        if (Input.GetKey(KeyCode.D)) rotationInput = 1;

        frenoMano = Input.GetKey(KeyCode.Space);
    }

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
                BrakeMano();
                rb.AddTorque(rotationInput * BrakeManorotationSpeed * transform.up * Time.fixedDeltaTime);

                if (!trail.emitting)
                {
                    trail.emitting = true;
                }
            }
            else
            {
                rb.linearVelocity -= speed * forwardDirection * Time.fixedDeltaTime;
            }
        }
        else
        {
            if (trail.emitting)
            {
                trail.emitting = false;
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("obstaculo"))
        {
            Debug.Log("¡Fin del juego!");
            mensajeFin.gameObject.SetActive(true);
            juegoTerminado = true;
            rb.isKinematic = true;  // Desactivar la física del jugador

            // Mostrar mensaje de reinicio
            mensajeFin.text = "Presiona Espacio para reiniciar";
            puedeReiniciar = true;
        }
    }

    void ReiniciarJuego()
    {
        // Reiniciar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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










