using UnityEngine;
using System.Collections;

// created by Ho Hoang Tung
public class Item : MonoBehaviour{

    
    public enum ItemType { Mushroom, FireFlower, Amazing_Star, Coin, Leaf, Flygon, Boomerang, Shield, Life, Blunt};


    protected IMovement _imovement;
    protected Rigidbody2D _rigidbody2d;
    protected Animator _animator;

    public ItemType _type = ItemType.Mushroom;
    //public Sprite[] _sprites;
    public Vector3 _speed;

    private float _delayNoneHit;
    
    // Use this for initialization
	protected virtual void Start () {
        // Nếu là leaf hoặc Fire Flower mà mario là small thì đổi thành Mushroom
        _rigidbody2d = GetComponent<Rigidbody2D>();
        
        _animator = GetComponent<Animator>();

        // Chọn kiểu di chuyển.
        _imovement = initMovement();

        // delay lại không thôi vừa mơi chạm được cục gạch thì ăn item.
        // cho nữa giây sau mới ăn được.
        if (this.transform.parent != null)
        {
            _delayNoneHit = 0.5f;
            transform.position = new Vector3(0, 1, 1);
        }
        if (GetComponent<SpriteRenderer>() != null)
            GetComponent<SpriteRenderer>().enabled = true;

	}

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_animator == null)
            return;
        _delayNoneHit -= Time.deltaTime;
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Normal"))
        {
            _animator.enabled = false;
            if (_type == ItemType.Coin)
            {
                Destroy(this.gameObject);
            }
        }
        run();

	}

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

        if (_delayNoneHit > 0)
            return;
        if (this._type == ItemType.Coin)
            return;
        string tag = collision.gameObject.tag;
        string name = collision.gameObject.name;
        if (tag == "Player")
        {
            Destroy(this.gameObject);
            Mario mario = collision.gameObject.GetComponent<Mario>() as Mario;
            updateStatusByItem(mario);
        }
        if (tag == "Ground")
            checkWithGround(collision);
    }

    protected void updateStatusByItem(Mario mario)
    {
        if (mario == null)
            return;
        // câp nhật trạng thái dựa trên type của item
        switch (_type)
        {
            case Item.ItemType.Mushroom:
                mario.GetComponent<Animator>().SetInteger("status", (int) Mario.eMarioStatus.BIG);
                break;
            case Item.ItemType.Boomerang:
                var soundmanager = SoundManager.getinstance();
                if (mario.GetComponent<Animator>().GetInteger("status") == (int)Mario.eMarioStatus.WHITE
                     && mario.WeaponType == Mario.eWeapontype.boomerang)
                {
                    mario.GameManager.GetComponent<GameManager>().UpdateCoin(5);
                    if (soundmanager != null)
                        soundmanager.Play(SoundManager.eIdentify.coinhit);
                }
                else
                {
                    mario.GetComponent<Animator>().SetInteger("status", (int)Mario.eMarioStatus.WHITE);
                    SceneController.setBoomerangPanelActive(true);
                    mario.WeaponType = Mario.eWeapontype.boomerang;
                    if (soundmanager != null)
                        soundmanager.Play(SoundManager.eIdentify.levelup);
                    mario.updateWeaponUI(ItemType.Boomerang);
                }

                break;
            case Item.ItemType.FireFlower:
                var soundmanager2 = SoundManager.getinstance();

                if (mario.GetComponent<Animator>().GetInteger("status") == (int)Mario.eMarioStatus.WHITE
                    && mario.WeaponType == Mario.eWeapontype.fire)
                {
                    mario.GameManager.GetComponent<GameManager>().UpdateCoin(5);
                    if (soundmanager2 != null)
                        soundmanager2.Play(SoundManager.eIdentify.coinhit);
                }
                else
                {
                    mario.GetComponent<Animator>().SetInteger("status", (int)Mario.eMarioStatus.WHITE);
                    SceneController.setBulletPanelActive(true);
                    mario.WeaponType = Mario.eWeapontype.fire;
                    if (soundmanager2 != null)
                        soundmanager2.Play(SoundManager.eIdentify.levelup);
                    mario.updateWeaponUI(ItemType.FireFlower);
                }


                break;
            case ItemType.Blunt:
                var soundmanager3 = SoundManager.getinstance();

                if (mario.GetComponent<Animator>().GetInteger("status") == (int)Mario.eMarioStatus.WHITE
                    && mario.WeaponType == Mario.eWeapontype.blunt)
                {
                    mario.GameManager.GetComponent<GameManager>().UpdateCoin(5);
                    if (soundmanager3 != null)
                        soundmanager3.Play(SoundManager.eIdentify.coinhit);
                }
                else
                {
                    mario.GetComponent<Animator>().SetInteger("status", (int)Mario.eMarioStatus.WHITE);
                    SceneController.setBulletPanelActive(true);
                    mario.WeaponType = Mario.eWeapontype.blunt;
                    if (soundmanager3 != null)
                        soundmanager3.Play(SoundManager.eIdentify.levelup);
                    mario.updateWeaponUI(ItemType.Blunt);
                }


                break;
            case Item.ItemType.Amazing_Star:
                break;
            case Item.ItemType.Leaf:
                mario.GetComponent<Animator>().SetInteger("status", (int)Mario.eMarioStatus.RACOON);
                break;
            case ItemType.Shield:
                mario.GetComponent<Mario>().Shield = 3;
                break;
            case ItemType.Life:
                mario.GetComponent<Mario>().pluslifeone();
                break;
            default:
                break;
        }
    }

    private void run()
    {
        if (_imovement != null)
            _imovement.Movement(this.gameObject);
    }

    private void checkWithGround(Collision2D collision)
    {
        float top = collision.collider.bounds.max.y;
        if (top - this.GetComponent<Collider2D>().bounds.min.y > 0.75)
            (_imovement as LinearMovement).Xspeed = -(_imovement as LinearMovement).Xspeed;
    }

    private IMovement initMovement()
    {
        switch (_type)
        {
            case ItemType.Mushroom:
                return new LinearMovement(_speed.x, _speed.y, _speed.z);
            case ItemType.FireFlower:
                return null;
            case ItemType.Amazing_Star:
                return null;
            default:
                return null;

        }
    }


}
