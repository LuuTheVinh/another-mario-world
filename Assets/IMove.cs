using UnityEngine;
using System.Collections;
using Assets;

using System.Text;

namespace Assets
{
    public interface IMove
    {
        void move(GameObject go);
    }
    public class LinearMove : IMove
    {
        public void move(GameObject go)
        {
            var trans = go.GetComponent<Transform>();
            Bullet bullet = go.GetComponent<Bullet>();
            float x = trans.position.x;
            x += bullet._speed;
            trans.position = trans.position + Vector3.right * bullet._speed;
        }
    }

    //public class SinMove : IMove
    //{
    //    private float _radian = 0.0f;
    //    private Vector3 _rootPosition;

    //    public SinMove(GameObject go)
    //    {
    //        _rootPosition = go.transform.position;

    //    }
    //    public void move(GameObject go)
    //    {
    //        var trans = go.GetComponent<Transform>();
    //        Bullet bullet = go.GetComponent<Bullet>();

    //        _radian += bullet._rad * Mathf.PI;
    //        var veloc = -bullet._amp * Mathf.Sin(_radian);

    //        trans.position = new Vector3(
    //            trans.position.x + bullet._speed,
    //            _rootPosition.y + veloc,
    //            trans.position.z);
    //    }
    //}
}
