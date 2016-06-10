using UnityEngine;
using System.Collections;

public class FlatSwingObjectCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        string name = collision.gameObject.name;
        if (tag == "Player")
        {

            this.transform.parent.GetComponent<Swing_Object>().run();
            this.transform.parent.GetComponent<Swing_Object>().setJoin(collision.gameObject);
            //collision.gameObject.GetComponent<Rigidbody2D>().velocity = this.transform.parent.GetComponent<Swing_Object>()._speed;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("trgiger");
        if (collider.gameObject.name == "head" || collider.name == "tail")
        {
            this.transform.parent.GetComponent<Swing_Object>().back();

        }
    }
    void OnTriggerUpdate2D(Collider2D collider)
    {
        if (collider.gameObject.name == "head" || collider.name == "tail")
        {
            //this.transform.parent.GetComponent<Swing_Object>().stop();

        }
    }

    // vinh 
    // gán cho player đi theo
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.parent = this.transform;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.parent = null;
        }
    }
}
