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
    public Vector3 startScale = new Vector3(1f, 1f, 1f);
    public Vector3 endScale = new Vector3(2f, 2f, 2f);
    public Quaternion startRotation = Quaternion.identity;
    public Quaternion endRotation = Quaternion.Euler(0f, 180f, 0f);
    public Color startColor = Color.white;
    public Color endColor = Color.blue;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        instantiatedObject = Instantiate(prefab);    
        meshRenderer = instantiatedObject.GetComponent<MeshRenderer>();      
        StartCoroutine(InstantiateOverTime());
    }

    IEnumerator InstantiateOverTime()
    {
     while (true) {
        float elapsedTime = 0;
        float normalizedElapsedTime;
        Vector3 finalValue;

        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            normalizedElapsedTime = elapsedTime / duration;

            
            finalValue = Vector3.Lerp(fromPosition.position, toPosition.position, curva.Evaluate(normalizedElapsedTime));
            Vector3 finalScale = Vector3.Lerp(startScale, endScale, normalizedElapsedTime);
            Quaternion rotacion = Quaternion.Lerp(startRotation, endRotation, normalizedElapsedTime);
            Color finalColor = Color.Lerp(startColor, endColor, normalizedElapsedTime);

           
            instantiatedObject.transform.localScale = finalScale;
            instantiatedObject.transform.rotation = rotacion;
            instantiatedObject.transform.position = finalValue;
            meshRenderer.material.color = finalColor;

            print(elapsedTime.ToString("F3") + "-->" + finalValue.ToString("F3"));

            yield return new WaitForEndOfFrame();
        }

        
        elapsedTime = 0;

        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            normalizedElapsedTime = elapsedTime / duration;

           
            finalValue = Vector3.Lerp(toPosition.position, fromPosition.position, curva.Evaluate(normalizedElapsedTime));
            Vector3 inicioScale = Vector3.Lerp(endScale, startScale, normalizedElapsedTime);
            Quaternion rotacion = Quaternion.Lerp(endRotation, startRotation, normalizedElapsedTime);
            Color finalColor = Color.Lerp(endColor, startColor, normalizedElapsedTime);

           
            instantiatedObject.transform.position = finalValue;
            instantiatedObject.transform.localScale = inicioScale;
            instantiatedObject.transform.rotation = rotacion;
            meshRenderer.material.color = finalColor;

            print(elapsedTime.ToString("F3") + "-->" + finalValue.ToString("F3"));

            yield return new WaitForEndOfFrame();
        }
            }
    }
}

