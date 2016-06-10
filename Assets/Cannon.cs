using UnityEngine;
using System.Collections;
using Assets;
public class Cannon : Enemy, ISwitchable {

    public GameObject _switch;
    public GameObject _player;
    public GameObject _bullet;

    public float _delaytime;
    public float _startTime;
    // độ xiêng của súng. (độ)
    public float _degree;
    protected override void Start()
    {

        _renderer = GetComponent<Renderer>();
        _aniamtor = GetComponent<Animator>();
        //_rigidBody2D = GetComponent<Rigidbody2D>();

        //runDirection();
        _isSleep = true;
    }


    private float _time;
    protected override void Update()
    {
        if (_isSleep == true)
            return;
        if (checkDestroyHit())
            _aniamtor.SetTrigger("outofscreen");

    }

    void FixedUpdate()
    {
        if (_isSleep == true)
            return;
        if (_startTime > 0)
            _startTime -= Time.deltaTime;
        if (_startTime <= 0)
        {
            if (_time <= 0)
            {
                fire();
                _time = _delaytime - _time;
            }

        }
        if (_time > 0)
            _time -= Time.deltaTime;
    }
    private bool checkDestroyHit()
    {
        //if (_renderer.isVisible == true)
        //    return false;
        //if (_aniamtor.GetInteger("status") != (int)eStatus.Hit)
        //    return false;
        //return true;
        return false;
    }

    private void fire()
    {

        var dir = getDirection(_player);
        GameObject bullet = (GameObject)GameObject.Instantiate(
            _bullet,
            this.transform.position,
            this.transform.rotation);

        if (dir.x < 0)
        {
            // new Bullet 
            // this._degree 
            bullet.GetComponent<CannonBullet>()._degree = this._degree;
        }
        else
        {
            // new bullet
            // this._degree + 180;
            bullet.GetComponent<CannonBullet>()._degree = this._degree + 180f;
        }

    }

    private Vector2 getDirection(GameObject _target)
    {

        var distance = _target.transform.position - this.transform.position;
        var manitutude = ((Vector2) distance).magnitude;

        float rad = Mathf.Deg2Rad * _degree + Mathf.Atan(distance.x / distance.y);
        Vector2 v = new Vector2(manitutude * Mathf.Cos(rad), manitutude * Mathf.Sin(rad));
        return v;
    }

    void ISwitchable._switch_on()
    {
        _isSleep = false;
    }

    public override void killPlayer(GameObject obj)
    {
        // do nothing
        // cây súng không giết player, đạn mới giết player
    }


}
