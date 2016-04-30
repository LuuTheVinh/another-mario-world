using UnityEngine;
using System.Collections;

public class MarioMovement : MonoBehaviour {
    
    private Mario _mario;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    // Use this for initialization
    void Start () {
        _mario = this.GetComponent<Mario>();
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        float h = Input.GetAxis("Horizontal");

        _rigidbody2D.AddForce(Vector2.right * _mario.MovingForce * h);
        
        if (Mathf.Abs(_rigidbody2D.velocity.x) > _mario.MaxSpeed)
        {
            _rigidbody2D.velocity = new Vector2(Mathf.Sign(_rigidbody2D.velocity.x) * _mario.MaxSpeed, _rigidbody2D.velocity.y);
        }

        // lật hình mario
        var sign = Mathf.Sign(_rigidbody2D.velocity.x);

        if (h > 0 && sign > 0 && !_spriteRenderer.flipX)
        {
            _spriteRenderer.flipX = true;
        }
        else if (h < 0 && sign < 0 && _spriteRenderer.flipX)
        {
            _spriteRenderer.flipX = false;
        }
    }

    public void GotoLeft()
    {
        //_spriteRenderer.flipX = false;
    }

    public void GotoRight()
    {
        //_spriteRenderer.flipX = true;
    }

    public void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _mario.JumpForce);
    }

    public void Dash()
    {
        //_rigidbody2D.AddForce(Vector2.right * 100 * (_spriteRenderer.flipX == true ? 1 : -1));
    }
}
