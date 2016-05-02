using UnityEngine;
using System.Collections;


public abstract class Enemy: MonoBehaviour {
    public enum eMoveDirection { LEFT, RIGHT, UP, DOWN, NONE }

    [HideInInspector]
    public Animator _aniamtor;

    protected IMovement _imovement;
    protected IHitByPlayer _hitbyplayer;

    public eMoveDirection _moveDirection;

    public Vector3 _speed;

    protected virtual void Start()
    {
        _aniamtor = GetComponent<Animator>();

        // Chọn hướng di chuyển.
        runDirection();
    }

    protected virtual void Update()
    {

    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Player")
            checkHitByPlayer(collision);
        if (tag == "Ground")
            checkWithGround(collision);
        if (tag == "Enemy")
            checkWithEnemy(collision);
    }

    public virtual void SetSpeed(Vector3 s)
    {
        this._speed = s;
        _imovement = new LinearMovement(_speed.x, _speed.y, _speed.z);

    }

    // Nếu đụng vật khác thì đi ngược lại
    protected virtual void checkWithGround(Collision2D collision)
    {
        if (collision.collider is EdgeCollider2D)
            return;
        float top = collision.collider.bounds.max.y;
        if (top - this.GetComponent<Collider2D>().bounds.min.y > 0.5)
        {
            this.back();
        }
    }

    // di chuyển ngược lại
    public virtual void back()
    {
        SetSpeed(new Vector3(-_speed.x, _speed.y, _speed.z));
    }

    protected virtual void checkWithEnemy(Collision2D collision)
    {
        back();
    }

    protected void checkHitByPlayer(Collision2D col)
    {
        //if (_aniamtor.GetCurrentAnimatorStateInfo(0).IsName("GoompaNormal") == false)
        //    return;

        // Nếu goompa đang trong trạng thái normal và va chạm với player
        // thì kiểm tra hướng va chạm.
        Vector3 distance = (this.transform.position - col.gameObject.transform.position).normalized;

        if (distance.y < 0 && Mathf.Abs(distance.x) < 0.5)
        {
            _hitbyplayer.Hit(this);
        }
        else
        {
            // Mario die.
        }
    }

    protected void runDirection()
    {

        switch (_moveDirection)
        {
            case eMoveDirection.LEFT:
                _speed.x = -Mathf.Abs(_speed.x);
                break;
            case eMoveDirection.RIGHT:
                _speed.x = Mathf.Abs(_speed.x);
                break;
            case eMoveDirection.UP:
                _speed.y = Mathf.Abs(_speed.y);
                break;
            case eMoveDirection.DOWN:
                _speed.y = -Mathf.Abs(_speed.y);
                break;
        }
    }
}
