using Guns;
using UnityEngine;
using UnityEngine.UI;

public class EtherCrystalGun : Gun
{
    //
    // [SerializeField] private Slider _energy;
    Color Color;
    float Store;
    //
    RaycastHit hit;
    [Header("Speed")]
    public float ColorSpeed = 0.05f;
    [Header("GameObjects")]
    GameObject hittedObj;
    [Header("Timer")]
    public float TimeCounter = 0;
    [Header("ShowingRay")]
    public GameObject BeamGameObject;
    public GameObject RayOriginMark;
    LineRenderer GunBeam;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit) && hit.collider.gameObject.layer == 10 && Input.GetButton("Fire1") && _energy.value != 0)
        {
            ShowLaser();

            hittedObj = hit.collider.gameObject;

            Color = hit.collider.gameObject.GetComponent<Renderer>().material.color;
            Color.a = 0.2f;
            hit.collider.gameObject.GetComponent<Renderer>().material.color = Color;

            hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;

            hit.collider.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (hittedObj != null)
        {
            //
            GunBeam.SetPosition(0, RayOriginMark.transform.position);
            //
            if (hittedObj.GetComponent<Renderer>().material.color.a == 0.2f)
            {
                TimeCounter += 1 / Time.fixedDeltaTime;
                //Debug.Log(TimeCounter);
                if (TimeCounter > 10000 )// && hittedObj != null)
                {
                    hittedObj.GetComponent<BoxCollider>().enabled = true;

                    hittedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                    Color.a = 1;
                    hittedObj.GetComponent<Renderer>().material.color = Color;
                    hittedObj = null;
                    TimeCounter = 0;
                    //
                    DestructLaser();
                    //
                }
            }
        }
    }
    void ShowLaser()
    {
        GunBeam = BeamGameObject.GetComponent<LineRenderer>();

        GunBeam.startWidth = 0.2f;
        GunBeam.endWidth = 0.1f;

        GunBeam.useWorldSpace = true;

        GunBeam.SetPosition(0, RayOriginMark.transform.position);
        GunBeam.SetPosition(1, hit.point);
        //Debug.Log(hit.point);   
    }
    void DestructLaser()
    {
        BeamGameObject.GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
        BeamGameObject.GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
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
//|| gameObject.activeSelf == false