using UnityEngine;
using System.Collections;

// Created by Ho Hoang Tung
public class Troopa : Enemy {
    public enum eStatus { Normal, Shell, Hit, SpeedShell}
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

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void checkHitByPlayer(Collision2D col)
    {
        if (this._aniamtor.GetInteger("status") == (int)eStatus.Shell)
        {
            _aniamtor.SetInteger("status", (int)Troopa.eStatus.SpeedShell);
            this.SetSpeed(new Vector3(0.3f, 0f, 0f));
        }
        else
            base.checkHitByPlayer(col);
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
