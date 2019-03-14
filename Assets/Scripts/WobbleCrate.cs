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

    AudioSource audioData;
    const float maxAngle = 10f;

    // Use this for initialization
    void Start () {
        ambSys.StartSystem();
        ambSys.Add(id, file, volume);
        originPosition = transform.position;
        audioData = GetComponent<AudioSource>();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // AMBISONICS SYSTEM
        {
            StartCoroutine(ShakeCoroutine(new Vector3(.2f, .2f, .2f), 5, .5f, true));
            // Properties p = new Properties(0f, 1f, .5f, 3f, 0f, 1f, 0f);
            // StartCoroutine(ShakeCoroutine(p));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("MainScene");
            ambSys.StopSystem();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(ShakeCoroutine(new Vector3(.2f, .2f, .2f), 5, .5f, false));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene("MainScene");
            audioData.Stop();
        }
    }

    /*
     * Box Shake Physics Implementation
     */
    private IEnumerator ShakeCoroutine(Vector3 magnitude, float duration, float wavelength, bool ambisonics)
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

        if (ambisonics)
        {
            ambSys.StartSound(id, false);
        } else
        {
            audioData.Play(0);
        }

    }

    /*
    * Perlin Noise Box Shake (testing)
    * adapted from Sebastian Lague https://github.com/SebLague/Camera-Shake 
    */
    private IEnumerator ShakeCoroutine(Properties properties)
    {
        float completionPercent = 0;
        float movePercent = 0;

        float angle_radians = properties.angle * Mathf.Deg2Rad - Mathf.PI;
        Vector3 previousWaypoint = Vector3.zero;
        Vector3 currentWaypoint = Vector3.zero;
        float moveDistance = 0;
        float speed = 0;

        Quaternion targetRotation = Quaternion.identity;
        Quaternion previousRotation = Quaternion.identity;

        do
        {
            if (movePercent >= 1 || completionPercent == 0)
            {
                float dampingFactor = DampingCurve(completionPercent, properties.dampingPercent);
                float noiseAngle = (Random.value - .5f) * Mathf.PI;
                angle_radians += Mathf.PI + noiseAngle * properties.noisePercent;
                currentWaypoint = new Vector3(Mathf.Cos(angle_radians), Mathf.Sin(angle_radians)) * properties.strength * dampingFactor;
                previousWaypoint = transform.localPosition;
                moveDistance = Vector3.Distance(currentWaypoint, previousWaypoint);

                targetRotation = Quaternion.Euler(new Vector3(currentWaypoint.y, currentWaypoint.x).normalized * properties.rotationPercent * dampingFactor * maxAngle);
                previousRotation = transform.localRotation;

                speed = Mathf.Lerp(properties.minSpeed, properties.maxSpeed, dampingFactor);

                movePercent = 0;
            }

            completionPercent += Time.deltaTime / properties.duration;
            movePercent += Time.deltaTime / moveDistance * speed;
            transform.localPosition = Vector3.Lerp(previousWaypoint, currentWaypoint, movePercent);
            transform.localRotation = Quaternion.Slerp(previousRotation, targetRotation, movePercent);


            yield return null;
        } while (moveDistance > 0);
    }

    float DampingCurve(float x, float dampingPercent)
    {
        x = Mathf.Clamp01(x);
        float a = Mathf.Lerp(2, .25f, dampingPercent);
        float b = 1 - Mathf.Pow(x, a);
        return b * b * b;
    }

    [System.Serializable]
    public class Properties
    {
        public float angle;
        public float strength;
        public float maxSpeed;
        public float minSpeed;
        public float duration;
        [Range(0, 1)]
        public float noisePercent;
        [Range(0, 1)]
        public float dampingPercent;
        [Range(0, 1)]
        public float rotationPercent;

        public Properties(float angle, float strength, float speed, float duration, float noisePercent, float dampingPercent, float rotationPercent)
        {
            this.angle = angle;
            this.strength = strength;
            this.maxSpeed = speed;
            this.duration = duration;
            this.noisePercent = Mathf.Clamp01(noisePercent);
            this.dampingPercent = Mathf.Clamp01(dampingPercent);
            this.rotationPercent = Mathf.Clamp01(rotationPercent);
        }


    }
}
