using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumbleSpace : MonoBehaviour {
    public AmbisonicsSystem ambSys;

	// Use this for initialization
	void Start () {
        ambSys.StartSystem();
    }
	
	// Update is called once per frame
	void Update () {
		AudioSource audioData; 
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("space pressed");
			audioData = GetComponent<AudioSource>();
			audioData.Play(0);
            ambSys.SetAmbient("child-scream.wav", 10, 10);
        }	
	}
}
