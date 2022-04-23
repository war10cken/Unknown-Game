using UnityEngine;
using UnityEngine.UI;

public class EtherCrystalGun : MonoBehaviour
{
    [SerializeField] private Slider _energy;
    
    RaycastHit hit;
    [Header("Speed")]
    public float ColorSpeed = 0.05f;

    Color Color;
    float Store;

    GameObject hittedObj;

    private float TimeCounter = 0;
    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit) && hit.collider.gameObject.layer == 6 && Input.GetButton("Fire1") && _energy.value != 0)
        {
            hittedObj = hit.collider.gameObject;

            Color = hit.collider.gameObject.GetComponent<Renderer>().material.color;
            Color.a = 0.2f;
            hit.collider.gameObject.GetComponent<Renderer>().material.color = Color;

            hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;

            hit.collider.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (hittedObj != null)
        {
            if (hittedObj.GetComponent<Renderer>().material.color.a == 0.2f)
            {
                TimeCounter += 1 / Time.fixedDeltaTime;
                //Debug.Log(TimeCounter);
                if (TimeCounter > 10000 && hittedObj != null)
                {
                    hittedObj.GetComponent<BoxCollider>().enabled = true;

                    hittedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                    Color.a = 1;
                    hittedObj.GetComponent<Renderer>().material.color = Color;
                    hittedObj = null;
                    TimeCounter = 0;
                }
            }
        }
    }
}

/*
        if (Physics.Raycast(ray.origin, ray.direction, out hit) && hit.collider.gameObject.layer == 6 && Input.GetButton("Fire1") && _energy.value != 0)
        {
            //
            if (obj != hit.collider.gameObject)
            {
                Color = hit.collider.gameObject.GetComponent<Renderer>().material.color;
                Color.a = 0.2f;
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color;
            }
            //
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
        */