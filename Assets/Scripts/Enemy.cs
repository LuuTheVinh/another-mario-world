using UnityEngine;
using System.Collections;


public abstract class Enemy : MonoBehaviour {
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

    //flag báo đã chết để không giết mario
    protected bool _isDie;

    protected bool _isSleep;
    protected virtual void Start()
    {
        _aniamtor = GetComponent<Animator>();
        _renderer = GetComponent<Renderer>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        //if (_rigidBody2D != null)
        //    _rigidBody2D.velocity = new Vector2(_speed.x, _speed.y);
        // Chọn hướng di chuyển.
        runDirection();
        _isSleep = true;
    }

    protected virtual void Update()
    {
        checkWakeUp();

        if (_isSleep == true)
            return;
        if (checkDestroyHit())
            _aniamtor.SetTrigger("outofscreen");

        if (_imovement != null)
            _imovement.Movement(this.gameObject);
        if (this.transform.position.y < GameObject.Find("/Controller").GetComponent<SceneController>()._botGame)
        {
            Destroy(this.gameObject);
        }
    }

    private void checkWakeUp()
    {
        if (_isSleep == true && _renderer.isVisible == true)
            _isSleep = false;

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
            killPlayer(collision.gameObject);
        if (tag == "Ground")
            checkWithGround(collision);
        if (tag == "Enemy")
            checkWithEnemy(collision);
        //if (name == "block")
        //    checkWithBlock(collision);

    }


    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        if (tag == "Player")
        {
            checkHitByPlayer(collider);
        }

    }

    public virtual void SetSpeed(Vector3 s)
    {
        this._speed = s;
        _imovement = new LinearMovement(_speed.x, _speed.y, _speed.z);
        //if (_rigidBody2D != null)
        //    _rigidBody2D.velocity = new Vector2(_speed.x, _speed.y);
            
    }

    // Nếu đụng vật khác thì đi ngược lại
    protected virtual void checkWithGround(Collision2D collision)
    {
        if (collision.collider is EdgeCollider2D)
            return;
        float top = collision.collider.bounds.max.y;
        float centerX = collision.collider.bounds.center.x;
        Collider2D thisCollider = this.GetComponent<Collider2D>();
        if (top - thisCollider.bounds.min.y > 0.5)
        //if (centerX > thisCollider.bounds.min.x && centerX < thisCollider.bounds.max.x)
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

    protected virtual void checkHitByPlayer(Collider2D col)
    {
        //killPlayer(col.gameObject);
        //if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y >= 0)
        //    this.killPlayer(col.gameObject);
        //else
        if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            float top = col.bounds.max.y;
            float centerX = col.bounds.center.x;
            Collider2D thisCollider = this.GetComponent<Collider2D>();
            if (centerX > thisCollider.bounds.min.x && centerX < thisCollider.bounds.max.x)
            {
                _isDie = true;

                if (_hitbyplayer != null)
                    _hitbyplayer.Hit(this);
                (col.gameObject.GetComponent<MarioMovement>() as MarioMovement).EnemyPushUp();
            }
            else
            {
                this.killPlayer(col.gameObject);
            }
        }
        else
        {
            this.killPlayer(col.gameObject);
        }
    }

    //protected virtual void checkWithBlock(Collision2D collision)
    //{
    //    return;
    //    Animator anim = collision.gameObject.GetComponent<Animator>();
    //    string parenttag = collision.gameObject.transform.parent.gameObject.tag;
    //    GameObject parent = collision.gameObject.transform.parent.gameObject;
    //    //if (parenttag == "Brick")
    //    //{
    //    //    if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Normal") == false)
    //    //    {
    //    //        this._aniamtor.SetInteger("status", (int)eStatus.Hit);
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    Debug.Log(anim.GetNextAnimatorStateInfo(0).IsName("Normal"));
    //    //    Debug.Log(anim.GetNextAnimatorStateInfo(0).IsName("Hit"));
    //    //    Debug.Log(anim.GetNextAnimatorStateInfo(0).IsName("Discovered"));
    //    //    if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Normal") == false && anim.GetCurrentAnimatorStateInfo(0).IsTag("Discovered") == false)
    //    //    {
    //    //        this._aniamtor.SetInteger("status", (int)eStatus.Hit);
    //    //    }

    //    //}
    //    if (collision.gameObject.transform.position.y > parent.transform.position.y)
    //        this._aniamtor.SetInteger("status", (int)eStatus.Hit);
    //}

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
        if (_isDie == false)
            (obj.GetComponent<Mario>() as Mario).GotHit();
    }
}
