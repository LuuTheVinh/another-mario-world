using UnityEngine;
using System.Collections;
using Assets;

public class HideGate : MonoBehaviour, ISwitchable {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void _switch_on()
    {
        appear();
    }

    private void appear()
    {
        var children = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            child.gameObject.SetActive(true);
        }
    }
}
