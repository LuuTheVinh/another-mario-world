using UnityEngine;
using System.Collections;
using System;

// created by Ho Hoang Tung
public class MysteryItem : MonoBehaviour {
    public enum Type { MYSTERY, BRICK}
    
    Animator _animator;
    Item _item;
    public Item Item{
        get { return _item; }
        set { _item = value; }
    }
    public Type _type;
	// Use this for initialization
	void Start () {
        this._animator = GetComponent<Animator>();
        _animator.SetInteger("type", Convert.ToInt32(_type));
        switch (_type)
        {
            case Type.MYSTERY:
                _animator.Play("Mystery Item");
                break;
            case Type.BRICK:
                _animator.Play("Mystery Brick");
                break;
        }
        _item = this.GetComponentInChildren<Item>(true);
	    
	}
	
	// Update is called once per frame
	void Update () {
        checkForActiveItem();

	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        // Kiểm tra bị player đội từ dưới  
        if (tag == "Player")
           checkHit(collision);

    }

    private void checkHit(Collision2D col)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Mystery Item Normal") == false)
            return;
        // Nếu mario đang rơi thì không tính
        if (col.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
            return;


        // Khoảng cách giữa vật mở item và player.
        // Dùng x và y để xác định hướng
        Vector3 distance = (this.transform.position - col.gameObject.transform.position).normalized;

        // Nếu player đấm từ dưới lên thì mở item
        if (distance.y > 0 && Mathf.Abs(distance.x) < distance.y)
        {

            // trigger hit làm nảy vật mở item lên
            this._animator.SetTrigger("hit");

            // Xác định player đội chệch về trái hay phải để xác định hướng chạy của item.
            if (distance.x < 0)
            {
                _item._appearMode = global::Item.AppearMode.LEFT;
            }
            else
            {
                _item._appearMode = global::Item.AppearMode.RIGHT;
            }
            if (Item.IsUseAnimator[System.Convert.ToInt32(Item._type)] == 1)
            {
                _item._appearMode = global::Item.AppearMode.INSTANT;
                _item.gameObject.SetActive(true);
            }

        }
    }
    private void checkForActiveItem()
    {
        if (_item == null)
            return;
        if (_item.gameObject == null)
            return;
        if (_item.gameObject.activeSelf == true)
            return;

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Mystery Item Discovered"))
        {
            // Active item (item Start)
            _item.gameObject.SetActive(true);
        }
    }

}
