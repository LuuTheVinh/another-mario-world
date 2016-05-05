using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

    [HideInInspector] public bool IsAttack = false;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

	// Use this for initialization
	void Start () {
        _animator = this.GetComponentInParent<Animator>();
        _spriteRenderer = this.GetComponentInParent<SpriteRenderer>();
        _boxCollider = this.GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        _boxCollider.offset = new Vector2(_boxCollider.offset.x, -_spriteRenderer.sprite.bounds.size.y / 2);
    }

    void OnTriggerEnter2D()
    {
        _animator.SetBool("isJumping", false);

        //nếu enemy tấn công được
        // if (...)
            IsAttack = true;
        // else
    }

    void OnTriggerExit2D()
    {
        IsAttack = false;
    }
}
