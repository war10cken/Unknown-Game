using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    enum Direction
    {
        Up,
        Down,
        Right,
        Left,
        Forward,
        Back,
        One,
        Zero
    }
    
    public class FallTrigger : MonoBehaviour
    {
        [SerializeField] private Direction _direction;
        [SerializeField] private int _amount;
        
        private readonly List<Vector3> _vector3s = new()
        {
            Vector3.up,
            Vector3.down,
            Vector3.right,
            Vector3.left,
            Vector3.forward,
            Vector3.back,
            Vector3.one,
            Vector3.zero
        };

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;

                if (other.TryGetComponent(out BoxCollider boxCollider))
                {
                    boxCollider.enabled = false;
                    
                    for (int i = 0; i < _amount; i++)
                    {
                        other.transform.position += _vector3s[(int) _direction];
                    }

                    boxCollider.enabled = true;

                    if (other.TryGetComponent(out Player.Player player))
                    {
                        rigidbody.constraints =
                            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                    }
                    else
                    {
                        rigidbody.constraints = RigidbodyConstraints.None;
                    }
                }
            }
        }
    }
}
