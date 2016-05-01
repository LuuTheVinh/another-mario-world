using UnityEngine;
using System.Collections;

public class PipeApproachBound : MonoBehaviour {

    private GameObject _mario;
    private GameObject _enemy = null;

    public GameObject Enemy
    {
        get { return _enemy; }
        set { _enemy = value; }
    }
	// Use this for initialization
	void Start () {
        _mario = GameObject.FindGameObjectsWithTag("Player")[0];
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Enemy == null)
            return;
        string tag = collider.gameObject.tag;
        if (tag == "Player")
            Enemy.GetComponent<Animator>().SetBool("approach", true);
        Debug.Log("Enter");
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (Enemy == null)
            return;
        string tag = collider.gameObject.tag;
        if (tag == "Player")
            Enemy.GetComponent<Animator>().SetBool("approach", false);
        Debug.Log("Exit");
    }
}
