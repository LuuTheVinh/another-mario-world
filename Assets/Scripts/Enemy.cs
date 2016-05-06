using UnityEngine;
using System.Collections;


public abstract class Enemy: MonoBehaviour {
    public enum eMoveDirection { LEFT, RIGHT, UP, DOWN, NONE }
    public enum eStatus { Normal, Die, Hit}
    [HideInInspector]
    public Animator _aniamtor;
    protected Renderer _renderer;
    protected Rigidbody2D _rigidBody2D;

    protected IMovement _imovement;
    [HideInInspector] public IHitByPlayer _hitbyplayer;

    public eMoveDirection _moveDirection;
    public bool _canHitByShell;
    public Vector3 _speed;
    public bool _isSmart;
    protected virtual void Start()
    {
        _aniamtor = GetComponent<Animator>();
        _renderer = GetComponent<Renderer>();
        _rigidBody2D = GetComponent<Rigidbody2D>();

        // Chọn hướng di chuyển.
        runDirection();

    }

    protected virtual void Update()
    {
        //if (_renderer.isVisible)
        //    _rigidBody2D.WakeUp();
        //else
        //{
        //    _rigidBody2D.Sleep();
        //}
        if (checkDestroyHit())
            _aniamtor.SetTrigger("outofscreen");

        if (_imovement != null)
            _imovement.Movement(this.gameObject);
    }

    private bool checkDestroyHit()
    {
        if (_renderer.isVisible == true)
            return false;
        if (this.transform.position.y > 0)
            return false;
        if (_aniamtor.GetInteger("status") != (int)eStatus.Hit)
            return false;
        return true;
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (this._aniamtor.GetInteger("status") == (int)eStatus.Hit)
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

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        if (tag == "Player")
        {
            if (_hitbyplayer != null)
                _hitbyplayer.Hit(this);
            (collider.gameObject.GetComponent<MarioMovement>() as MarioMovement).EnemyPushUp();
        }

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

    protected virtual void checkHitByPlayer(Collision2D col)
    {
        killPlayer(col.gameObject);

    }

    protected virtual void checkWithBlock(Collision2D collision)
    {
        Animator anim = collision.gameObject.GetComponent<Animator>();
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Normal") == false)
        {
            this._aniamtor.SetInteger("status", (int)eStatus.Hit);
        }

    }

    protected virtual void runDirection()
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

    public virtual void killPlayer(GameObject obj)
    {
        (obj.GetComponent<Mario>() as Mario).GotHit();
    }
}
