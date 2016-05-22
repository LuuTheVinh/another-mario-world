using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        _camera = this.GetComponent<Camera>();
    }

    public float dampTime = 0.15f;
    public Vector2 Max;
    public Vector2 Min;
    private Vector3 velocity = Vector3.zero;

    public Transform target;

    private Camera _camera;
    

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = _camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;

            //if (destination.y < 0)
            //    destination.y = 0;
            if (destination.y < Min.y)
                destination.y = Min.y;
            if (destination.y > Max.y)
                destination.y = Max.y;

            if (destination.x < Min.x)
                destination.x = Min.x;
            if (destination.x > Max.x)
                destination.x = Max.x;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            
        }

    }
}
