using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class quadratic : MonoBehaviour
{
    [SerializeField] Transform PointA;
    [SerializeField] Transform PointB;
    [SerializeField] Transform PointC;
    [SerializeField] Transform PointD;
    [SerializeField] Transform asa;
    [SerializeField] float duration;
    [SerializeField] GameObject prefab;
    [SerializeField] AnimationCurve curva;
    public GameObject instantiatedObject;
    public Color startColor = Color.white;
    public Color endColor = Color.blue;
    public Color startColor2 = Color.white;
    public Color endColor2 = Color.blue;
    public Color startColor3 = Color.white;
    public Color endColor3 = Color.blue;
    public MeshRenderer meshRenderer;
    public MeshRenderer meshRenderer2;
    public MeshRenderer meshRenderer3;
    public MeshRenderer meshRenderer4;
    public MeshRenderer meshRenderer5;





    void Start()
    {
        StartCoroutine(InstantiateOverTime());
        instantiatedObject = Instantiate(prefab);
        meshRenderer = instantiatedObject.GetComponent<MeshRenderer>();
        



    }


    IEnumerator InstantiateOverTime()
    {
        while (true)
        {

        
            float elapsedTime = 0;
            float normalizedElapsedTime;
            Vector3 interpolacionC;
            Vector3 interpolacionD;
            Vector3 finalValue;
            



            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                normalizedElapsedTime = elapsedTime / duration;
                interpolacionC = Vector3.Lerp(PointA.position, asa.position, curva.Evaluate(normalizedElapsedTime));
                interpolacionD = Vector3.Lerp(asa.position, PointB.position, curva.Evaluate(normalizedElapsedTime));
                finalValue = Vector3.Lerp(PointC.position,PointD.position,curva.Evaluate(normalizedElapsedTime));
                Color finalColor = Color.Lerp(startColor, endColor, normalizedElapsedTime);
                Color finalColor2 = Color.Lerp(startColor2, endColor2, normalizedElapsedTime);
                Color finalColor3 = Color.Lerp(startColor3, endColor3, normalizedElapsedTime);
                Color pointB = Color.green;
                Color point = Color.blue;


                meshRenderer.material.color = finalColor;
                meshRenderer2.material.color = finalColor2;
                meshRenderer3.material.color = finalColor3;
                meshRenderer4.material.color = pointB;
                meshRenderer5.material.color = point;






                instantiatedObject.transform.position = finalValue;
                PointC.transform.position = interpolacionC;
                PointD.transform.position = interpolacionD;
                




                yield return new WaitForEndOfFrame();
            }
       

        }
    }
}
