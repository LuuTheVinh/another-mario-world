using UnityEngine;
using System.Collections;

public class MarioMovement : MonoBehaviour {
    
    private Mario _mario;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    
    [HideInInspector] public bool CollidingSide = false;
    private int _direction = 0;

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

        // nếu mà ko chạm 2 bên hoặc chạm 2 bên mà đi ngược lại thì đi được
        if ((_direction == 0 || _direction * h < 0) && !this.GetComponent<Animator>().GetBool("isSitting"))
        {
            _rigidbody2D.AddForce(Vector2.right * _mario.MovingForce * h);
            if (Mathf.Abs(_rigidbody2D.velocity.x) > _mario.MaxSpeed)
            {
                _rigidbody2D.velocity = new Vector2(Mathf.Sign(_rigidbody2D.velocity.x) * _mario.MaxSpeed, _rigidbody2D.velocity.y);
            }
        }
        else if (this.transform.localScale.x * h > 0)
        {
            CollidingSide = false;
        }

        // lật hình mario
        var sign = Mathf.Sign(_rigidbody2D.velocity.x);

        if (h > 0 && sign > 0 && this.transform.localScale.x != -1)
        {
            //_spriteRenderer.flipX = true;
            // phải
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h < 0 && sign < 0 && this.transform.localScale.x != 1)
        {
            // _spriteRenderer.flipX = false;
            // trái
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
    }

    void OnCollisionStay2D(Collision2D col)
    {
        var thisBounds = this.GetComponent<Collider2D>().bounds;

        // chạm top / bot
        if (thisBounds.min.y > col.collider.bounds.max.y || thisBounds.max.y < col.collider.bounds.min.y)
        {
            _direction = 0;
        }
        else
        {
            if (thisBounds.min.x < col.collider.bounds.center.x)
                _direction = 1;     // trái
            else
                _direction = -1;    // phải
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        _direction = 0;
    }

    public void GotoLeft()
    {
        //_spriteRenderer.flipX = false;
    }

    public void GotoRight()
    {
        //_spriteRenderer.flipX = true;
    }

    public void EnemyPushUp()
    {
        _rigidbody2D.AddForce(Vector2.up * Mario.PushUpForce, ForceMode2D.Impulse);
    }

    public void Jump(bool max = false)
    {
        if (!max)
            _rigidbody2D.AddForce(Vector2.up * _mario.JumpForce, ForceMode2D.Impulse);
        else
            _rigidbody2D.AddForce(Vector2.up * _mario.JumpMaxForce, ForceMode2D.Impulse);
    }

    public void Dash()
    {
        //_rigidbody2D.AddForce(Vector2.right * 100 * (_spriteRenderer.flipX == true ? 1 : -1));
    }
}
