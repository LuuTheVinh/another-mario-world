using UnityEngine;
using System.Collections;

public class MarioController : MonoBehaviour {

    public float DashSpeed = 1;
    public float HoldJumpTime = 0.25f;
    public float HoldJumpForce = 300f;
    public LayerMask WhatIsGround;
    
    public Transform groundCheck;
    public float groundedRadius = 0.175f;

    private Animator _animator;
    private MarioMovement _marioMovement;
    private Rigidbody2D _rigidbody2D;
    
    private float _timer = 0;   // đếm thời gian giữ nhảy
    private bool _canHoldJump = false;
    private bool _grounded;
    private bool _canJump = false;

    public GameObject _bullet_big;
    public GameObject _bullet_small;
    public GameObject _bullet_bar;
    public GameObject _boomerang_big;
    public GameObject _boomerang_small;
    public GameObject _blunt;

    public float _fireBulletCountDown;
    private const float COUNT_JUMP_FIRE = 0.33f;
    public static float _speedBulletCountDown = COUNT_JUMP_FIRE;
    public bool enable;

    private bool _is_jump_press;
    // Use this for initialization
    void Start () {
        _animator = this.GetComponent<Animator>();
        _marioMovement = this.GetComponent<MarioMovement>();
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
        enable = true;
    }

    void Update()
    {
        // check ground
        _grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, WhatIsGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                _grounded = true;
        }

        if (_grounded)
        {
            Grounded();
        }

        if (Input.GetButtonDown("Jump") && _rigidbody2D.velocity.y <= 0)
        {
            _is_jump_press = true;
            Invoke("Cancel_jump_press", 0.2f);
        }
        
         //nhảy

        if (_is_jump_press && _grounded && enable)
        {
            _canJump = true;
            _is_jump_press = false;
        }

