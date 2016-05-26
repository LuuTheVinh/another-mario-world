using UnityEngine;
using System.Collections;

public class MarioController : MonoBehaviour {

    public float DashSpeed = 1;
    public float HoldJumpTime = 0.25f;
    public float HoldJumpForce = 300f;
    public LayerMask WhatIsGround;
    
    public Transform groundCheck;
    private float _groundedRadius = 0.1f;

    private Animator _animator;
    private MarioMovement _marioMovement;
    private Rigidbody2D _rigidbody2D;
    
    private float _timer = 0;   // đếm thời gian giữ nhảy
    private bool _canHoldJump = false;
    private bool _grounded;
    private bool _canJump = false;

    // Use this for initialization
    void Start () {
        _animator = this.GetComponent<Animator>();
        _marioMovement = this.GetComponent<MarioMovement>();
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // check ground
        _grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, _groundedRadius, WhatIsGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                _grounded = true;
        }

        if (_grounded)
        {
            Grounded();
        }

        // nhảy
        if (Input.GetButtonDown("Jump") && _grounded)
        {
            _canJump = true;
        }

        if (Input.GetButton("Jump") && _canHoldJump)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _timer = 0;
        }
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

        //Debug.Log("Timer: " + _timer);

        // nhảy
        if (_canJump)
        {
            this.JumpWithAnimate(false);
            _canHoldJump = true;
        }

        if (_canHoldJump && _timer > HoldJumpTime && _rigidbody2D.velocity.y > 0)
        {
            _rigidbody2D.AddForce(Vector2.up * HoldJumpForce, ForceMode2D.Force);
            _canHoldJump = false;
            _timer = 0;
        }

        // đá
        if (Input.GetButtonDown("Attack"))
        {
            // Tung
            kick();
        }

        // ngồi
        if(_animator.GetInteger("status") != 0 && Input.GetKey("down"))
        {
            if (!_animator.GetBool("isSitting"))
            {
                _animator.SetBool("isSitting", true);
            }
        }
        else if(_animator.GetBool("isSitting"))
        {
            _animator.SetBool("isSitting", false);
        }

    }
    
    public void JumpWithAnimate(bool max)
    {
        if (_animator.GetBool("isJumping"))
        {
            return;
        }

        _canJump = false;
        _animator.SetBool("isJumping", true);
        _animator.SetTrigger("Jump");
        _animator.ResetTrigger("dash");
        
        _marioMovement.Jump(max);
    }

    public void kick()
    {
        // Tung
        _animator.SetTrigger("kick");
    }

    public void Grounded()
    {
        _animator.SetBool("isJumping", false);
    }
}
