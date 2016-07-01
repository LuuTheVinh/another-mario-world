using UnityEngine;
using System.Collections;

public class Explosion_Area : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.childCount == 0)
            Destroy(this.gameObject);
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;

        if (tag == "Player")
        {
            collider.gameObject.GetComponent<Mario>().GotHit();
        }
    }
}
