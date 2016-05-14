using UnityEngine;
using System.Collections;

public class Flygon : MonoBehaviour {

    public bool _isLeft;
    [HideInInspector] public GameObject _player;

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
            getPlayer(collider);
        }
    }

    private void getPlayer(Collider2D collider)
    {
        Rigidbody2D _rigidbody = collider.gameObject.GetComponent<Rigidbody2D>();
        if (_rigidbody.velocity.y >= 0)
            return;
        float left = collider.bounds.min.x;
        float right = collider.bounds.max.x;
        float thisleft = this.GetComponent<BoxCollider2D>().bounds.min.x;
        float thisright = this.GetComponent<BoxCollider2D>().bounds.max.x;
        if (left >= thisleft && right <= thisright)
        {
            Debug.Log("Hit");

          //  _player.GetComponent<Transform>().position = 

            _player = collider.gameObject;
            connectObject(_player);
        }

    }

    private void connectObject(GameObject obj)
    {
        //obj.GetComponent<SpriteRenderer>().sortingOrder = 0;
        Vector3 p = this.transform.parent.transform.position;
        this.transform.parent.transform.position = new Vector3(p.x, p.y, -1.0f);
        //obj.GetComponent<MarioController>().enabled = false;
        this.GetComponent<FlygonController>().enabled = true;
        this.GetComponent<FlygonController>()._player = obj;
        this.GetComponent<HingeJoint2D>().connectedBody = obj.GetComponent<Rigidbody2D>();
        this.GetComponent<HingeJoint2D>().enabled = true;

    }
    
}
