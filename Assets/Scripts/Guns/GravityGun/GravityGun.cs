using UnityEngine;

namespace Guns.GravityGun
{
    public class GravityGun : Gun
    {
        private void Update()
        {
            var item = GrabItem();
            
            if (Input.GetKey(KeyCode.R))
            {
                item.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            
            if (Input.GetKey(KeyCode.F))
            {
                item.transform.Rotate(0, 0.6f, 0);
            }
            
            if (Input.GetKey(KeyCode.E))
            {
                item.transform.Rotate(0, 0, 0.6f);
            }
            
            if (Input.GetKey(KeyCode.Q))
            {
                item.transform.Rotate(0.6f, 0, 0);
            }
        }
    }
}
