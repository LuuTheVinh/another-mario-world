using UnityEngine;
using System.Collections;

public class Blunt : Bullet {

    public Vector2 _Force;
	// Use this for initialization
	void Start () {
        this._type = eType.blunt;
        this.GetComponent<Rigidbody2D>().AddForce(_Force);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }

    protected override void collideWithEnemy(Collider2D collider)
    {
        base.collideWithEnemy(collider);
    }
    
    protected override void collideWithGround(Collider2D collider)
    {
        base.collideWithGround(collider);
    }
}
