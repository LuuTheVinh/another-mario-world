using UnityEngine;
using System.Collections;

// Created by Ho Hoang Tung
public class Troopa : Enemy {
    public enum eStatus { Normal, Shell, Hit, SpeedShell}
    private bool _flagHit;
    private bool _justSlide;
    protected override void Start()
    {

        base.Start();

        _imovement = new LinearMovement(_speed.x, _speed.y, _speed.z);
        _hitbyplayer = new TroopaHitByPlayer();
        if ((_imovement as LinearMovement).Xspeed > 0)
            _aniamtor.SetBool("left", false);
        else
            _aniamtor.SetBool("left", true);
    }

    protected override void Update()
    {
        //if (_renderer.isVisible)
        //    _rigidBody2D.WakeUp();
        //else
        //{
        //    _rigidBody2D.Sleep();
        //}
        base.Update();

        
    }

    void FixedUpdate()
    {
        InvokeRepeating("flagfalse", 1, 1);
    }

    private void flagfalse()
    {
        _flagHit = false;
        _justSlide = false;

    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Player")
        {
            int status = _aniamtor.GetInteger("status");
            switch (status)
            {
                case (int)eStatus.Normal:
                case (int)eStatus.SpeedShell:
                    if (_justSlide == false)
                        killPlayer(collision.gameObject);
                    break;
                case (int)eStatus.Shell:
                    collision.gameObject.GetComponent<MarioController>().kick();
                    speedSlide(collision.gameObject);
                    _justSlide = true;
                    break;
                case (int)eStatus.Hit:
                    return;
            }
            _flagHit = true;
        }
        else 
            base.OnCollisionEnter2D(collision);
    }

    private void checkWithBrick(Collision2D col)
    {
        if (_aniamtor.GetInteger("status") == (int) eStatus.SpeedShell)
            col.gameObject.GetComponent<Animator>().SetTrigger("smash");
    }

    public override void killPlayer(GameObject obj)
    {
        if (_justSlide == false)
            (obj.GetComponent<Mario>() as Mario).GotHit();

    }


    //protected override void OnTriggerEnter2D(Collider2D collider)
    //{
    //    int status = _aniamtor.GetInteger("status");
    //    switch (status)
    //    {
    //        case (int)eStatus.Normal:
    //            _aniamtor.SetInteger("status", (int)eStatus.Shell);
    //            break;
    //        case (int)eStatus.SpeedShell:
    //            _aniamtor.SetInteger("status", (int)eStatus.Shell);
    //            break;
    //        case (int)eStatus.Shell:
    //            return;
    //        case (int)eStatus.Hit:
    //            return;
    //    }
    //}

    protected override void checkHitByPlayer(GameObject obj)
    {
        if (this._aniamtor.GetInteger("status") == (int)eStatus.Shell)
        {
            speedSlide(obj);
        }
        else
            base.checkHitByPlayer(obj);
        _isDie = false;
    }


    private void speedSlide(GameObject player)
    {
        Vector3 distance = this.transform.position - player.transform.position;
        _aniamtor.SetInteger("status", (int)Troopa.eStatus.SpeedShell);
        if (distance.x <= 0)
            this.SetSpeed(new Vector3(-0.3f, 0f, 0f));
        else
            this.SetSpeed(new Vector3(0.3f, 0f, 0f));
    }
    public override void back()
    {
        base.back();
        if ((_imovement as LinearMovement).Xspeed > 0)
            _aniamtor.SetBool("left", false);
        else
            _aniamtor.SetBool("left", true);
    }

    // Nếu đụng vật khác thì đi ngược lại
    protected override void checkWithGround(Collision2D collision)
    {
        base.checkWithGround(collision);
        if (collision.gameObject.transform.parent != null && collision.gameObject.transform.parent.gameObject.tag == "Brick")
        {
            float top = collision.collider.bounds.max.y;
            if (top - this.GetComponent<Collider2D>().bounds.min.y > 0.5)
                checkWithBrick(collision);
        }
    }

    protected override void checkWithEnemy(Collision2D collision)
    {
        eStatus status = (eStatus)_aniamtor.GetInteger("status");
        switch (status)
        {
            case eStatus.Normal:
                back();
                break;
            case eStatus.Shell:
                break;
            case eStatus.SpeedShell:
                // kill them all :v
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy._canHitByShell)
                    enemy.GetComponent<Animator>().SetInteger("status", (int)Enemy.eStatus.Hit);
                break;

        }
    }

}
