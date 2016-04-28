using UnityEngine;
using System.Collections;

public class Mario : MonoBehaviour {

    public float Velocity = 3.0f;
    public float JumpSpeed = 5.0f;


    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
 
	// Use this for initialization
	void Start () {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _boxCollider2D = this.GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {

        //test, chỉnh lại size collider theo sprite
        Vector2 spriteSize = _spriteRenderer.sprite.bounds.size;
        if(_boxCollider2D.size != spriteSize)
        {
            _boxCollider2D.size = spriteSize;
            _boxCollider2D.offset = new Vector2(spriteSize.x / 2, 0);
        }
	}
}
