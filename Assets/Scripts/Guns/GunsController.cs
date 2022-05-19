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
    [SerializeField] private TMP_Text _hotKeys;

    private List<Gun> _guns = new();

    private void Start()
    {
        _gravityGun.gameObject.SetActive(true);
        _gunName.text = "Текущее оружее: " + _gravityGun.Name;
        _hotKeys.text = _gravityGun.HotKeys;
        
        _guns.AddRange(new List<Gun>
        {
            _gravityGun, _physicalGun, 
            _deformationGun, _etherCrystalGun
        });
    }

    private void DisableGuns()
    {
        foreach (var gun in _guns)
        {
            gun.gameObject.SetActive(false);
        }
    }

    private void SetWeapon(Component gun, string weaponName, string hotKeys)
    {
        DisableGuns();
        gun.gameObject.SetActive(true);
        _gunName.text = "Текущее оружее: " + weaponName;
        _hotKeys.text = hotKeys;
    }
    
    private void Update()
    {
        _gunName.characterSpacing -= 0.0000000001f;
        _gunName.characterSpacing += 0.0000000001f;
        
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SetWeapon(_gravityGun, _gravityGun.Name, _gravityGun.HotKeys);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            SetWeapon(_physicalGun, _physicalGun.Name, _physicalGun.HotKeys);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            SetWeapon(_etherCrystalGun, _etherCrystalGun.Name, _etherCrystalGun.HotKeys);
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            SetWeapon(_deformationGun, _deformationGun.Name, _deformationGun.HotKeys);
        }
    }
}
