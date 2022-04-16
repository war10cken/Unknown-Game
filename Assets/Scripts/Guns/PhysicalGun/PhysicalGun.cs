#region

using UnityEngine;

#endregion

namespace Guns.PhysicalGun
{
    public class PhysicalGun : Gun
    {
        private float _forcePower;

        private void FixedUpdate()
        {
            var item = GrabItem();
            
            if (Input.GetKey(KeyCode.Q)) _forcePower += 7;

            if (Input.GetKey(KeyCode.E) && _forcePower != 0)
            {
                ItemRigidbody.constraints = RigidbodyConstraints.None;
                ItemRigidbody.AddForce(transform.forward * _forcePower, ForceMode.Force);
                Energy.value -= _forcePower / 1000;
            
                Selectable.SetDamage(_forcePower / 50, item);
                
                _forcePower = 0;
            }
        }
    }
}
