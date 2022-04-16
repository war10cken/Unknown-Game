#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Guns
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected Slider Energy;
        [SerializeField] protected string Name;
        
        private float _horizontal;
        private Selectable _item;
        private float _leftMouseClick;
        private float _rightMouseClick;
        private float _vertical;

        protected const float MaxGrabDistance = 30;
        protected RaycastHit Hit;
        protected Rigidbody ItemRigidbody;
        protected Ray Ray;

        public string GetName => Name;

        protected Selectable GrabItem()
        {
            Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            _leftMouseClick = Input.GetAxisRaw("Fire1");
            _vertical = Input.GetAxisRaw("Vertical");
            _horizontal = Input.GetAxisRaw("Horizontal");

            if (_leftMouseClick > 0 && Energy.value != 0)
                if (Physics.Raycast(Ray, out Hit, MaxGrabDistance))
                    if (Hit.collider.gameObject.TryGetComponent(out Selectable item))
                    {
                        Energy.value -= 0.003f;

                        if (_horizontal > 0) item.transform.parent.Translate(0, 0, 0.07f);

                        if (_horizontal < 0) item.transform.parent.Translate(0, 0, -0.07f);

                        if (_vertical > 0) item.transform.parent.Translate(-0.07f, 0, 0);

                        if (_vertical < 0) item.transform.parent.Translate(0.07f, 0, 0);

                        if (item.TryGetComponent(out ItemRigidbody))
                        {
                            _rightMouseClick = Input.GetAxisRaw("Fire2");

                            if (_rightMouseClick > 0) ItemRigidbody.constraints = RigidbodyConstraints.FreezeAll;

                            if (Input.GetKey(KeyCode.T)) ItemRigidbody.constraints = RigidbodyConstraints.None;
                        }

                        float x = Input.GetAxisRaw("Mouse ScrollWheel") * 5f / ItemRigidbody.mass;
                        float y = Input.GetAxisRaw("Mouse Y") / 2f / ItemRigidbody.mass;
                        float z = Input.GetAxisRaw("Mouse X") / 2f / ItemRigidbody.mass;

                        // item.transform.rotation = transform.parent.rotation;

                        item.transform.parent.Translate(-x / 1.5f, y / 1.5f, z / 1.5f);
                        
                        _item = item;
                    }

            if (_leftMouseClick == 0) Energy.value += 0.003f;

            return _item;
        }
    }
}
