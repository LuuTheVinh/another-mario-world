using UnityEngine;
using System.Collections;

public class Exploision : MonoBehaviour {

    public float TimeDelay = 0.25f;

	// Use this for initialization
	void Start () {
    }

    void Awake()
    {
        Destroy(this.gameObject, TimeDelay);
    }
    
    // Update is called once per frame
    void Update () {
	
	}
}
