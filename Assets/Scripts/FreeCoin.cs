using UnityEngine;
using System.Collections;

public class FreeCoin : Item {

	// Use this for initialization
	protected override void Start () {
	
	}
	
	// Update is called once per frame
	protected override void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        if (tag == "Player")
        {
            // Bỏ tiền vô túi nè.
            Destroy(this.gameObject);
            var soundmanager = SoundManager.getinstance();
            if (soundmanager != null)
                soundmanager.Play(SoundManager.eIdentify.coinhit);

            var controller = GameObject.Find("/Controller");
            if (controller != null)
            {
                controller.GetComponent<SceneController>().upCoin();
            }
        }
    }
}
