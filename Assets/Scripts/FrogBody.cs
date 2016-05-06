using UnityEngine;
using System.Collections;

public class FrogBody : Enemy {

	// Use this for initialization
    protected override void Start()
    {
        base.Start();
        _hitbyplayer = new GoompaHitByPlayer();
    }
	
    // Update is called once per frame
    protected override void Update()
    {

        base.Update();

    }
    //protected override void checkHitByPlayer(Collision2D col)
    //{
    //    Vector3 distance = (this.transform.position - col.gameObject.transform.position);
    //    if (distance.y < 0 && Mathf.Abs(distance.x) < 0.85)
    //    {
    //        if (_hitbyplayer != null)
    //            _hitbyplayer.Hit(this);
    //        if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
    //            (col.gameObject.GetComponent<MarioMovement>() as MarioMovement).Jump();
    //    }
    //    else
    //    {
    //         Mario die.
    //        killPlayer(col.gameObject);
    //    }
    //}
}
