using UnityEngine;
using System.Collections;

public class Swing_Object : MonoBehaviour {

    public Vector2 _speed;
    private bool _isSleep;

    Rigidbody2D _rigidbody;
    Renderer _renderer;

    public bool _isRun;
    private GameObject _player;
	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<Renderer>();
        //_rigidbody.velocity = _speed;

        _isSleep = true;
	}
	
	// Update is called once per frame
	void Update () {

        //checkWakeUp();
        if (_isRun)
        {
            this.transform.position = new Vector3(
                this.transform.position.x + _speed.x,
                this.transform.position.y + _speed.y,
                this.transform.position.z);
            if (_player != null)
            {
                if (_player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
                    _player.transform.position = new Vector3(
                        _player.transform.position.x + _speed.x,
                        _player.transform.position.y + _speed.y,
                        _player.transform.position.z);
                if (_player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
                {
                    _player = null;
                    return;
                }
                var playerBound = _player.GetComponent<BoxCollider2D>().bounds;
                var thisBound = this.GetComponent<BoxCollider2D>().bounds;
                if (playerBound.max.x < thisBound.min.x || playerBound.min.x > thisBound.max.x)
                {
                    _player = null;
                }
            }
        }
        
	}

    public void run()
    {
        //_rigidbody.velocity = _speed;
        _isRun = true;
        //Debug.Log("run");
    }

    public void setJoin(GameObject go)
    {
        _player = go;
    }

    public void stop()
    {
        //_rigidbody.velocity = Vector2.zero;
        _isRun = false;
        _speed = Vector2.zero;
        //Debug.Log("Stop");  

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Col");
        string tag = collision.gameObject.tag;
        string name = collision.gameObject.name;
        //if (tag == "Player")
        //{

        //    //Invoke("run", 2);
        //    stop();

        //}
        if (name == "head" || name == "tail")
            stop();
    }

    private void checkWakeUp()
    {
        if (_isSleep == true && _renderer.isVisible == true)
        {
            _isSleep = false;
            Invoke("run", 2);
        }

    }
    public void back()
    {
        _speed = new Vector2(- _speed.x,- _speed.y);
        //Debug.Log("back");
    }
}
