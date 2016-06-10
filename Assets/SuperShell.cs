using UnityEngine;
using System.Collections;

public class SuperShell : Enemy {

	// Use this for initialization
    protected override void Start()
    {

        base.Start();

        _imovement = new LinearMovement(_speed.x, _speed.y, _speed.z);
        //_hitbyplayer = new TroopaHitByPlayer();
        if ((_imovement as LinearMovement).Xspeed > 0)
            this.flipLeft(false);
        else
            this.flipLeft(true);
    }
	

    protected override void Update()
    {
        base.Update();
    }

    private void flipLeft(bool isLeft)
    {
        //var scale = this.gameObject.transform.parent.transform.localScale;
        //scale.x = (isLeft == true) ? (1 * Mathf.Abs(scale.x)) : (- 1 * Mathf.Abs(scale.x));
        //this.gameObject.transform.parent.transform.localScale = scale;
        this.GetComponent<SpriteRenderer>().flipX = !isLeft;
    }


    public override void back()
    {
        base.back();
        if ((_imovement as LinearMovement).Xspeed > 0)
            flipLeft(false);
        else
            flipLeft(true);
    }

}
