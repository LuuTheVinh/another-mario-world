using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour {

    private float _delayTime;
    public GameObject _explosion;

    private bool _isSleep;
    private Renderer _renderer;

    
	// Use this for initialization
	void Start () {
        _isSleep = true;
        _renderer = GetComponent<Renderer>();
        _delayTime = Random.Range(2.0f, 5.0f) - 1;
	}
	
	// Update is called once per frame
	void Update () {
        checkWakeUp();

        if (_isSleep == true)
            return;
        Invoke("red", _delayTime - 1.0f);
        Invoke("boom", _delayTime);
	}

    private void checkWakeUp()
    {
        if (_isSleep == true && _renderer.isVisible == true)
            _isSleep = false;

    }

    private void boom()
    {
        Destroy(this.gameObject);
        GameObject.Instantiate(_explosion,
            this.gameObject.transform.position,
            this.gameObject.transform.rotation);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        string name = collision.gameObject.name;

        if (tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
        this.boom();
       
    }

    private static bool flagflash;
    private void red()
    {
        if (flagflash == true)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;

        }
        flagflash = !flagflash;
        Invoke("red", 0.15f);
    }

}
