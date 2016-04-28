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
	void Update () {
	
	}

    public void GotoLeft()
    {
        this.transform.Translate(Vector2.left * _mario.Velocity * Time.deltaTime);
        _spriteRenderer.flipX = false;
    }

    public void GotoRight()
    {
        this.transform.Translate(Vector2.right * _mario.Velocity * Time.deltaTime);
        _spriteRenderer.flipX = true;
    }

    public void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _mario.JumpSpeed);
    }
}
