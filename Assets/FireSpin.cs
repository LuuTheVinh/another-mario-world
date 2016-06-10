using UnityEngine;
using System.Collections;
using Assets;

public class FireSpin : Enemy, ISwitchable {

    private bool _isSwitch;
    protected override void Update()
    {
        base.Update();
        if (_isSwitch && this.GetComponent<SpriteRenderer>().isVisible == true)
        {
            this.GetComponent<Animator>().SetInteger("status", 1);
            _isSwitch = false;
        }
    }

    public void _switch_on()
    {
        _isSwitch = true;
    }

    public override void killPlayer(GameObject obj)
    {
        if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Off"))
            return;
        base.killPlayer(obj);
    }
}
