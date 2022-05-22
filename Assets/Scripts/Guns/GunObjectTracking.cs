using UnityEngine;

public class GunObjectTracking : MonoBehaviour
{
    RaycastHit Hit;
    LayerMask Mask;
    public GameObject TargetObject;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Mask = ~LayerMask.GetMask("NoneCollision");
        if ( Physics.Raycast(ray, out Hit, Mathf.Infinity, Mask) )
        {
            TargetObject.transform.position = Hit.point;
            Debug.DrawRay(ray.origin, ray.direction * 10000f, Color.black);
        }
    }
}
