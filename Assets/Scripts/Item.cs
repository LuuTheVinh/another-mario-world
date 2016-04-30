using UnityEngine;
using System.Collections;

// created by Ho Hoang Tung
public class Item : MonoBehaviour{

    
    public enum ItemType { MUSHROOM, FIREFLOWER, AMAZING_STAR };

    public enum AppearMode { LEFT, RIGHT, INSTANT}

    private SpriteRenderer _spriteRenderer;
    private IMovement _imovement;
    private Rigidbody2D _rigidbody2d;

    public ItemType _type = ItemType.MUSHROOM;
    public Sprite[] _sprites;
    public float _speedX;
    public float _speedY;
    public float _floatingUp;
    public float _deltaUp;
    public float _delayTime;
    private float _height;

    public bool IsRunable { get; set; }
    public AppearMode _appearMode { get; set; }

	// Use this for initialization
	void Start () {

        _rigidbody2d = GetComponent<Rigidbody2D>();

        // Chọn sprite dựa theo enum chọn từ inspector
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _sprites[System.Convert.ToInt32(_type)];

        // Delta Up chọn từ inspector xác định độ cao tối đa trồi lên của item.
        _height = this.transform.position.y + _deltaUp;

        // Note: chọn hướng di chuyển trước, chọn kiểu di chuyển sau.
        // Chọn hướng di chuyển.
        runDirection();

        // Chọn kiểu di chuyển.
        _imovement = initMovement();


	}
	
	// Update is called once per frame
	void Update () {
        // Runable là flag để xác định item có bắt đầu chạy hay không.
        if (IsRunable == false)
        {
            // Kiểm tra độ cao đã tròi lên nếu quá giới hạn đặt ra trước thì không trồi lên nữa
            if (this.transform.position.y <= _height){
                this.gameObject.transform.position += new Vector3(0, _floatingUp, 0);
            }
            else{
                // Giảm Delay Time từ số đặt trước từ inspector đến 0.
                // Nếu bé hơn 0 rồi thì bắt đầu Run
                if (_delayTime > 0){
                    _delayTime -= Time.deltaTime;
                }
                else{
                    IsRunable = true;
                }
            }
        }
        if (IsRunable == true)
        {
            if (_rigidbody2d.isKinematic == true)
                _rigidbody2d.isKinematic = false;

            _imovement.Movement(this.gameObject);
        }

	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        string name = collision.gameObject.name;
        if (tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    private IMovement initMovement()
    {
        switch (_type)
        {
            case ItemType.MUSHROOM:
                return new LinearMovement(this._speedX, this._speedY);
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
        switch (_appearMode)
        {
            case AppearMode.LEFT:
                this._speedX = - Mathf.Abs(_speedX);
                break;
            case AppearMode.RIGHT:
                this._speedX = Mathf.Abs(_speedX);
                break;
            case AppearMode.INSTANT:
                break;
            default:
                break;
        }
    }
}
