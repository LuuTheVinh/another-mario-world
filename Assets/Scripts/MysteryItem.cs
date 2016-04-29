using UnityEngine;
using System.Collections;

// created by Ho Hoang Tung
public class MysteryItem : MonoBehaviour {

    Animator _animator;
    Item _item;
    public Item Item{
        get { return _item; }
        set { _item = value; }
    }
	// Use this for initialization
	void Start () {
        this._animator = GetComponent<Animator>();
        _item = this.GetComponentInChildren<Item>(true);
	
	}
	
	// Update is called once per frame
	void Update () {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Mystery Item Discovered")
            && (_item.gameObject.activeSelf == false))
        {
            // Active item (item Start)
            _item.gameObject.SetActive(true);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Khoảng cách giữa vật mở item và player.
        // Dùng x và y để xác định hướng
        Vector3 distance = (this.transform.position - col.gameObject.transform.position).normalized;

        // Nếu player đấm từ dưới lên thì mở item
        if (distance.y > 0 && Mathf.Abs(distance.x) < distance.y){
            
            // trigger hit làm nảy vật mở item lên
            this._animator.SetTrigger("hit");

            // Xác định player đội chệch về trái hay phải để xác định hướng chạy của item.
            if (distance.x > 0)
            {
                _item._appearMode = global::Item.AppearMode.LEFT;
            }
            else
            {
                _item._appearMode = global::Item.AppearMode.RIGHT;
            }


        }
        Debug.Log("Y : " + distance.y);

    }
}
