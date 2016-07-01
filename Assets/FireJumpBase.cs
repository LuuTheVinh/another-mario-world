using UnityEngine;
using System.Collections;

public class FireJumpBase : MonoBehaviour {

    public GameObject _fire;
    public float _delayTime;

	// Use this for initialization
	void Start () {
        Invoke("fire", _delayTime);
	}
	
	// Update is called once per frame
	void Update () {
	    if (this.transform.childCount == 0)
            Invoke("fire", _delayTime);

	}

    void fire()
    {
        var fire = (GameObject) Instantiate(_fire,
            this.transform.position + Vector3.up + new Vector3(0,0.2f, 0),
            this.transform.rotation);
        fire.transform.parent = this.gameObject.transform;
        CancelInvoke();
    }
}
