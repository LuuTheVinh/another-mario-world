using UnityEngine;
using System.Collections;

public class Swing_Object : MonoBehaviour {

    public Vector2 _speed;
    private bool _isSleep;

    Rigidbody2D _rigidbody;
    Renderer _renderer;
	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<Renderer>();
        //_rigidbody.velocity = _speed;

        _isSleep = true;
	}
	
	// Update is called once per frame
	void Update () {

        checkWakeUp();
        //if (_isSleep == false)
        //    Invoke("run", 2);
	}

    void run()
    {
        _rigidbody.velocity = _speed;
        Debug.Log("run");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        string name = collision.gameObject.name;
        if (tag == "Player")
        {
            _isSleep = false;
        }
        if (name == "head" || name == "tail")
            back();
    }

    private void checkWakeUp()
    {
        if (_isSleep == true && _renderer.isVisible == true)
        {
            _isSleep = false;
            Invoke("run", 2);
        }

    }
    private void back()
    {
        _rigidbody.velocity = new Vector2( -_rigidbody.velocity.x, _rigidbody.velocity.y);
        Debug.Log("back");
    }
}
