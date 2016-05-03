using UnityEngine;
using System.Collections;

public class Mario : MonoBehaviour {
    
    // Tung: Hiện tại tui kiểm tra status >= BIG thì cho phép đập đá
    // nên nếu có thêm status nào mà không đập đá được thì cho nó nhỏ hơn BIG nhé
    public enum eMarioStatus
    {
        SMALL = 0,
        BIG = 1,
        WHITE = 2,
        RACOON = 3,
    }

    public float JumpHeight = 2;    // nhảy bt
    public float HoldJumpHeight = 4; // nhấn giữ để nhảy
    public float MovingForce = 20.0f;
    public float MaxSpeed = 3.0f;

    [HideInInspector] public float JumpForce = 200.0f;
    [HideInInspector] public float JumpMaxForce = 200.0f;
    
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;

    // Use this for initialization
    void Start () {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _boxCollider2D = this.GetComponent<BoxCollider2D>();
        _rigidbody2D = this.GetComponent<Rigidbody2D>();

        // lực nhảy = căn (2 * g * scale (tại thằng này gravity gấp 2) * độ cao) + khối lượng (lực kéo xuống)
        JumpForce = Mathf.Sqrt(2 * Physics.gravity.magnitude * _rigidbody2D.gravityScale * JumpHeight) + _rigidbody2D.mass;
        
        JumpMaxForce = Mathf.Sqrt(2 * Physics.gravity.magnitude * _rigidbody2D.gravityScale * HoldJumpHeight) + _rigidbody2D.mass;
    }
	
	// Update is called once per frame
	void Update () {

        //test, chỉnh lại size collider theo sprite
        Vector2 spriteSize = _spriteRenderer.sprite.bounds.size;
        if(_boxCollider2D.size != spriteSize)
        {
            _boxCollider2D.size = spriteSize;
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        string name = collision.gameObject.name;
        if (tag == "Item")
        {
            //Item item = collision.gameObject.GetComponents(typeof(Item))[0] as Item;
            //updateStatusByItem(item);
        }
    }

    //private void updateStatusByItem(Item item)
    //{
    //    if (item == null)
    //        return;
    //    // câp nhật trạng thái dựa trên type của item
    //    switch (item._type)
    //    {
    //        case Item.ItemType.Mushroom:
    //            this.GetComponent<Animator>().SetInteger("status", (int)eMarioStatus.BIG);
    //            break;
    //        case Item.ItemType.FireFlower:
    //            break;
    //        case Item.ItemType.Amazing_Star:
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
