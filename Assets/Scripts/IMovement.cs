using UnityEngine;
using System.Collections;

// created by Ho Hoang Tung
public interface IMovement  {
    void Movement(GameObject item);

}

public class LinearMovement : IMovement
{
    private float _xspeed;


    private float _yspeed;
    private float _zspeed;
    public float Xspeed
    {
        get { return _xspeed; }
        set { _xspeed = value; }
    }
    public LinearMovement(float xspeed, float yspeed, float zspeed = 0)
    {
        _xspeed = xspeed;
        _yspeed = yspeed;
        _zspeed = zspeed;
    }
    public void Movement(GameObject go)
    {
        go.transform.position += new Vector3(
        Xspeed,
        _yspeed,
        _zspeed);
    }
}