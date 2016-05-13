using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {
    // scenecotroller cho bắt đầu màn chơi.
    public GameObject[] _nonDestroyObjects;

    public GameObject _player;
    void Awake()
    {
        foreach (GameObject obj in _nonDestroyObjects)
	    {
            Object.DontDestroyOnLoad(obj);
            Object.DontDestroyOnLoad(this.gameObject);		    
	    }

        //_player.SetActive(false);

    }
}
