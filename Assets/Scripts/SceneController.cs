using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

    public GameObject[] _nonDestroyObjects;

    void Awake()
    {
        foreach (GameObject obj in _nonDestroyObjects)
	    {
            Object.DontDestroyOnLoad(obj);
            Object.DontDestroyOnLoad(this.gameObject);		    
	    }
    }
}
