using UnityEngine;
using System.Collections;

public class DoorOut : MonoBehaviour {

    public GameObject _player;

    private Color _colorStart;
    private Color _colorEnd;

    // Chờ 1 giây mở cửa.
    private bool _isActive;

    private const float _duration = 0.6f;
    private float _countTime;
    private bool _isFinishFade;
	// Use this for initialization
	void Start () {

        _colorStart = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _colorEnd = Color.white;
        _player.SetActive(false);
        _player.GetComponent<Transform>().position = this.transform.position;
        _player.GetComponent<Transform>().localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        _countTime = 0.0f;
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
        _countTime += Time.deltaTime;
        var renderer = _player.GetComponent<SpriteRenderer>();
        if (_countTime <= _duration)
        {
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
