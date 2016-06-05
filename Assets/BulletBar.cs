using UnityEngine;
using System.Collections;

public class BulletBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateBar(float ratio)
    {
        this.gameObject.transform.localScale = new Vector3(ratio, 1, 1);
    }
}
