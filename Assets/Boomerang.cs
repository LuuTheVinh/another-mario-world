using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boomerang : Bullet {


	// Use this for initialization
	void Start () {
        _type = Bullet.eType.boomerang;
        if (this.transform.parent.transform.localScale.x == 0.5)
        {
            this.GetComponent<Animator>().Play("Normal Small");
            
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    List<GameObject> _listIgnore = new List<GameObject>();
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }

    protected override void collideWithEnemy(Collider2D collider)
    {
        if (_listIgnore.Contains(collider.gameObject) == false)
        {
            collider.gameObject.GetComponent<Enemy>().hitByBullet(this._damage, _type);
            _listIgnore.Add(collider.gameObject);
            var soundmanager = SoundManager.getinstance();
            if (soundmanager != null)
                soundmanager.Play(SoundManager.eIdentify.metalhit);
        }
    }

}
