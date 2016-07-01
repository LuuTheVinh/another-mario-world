using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    private static SoundManager instance;
    public static SoundManager getinstance()
    {
        return instance;
    }

    public List<AudioSource>  _audio = new List<AudioSource>();
    public enum eIdentify {
        background, coinhit, brickbreak, bonusAppear, bulletbreak, 
        enemydie, gameover, jump, levelup, shoot,
        small, metalhit, extralife, lavender, cannon_attack }
    private List<string> _names = new List<string>() { 
    };

    void Awake()
    {
        UnityEngine.Object.DontDestroyOnLoad(this);
        instance = this;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            _audio.Add(this.transform.GetChild(i).GetComponent<AudioSource>());
        }
    }
	// Use this for initialization
	void Start () {
        //this.gameObject.GetComponent<SoundManager>().Play(SoundManager.eIdentify.background);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play(eIdentify name)
    {
        if (_audio[(int)name].isPlaying)
            _audio[(int)name].Stop();
        _audio[(int) name].Play();
    }

    public void Stop(eIdentify name)
    {
        if (_audio[(int)name].isPlaying)
            _audio[(int)name].Stop();
    }


    public void StopAll()
    {
        foreach (var a in _audio)
        {
            a.Stop();
        }
    }
}
