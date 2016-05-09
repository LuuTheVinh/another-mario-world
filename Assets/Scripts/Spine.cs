using UnityEngine;
using System.Collections;

public class Spine : Enemy {

    public Vector2 _forceJump;
    
    // Thời gian delay trước khi nhảy.
    public float _delayTime;
    // Thời gian đếm để nhảy.
    private float _countingTime;

	// Use this for initialization
	protected override void Start () {
        base.Start();

	}
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (_isSleep == true)
            return;
        jump();
	}

    private void jump()
    {
        if (_aniamtor.GetInteger("status") != (int)Enemy.eStatus.Normal)
            return;
        _countingTime += Time.deltaTime;
        if (_countingTime >= _delayTime)
        {

            _countingTime -= _delayTime;
            _rigidBody2D.AddForce(_forceJump);
        }
    }

    //protected override void checkHitByPlayer(Collision2D col)
    //{
    //    killPlayer(col.gameObject);        
    //}

    protected virtual void checkWithBlock(Collision2D collision)
    {
        //do no thing.
    }

    public override void back()
    {
        if (_moveDirection == eMoveDirection.LEFT)
            _moveDirection = eMoveDirection.RIGHT;
        else if (_moveDirection == eMoveDirection.RIGHT)
            _moveDirection = eMoveDirection.LEFT;

        runDirection();
    }
    protected override void runDirection()
    {
        switch (_moveDirection)
        {
            case eMoveDirection.LEFT:
                this._forceJump = new Vector2(-Mathf.Abs(_forceJump.x), _forceJump.y);
                break;
            case eMoveDirection.RIGHT:
                this._forceJump = new Vector2(Mathf.Abs(_forceJump.x), _forceJump.y);
                break;

        }
    }
}
