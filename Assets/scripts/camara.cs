using UnityEngine;
using System.Collections;

public class Camara : MonoBehaviour
{
    public Transform target;                  // El vehículo a seguir
    public float smoothTime = 0.3f;           // Suavidad del movimiento
    private Vector3 velocity = Vector3.zero;

    public Vector3 offset = new Vector3(0, 3, -10);  // Posición relativa detrás y arriba del vehículo

    void FixedUpdate()
    {
        // Posición deseada basada en la rotación del vehículo
        Vector3 targetPosition = target.TransformPoint(offset);

        // Movimiento suave hacia esa posición
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Hacemos que la cámara mire hacia el vehículo (opcional, pero útil)
        transform.LookAt(target);
    }
}

