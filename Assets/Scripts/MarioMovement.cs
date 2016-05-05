using UnityEngine;
using System.Collections;

public class MarioMovement : MonoBehaviour {
    
    private Mario _mario;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    private int _direction = 1;

    // Use this for initialization
    void Start () {
        _mario = this.GetComponent<Mario>();
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        float h = Input.GetAxis("Horizontal");
        // Move();

        _rigidbody2D.AddForce(Vector2.right * _mario.MovingForce * h);


        if (Mathf.Abs(_rigidbody2D.velocity.x) > _mario.MaxSpeed)
        {
            _rigidbody2D.velocity = new Vector2(Mathf.Sign(_rigidbody2D.velocity.x) * _mario.MaxSpeed, _rigidbody2D.velocity.y);
        }

        // lật hình mario
        var sign = Mathf.Sign(_rigidbody2D.velocity.x);

        if (h > 0 && sign > 0 && this.transform.localScale.x != -1)
        {
            //_spriteRenderer.flipX = true;
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h < 0 && sign < 0 && this.transform.localScale.x != 1)
        {
            // _spriteRenderer.flipX = false;
            this.transform.localScale = new Vector3(1, 1, 1);
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

    public void Jump(bool max = false)
    {
        if(!max)
            _rigidbody2D.AddForce(Vector2.up * _mario.JumpForce, ForceMode2D.Impulse);
        else
            _rigidbody2D.AddForce(Vector2.up * _mario.JumpMaxForce, ForceMode2D.Impulse);
    }

    public void Dash()
    {
        //_rigidbody2D.AddForce(Vector2.right * 100 * (_spriteRenderer.flipX == true ? 1 : -1));
    }

    //private float Speed = 0f;
    //private float MaxSpeed = 3f;
    //private float Acceleration = 5.0f;
    //private float Deceleration = 5.0f;

    //void Move()
    //{
    //    if((Input.GetKey("left")) && (Speed < MaxSpeed))
    //        Speed = Speed - Acceleration * Time.deltaTime;
    //    else if ((Input.GetKey("right")) && (Speed > -MaxSpeed))
    //        Speed = Speed + Acceleration * Time.deltaTime;
    //    else
    //    {
    //        if (Speed > Deceleration * Time.deltaTime)
    //            Speed = Speed - Deceleration   * Time.deltaTime;
    //        else if (Speed < -Deceleration * Time.deltaTime)
    //            Speed = Speed + Deceleration * Time.deltaTime;
    //        else
    //            Speed = 0;
    //    }

    //    transform.Translate(new Vector2(Speed * Time.deltaTime, 0));
    //}
}
