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

    public override void hitByBullet(float dmg, Bullet.eType type)
    {
        if (_canHitByFire == false)
            return;
        if (_canHitByBoomerang == false)
            return;
        _hp -= dmg;
        if (this._hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public override void killPlayer(GameObject obj)
    {
        if (_isDie == false)
        {
            if (obj.GetComponent<Mario>().Shield > 0)
                Destroy(this.gameObject);

            (obj.GetComponent<Mario>() as Mario).GotHit();
        }
    }
}
