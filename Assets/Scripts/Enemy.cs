using UnityEngine;
using System.Collections;


public abstract class Enemy: MonoBehaviour {
    public enum eMoveDirection { LEFT, RIGHT, UP, DOWN, NONE }
    public enum eStatus { Normal, Die, Hit}
    [HideInInspector]
    public Animator _aniamtor;

    protected IMovement _imovement;
    protected IHitByPlayer _hitbyplayer;

    public eMoveDirection _moveDirection;
    public bool _canHitByShell;
    public Vector3 _speed;
    public bool _isSmart;
    protected virtual void Start()
    {
        _aniamtor = GetComponent<Animator>();

        // Chọn hướng di chuyển.
        runDirection();
    }

    protected virtual void Update()
    {
        if (this.GetComponent<Renderer>().isVisible == false && _aniamtor.GetInteger("status") == (int)eStatus.Hit)
            _aniamtor.SetTrigger("outofscreen");
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (this._aniamtor.GetInteger("status") == 2)
            return;
        string name = collision.gameObject.name;
        string tag = collision.gameObject.tag;
        if (tag == "Player")
            checkHitByPlayer(collision);
        if (tag == "Ground")
            checkWithGround(collision);
        if (tag == "Enemy")
            checkWithEnemy(collision);
        if (name == "block")
            checkWithBlock(collision);
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
        Vector3 distance = (this.transform.position - col.gameObject.transform.position);
        if (distance.y < 0 && Mathf.Abs(distance.x) < 0.65)
        {
            _hitbyplayer.Hit(this);
        }
        else
        {
            // Mario die.
        }
    }

    private void checkWithBlock(Collision2D collision)
    {
        Animator anim = collision.gameObject.GetComponent<Animator>();
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Normal") == false)
        {
            this._aniamtor.SetInteger("status", (int)eStatus.Hit);

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
