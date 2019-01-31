using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WobbleCrate : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(ShakeBoxes());
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("hi");
            SceneManager.LoadScene("MainScene");
        }
	}

    IEnumerator ShakeBoxes()
    {
        Vector3 explosionPos = transform.position;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space pressed");
            var rb = GetComponent<Rigidbody>();
            if (rb)
            {
                Debug.Log("found rb");
                yield return new WaitForSeconds(3);
                rb.AddExplosionForce(-300, Vector3.zero, 1000.0f, 1.0f);
            }
        }
    }
}
