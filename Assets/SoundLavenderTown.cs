using UnityEngine;
using System.Collections;
using Assets;

public class SoundLavenderTown : MonoBehaviour, ISwitchable
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void _switch_on()
    {
        var soundmanager = SoundManager.getinstance();
        if (soundmanager != null)
        {
            soundmanager.Play(SoundManager.eIdentify.lavender);
            soundmanager.Stop(SoundManager.eIdentify.background);
        }
    }
}
