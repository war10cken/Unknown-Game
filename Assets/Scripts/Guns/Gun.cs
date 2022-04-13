using UnityEngine;
using UnityEngine.UI;
using Selectable = Guns.Selectable;

namespace Guns
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected Slider Energy;

        protected RaycastHit Hit;
        protected Ray Ray;
        protected Rigidbody ItemRigidbody;

        protected const float MaxGrabDistance = 30;
        
        private Selectable _item;
        private float _rightMouseClick;
        private float _leftMouseClick;
        private float _vertical;
        private float _horizontal;

        protected Selectable GrabItem()
        {
            Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
            _leftMouseClick = Input.GetAxisRaw("Fire1");
            _vertical = Input.GetAxisRaw("Vertical");
            _horizontal = Input.GetAxisRaw("Horizontal");

            if (_leftMouseClick > 0 && Energy.value != 0)
            {
                if (Physics.Raycast(Ray, out Hit, MaxGrabDistance))
                {
                    transform.LookAt(Hit.point);

                    if (Hit.collider.gameObject.TryGetComponent(out Selectable item))
                    {
                        Energy.value -= 0.003f;

                        if (_horizontal > 0)
                        {
                            item.transform.parent.Translate(0.07f, 0, 0);
                        }
                    
                        if (_horizontal < 0)
                        {
                            item.transform.parent.Translate(-0.07f, 0, 0);
                        }

                        if (_vertical > 0)
                        {
                            item.transform.parent.Translate(0, 0, 0.07f);
                        }

                        if (_vertical < 0)
                        {
                            item.transform.parent.Translate(0, 0, -0.07f);
                        }
                    
                        if (item.TryGetComponent(out ItemRigidbody))
                        {
                            _rightMouseClick = Input.GetAxisRaw("Fire2");

                            if (_rightMouseClick > 0)
                            {
                                ItemRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                            }

                            if (Input.GetKey(KeyCode.T))
                            {
                                ItemRigidbody.constraints = RigidbodyConstraints.None;
                            }
                        }

                        if (item)
                        {
                            item.transform.parent.Translate(Input.GetAxisRaw("Mouse X") / 2f / ItemRigidbody.mass,
                                                            Input.GetAxisRaw("Mouse Y") / 2f / ItemRigidbody.mass, 
                                                            Input.GetAxisRaw("Mouse ScrollWheel") * 5f / ItemRigidbody.mass);
                        }
                        
                        _item = item;
                    }

                }
            }

            if (_leftMouseClick == 0)
            {
                Energy.value += 0.003f;
            }

            return _item;
        }
    }
}
