using UnityEngine;
using System.Collections;

public class lerps : MonoBehaviour
{
    [SerializeField] Transform[] Points;
    [SerializeField] GameObject prefab;
    [SerializeField] AnimationCurve curva;
    [SerializeField] float duration;
    [SerializeField] Transform objeto;

    public GameObject instantiatedObject;

    void Start()
    {
        StartCoroutine(MoveObjectBetweenPoints());
        instantiatedObject = Instantiate(prefab);
    }

    IEnumerator MoveObjectBetweenPoints()
    {
        int currentPointIndex = 0;
        int nextPointIndex = 1;
        float elapsedTime = 0;

        while (true)
        {
          
          

            Vector3 fromPosition = Points[currentPointIndex].position;
            Vector3 toPosition = Points[nextPointIndex].position;

            
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float normalizedElapsedTime = elapsedTime / duration;
                instantiatedObject.transform.position = Vector3.Lerp(fromPosition, toPosition, curva.Evaluate(normalizedElapsedTime));
                yield return null;
            }

           
            currentPointIndex = nextPointIndex;
            nextPointIndex = (nextPointIndex + 1) % Points.Length;
            elapsedTime = 0;  
        }
    }
}

