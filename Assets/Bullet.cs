using UnityEngine;
using System.Collections;
using Assets;

public class Bullet : MonoBehaviour {

    public enum eType
    {
        fire,
        boomerang
    }

    public GameObject ExplosionEffect;
    public GameObject CoinEffect;

    public float _speed;

    private IMove _move;

    public float _damage;
    [HideInInspector] public eType _type;
	// Use this for initialization
	void Start () {
        _move = new LinearMove();
        _type = eType.fire;
	}
	
	// Update is called once per frame
	void Update () {
        if (_move != null)
            _move.move(this.gameObject);
        //if (this.GetComponent<Renderer>().isVisible == false)
        //    Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        string name = collider.gameObject.name;
        
        if (tag == "Enemy")
        {
            collideWithEnemy(collider);
        }
        if (tag == "Ground")
        {
            collideWithGround(collider);
        }
    }

    protected virtual void collideWithEnemy(Collider2D collider)
    {
        Instantiate(ExplosionEffect, collider.gameObject.transform.position, this.transform.rotation);
        
        Destroy(this.gameObject);
        collider.gameObject.GetComponent<Enemy>().hitByBullet(this._damage, _type);
        
        // cộng tiền, find đỡ khỏi phải truyền object
        var gamemanager = GameObject.Find("_GameManager");

        if (gamemanager != null)
        {
            var status = collider.gameObject.GetComponent<Animator>().GetInteger("status");
            if (status == (int)Enemy.eStatus.Hit)
            {
                gamemanager.GetComponent<GameManager>().UpdateCoin();
                Instantiate(CoinEffect, collider.gameObject.transform.position, this.transform.rotation);
            }
        }
    }
    protected virtual void collideWithGround(Collider2D collider)
    {
        if (collider is EdgeCollider2D)
            return;
        Instantiate(ExplosionEffect, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);

    }

    void OnDestroy()
    {
        if (this._type == eType.fire)
        {
            var soundmanager = SoundManager.getinstance();
            if (soundmanager != null)
                soundmanager.Play(SoundManager.eIdentify.bulletbreak);
        }
        //if (this._type == eType.boomerang)
        //{
        //    var soundmanager = SoundManager.getinstance();
        //    if (soundmanager != null)
        //        soundmanager.Play(SoundManager.eIdentify.metalhit);
        //}
    }
}
