#region

using System;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Guns.PhysicalGun
{
    public class PhysicalGun : Gun
    {
        [SerializeField] private Slider _forcePower;
        [SerializeField] private GameObject _forcePowerPanel;

        private void OnEnable()
        {
            _forcePowerPanel.SetActive(true);
        }

        private void OnDisable()
        {
            _forcePowerPanel.SetActive(false);
        }

        private void FixedUpdate()
        {
            var item = GrabItem();
            
            if (Input.GetKey(KeyCode.Q)) _forcePower.value += 0.005f;

            if (Input.GetKey(KeyCode.E) && _forcePower.value != 0)
            {
                ItemRigidbody.constraints = RigidbodyConstraints.None;
                ItemRigidbody.AddForce(transform.forward * _forcePower.value * 1000, ForceMode.Force);
                _energy.value -= _forcePower.value;
                _forcePower.value = 0;

                // Selectable.SetDamage(_forcePower.value * 10, item);
            }
        }
    }
}
