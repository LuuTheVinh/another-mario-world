using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour {

    public enum ePipeType{None, PiranhaPlant, FirePiranha}

    public ePipeType _pipeType;
    public GameObject prefabs;

    private GameObject _mario;
    private Animator _plant_animator;
	// Use this for initialization
	void Start () {
        initPipetype();
	}
	
	// Update is called once per frame
	void Update () {

        checkPlayerLeave();

	}

    private void checkPlayerLeave()
    {
        // Nếu cây không xuất hiện thì kiểm tra khoảng cách với mario cho nó xuất hiện lại
        if (_plant_animator.GetBool("approach") == false)
            return;
        Vector3 distance = this.transform.position - _mario.transform.position;
        if (Mathf.Abs(distance.y) < 2)
            return;
        if (Mathf.Abs(distance.x) < 2)
            return;
        _plant_animator.SetBool("approach", false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
       
        // Nếu player chạm cống thì cây không xuất hiện nữa.
        if (tag == "Player")
        {
            if (this._mario == null)
                this._mario = collision.gameObject;
            _plant_animator.SetBool("approach", true);
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
            obj.gameObject.transform.parent = this.transform;
            _plant_animator = obj.gameObject.GetComponent<Animator>();
        }
    }

}
