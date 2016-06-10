using UnityEngine;
using System.Collections;

public class TeleportGate : MonoBehaviour {

    // Nếu cống này là cống vào thì set outpipe là cống khác, inpie null

    public GameObject _OutPipe;

    public enum eDir { left, right, top, bottom}
    public eDir _Direction;
    public eDir _Out_Dir;
    [HideInInspector] public GameObject _Player;

    private bool _isDisapearing;
    private bool _isAppearing;

    private const float _timeAnim = 3.0f;
    private float _time;
    private Vector3 _oldPosition;
    private bool _isfinish;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (_isfinish == true)
            return;
        if (_isDisapearing)
        {
            _time += Time.deltaTime;

            playerMove(_Direction, _time, 2.0f);

            if (_time > _timeAnim)
            {
                Debug.Log("end");
                _isDisapearing = false;
                _isfinish = true;
                //  chuyển tới cái cống khác
                TeleportGate gate = _OutPipe.GetComponentInChildren<TeleportGate>();
                gate.player_appear(this._Player);
                this._Player = null;
            }
        }
        if (_isAppearing)
        {
            _time += Time.deltaTime;
            playerMove(_Direction, _time, 2.0f);
            if (_time > _timeAnim)
            {
                _isDisapearing = false;
                _isfinish = true;
                finish();
            }
        }
	}

    private void playerMove(eDir _Direction, float _time, float p)
    {
        switch (_Direction)
        {
            case eDir.left:
                _Player.transform.position = new Vector3(
                    _oldPosition.x - _time / _timeAnim * 2.0f,
                    _oldPosition.y,
                    _oldPosition.z);
                break;
            case eDir.right:
                _Player.transform.position = new Vector3(
                    _oldPosition.x + _time / _timeAnim * 2.0f,
                    _oldPosition.y,
                    _oldPosition.z);
                break;
            case eDir.top:
                _Player.transform.position = new Vector3(
                    _oldPosition.x,
                    _oldPosition.y + _time / _timeAnim * 2.0f,
                    _oldPosition.z);
                break;
            case eDir.bottom:
                _Player.transform.position = new Vector3(
                    _oldPosition.x,
                    _oldPosition.y - _time / _timeAnim * 2.0f,
                    _oldPosition.z);
                break;
            default:
                break;
        }

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;

        if (tag == "Player")
        {


            if (Input.GetKey("down"))
            {
                player_disapear(collider.gameObject);
            }
        }
    }

    private void player_disapear(GameObject go)
    {
        
        _Player = go;
        _Player.GetComponent<Rigidbody2D>().isKinematic = true;
        _Player.GetComponent<MarioController>().enabled = false;
        _oldPosition = _Player.transform.position;
        _isDisapearing = true;
    }

    private void player_appear(GameObject go)
    {
        _Player = go;
        _Player.transform.position = this.transform.position;
        _Player.GetComponent<Rigidbody2D>().isKinematic = true;
        _Player.GetComponent<MarioController>().enabled = false;
        _oldPosition = _Player.transform.position;
        _isAppearing = true;
        _Player.transform.position = this.transform.position;
        if (_Out_Dir == eDir.left)
        {
            _Player.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            _Player.GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    private void finish()
    {
        _Player.GetComponent<Rigidbody2D>().isKinematic = false;
        _Player.GetComponent<MarioController>().enabled = true;
        _isfinish = true;
    }
}
