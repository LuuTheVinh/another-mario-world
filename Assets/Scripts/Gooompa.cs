using UnityEngine;
using System.Collections;

// Creaeted by Ho Hoang Tung
public class Gooompa : Enemy {

    // Use this for initialization
	protected override void Start () {

        base.Start();

        // Chọn kiểu di chuyển.
        _imovement = new LinearMovement(_speed.x, _speed.y, _speed.z);
        _hitbyplayer = new GoompaHitByPlayer();
	}
	
	// Update is called once per frame
	protected override void Update () {
        //if (_renderer.isVisible)
        //    _rigidBody2D.WakeUp();
        //else
        //{
        //    _rigidBody2D.Sleep();
        //    return;
        //}
        base.Update();
        //if (_imovement != null)
        //    _imovement.Movement(this.gameObject);

	}

    protected override void OnCollisionEnter2D(Collision2D collision)
    {

        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.name == "Hign_land_6")
        {
            float top = collision.collider.bounds.max.y;
            Debug.Log(top);

            Collider2D thisCollider = this.GetComponent<Collider2D>();
            Debug.Log(thisCollider.bounds.min.y);
        }

    }

}
