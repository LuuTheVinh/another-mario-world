using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject CoinValueUI;
    public GameObject LifeValueUI;
    public GameObject[] WeaponUI;

    private int _currentCoin = 0;
    private int _currentLife = 3;
    private Text _coinValue;

    private Mario.eWeapontype _weaponType;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	  
	}

    public void UpdateCoin(int value = 1)
    {
        _currentCoin += value;
        if (_currentCoin >= 150)
        {
            _currentCoin %= 120;
            setCurrentLife(_currentCoin / 120);
        }

        updateCoinUI();
    }

    public void UpdateWeaponUI(Mario.eWeapontype type)
    {
        foreach (var item in WeaponUI)
        {
            item.SetActive(false);
        }

        switch (type)
        {
            case Mario.eWeapontype.none:
                {
                    break;
                }
            case Mario.eWeapontype.fire:
                {
                    WeaponUI[1].SetActive(true);
                    break;
                }
            case Mario.eWeapontype.boomerang:
                {
                    WeaponUI[0].SetActive(true);
                    break;
                }
            default:
                break;
        }
    }

    public void UpdateLife(int life)
    {
        _currentLife = life;
        LifeValueUI.GetComponent<Text>().text = _currentLife.ToString();
    }

    private void updateCoinUI()
    {
        CoinValueUI.GetComponent<Text>().text = _currentCoin.ToString();
    }

    public void setCurrentCoin(int value)
    {
        this._currentCoin = value;
        updateCoinUI();
    }

    public void setCurrentLife(int value)
    {
        this._currentLife = value;
        UpdateLife(value);
    }
}
