using Guns.PhysicalGun;
using Guns.GravityGun;
using UnityEngine;

public class GunsController : MonoBehaviour
{
    public GameObject _Player;
    void Update()
    {
        if (Input.GetButtonDown("1"))
        {
            GetComponent<DeformGun>().enabled = true;
            GetComponent<EtherCrystalGun>().enabled = false;
            _Player.GetComponent<PhysicalGun>().enabled = false;
            _Player.GetComponent<GravityGun>().enabled = false;
        }
        else if (Input.GetButtonDown("2"))
        {
            GetComponent<DeformGun>().enabled = false;
            GetComponent<EtherCrystalGun>().enabled = true;
            _Player.GetComponent<PhysicalGun>().enabled = false;
            _Player.GetComponent<GravityGun>().enabled = false;
        }
        else if (Input.GetButtonDown("3"))
        {
            GetComponent<DeformGun>().enabled = false;
            GetComponent<EtherCrystalGun>().enabled = false;
            _Player.GetComponent<PhysicalGun>().enabled = true;
            _Player.GetComponent<GravityGun>().enabled = false;
        }else if (Input.GetButtonDown("4"))
        {
            GetComponent<DeformGun>().enabled = false;
            GetComponent<EtherCrystalGun>().enabled = false;
            _Player.GetComponent<PhysicalGun>().enabled = false;
            _Player.GetComponent<GravityGun>().enabled = true;
        }
    }
}
