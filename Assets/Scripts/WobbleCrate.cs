using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WobbleCrate : MonoBehaviour {

    /* Ambisonic Variables */
    public AmbisonicsSystem ambSys;
    public string file;
    public string id;
    public float volume;

    /* Wobble Variables */
    public Vector3 originPosition;
    public float shake_speed;
    public float shake_intensity;

    // Use this for initialization
    void Start () {
        ambSys.StartSystem();
        ambSys.Add(id, file, volume);
        originPosition = transform.position;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("hi");
            StartCoroutine(ShakeCoroutine(new Vector3(.2f, .2f, .2f), 5, .5f));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SceneManager.LoadScene("MainScene");
            ambSys.StopSystem();
        }
    }

    /*
     * Box Shake Physics Implementation
     */
    private IEnumerator ShakeCoroutine(Vector3 magnitude, float duration, float wavelength)
    {
        Vector3 startPos = transform.localPosition;
        float endTime = Time.time + duration;
        float currentX = 0;

        while (Time.time < endTime)
        {
            Vector3 shakeAmount = new Vector3(
                Mathf.PerlinNoise(currentX, 0) - .5f,
                Mathf.PerlinNoise(currentX, 7) - .5f,
                Mathf.PerlinNoise(currentX, 19) - .5f
            );

            transform.localPosition = Vector3.Scale(magnitude, shakeAmount) + startPos;
            currentX += wavelength;
            yield return null;
        }

        transform.localPosition = startPos;
        var rb = GetComponent<Rigidbody>();
        rb.AddExplosionForce(-300, Vector3.zero, 1000.0f, 1.0f);
        ambSys.StartSound(id, false);
    }
}
