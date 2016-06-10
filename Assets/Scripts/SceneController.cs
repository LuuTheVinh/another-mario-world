using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {
    // scenecotroller cho bắt đầu màn chơi.
    public GameObject[] _nonDestroyObjects;
    public float _botGame;

    public int _life;
    public int _coin;

    void Awake()
    {
        foreach (GameObject obj in _nonDestroyObjects)
	    {
            Object.DontDestroyOnLoad(obj);
	    }
        Object.DontDestroyOnLoad(this.gameObject);
        Object.DontDestroyOnLoad(this);

        //_player.SetActive(false);
        GetComponent<AudioSource>().Play();
    }

    public void downLife()
    {
        --_life;
        setLifeText(_life);
    }

    public void upLife()
    {
        ++_life;
        setLifeText(_life);

    }

    public void setCoin(int coin)
    {
        _coin = coin;
        setCoinText(_coin);
    }
    
    public void upCoin()
    {
        ++_coin;
        setCoinText(_coin);
    }

    public static void setLifeText(int life)
    {
        var bullet_pannel = GameObject.Find("/Canvas");
        if (bullet_pannel != null)
        {
            bullet_pannel.transform.FindChild("life/Text").gameObject.GetComponent<Text>().text = "x " + life.ToString();
        }
    }

    public static void setCoinText(int coin)
    {
        var bullet_pannel = GameObject.Find("/Canvas");
        if (bullet_pannel != null)
        {
            bullet_pannel.transform.FindChild("coin/Text").gameObject.GetComponent<Text>().text = "x " + coin.ToString();
        }
    }

    public static void setBulletPanelActive(bool isActive)
    {
        var bullet_pannel = GameObject.Find("/Canvas");
        if (bullet_pannel != null)
        {
            bullet_pannel.transform.FindChild("bullet").gameObject.SetActive(isActive);
        }
    }

    public static void setBoomerangPanelActive(bool p)
    {
        //throw new System.NotImplementedException();
    }
}
