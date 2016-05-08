using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

    private Animator _animator;
	// Use this for initialization
	void Start () {
        _animator = this.GetComponent<Animator>();   
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Player")
            collideWithPlayer(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        string tag = collider.gameObject.tag;
        if (tag == "Enemy")
        {
            if (collider is EdgeCollider2D)
                return;
            collider.gameObject.GetComponent<Enemy>()._aniamtor.SetInteger("status", (int)Enemy.eStatus.Hit);
        }
    }

    private void collideWithPlayer(GameObject gameObject)
    {
        Vector3 distance = this.transform.position - gameObject.transform.position;
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
            return;
        if (distance.y > 0 && Mathf.Abs(distance.x) < 0.3 )
        {
            int status = gameObject.GetComponent<Animator>().GetInteger("status");
            if (status == (int)Mario.eMarioStatus.SMALL)
            {
                _animator.SetTrigger("push");
            }
            else if (status >= (int) Mario.eMarioStatus.BIG)
            {
                _animator.SetTrigger("smash");
            }
        }
    }
}
