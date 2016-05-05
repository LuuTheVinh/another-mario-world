using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pipe : MonoBehaviour {

    //public enum ePipeType{None, PiranhaPlant, FirePiranha}

    //public ePipeType _pipeType;
    public GameObject _prefabs;

    private GameObject _enemy;

    public float _goompaAppearTime;
    private float _goopaCountTime = 0f;
    private List<GameObject> _listGoompa = new List<GameObject>();
	// Use this for initialization
	void Start () {
        initPipetype();
        
        if (_prefabs != null && _prefabs.name != "Goompa")
            (this.transform.FindChild(  "approach_bound")
                .GetComponent(typeof(PipeApproachBound)) as PipeApproachBound)
                .Enemy = _enemy;
	}
	
	// Update is called once per frame
	void Update () {

        if (_prefabs != null && _prefabs.name == "Goompa")
            Update(_listGoompa);
	}

    private void Update(List<GameObject> _listGoompa)
    {
        _listGoompa.RemoveAll(item => item == null);
        Vector3 distance = Vector3.zero;
        foreach (GameObject go in _listGoompa)
        {
            distance = go.transform.position - this.transform.position;
            if (Mathf.Abs(distance.x) > 1 )
            {
                go.GetComponent<BoxCollider2D>().isTrigger = false;
                go.GetComponent<Rigidbody2D>().isKinematic = false;
                go.GetComponent<Rigidbody2D>().WakeUp();
            }
        }
        //Vector3 vp = Camera.current.WorldToViewportPoint(this.gameObject.transform.position);
        if (this.gameObject.GetComponent<Renderer>().isVisible){
            if (_listGoompa.Count < 3)
            {
                _goopaCountTime += Time.deltaTime;
                if (_goopaCountTime >= _goompaAppearTime)
                {
                    _goopaCountTime -= _goompaAppearTime;
                    GameObject obj = (GameObject)Object.Instantiate(
                        _prefabs,
                        this.transform.position,
                        new Quaternion());
                    obj.transform.parent = this.transform;
                    Enemy enemy = (obj.GetComponent<Enemy>() as Enemy);
                    if (this.transform.rotation.z < 0)
                    {

                        enemy._moveDirection = Enemy.eMoveDirection.RIGHT;
                        enemy.transform.position = this.gameObject.transform.position + new Vector3(1.1f, 0f, 1f);
                    }
                    else if (this.transform.rotation.z > 0)
                    {

                        enemy._moveDirection = Enemy.eMoveDirection.LEFT;
                        enemy.transform.position = this.gameObject.transform.position - new Vector3(1.1f, 0f, 1f);
                    }
                    _listGoompa.Add(obj);
                }
            }
        }

    }

    private bool isInViewPort(Vector3 vp)
    {
        if (vp.x < 0 || vp.x > 1)
            return false;
        if (vp.y < 0 || vp.y > 1)
            return false;
        return true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
       
        if (tag == "Player")
        {

        }

    }

    private void initPipetype()
    {
        
        if (_prefabs != null)
        {
            if (_prefabs.name == "Goompa")
                return;
            GameObject obj = (GameObject)Object.Instantiate(
                _prefabs,
                new Vector3(0, -2, 1),
                this.transform.rotation);
            obj.gameObject.transform.parent = this.transform;
            _enemy = obj;

        }
 
    }

}
