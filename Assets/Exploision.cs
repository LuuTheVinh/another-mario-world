using UnityEngine;
using System.Collections;

public class Exploision : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }

    void Awake()
    {
        Destroy(this.gameObject, 0.25f);
    }
    
    // Update is called once per frame
    void Update () {
	
	}
}
