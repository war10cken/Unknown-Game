using System;
using System.Collections;
using System.Collections.Generic;
using Guns;
using Guns.PhysicalGun;
using Guns.GravityGun;
using TMPro;
using UnityEngine;

public class GunsController : MonoBehaviour
{
    [SerializeField] private Gun _gravityGun;
    [SerializeField] private Gun _physicalGun;
    [SerializeField] private Gun _deformationGun;
    [SerializeField] private Gun _etherCrystalGun;

    [SerializeField] private TMP_Text _gunName;

    private List<Gun> _guns = new();

    private void Start()
    {
        _gravityGun.gameObject.SetActive(true);
        _gunName.text = "Текущее оружее: " + _gravityGun.GetName;
        
        _guns.AddRange(new List<Gun>
        {
            _gravityGun, _physicalGun, 
            _deformationGun, _etherCrystalGun
        });
    }

    private IEnumerator DisableGuns()
    {
        foreach (var gun in _guns)
        {
            gun.gameObject.SetActive(false);
        }

        yield return null;
    }

    private void SetWeapon(Component gun, string weaponName)
    {
        StartCoroutine(DisableGuns());
        gun.gameObject.SetActive(true);
        _gunName.text = "Текущее оружее: " + weaponName;
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SetWeapon(_gravityGun, _gravityGun.GetName);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            SetWeapon(_physicalGun, _physicalGun.GetName);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            SetWeapon(_etherCrystalGun, "Эфирный кристалл");
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            SetWeapon(_deformationGun, "Деформирующая пушка");
        }
    }
}
