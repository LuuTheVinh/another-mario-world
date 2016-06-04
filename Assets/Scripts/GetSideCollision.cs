using UnityEngine;
using System.Collections;

public class GetSideCollision : MonoBehaviour {

    void Start()
    {
    }

    public enum HitDirection { None, Top, Bottom, Left, Right }

    public HitDirection ReturnDirection(GameObject Object, GameObject ObjectHit)
    {
        HitDirection hitDirection = HitDirection.None;
        RaycastHit2D MyRayHit;
        Vector2 direction = (Object.transform.position - ObjectHit.transform.position).normalized;
        
        if (MyRayHit = Physics2D.Raycast(new Vector2(ObjectHit.transform.position.x, ObjectHit.transform.position.y), direction))
        {
            if (MyRayHit.collider != null)
            {
                Vector3 MyNormal = MyRayHit.normal;
                MyNormal = MyRayHit.transform.TransformDirection(MyNormal);

                if (MyNormal == MyRayHit.transform.up) { hitDirection = HitDirection.Top; }
                if (MyNormal == -MyRayHit.transform.up) { hitDirection = HitDirection.Bottom; }
                if (MyNormal == MyRayHit.transform.right) { hitDirection = HitDirection.Right; }
                if (MyNormal == -MyRayHit.transform.right) { hitDirection = HitDirection.Left; }
            }
        }

        return hitDirection;
    }
}
