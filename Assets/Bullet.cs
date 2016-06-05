using UnityEngine;
using System.Collections;
using Assets;

public class Bullet : MonoBehaviour {

    public enum eType
    {
        big,
        small,
        sin
    }

    public float _speed;
    public float _amp;
    public float _rad;
    private IMove _move;

    public float _damage;
	// Use this for initialization
	void Start () {
        if (true)
            _move = new LinearMove();
        if (false)
        {
           _move = new SinMove(this.gameObject);
         //   GetComponent<Animator>().SetInteger("type", (int)eMoveType.sin);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (_move != null)
            _move.move(this.gameObject);
        //if (this.GetComponent<Renderer>().isVisible == false)
        //    Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        string name = collider.gameObject.name;
        if (tag == "Enemy")
        {
            Destroy(this.gameObject);

            collider.gameObject.GetComponent<Enemy>().hitByBullet(this._damage);

        }
        if (tag == "Ground")
        {
            Destroy(this.gameObject);

        }
    }

}
