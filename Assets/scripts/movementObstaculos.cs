using System.Collections;
using UnityEngine;

public class interpolacion : MonoBehaviour
{
    [SerializeField] Transform fromPosition;
    [SerializeField] Transform toPosition;
    [SerializeField] float duration;
    [SerializeField] GameObject prefab;
    [SerializeField] AnimationCurve curva;
    private GameObject instantiatedObject;
    
   
   
  

    private void Start()
    {
        instantiatedObject = Instantiate(prefab);
       
        StartCoroutine(InstantiateOverTime());
    }

    IEnumerator InstantiateOverTime()
    {
        while (true)
        {
            float elapsedTime = 0;
            float normalizedElapsedTime;
            Vector3 finalValue;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                normalizedElapsedTime = elapsedTime / duration;
                finalValue = Vector3.Lerp(fromPosition.position, toPosition.position, curva.Evaluate(normalizedElapsedTime));

                
                instantiatedObject.transform.position = finalValue;

                yield return new WaitForEndOfFrame();
            }

            elapsedTime = 0;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                normalizedElapsedTime = elapsedTime / duration;
                finalValue = Vector3.Lerp(toPosition.position, fromPosition.position, curva.Evaluate(normalizedElapsedTime));
                instantiatedObject.transform.position = finalValue;

                yield return new WaitForEndOfFrame();
            }
        }
    }

}
