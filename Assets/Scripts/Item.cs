using UnityEngine;
using System.Collections;
using System;

// created by Ho Hoang Tung
public class Item : MonoBehaviour{

    
    public enum ItemType { MUSHROOM, FIREFLOWER, AMAZING_STAR, COIN };

    public enum AppearMode { LEFT, RIGHT, INSTANT}

    public static int[] IsUseAnimator = new int[]{-1, -1, -1, 1};

    private SpriteRenderer _spriteRenderer;
    private IMovement _imovement;
    private Rigidbody2D _rigidbody2d;
    private Animator _animator;

    public ItemType _type = ItemType.MUSHROOM;
    public Sprite[] _sprites;
    public Vector3 _speed;
    public float _floatingUp;
    public float _deltaUp;
    public float _delayTime;

    public bool IsRunable { get; set; }
    public AppearMode _appearMode { get; set; }

    private int type;
    private float _height;
    private float _delayNoneHit;
    // Use this for initialization
	void Start () {
        type = System.Convert.ToInt32(_type);

        _rigidbody2d = GetComponent<Rigidbody2D>();
        
        // Chọn sprite dựa theo enum chọn từ inspector
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _sprites[type];

        _animator = GetComponent<Animator>();

        checkeforEnableAnimator(type);


        // Delta Up chọn từ inspector xác định độ cao tối đa trồi lên của item.
        _height = this.transform.position.y + _deltaUp;

        // Note: chọn hướng di chuyển trước, chọn kiểu di chuyển sau.
        // Chọn hướng di chuyển.
        runDirection();

        // Chọn kiểu di chuyển.
        _imovement = initMovement();

        // delay lại không thôi vừa mơi chạm được cục gạch thì ăn item.
        // cho nữa giây sau mới ăn được.
        _delayNoneHit = 0.5f;
	}

    // Update is called once per frame
	void Update () {
        if (IsUseAnimator[type] == -1)
            customRun();
        _delayNoneHit -= Time.deltaTime;
 
	}

    private void customRun()
    {
        // Runable là flag để xác định item có bắt đầu chạy hay không.
        if (IsRunable == false)
        {
            // Kiểm tra độ cao đã tròi lên nếu quá giới hạn đặt ra trước thì không trồi lên nữa
            if (this.transform.position.y <= _height)
            {
                this.gameObject.transform.position += new Vector3(0, _floatingUp, 0);
            }
            else
            {
                // Giảm Delay Time từ số đặt trước từ inspector đến 0.
                // Nếu bé hơn 0 rồi thì bắt đầu Run
                if (_delayTime > 0)
                {
                    _delayTime -= Time.deltaTime;
                }
                else
                {
                    IsRunable = true;
                }
            }
        }
        if (IsRunable == true && this._appearMode != AppearMode.INSTANT)
        {
            if (_rigidbody2d.isKinematic == true)
                _rigidbody2d.isKinematic = false;

            _imovement.Movement(this.gameObject);

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_delayNoneHit > 0)
            return;
        if (this._type == ItemType.COIN)
            return;
        string tag = collision.gameObject.tag;
        string name = collision.gameObject.name;
        if (tag == "Player")
        {
            Destroy(this.gameObject);
        }
        if (tag == "Ground")
            checkWithGround(collision);
    }

    private void checkWithGround(Collision2D collision)
    {
        Vector3 distance = (this.transform.position - collision.gameObject.transform.position).normalized;
        if (distance.y < 0 && Mathf.Abs(distance.x) < 0.5)
            (_imovement as LinearMovement).Xspeed = -(_imovement as LinearMovement).Xspeed;
    }

    private IMovement initMovement()
    {
        switch (_type)
        {
            case ItemType.MUSHROOM:
                return new LinearMovement(_speed.x, _speed.y, _speed.z);
            case ItemType.FIREFLOWER:
                return null;
            case ItemType.AMAZING_STAR:
                return null;
            default:
                return null;

        }
    }

    private void runDirection()
    {
        // Những item không di chuyển
        switch (_type)
        {
            case ItemType.FIREFLOWER:
                _appearMode = AppearMode.INSTANT;
                break;
            default:
                break;
        }

        switch (_appearMode)
        {
            case AppearMode.LEFT:
                this._speed.x = - Mathf.Abs(_speed.x);
                break;
            case AppearMode.RIGHT:
                this._speed.x = Mathf.Abs(_speed.x);
                break;
            default:
                break;
        }
    }

    private void checkeforEnableAnimator(int type)
    {
        if (IsUseAnimator[type] == 1)
        {
            _animator.enabled = true;
            _animator.SetInteger("Status", System.Convert.ToInt32(_type));
        }
    }
	

}
