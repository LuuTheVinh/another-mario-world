using UnityEngine;
using System.Collections;

public class RedPiranhaPlant : Enemy {

	// Use this for initialization
    protected override void Start()
    {
        base.Start();
    }
	
	// Update is called once per frame
    protected override void Update()
    {

        base.Update();
        if (_aniamtor.GetCurrentAnimatorStateInfo(0).IsName("Destroy"))
            Destroy(this.gameObject);
    }

    public override void hitByBullet(float dmg)
    {
        if (_canHitByFire == false)
            return;
        _hp -= dmg;
        if (this._hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
