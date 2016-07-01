using UnityEngine;
using System.Collections;

public class FallingObject : MonoBehaviour {

    public float _delayTime;
    private float _tempTime;
    private bool _isTouch;
	// Use this for initialization
	void Start () {
        _tempTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (_isTouch == true)
        {
            _tempTime += Time.deltaTime;
            if (_tempTime >= _delayTime)
            {
                GetComponentInParent<Rigidbody2D>().isKinematic = false;

                GetComponent<Animator>().SetTrigger("fall");

            }
        }
	}

    void OnTriggerStay2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        if (tag == "Player")
        {
            _isTouch = true;
            GetComponent<Animator>().SetTrigger("swing");

        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        string name = collider.gameObject.name;
        if (name.Contains("spike"))
        {
            Destroy(this.gameObject);
        }
    }
}
