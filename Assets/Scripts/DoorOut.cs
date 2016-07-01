using UnityEngine;
using System.Collections;

public class DoorOut : MonoBehaviour {

    public GameObject _player;

    public GameObject _cam;
    // Chờ 1 giây mở cửa.
    private bool _isActive;

    private const float _duration = 1.0f;
    private float _countTime;
    private bool _isFinishFade;

    public SoundManager.eIdentify _background;
	// Use this for initialization
	void Start () {

        if (_player == null)
        {
            _player = getPlayerFromPreviousScene();
            setFollowCamera(_player);
            setCheckPoint(_player);
        }
        _player.GetComponent<Transform>().position = this.transform.position;
        _countTime = 0.0f;
        _isFinishFade = false;

        var renderer = _player.GetComponent<SpriteRenderer>();
        renderer.color = new Color(
            renderer.color.r,
            renderer.color.g,
            renderer.color.b,
            0.0f);
        var soundmanager = SoundManager.getinstance();
        if (soundmanager != null)
        {
            soundmanager.StopAll();
            soundmanager.Play(_background);
        }
	}

    private void setCheckPoint(GameObject _player)
    {
        _player.GetComponent<Mario>().CheckPoint = this.gameObject;
    }

    private void setFollowCamera(GameObject player)
    {
        if (_cam != null)
        {
            _cam.GetComponent<FollowCamera>().target = player.transform;
        }
    }

    private GameObject getPlayerFromPreviousScene()
    {
        var objs = GameObject.Find("Controller").GetComponent<SceneController>()._nonDestroyObjects;
        foreach (var item in objs)
        {
            if (item.tag == "Player")
            {
                return item;
            }
        }
        return null;

    }
	
	// Update is called once per frame
	void Update () {
        Invoke("activeMe", 1);


        if (_isActive == false)
            return;
        _player.SetActive(true);
        if (_isFinishFade == false)
            FadeOutObject();
        else
            this.enabled = false;

	}

    private void activeMe()
    {
        if (_isActive == false)
            this.GetComponentInChildren<Animator>().SetTrigger("open");

        _isActive = true;
    }

    private void FadeOutObject()
    {
        //Debug.Log("Fade Out");
        _countTime += Time.deltaTime;
        var renderer = _player.GetComponent<SpriteRenderer>();
        if (_countTime <= _duration)
        {
            //Debug.Log("Fade...");
            renderer.color = new Color(
                renderer.color.r,
                renderer.color.g,
                renderer.color.b,
                _countTime / _duration);
        }
        else
        {
            renderer.color = new Color(
                renderer.color.r,
                renderer.color.g,
                renderer.color.b,
                1.0f);
            _isFinishFade = true;
        }

    }
}
