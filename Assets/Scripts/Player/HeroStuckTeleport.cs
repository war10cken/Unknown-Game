using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HeroStuckTeleport : MonoBehaviour
{
    public Vector3 TeleportPosition = new(0.5f,-1f,-0.4f);
    public GameObject EtherCrystalGun;
    public GameObject TargetObject;

    private BoxCollider _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        transform.position = TargetObject.transform.position;
        _boxCollider.enabled = EtherCrystalGun.GetComponent<EtherCrystalGun>().TimeCounter == 0;
        //Debug.Log(EtherCrystalGun.activeInHierarchy);
    }
    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.layer == 2)
        {
            collision.gameObject.transform.position = TeleportPosition;
            Debug.Log("Teleported");
        }
    }
}
