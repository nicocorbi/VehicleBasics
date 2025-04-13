using TMPro;
using System.Collections;
using UnityEngine;

public class spawnMoneda : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform[] spawnPoints;
    private GameObject monedaInstanciada;

    public int monedasRecogidas = 0;

    [SerializeField] public TextMeshProUGUI textoMonedas;

    [SerializeField] private AnimationCurve parpadeoCurve;
    [SerializeField] private float duracionParpadeo = 0.5f;
    [SerializeField] private int vecesParpadeo = 5;

    private Coroutine animCoroutine;

    void Start()
    {
        ActualizarTexto();
        SpawnMoneda();
    }

    private void OnEnable()
    {
        GameEvents.puntosRecolectados.AddListener(SpawnMoneda);
    }

    private void OnDisable()
    {
        GameEvents.puntosRecolectados.RemoveListener(SpawnMoneda);
    }

    public void SpawnMoneda()
    {

        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];
        monedaInstanciada = Instantiate(prefab, spawnPoint.position, Quaternion.identity);


        monedasRecogidas++;
        ActualizarTexto();



        animCoroutine = StartCoroutine(AnimarParpadeo());

        Debug.Log("Monedas recogidas: " + monedasRecogidas);
    }

    private void ActualizarTexto()
    {
        
            textoMonedas.text = "Monedas: " + monedasRecogidas;
        
    }

    private IEnumerator AnimarParpadeo()
    {
        Color colorOriginal = textoMonedas.color;

        for (int i = 0; i < vecesParpadeo; i++)
        {
            float tiempo = 0f;


            while (tiempo < duracionParpadeo)
            {
                float alpha = parpadeoCurve.Evaluate(tiempo / duracionParpadeo);
                textoMonedas.color = new Color(colorOriginal.r, colorOriginal.g, colorOriginal.b, alpha);
                tiempo += Time.deltaTime;
                yield return null;
            }


            textoMonedas.color = colorOriginal;
        }
    }
}








