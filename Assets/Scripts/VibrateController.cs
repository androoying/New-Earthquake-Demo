using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VibrateController : MonoBehaviour {
    [SteamVR_DefaultAction("Haptic")]
    public SteamVR_Action_Vibration hapticAction;
    public float microSecondsDuration = 1000;
    public SteamVR_Input_Sources handType;


    // Use this for initialization
    void Start () {
        //trackedObject = GetComponent<SteamVR_TrackedObject>();
}
	
	// Update is called once per frame
	void Update () {
        // device = SteamVR_Controller.Input((int)trackedObject.index);
        float seconds = (float)microSecondsDuration / 1000000f;
        hapticAction.Execute(0, seconds, 1f / seconds, 1, handType);
    }

}
