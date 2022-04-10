using DG.Tweening;
using TMPro;
using UnityEngine;
using Utilities;

namespace Guns.PhysicalGun
{
    public class PhysicalGun : Gun
    {
        [SerializeField] private TMP_Text _forcePowerText;
        
        private float _forcePower;
        private bool _lockTarget;
        private Selectable _item;
    
        private void FixedUpdate()
        {
            _item = GrabItem();
            
            if (Input.GetKey(KeyCode.Q))
            {
                _forcePower += 7;
                // _forcePowerText.text = $"Force Power - {_forcePower / 1000}";
            }
            
            if (RightMouseClick > 0 && _forcePower != 0)
            {
                ItemRigidbody.constraints = RigidbodyConstraints.None;
                ItemRigidbody.AddForce(transform.forward * _forcePower, ForceMode.Force);
                Energy.value -= _forcePower / 1000;
            
                Selectable.SetForcePower(_forcePower / 50, _item);
                
                _forcePower = 0;
                // _forcePowerText.text = "Force Power - 0";
            }
        }
    }
}
