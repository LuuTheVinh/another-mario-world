using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    [HideInInspector] public GameObject _openner;
    private Animator _animator;
    public Object _scene;
	// Use this for initialization
	void Start () {
        _animator = GetComponentInChildren<Animator>();
        _openner = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        if (tag == "Player")
        {
            _animator.SetTrigger("open");
            _openner = collider.gameObject;
        }
    }
}
