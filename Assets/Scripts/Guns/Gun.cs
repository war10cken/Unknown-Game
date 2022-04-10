using UnityEngine;
using UnityEngine.UI;
using Selectable = Utilities.Selectable;

namespace Guns
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected Slider Energy;

        protected RaycastHit Hit;
        protected Ray Ray;
        protected const float MaxGrabDistance = 30;
        protected float RightMouseClick;
        protected Rigidbody ItemRigidbody;

        private Selectable _item;
        private bool _lockTarget;

        protected Selectable GrabItem()
        {
            if (!_lockTarget)
            {
                Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
        
            float leftMouseClick = Input.GetAxisRaw("Fire1");
            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");

            if (leftMouseClick > 0 && Energy.value != 0)
            {
                if (Physics.Raycast(Ray, out Hit, MaxGrabDistance))
                {
                    Vector3 targetDirection = Hit.point - transform.position;
                
                    Debug.DrawRay(transform.position, targetDirection, Color.red, 0.01f);

                    transform.LookAt(Hit.point);

                    if (Hit.collider.gameObject.TryGetComponent(out Selectable item))
                    {
                        Energy.value -= 0.003f;
                        _lockTarget = true;

                        if (horizontal > 0)
                        {
                            item.transform.parent.Translate(0.07f, 0, 0);
                        }
                    
                        if (horizontal < 0)
                        {
                            item.transform.parent.Translate(-0.07f, 0, 0);
                        }

                        if (vertical > 0)
                        {
                            item.transform.parent.Translate(0, 0, 0.07f);
                        }

                        if (vertical < 0)
                        {
                            item.transform.parent.Translate(0, 0, -0.07f);
                        }
                    
                        if (item.TryGetComponent(out ItemRigidbody))
                        {
                            RightMouseClick = Input.GetAxisRaw("Fire2");

                            if (RightMouseClick > 0)
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

                        if (_lockTarget)
                        {
                            Ray = new Ray(transform.position, item.transform.position);
                        }
                        
                        _item = item;
                    }

                }
            }

            if (leftMouseClick == 0)
            {
                _lockTarget = false;
                Energy.value += 0.003f;
            }

            return _item;
        }
    }
}
