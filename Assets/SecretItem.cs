using UnityEngine;
using System.Collections;

public class SecretItem : MonoBehaviour {

    public enum status { normal, discovered}


    public GameObject _item;
    public Vector3 _position;

    public status _status;
    public float _hp;

    private bool _flag;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void discover()
    {
        if (_flag == true)
            return;
        _flag = true;
        this.GetComponent<Animator>().SetInteger("status", (int)status.discovered);
        //if (_item != null)
        //{
        //    GameObject.Instantiate(
        //        _item,
        //        this.gameObject.transform.position + this._position,
        //        this.gameObject.transform.rotation);
        //}
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        string name = collider.gameObject.name;
        if (tag == "Bullet")
        {
            _hp -= collider.gameObject.GetComponent<Bullet>()._damage;
            hitByBullet();
            if (_hp <= 0)
                discover();
            Destroy(collider.gameObject);
        }
    }

    private void hitByBullet()
    {
        var bricks = GetComponentsInChildren<Brick>();
        foreach (var brick in bricks)
        {
            brick.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        Invoke("normalSpriteAfterHit", 0.2f);
    }
    private void normalSpriteAfterHit()
    {
        var bricks = GetComponentsInChildren<Brick>();
        foreach (var brick in bricks)
        {
            brick.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        CancelInvoke("normalSpriteAfterHit");
    }
}
