using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashOnFloorCollide : MonoBehaviour
{
    public AmbisonicsSystem ambSys;
    public string file;
    public string id;
    public float volume;

    // Start is called before the first frame update
    void Start()
    {
        ambSys.StartSystem();
        ambSys.Add(id, file, volume);
        Debug.Log("id is :" + id);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Wooden Plank") // col.gameObject.name.Substring(0,11) == "CratePrefab" || 
        {
            Debug.Log("success");
            ambSys.StartSound(id, false);
        }
    }
}
