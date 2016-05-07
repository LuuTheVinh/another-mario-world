using UnityEngine;
using System.Collections;

public class MarioController : MonoBehaviour {

    public float DashSpeed = 1;
    public float HoldJumpTime = 0.25f;

    private Animator _animator;
    private MarioMovement _marioMovement;
    private Rigidbody2D _rigidbody2D;
    
    private float _timer = 0;   // đếm thời gian giữ nhảy

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

                if (_timer >= HoldJumpTime)
                {
                    JumpWithAnimate(true);  // nhảy tối đa
                }
            }
            else if (Input.GetButtonUp("Jump"))
            {
                JumpWithAnimate(false);     // nhảy bình thường
            }
        }
        else
            _timer = 0;

        if (Input.GetButtonUp("Jump") && !_canJump)
        {
            _canJump = true;
        }

        //Debug.Log("Timer: " + _timer);

        // đá
        if(Input.GetButtonDown("Attack"))
        {
            // Tung
            kick();
        }
    }
    
    public void JumpWithAnimate(bool max)
    {
        if (!_canJump)
            return;

        _animator.SetBool("isJumping", true);
        _animator.SetTrigger("Jump");
        _animator.ResetTrigger("dash");

        _canJump = false;
        _marioMovement.Jump(max);
        _timer = 0;
    }

    public void kick()
    {
        // Tung
        _animator.SetTrigger("kick");
    }
}
