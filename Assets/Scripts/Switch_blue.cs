using UnityEngine;
using System.Collections;
using Assets;

public class Switch_blue : MonoBehaviour {

    public GameObject[] _gameObjects;
    public GameObject _sourceObject;
    public Object _scene;
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
                //switchBlock();
                //Debug.Log("Player Hit");
                foreach (var obj in _gameObjects)
                {
                    obj.GetComponent<ISwitchable>()._switch_on();
                }
            }
        }
    }

    //private void switchBlock()
    //{
    //    Vector3 position = _gameObject.transform.position;
    //    Quaternion rotation = _gameObject.transform.rotation;
    //    Object.Instantiate(_sourceObject, position, rotation);
    //    _sourceObject.transform.parent = _gameObject.transform.parent;
    //    _sourceObject.GetComponentInChildren<Door>()._scene = this._scene;
    //    Debug.Log(_scene.name);
    //    Destroy(_gameObject);
    //}
}
