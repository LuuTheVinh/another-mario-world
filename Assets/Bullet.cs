using UnityEngine;
using System.Collections;
using Assets;

public class Bullet : MonoBehaviour {

    public enum eType
    {
        fire,
        boomerang
    }


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
        Destroy(this.gameObject);
        collider.gameObject.GetComponent<Enemy>().hitByBullet(this._damage, _type);
    }
    protected virtual void collideWithGround(Collider2D collider)
    {
        Destroy(this.gameObject);
    }
}
