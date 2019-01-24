using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleCrate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 explosionPos = transform.position;
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("space pressed");
			var rb = GetComponent<Rigidbody>();
			if ( rb )
			{
				Debug.Log("found rb");
				rb.AddExplosionForce( -500, Vector3.zero, 1000.0f, 1.0f );
			}
		}
	}
}
