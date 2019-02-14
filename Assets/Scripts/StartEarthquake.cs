using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class StartEarthquake : MonoBehaviour {
    public AmbisonicsSystem ambSys;
    public string file;
    public string id;
    public float volume;

    // Use this for initialization
    void Start () {
        ambSys.StartSystem();
        ambSys.Add(id, file, volume);
        Debug.Log("id is :" + id);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("space pressed");
            ambSys.StartSound(id, false);
            ambSys.Shake((float)0.5);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("STOP");
            ambSys.StopSystem();
        }
	}
}
