using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruebas : MonoBehaviour
{
    [SerializeField] int line;
    [SerializeField] int column;
    [SerializeField] int depth;
    [SerializeField] int start;

    [SerializeField] GameObject cubo;

    [SerializeField] float timeBetweenSpawns;
    Vector3 position = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstantiateOverTime());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator InstantiateOverTime()
    {
        for (int i = start; i <= line + start - 1; i++)
        {
            for (int j = start; j <= column + start - 1; j++)
            {
                for (int k = start; k <= depth + start - 1; k++)
                {

                    if ((i == start || i == line + start - 1) && (j == start || j == column + start - 1) ||
                        (j == start || j == column + start - 1) && (k == start || k == depth + start - 1) ||
                        (i == start || i == line + start - 1) && (k == start || k == depth + start - 1))
                    {
                        position = new Vector3(i, j, k);
                        Instantiate(cubo, position, Quaternion.identity);
                        yield return new WaitForSeconds(timeBetweenSpawns);
                    }
                }
            }
        }
    }
}
