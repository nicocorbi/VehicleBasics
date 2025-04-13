using UnityEngine;

public class Punto : MonoBehaviour
{
    [SerializeField] private float velocidadRotacion = 100f; 

    private void Update()
    {
        
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameEvents.puntosRecolectados.Invoke(); 
            Destroy(gameObject); 
        }
    }
}





