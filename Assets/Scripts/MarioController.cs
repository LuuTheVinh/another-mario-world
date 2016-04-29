using UnityEngine;
using System.Collections;

public class MarioController : MonoBehaviour {

    private Animator _animator;
    private MarioMovement _marioMovement;

    private float _currentSpeed = 0;

    // Use this for initialization
    void Start () {
        _animator = this.GetComponent<Animator>();
        _marioMovement = this.GetComponent<MarioMovement>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (!_animator.GetBool("isRunning"))
                _animator.SetBool("isRunning", true);

            if (Input.GetAxis("Horizontal") > 0)
            {
                //Debug.Log(Input.GetAxis("Horizontal"));
                _marioMovement.GotoRight();
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                _marioMovement.GotoLeft();
            }
        }
        else
        {
            if (_animator.GetBool("isRunning"))
                _animator.SetBool("isRunning", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            _animator.SetBool("isJumping", true);
            _marioMovement.Jump();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            _animator.SetBool("isJumping", false);
        }
    }
}
