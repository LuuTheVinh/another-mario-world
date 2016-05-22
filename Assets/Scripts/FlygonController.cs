using UnityEngine;
using System.Collections;

public class FlygonController : MonoBehaviour {

    public enum eAxis { Horizontal, Vertical };
    public float speed;
    public eAxis _axis;

    [HideInInspector] public GameObject _player;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Hey man im here");
	}

    void FixedUpdate()
    {
        if (_player == null)
            return;
        float h = Input.GetAxis("Horizontal");
        if (h > 0)
            moveright();
        if (h < 0)
            moveleft();
        //if (Input.GetButtonDown("Jump"))
        //{
        //    disconnectObject(_player);
        //}
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        string name = collider.gameObject.name;
        if (name == "tail"){
            _player.GetComponent<MarioController>().JumpWithAnimate(false);
            disconnectObject(_player);
            Destroy(this.transform.parent.transform.parent.gameObject);

        }
        if (name == "head")
        {
            _player.GetComponent<MarioController>().JumpWithAnimate(false);
            disconnectObject(_player);
            //Destroy(this.transform.parent.transform.parent.gameObject);
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void moveleft()
    {
        speed = -Mathf.Abs(speed);
        this.GetComponent<Animator>().SetBool("left", true);
        if (_axis == eAxis.Horizontal){
            Vector3 p = this.transform.parent.position;
            this.transform.parent.position = p + new Vector3(speed * Time.deltaTime * 1000, 0.0f, 0.0f );
        }
        if (_axis == eAxis.Vertical)
        {
            Vector3 p = this.transform.parent.position;
            this.transform.parent.position = p + new Vector3(0.0f, speed * Time.deltaTime * 1000, 0.0f);
        }

    }

    private void moveright()
    {
        speed = Mathf.Abs(speed);
        this.GetComponent<Animator>().SetBool("left", false);

        if (_axis == eAxis.Horizontal)
        {
            Vector3 p = this.transform.parent.position;
            this.transform.parent.position = p + new Vector3(speed * Time.deltaTime * 1000, 0.0f, 0.0f);
        }
        if (_axis == eAxis.Vertical)
        {
            Vector3 p = this.transform.parent.position;
            this.transform.parent.position = p + new Vector3(0.0f, speed * Time.deltaTime * 1000, 0.0f);
        }
    }

    private void disconnectObject(GameObject obj)
    {
        //obj.GetComponent<SpriteRenderer>().sortingOrder = 1;
        Vector3 p = this.transform.parent.transform.position;
        this.transform.parent.transform.position = new Vector3(p.x, p.y, 1.0f);
        obj.GetComponent<MarioController>().enabled = true;
        this.GetComponent<HingeJoint2D>().connectedBody = null;
        this.GetComponent<HingeJoint2D>().enabled = false;
        _player = null;
    }
}
