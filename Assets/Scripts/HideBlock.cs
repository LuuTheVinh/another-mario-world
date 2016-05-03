using UnityEngine;
using System.Collections;

public class HideBlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject obj = collider.gameObject;
        string tag = obj.tag;
        if (tag != "Enemy")
            return;
        if (obj.GetComponent<Enemy>()._isSmart)
            obj.GetComponent<Enemy>().back();
    }
}
