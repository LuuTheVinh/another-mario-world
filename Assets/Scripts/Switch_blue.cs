using UnityEngine;
using System.Collections;

public class Switch_blue : MonoBehaviour {

    public GameObject _gameObject;
    public GameObject _sourceObject;

    private Animator _animator;
	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_animator.GetBool("isActive"))
            return;
        string tag = collision.gameObject.tag;
        if (tag == "Player")
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                this.GetComponent<Animator>().SetBool("isActive",true);
                switchBlock();
                Debug.Log("Player Hit");
            }
        }
    }

    private void switchBlock()
    {
        Vector3 position = _gameObject.transform.position;
        Quaternion rotation = _gameObject.transform.rotation;
        Object.Instantiate(_sourceObject, position, rotation);
        _sourceObject.transform.parent = _gameObject.transform.parent;
        Destroy(_gameObject);
    }
}
