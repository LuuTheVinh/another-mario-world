using UnityEngine;
using System.Collections;

public class FrogHand : Enemy {

	// Use this for initialization
    protected override void Start()
    {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
	
	}

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }


    protected override void checkHitByPlayer(Collision2D col)
    {
        (col.gameObject.GetComponent<Mario>() as Mario).GotHit();

    }
}
