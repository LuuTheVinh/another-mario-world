using UnityEngine;
using System.Collections;

public class FireJump : MonoBehaviour {
    private enum eStatus {up, down}
    
    public Vector2 _force;
    // 0 là up, 1 là down
    public Sprite[] _sprite;
    private eStatus _status;
    private bool _statusUpdateFlag;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(_force);
        _status = eStatus.up;
        _statusUpdateFlag = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponent<Rigidbody2D>().velocity.y < 0)
            _status = eStatus.down;
        UpdateSpriteByStatus();
	}

    private void UpdateSpriteByStatus()
    {
        if (_statusUpdateFlag == false)
            return;
        _statusUpdateFlag = true;
        this.GetComponent<SpriteRenderer>().sprite = _sprite[(int)_status];
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        string name = collider.gameObject.name;
        if (tag == "Player")
        {
            collider.gameObject.GetComponent<Mario>().GotHit();
        }
        if (name.Contains("fire_jump_base"))
        {
            Destroy(this.gameObject);
        }
    }
}
