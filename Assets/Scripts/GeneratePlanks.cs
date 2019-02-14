using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlanks : MonoBehaviour
{
    public Transform prefab;

    // Start is called before the first frame update
    void Start()
    {

        Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        Instantiate(prefab, position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
