using UnityEngine;
using System.Collections;

public class FightingFrogContainer : MonoBehaviour {

    enum eDirection { Left, Right };

    public float _loopJumpTime;
    
    private float _countingTimeJump;
    private GameObject _hand;
    private GameObject _body;
    private eDirection _direction;
    private GameObject _mario;

    private bool _isSleep;
    private Renderer _renderer;

	// Use this for initialization
	void Start () {
        Transform[] trans = this.gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform tran in trans)
        {
            if (tran.gameObject.name == "hand")
                this._hand = tran.gameObject;
            if (tran.gameObject.name == "body")
                this._body = tran.gameObject;
        }
        _mario = GameObject.FindGameObjectWithTag("Player");
        _isSleep = true;
        _renderer = this.GetComponentInChildren<Renderer>();

	}

    void FixedUpdate()
    {
        checkWakeUp();
        if (_isSleep == true)
            return;
        _direction = checkDirection();
        if (_direction == eDirection.Left)
            _body.GetComponent<Animator>().SetBool("left", true);
        else
            _body.GetComponent<Animator>().SetBool("left", false);
    }

	// Update is called once per frame
	void Update () {
        if (_isSleep == true)
            return;
        jump();
	}

    private void checkWakeUp()
    {
        if (_isSleep == true && _renderer.isVisible == true)
            _isSleep = false;

    }

    private void jump()
    {
        _countingTimeJump += Time.deltaTime;
        if (_countingTimeJump >= _loopJumpTime)
        {
            _countingTimeJump -= _loopJumpTime;


            _body.GetComponent<Animator>().SetTrigger("jump");
            if (_hand != null)
                _hand.GetComponent<Animator>().SetTrigger("jump");

            if (_direction == eDirection.Left)
            {
                _body.GetComponent<Animator>().SetBool("left", true);
                if (_hand != null)
                    _hand.GetComponent<Animator>().SetBool("left", true);
            }
            else
            {
                _body.GetComponent<Animator>().SetBool("left", false);
                if (_hand != null)
                    _hand.GetComponent<Animator>().SetBool("left", false);
            }
        }
    }

    private eDirection checkDirection()
    {
        Vector3 distance = this.transform.position - _mario.transform.position;
        if (distance.x >= 0)
            return eDirection.Left;
        else
            return eDirection.Right;
    }
}
