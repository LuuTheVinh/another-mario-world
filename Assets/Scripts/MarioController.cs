using UnityEngine;
using System.Collections;

public class MarioController : MonoBehaviour {

    public float DashSpeed = 1;
    public float HoldJumpTime = 0.25f;

    private Animator _animator;
    private MarioMovement _marioMovement;
    private Rigidbody2D _rigidbody2D;
    
    private float _timer = 0;

    private bool _canJump = true;

    // Use this for initialization
    void Start () {
        _animator = this.GetComponent<Animator>();
        _marioMovement = this.GetComponent<MarioMovement>();
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate() {

        var h = Input.GetAxis("Horizontal");

        if (h != 0)
        {
            if (!_animator.GetBool("isRunning"))
            {
                _animator.SetBool("isRunning", true);
            }

            if (h > 0)
            {
                if (_rigidbody2D.velocity.x < -DashSpeed && !_animator.GetBool("isJumping"))
                {
                    _animator.SetTrigger("dash");
                }

                _marioMovement.GotoRight();
            }
            else if (h < 0)
            {
                if (_rigidbody2D.velocity.x > DashSpeed && !_animator.GetBool("isJumping"))
                {
                    _animator.SetTrigger("dash");
                }

                _marioMovement.GotoLeft();
            }
        }
        else
        {
            if (_animator.GetBool("isRunning"))
                _animator.SetBool("isRunning", false);

            _animator.ResetTrigger("dash");
        }

        // nhảy
        if (!_animator.GetBool("isJumping"))
        {
            if (Input.GetButton("Jump"))
            {
                _timer += Time.deltaTime;

                if (_timer >= HoldJumpTime && _canJump)
                {
                    _animator.SetTrigger("Jump");
                    _animator.SetBool("isJumping", true);
                    _animator.ResetTrigger("dash");
                    _marioMovement.Jump(true);
                    _canJump = false;
                }
            }
            else if (Input.GetButtonUp("Jump") && _canJump)
            {
                _animator.SetTrigger("Jump");
                _animator.SetBool("isJumping", true);
                _animator.ResetTrigger("dash");
                _marioMovement.Jump();
            }
        }
        else
        {
            _timer = 0;
        }

        if (Input.GetButtonUp("Jump") && !_canJump)
        {
            _canJump = true;
        }

        Debug.Log("Timer: " + _timer);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject.tag == "Ground")
        //{
        //    _animator.SetBool("isJumping", false);
        //}
    }
}
