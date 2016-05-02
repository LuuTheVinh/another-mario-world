using UnityEngine;
using System.Collections;

// Created by Ho Hoang Tung
public class Troopa : Enemy {
    public enum eStatus { Normal, Shell, SpeedShell}
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
        base.Update();
        if (_imovement != null)
            _imovement.Movement(this.gameObject);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
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

                break;

        }
    }
}
