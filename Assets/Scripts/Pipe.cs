using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour {

    public enum ePipeType{None, PiranhaPlant, FirePiranha}

    public ePipeType _pipeType;
    public GameObject prefabs;

    private GameObject _mario;
    private Animator _plant_animator;
    private GameObject _enemy;
	// Use this for initialization
	void Start () {
        initPipetype();
        _mario = GameObject.FindGameObjectsWithTag("Player")[0];
        (this.transform.FindChild("approach_bound")
            .GetComponent(typeof(PipeApproachBound)) as PipeApproachBound)
            .Enemy = _enemy;
	}
	
	// Update is called once per frame
	void Update () {

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
        GameObject obj = null;
        switch (_pipeType)
        {
            case ePipeType.None:
                obj = null;
                break;
            case ePipeType.PiranhaPlant:
                obj = (GameObject)Object.Instantiate(
                    prefabs,
                    new Vector3(0, -2, 1),
                    this.transform.rotation);
                break;
            case ePipeType.FirePiranha:
                break;
            default:
                break;
        }
        if (obj != null)
        {
            _enemy = obj;
            obj.gameObject.transform.parent = this.transform;
            _plant_animator = obj.gameObject.GetComponent<Animator>();
        }
    }

}
