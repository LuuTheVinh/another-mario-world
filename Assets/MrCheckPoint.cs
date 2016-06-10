using UnityEngine;
using System.Collections;

public class MrCheckPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        if (tag == "Player")
        {
            collider.gameObject.GetComponent<Mario>().CheckPoint = this.gameObject;
        }
    }
}
