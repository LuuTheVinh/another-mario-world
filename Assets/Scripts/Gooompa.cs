using UnityEngine;
using System.Collections;

// Creaeted by Ho Hoang Tung
public class Gooompa : MonoBehaviour {

    public enum eMoveDirection { LEFT, RIGHT, UP, DOWN, NONE}
    private Animator _aniamtor;
    private IMovement _imovement;

    public eMoveDirection _moveDirection;

    public Vector3 _speed;

    // Use this for initialization
	void Start () {
        _aniamtor = GetComponent<Animator>();

        // Chọn hướng di chuyển.
        runDirection();

        // Chọn kiểu di chuyển.
        _imovement = new LinearMovement(_speed.x, _speed.y, _speed.z);
	}
	
	// Update is called once per frame
	void Update () {
        if (_imovement != null)
            _imovement.Movement(this.gameObject);
	}

    void OnCollisionEnter2D(Collision2D collisiion) 
    {
        checkHitByPlayer(collisiion);

    }

    public void SetSpeed(Vector3 s)
    {
        _speed = s;
        _imovement = new LinearMovement(s.x, s.y, s.z);
    }

    private void checkHitByPlayer(Collision2D col)
    {
        if (_aniamtor.GetCurrentAnimatorStateInfo(0).IsName("GoompaNormal") == false)
            return;
        if (col.gameObject.tag != "Player")
            return;
        // Nếu goompa đang trong trạng thái normal và va chạm với player
        // thì kiểm tra hướng va chạm.
        Vector3 distance = (this.transform.position - col.gameObject.transform.position).normalized;

        if (distance.y < 0 && Mathf.Abs(distance.x) < -distance.y)
        {
            // Hướng từ trên xuống, goompa chết.
            _aniamtor.SetInteger("status", 1);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            this.SetSpeed(Vector3.zero);
        }
        else
        {
            // Mario die.
        }
    }

    private void runDirection()
    {
        switch (_moveDirection)
        {
            case eMoveDirection.LEFT:
                _speed.x = -Mathf.Abs(_speed.x);
                break;
            case eMoveDirection.RIGHT:
                _speed.x = Mathf.Abs(_speed.x);
                break;
            case eMoveDirection.UP:
                _speed.y = Mathf.Abs(_speed.y);
                break;
            case eMoveDirection.DOWN:
                _speed.y = -Mathf.Abs(_speed.y);
                break;
        }
    }
}
