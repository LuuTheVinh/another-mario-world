using UnityEngine;
using System.Collections;

public class FreeCoin : Item {

    public float _minX;
    public float _maxX;
	// Use this for initialization
	protected override void Start () {
	
	}
	
	// Update is called once per frame
	protected override void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        if (tag == "Player")
        {
            // Bỏ tiền vô túi nè.
            playerhit();
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        string name = collision.gameObject.name;

        if (tag == "Player")
        {
            playerhit();
        }
        else
        {

            var forceX = Random.Range(_minX, _maxX);
            //var temp_pos = this.GetComponent<Rigidbody2D>().velocity;
            //temp_pos.x = 0;
            //this.GetComponent<Rigidbody2D>().velocity = temp_pos;
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, 0.0f));
        }
    }

    private void playerhit()
    {
        Destroy(this.gameObject);
        var soundmanager = SoundManager.getinstance();
        if (soundmanager != null)
            soundmanager.Play(SoundManager.eIdentify.coinhit);

        var controller = GameObject.Find("/Controller");
        if (controller != null)
        {
            controller.GetComponent<SceneController>().upCoin();
        }
    }
}
