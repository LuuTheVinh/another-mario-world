using UnityEngine;
using System.Collections;

public class Mario : MonoBehaviour {

    public float Velocity = 3.0f;
    public float JumpSpeed = 5.0f;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
 
	// Use this for initialization
	void Start () {
        _animator = this.GetComponent<Animator>();
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
        _boxCollider2D = this.GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {

        //test, chỉnh lại size collider theo sprite
        Vector2 spriteSize = this.GetComponent<SpriteRenderer>().sprite.bounds.size;
        if(_boxCollider2D.size != spriteSize)
        {
            _boxCollider2D.size = spriteSize;
            _boxCollider2D.offset = new Vector2(spriteSize.x / 2, 0);
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (!_animator.GetBool("isRunning"))
                _animator.SetBool("isRunning", true);

            if (Input.GetAxis("Horizontal") > 0)
            {
                this.transform.Translate(Vector2.right * Velocity * Time.deltaTime);
                _spriteRenderer.flipX = true;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                this.transform.Translate(Vector2.left * Velocity * Time.deltaTime);
                _spriteRenderer.flipX = false;
            }
        }
        else
        {
            if (_animator.GetBool("isRunning"))
                _animator.SetBool("isRunning", false);
        }

	    if(Input.GetButtonDown("Jump"))
        {
            _rigidbody2D.AddForce(Vector2.up * JumpSpeed);
            _animator.SetBool("isJumping", true);
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
