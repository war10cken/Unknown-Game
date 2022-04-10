using UnityEngine;
using Utilities;

namespace Guns.GravityGun
{
    public class GravityGun : Gun
    {
        private Selectable _item;
        
        private void FixedUpdate()
        {
            _item = GrabItem();
            
            if (Input.GetKey(KeyCode.R))
            {
                _item.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            
            if (Input.GetKey(KeyCode.F))
            {
                _item.transform.Rotate(0, 0.6f, 0);
            }
            
            if (Input.GetKey(KeyCode.E))
            {
                _item.transform.Rotate(0, 0, 0.6f);
            }
            
            if (Input.GetKey(KeyCode.Q))
            {
                _item.transform.Rotate(0.6f, 0, 0);
            }
        }
    }
}
