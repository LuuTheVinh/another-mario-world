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
        Enemy enemy = obj.GetComponent<Enemy>();
        if (enemy._isSmart && enemy._aniamtor.GetInteger("status") != (int)Troopa.eStatus.SpeedShell)
        {
            enemy.back();
        }
    }
}
