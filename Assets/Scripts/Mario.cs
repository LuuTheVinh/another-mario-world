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

    public GameObject CheckPoint;

    [HideInInspector] public float JumpForce = 200.0f;
    [HideInInspector] public float JumpMaxForce = 300.0f;
    
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    [HideInInspector] public eMarioStatus Status;
    public static float PushUpForce;

    private float _protectTime = 0f;  // thời gian ko chết

    // Use this for initialization
    void Start () {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _boxCollider2D = this.GetComponent<BoxCollider2D>();
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
        _animator = this.GetComponent<Animator>();

        Status = eMarioStatus.SMALL;

        // lực nhảy = căn (2 * g * scale (tại thằng này gravity gấp 2) * độ cao) + khối lượng (lực kéo xuống)
        JumpForce = Mathf.Sqrt(2 * Physics.gravity.magnitude * _rigidbody2D.gravityScale * JumpHeight) + _rigidbody2D.mass + _rigidbody2D.drag;
        
        JumpMaxForce = Mathf.Sqrt(2 * Physics.gravity.magnitude * _rigidbody2D.gravityScale * HoldJumpHeight) + _rigidbody2D.mass + _rigidbody2D.drag;

        PushUpForce = Mathf.Sqrt(2 * Physics.gravity.magnitude * _rigidbody2D.gravityScale * 1.5f) + _rigidbody2D.mass + _rigidbody2D.drag;
    
    }
	
	// Update is called once per frame
	void Update () {

        if(Status != (eMarioStatus)_animator.GetInteger("status"))
        {
            Status = (eMarioStatus)_animator.GetInteger("status");
        }

        if(_protectTime > 0)
        {
            _protectTime -= Time.deltaTime;

            //Debug.Log("Protect in " + _protectTime);
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
    
    /// <summary>
    /// Mario đụng Enemy
    /// </summary>
    public void GotHit()
    {
        if (_protectTime > 0)
            return;
        
        switch (Status)
        {
            case eMarioStatus.SMALL:
                {
                    Die();
                    return;
                }
            case eMarioStatus.BIG:
                {
                    _animator.SetInteger("status", (int)eMarioStatus.SMALL);
                    break;
                }
            case eMarioStatus.WHITE:
                {
                    _animator.SetInteger("status", (int)eMarioStatus.BIG);
                    break;
                }
            case eMarioStatus.RACOON:
                {
                    _animator.SetInteger("status", (int)eMarioStatus.BIG);
                    break;
                }
            default:
                break;
        }

        _protectTime = 3.0f;
    }

    private void Die()
    {
        this.GetComponent<Animator>().SetBool("isDead", true);

        _rigidbody2D.velocity = new Vector2(0, 0);
        this.GetComponent<MarioMovement>().Jump();

        Invoke("returnCheckPoint", 3);
    }

    private void returnCheckPoint()
    {
        // test
        this.transform.position = CheckPoint.transform.position;

        this.GetComponent<Animator>().SetBool("isDead", false);
        _rigidbody2D.velocity = new Vector2(0, 0);

        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<Mario>().enabled = true;
        this.GetComponent<MarioController>().enabled = true;
        this.GetComponent<MarioMovement>().enabled = true;
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponent<CircleCollider2D>().enabled = true;
    }

    private void checkHoleDie()
    {
        // kiểm tra chết do rớt xuống hố.
        if (this.transform.position.y < GameObject.Find("/Controller").GetComponent<SceneController>()._botGame)
        { 
            // Die
        }
    }
}
