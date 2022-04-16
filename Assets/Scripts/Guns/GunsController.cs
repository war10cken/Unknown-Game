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
    [SerializeField] private GameObject _deformationGun;
    [SerializeField] private GameObject _etherCrystalGun;

    [SerializeField] private TMP_Text _gunName;

    private List<GameObject> _guns = new();

    private void Start()
    {
        _gravityGun.gameObject.SetActive(true);
        _gunName.text = "Текущее оружее: " + _gravityGun.GetName;
        
        _guns.AddRange(new List<GameObject>
        {
            _gravityGun.gameObject, _physicalGun.gameObject, 
            _deformationGun, _etherCrystalGun
        });
    }

    private IEnumerator DisableGuns()
    {
        foreach (var gun in _guns)
        {
            gun.SetActive(false);
        }

        yield return null;
    }

    private void SetWeapon(GameObject gun, string weaponName)
    {
        StartCoroutine(DisableGuns());
        gun.gameObject.SetActive(true);
        _gunName.text = "Текущее оружее: " + weaponName;
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SetWeapon(_gravityGun.gameObject, _gravityGun.GetName);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            SetWeapon(_physicalGun.gameObject, _physicalGun.GetName);
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