        if (Input.GetButton("Jump") && _canHoldJump && enable)
        {
            _timer += Time.deltaTime;
            _speedBulletCountDown -= Time.deltaTime;
            if (_speedBulletCountDown < 0)
            {
                _speedBulletCountDown = COUNT_JUMP_FIRE;
            }
        }
        else
        {
            _timer = 0;
            _speedBulletCountDown = COUNT_JUMP_FIRE;

        }
        if (_bullet_bar != null)
            _bullet_bar.GetComponent<BulletBar>().updateBar(1 - _frezeeBullet / _fireBulletCountDown);
    }
    
	// Update is called once per frame
	void FixedUpdate() {
        
        var h = Input.GetAxis("Horizontal");

        if (h != 0)
        {
            if (!_animator.GetBool("isRunning") && enable)
            {
                _animator.SetBool("isRunning", true);
            }

            if (h > 0)
            {
                if (_rigidbody2D.velocity.x < -DashSpeed && !_animator.GetBool("isJumping"))
                {
                    _animator.SetTrigger("dash");
                }

                _marioMovement.GotoRight();
            }
            else if (h < 0)
            {
                if (_rigidbody2D.velocity.x > DashSpeed && !_animator.GetBool("isJumping"))
                {
                    _animator.SetTrigger("dash");
                }

                _marioMovement.GotoLeft();
            }
        }
        else
        {
            if (_animator.GetBool("isRunning"))
                _animator.SetBool("isRunning", false);

            _animator.ResetTrigger("dash");
        }

        // nhảy
        if (_canJump)
        {
            this.JumpWithAnimate(false);
            _canHoldJump = true;
        }

        if (_canHoldJump && _timer > HoldJumpTime && _rigidbody2D.velocity.y > 0)
        {
            _rigidbody2D.AddForce(Vector2.up * HoldJumpForce, ForceMode2D.Force);
            _canHoldJump = false;
            _timer = 0;
        }

        // đá
        if (Input.GetButtonDown("Attack"))
        {
            // Tung
            //kick();
            //if (_speedBulletCountDown < COUNT_JUMP_FIRE && _speedBulletCountDown > 0)
            //    speedFire();
            //else
            if (GetComponent<Mario>().Status == Mario.eMarioStatus.WHITE)
            {
                if (GetComponent<Mario>().WeaponType == Mario.eWeapontype.fire)
                    fire();
                if (GetComponent<Mario>().WeaponType == Mario.eWeapontype.boomerang)
                    boomerang();
                if (GetComponent<Mario>().WeaponType == Mario.eWeapontype.blunt)
                    blunt();
            }
  
        }

        // ngồi
        if (_animator.GetInteger("status") != 0 && Input.GetKey("down"))
        {
            if (!_animator.GetBool("isSitting"))
            {
                _animator.SetBool("isSitting", true);
            }
        }
        else if(_animator.GetBool("isSitting"))
        {
            _animator.SetBool("isSitting", false);
        }

        if (_frezeeBullet < _fireBulletCountDown && _frezeeBullet > 0)
        {
            _frezeeBullet += Time.deltaTime;
            if (_frezeeBullet > _fireBulletCountDown)
                _frezeeBullet = 0;
        }
    }

    private void boomerang()
    {
        GameObject boomerang;
        //if (this.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
        //{
        //    // Jump Attack :
        //    // không bị giới hạn countdown
        //    // giảm nữa coldown (count down đối với ground atk)
        //    // giảm nữa damage
        //    // nhân đôi speed.
        //    _frezeeBullet = _fireBulletCountDown / 2;
        //    boomerang = (GameObject)Object.Instantiate(
        //        _boomerang_small,
        //        this.transform.position,
        //        this.transform.rotation);
        //}
        //else
        {
            if (_frezeeBullet > 0)
                return;
            _frezeeBullet = Time.deltaTime;
            boomerang = (GameObject)Object.Instantiate(
               _boomerang_big,
               this.transform.position,
               this.transform.rotation);
            var soundmanager = SoundManager.getinstance();
            if (soundmanager != null)
                soundmanager.Play(SoundManager.eIdentify.shoot);
        }

        int dir = (int)this.gameObject.GetComponent<MarioMovement>()._dir;

        if (dir == -1)
        {
            var scale = boomerang.transform.localScale;
            scale.x *= -1;
            boomerang.transform.localScale = scale;
        }
    }

    private static float _frezeeBullet;
    private void fire()
    {

        GameObject bullet;
        //if (this.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
        //{
        //    // Jump Attack :
        //    // không bị giới hạn countdown
        //    // giảm nữa coldown (count down đối với ground atk)
        //    // giảm nữa damage
        //    // nhân đôi speed.
        //    _frezeeBullet = _fireBulletCountDown / 2;
        //    bullet = (GameObject)Object.Instantiate(
        //        _bullet_small,
        //        this.transform.position,
        //        this.transform.rotation);
        //}
        //else
        {
            if (_frezeeBullet > 0)
                return;
            _frezeeBullet = Time.deltaTime;
             bullet = (GameObject)Object.Instantiate(
                _bullet_big,
                this.transform.position,
                this.transform.rotation);
             var soundmanager = SoundManager.getinstance();
             if (soundmanager != null)
                 soundmanager.Play(SoundManager.eIdentify.shoot);
        }

        int dir = (int)this.gameObject.GetComponent<MarioMovement>()._dir;
        bullet.GetComponent<Bullet>()._speed *= dir;
    }

    private void blunt()
    {
        if (_frezeeBullet > 0)
            return;
        _frezeeBullet = Time.deltaTime;
        int dir = (int)this.gameObject.GetComponent<MarioMovement>()._dir;

        var bullet = (GameObject)Object.Instantiate(
           _blunt,
           new Vector2(this.transform.position.x + dir * 1, this.transform.position.y),
           this.transform.rotation);
        var soundmanager = SoundManager.getinstance();
        if (soundmanager != null)
            soundmanager.Play(SoundManager.eIdentify.shoot);
        bullet.GetComponentInChildren<Blunt>()._Force.x *= dir;
    }

    private static float _speedFire;
    private void speedFire()
    {
        if (_speedFire > 0)
            return;
        if (_speedBulletCountDown == COUNT_JUMP_FIRE)
            return;
        _speedBulletCountDown = COUNT_JUMP_FIRE;
        _frezeeBullet = Time.deltaTime;
        GameObject bullet = (GameObject)Object.Instantiate(
            _bullet_big,
            this.transform.position,
            this.transform.rotation);
        int dir = (int)this.gameObject.GetComponent<MarioMovement>()._dir;
        bullet.GetComponent<Bullet>()._speed *= dir;

    }
    
    public void JumpWithAnimate(bool max)
    {
        if (_animator.GetBool("isJumping"))
        {
            return;
        }

        _canJump = false;
        _animator.SetBool("isJumping", true);
        _animator.SetTrigger("Jump");
        _animator.ResetTrigger("dash");
        
        _marioMovement.Jump(max);

        var soundmanager = SoundManager.getinstance();
        if (soundmanager != null)
        {
            soundmanager.Play(SoundManager.eIdentify.jump);
        }
    }

    public void kick()
    {
        // Tung
        _animator.SetTrigger("kick");
    }

    public void Grounded()
    {
        _animator.SetBool("isJumping", false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            if (Input.GetButtonDown("Jump") && enable)
            {
                _canJump = true;
                _is_jump_press = false;
            }
        }
    }

    void Cancel_jump_press()
    {
        _is_jump_press = false;
        Debug.Log("Cancel");
    }


}
