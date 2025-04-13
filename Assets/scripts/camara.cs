using UnityEngine;
using System.Collections;

public class Camara : MonoBehaviour
{
    public Transform target;                  
    public float smoothTime = 0.3f;           
    private Vector3 velocity = Vector3.zero;

    public Vector3 offset = new Vector3(0, 3, -10);  

    void FixedUpdate()
    {
       
        Vector3 targetPosition = target.TransformPoint(offset);       
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        
        transform.LookAt(target);
    }
}

