using UnityEngine;
using System.Collections;

public class Camara : MonoBehaviour
{
    public Transform target;                  // El veh�culo a seguir
    public float smoothTime = 0.3f;           // Suavidad del movimiento
    private Vector3 velocity = Vector3.zero;

    public Vector3 offset = new Vector3(0, 3, -10);  // Posici�n relativa detr�s y arriba del veh�culo

    void FixedUpdate()
    {
        // Posici�n deseada basada en la rotaci�n del veh�culo
        Vector3 targetPosition = target.TransformPoint(offset);

        // Movimiento suave hacia esa posici�n
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Hacemos que la c�mara mire hacia el veh�culo (opcional, pero �til)
        transform.LookAt(target);
    }
}

