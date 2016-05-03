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
        base.Update();
        if (_imovement != null)
            _imovement.Movement(this.gameObject);
	}

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    public override void SetSpeed(Vector3 s)
    {
        base.SetSpeed(s);
        _imovement = new LinearMovement(s.x, s.y, s.z);
    }
}
