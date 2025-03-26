using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class cubica : MonoBehaviour
{
    [SerializeField] Transform PointA;
    [SerializeField] Transform PointB;
    [SerializeField] Transform PointC;
    [SerializeField] Transform PointD;
    [SerializeField] Transform PointE;
    [SerializeField] Transform PointF;
    [SerializeField] Transform PointG;
    [SerializeField] Transform asaA;
    [SerializeField] Transform asaB;

    public GameObject instantiatedObject;
    [SerializeField] AnimationCurve curva;
    [SerializeField] GameObject prefab;
    [SerializeField] float duration;
    public MeshRenderer meshRenderer;
    public MeshRenderer meshRenderer2;
    public MeshRenderer meshRenderer3;
    public MeshRenderer meshRenderer4;
    public MeshRenderer meshRenderer5;
    public MeshRenderer meshRenderer6;
    public MeshRenderer meshRenderer7;
    public MeshRenderer meshRenderer8;
    public MeshRenderer meshRenderer9;
    public Color startColor = Color.white;
    public Color endColor = Color.blue;
    public Color startColor2 = Color.white;
    public Color endColor2 = Color.blue;
    public Color startColor3 = Color.white;
    public Color endColor3 = Color.blue;
    public Color startColor4 = Color.white;
    public Color endColor4 = Color.blue;
    public Color startColor5 = Color.white;
    public Color endColor5 = Color.blue;
    



    void Start()
    {
        StartCoroutine(InstantiateOverTime());
        instantiatedObject = Instantiate(prefab);
        meshRenderer = instantiatedObject.GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    IEnumerator InstantiateOverTime()
    {
        while (true)
        {


            float elapsedTime = 0;
            float normalizedElapsedTime;
            Vector3 interpolacionC;
            Vector3 interpolacionD;
            Vector3 interpolacionE;
            Vector3 interpolacionF;
            Vector3 interpolacionG;

            //c: de a al asa de a, d:lerp del asa de a al asa de b, e:lerp del asa de b a b , f:lerp de c a d , g: lerp de d a e, target de f a g


            Vector3 finalValue;




            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                normalizedElapsedTime = elapsedTime / duration;
                interpolacionC = Vector3.Lerp(PointA.position, asaA.position, curva.Evaluate(normalizedElapsedTime));
                interpolacionD = Vector3.Lerp(asaA.position, asaB.position, curva.Evaluate(normalizedElapsedTime));
                interpolacionE = Vector3.Lerp(asaB.position, PointB.position, curva.Evaluate(normalizedElapsedTime));
                interpolacionF = Vector3.Lerp(PointC.position, PointD.position, curva.Evaluate(normalizedElapsedTime));
                interpolacionG = Vector3.Lerp(PointD.position, PointE.position, curva.Evaluate(normalizedElapsedTime));
                finalValue = Vector3.Lerp(PointF.position, PointG.position, curva.Evaluate(normalizedElapsedTime));
                Color finalColor = Color.Lerp(startColor, endColor, normalizedElapsedTime);
                Color finalColor2 = Color.Lerp(startColor2, endColor2, normalizedElapsedTime);
                Color finalColor3 = Color.Lerp(startColor3, endColor3, normalizedElapsedTime);
                Color finalColor4 = Color.Lerp(startColor4, endColor4, normalizedElapsedTime);
                Color finalColor5 = Color.Lerp(startColor5, endColor5, normalizedElapsedTime);




                Color pointB = Color.green;
                Color pointA = Color.blue;
                Color asaAcolor = Color.red;
                Color asaBcolor = Color.yellow;


                meshRenderer5.material.color = finalColor;
                meshRenderer6.material.color = finalColor2;
                meshRenderer7.material.color = finalColor3;
                meshRenderer8.material.color = finalColor4;
                meshRenderer9.material.color = finalColor5;

                meshRenderer.material.color = pointB;
                meshRenderer2.material.color = pointA;
                meshRenderer3.material.color = asaAcolor;
                meshRenderer4.material.color = asaBcolor;

                instantiatedObject.transform.position = finalValue;
                PointC.transform.position = interpolacionC;
                PointD.transform.position = interpolacionD;
                PointE.transform.position = interpolacionE;
                PointF.transform.position = interpolacionF;
                PointG.transform.position = interpolacionG;






                yield return new WaitForEndOfFrame();
            }
        }
    }
}
