using UnityEngine;

public class HeroStuckTeleport : MonoBehaviour
{
    public Vector3 TeleportPosition = new(0.5f,-1f,-0.4f);
    public GameObject EtherCrystalGun;
    public GameObject TargetObject;
    void Update()
    {
        transform.position = TargetObject.transform.position;
        if (EtherCrystalGun.GetComponent<EtherCrystalGun>().TimeCounter == 0)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
        }
        //Debug.Log(EtherCrystalGun.activeInHierarchy);
    }
    void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.layer == 2)
        {
            collision.gameObject.transform.position = TeleportPosition;
            Debug.Log("Teleported");
        }
    }
}
