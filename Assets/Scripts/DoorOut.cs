using UnityEngine;
using System.Collections;

public class DoorOut : MonoBehaviour {

    public GameObject _player;

    // Chờ 1 giây mở cửa.
    private bool _isActive;

    private const float _duration = 1.0f;
    private float _countTime;
    private bool _isFinishFade;
	// Use this for initialization
	void Start () {

       // _player.SetActive(false);
        _player.GetComponent<Transform>().position = this.transform.position;
        _countTime = 0.0f;
        _isFinishFade = false;

        var renderer = _player.GetComponent<SpriteRenderer>();

        renderer.color = new Color(
            renderer.color.r,
            renderer.color.g,
            renderer.color.b,
            0.0f);

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
            Destroy(this);

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
