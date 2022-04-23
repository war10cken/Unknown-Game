#region

using System;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Guns
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected Slider Energy;
        [SerializeField] protected string Name;
        [SerializeField] private Material _rayMaterial;
        [SerializeField] private GameObject _rayOriginMark;
        [SerializeField] private float _beamDisappearedTime;
        
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

        private GameObject _beam;

        private void Awake()
        {
            _beam = new GameObject("Beam");
            _beam.AddComponent<LineRenderer>();
            _beam.SetActive(false);
        }

        protected void TrackMouse(RaycastHit hit)
        {
            Vector3 direction = hit.point - transform.position;
            Debug.Log(direction);
            transform.rotation = Quaternion.LookRotation(direction);
        }
        
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
                        ShowBeam();
                        _beam.SetActive(true);

                        Energy.value -= 0.003f;
                        
                        TrackMouse(Hit);
                        
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

                        item.transform.parent.Translate(-x * 1.2f, y * 1.2f, z * 1.2f);

                        _rayOriginMark.transform.rotation = transform.rotation;
                        
                        // _rayOriginMark.transform.Translate(0, -transform.rotation.x, 0);
                        
                        _item = item;
                    }

            if (_leftMouseClick == 0)
            {
                Energy.value += 0.003f;
                _beam.SetActive(false);
            }

            return _item;
        }
        
        private void ShowBeam()
        {
            var gunBeam = _beam.GetComponent<LineRenderer>();
            gunBeam.material = _rayMaterial;
            gunBeam.startWidth = 0.1f;
            gunBeam.endWidth = 0.1f;
            gunBeam.useWorldSpace = true;
            gunBeam.SetPosition(0, _rayOriginMark.transform.position);
            gunBeam.SetPosition(1, Hit.point);
        }
    }
}
