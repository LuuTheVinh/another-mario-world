using UnityEngine;
using System.Collections;

public class EnemyCollisionPlayer : MonoBehaviour {

    private Enemy _enemy;
	// Use this for initialization
	void Start () {
        _enemy = (this.gameObject.GetComponentInParent<Enemy>() as Enemy);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        //string tag = collider.gameObject.tag;
        //if (tag == "Player")
        //    checkHitByPlayer(collider.gameObject);

        //if (_enemy._hitbyplayer != null)
        //    _enemy._hitbyplayer.Hit(_enemy);
        ////if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
        //(collider.gameObject.GetComponent<MarioMovement>() as MarioMovement).Jump();
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
    }

    protected virtual void checkHitByPlayer(GameObject obj)
    {



        //if (_aniamtor.GetCurrentAnimatorStateInfo(0).IsName("GoompaNormal") == false)
        //    return;

        // Nếu goompa đang trong trạng thái normal và va chạm với player
        // thì kiểm tra hướng va chạm.
        //Vector3 distance = (this.transform.position - obj.transform.position);
        //if (distance.y < 0 && obj.GetComponent<Rigidbody2D>().velocity.y <= 0)
        //{
        //    if (_enemy._hitbyplayer != null)
        //        _enemy._hitbyplayer.Hit(_enemy);
        //    //if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
        //    (obj.GetComponent<MarioMovement>() as MarioMovement).Jump();
        //}
        //else
        //{
        //    // Mario die.
        //    _enemy.killPlayer(obj);
        //}
    }
}
