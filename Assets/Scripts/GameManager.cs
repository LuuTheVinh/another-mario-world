using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject CoinValueUI;

    private int _currentCoin = 0;
    private Text _coinValue;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateCoin(int value = 1)
    {
        _currentCoin += value;

        updateUI();
    }

    private void updateUI()
    {
        CoinValueUI.GetComponent<Text>().text = _currentCoin.ToString();
    }
}
