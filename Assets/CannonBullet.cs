using UnityEngine;
using System.Collections;

public class CannonBullet : Enemy {


    public float _degree;

    protected override void Start()
    {
        base.Start();
        this.SetSpeed(_speed);
        _isSleep = false;
    }


    protected override void Update()
    {
        if (checkDestroyHit())
            _aniamtor.SetTrigger("outofscreen");

        if (_imovement != null)
            _imovement.Movement(this.gameObject);


    }

    private bool checkDestroyHit()
    {
        if (_renderer.isVisible == true)
            return false;
        if (_aniamtor.GetInteger("status") != (int)eStatus.Hit)
            return false;
        return true;
        //return false;
    
    }

    public override void SetSpeed(Vector3 s)
    {
        this._speed = s;
        _imovement = new LinearMovement(
            ((Vector2)s).magnitude * Mathf.Cos(Mathf.Deg2Rad * _degree),
            ((Vector2)s).magnitude * Mathf.Sin(Mathf.Deg2Rad * _degree),
            _speed.z);

    }

    protected override void runDirection()
    {
        _speed.x = Mathf.Abs(_speed.x);
        _speed.y = Mathf.Abs(_speed.y);
        _speed.z = Mathf.Abs(_speed.z);
    }

    protected override void checkWithGround(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        if (tag == "Ground")
            checkWithGround(null);
        if (tag == "Player")
            killPlayer(collider.gameObject);
    }
}
