using UnityEngine;

public class EtherCrystalGun : MonoBehaviour
{
    RaycastHit hit;
    [Header("Speed")]
    public float ColorSpeed = 0.05f;

    Color Color;

    private GameObject obj;
    void FixedUpdate()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        //PlayerTracking(ray2);
        //GunPosRot();
        //DeformGunTracking();

        if (Physics.Raycast(ray.origin, ray.direction, out hit) && hit.collider.gameObject.layer == 6 && Input.GetButton("Fire1"))
        {
            if (obj != hit.collider.gameObject)
            {
                Color = hit.collider.gameObject.GetComponent<Renderer>().material.color;
                Color.a = 0.2f;
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color;
                Debug.Log(hit.collider.gameObject.GetComponent<Renderer>().material.color.a);
            }

            obj = hit.collider.gameObject;

            hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;

            hit.collider.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }else if (obj != null && !Input.GetButton("Fire1"))
        {
            obj.GetComponent<BoxCollider>().enabled = true;

            obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            Color.a = 1;
            obj.GetComponent<Renderer>().material.color = Color;
            obj = null;     
        }
    }
}