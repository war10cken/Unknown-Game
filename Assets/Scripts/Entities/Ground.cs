using System;
using UnityEngine;

namespace Entities
{
    public class Ground : MonoBehaviour
    {
        private void OnCollisionStay(Collision other)
        {
            if (Math.Abs(other.gameObject.transform.position.y - transform.position.y) < 0.1f)
            {
                
            }
        }
    }
}
