using UnityEngine;
using System.Collections;
using Assets;

public class SuperSUPERShell : SuperShell, ISwitchable {

    public GameObject _prize;

    private bool _frezee;

    protected override void Start()
    {
        _frezee = true;
        base.Start();
    }

    protected override void Update()
    {
        if (_frezee == true)
            return;
        base.Update();
    }

    public void _switch_on()
    {
        _frezee = false;
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
            Destroy(this.gameObject.transform.parent.gameObject);
            var soundmanager = SoundManager.getinstance();
            if (soundmanager != null)
                soundmanager.Play(SoundManager.eIdentify.enemydie);
            GameObject.Instantiate(_prize,
                this.gameObject.transform.position + Vector3.up * 2 - Vector3.right * 1.5f,
                this.gameObject.transform.rotation);
        }
        else
        {
            this.GetComponent<Animator>().SetTrigger("gotHit");
        }
    }
}
