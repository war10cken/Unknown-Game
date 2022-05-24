#region

using System;
using UnityEngine;
using UnityEngine.UI;
using Selectable = Entities.Selectable;

#endregion

namespace Guns
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected Slider _energy;
        [SerializeField] protected string _name;
        [SerializeField] private Material _rayMaterial;
        [SerializeField] private GameObject _rayOriginMark;
        
        [TextArea]
        [SerializeField] protected string _hotKeys;
        
        private float _horizontal;
        private Selectable _item;
        private Selectable _currentItem;
        private float _leftMouseClick;
        private float _rightMouseClick;
        private float _vertical;
        private const float _energyValue = 0.003f;

        protected const float MaxGrabDistance = 30;
        protected RaycastHit Hit;
        protected Rigidbody ItemRigidbody;
        protected Ray Ray;

        public string HotKeys => _hotKeys;
        public string Name => _name;

        private GameObject _beam;
        private LineRenderer _gunBeam;

        private void Awake()
        {
            _beam = new GameObject("Beam");
            _beam.AddComponent<LineRenderer>();
            _beam.SetActive(false);
            
            _gunBeam = _beam.GetComponent<LineRenderer>();
            _gunBeam.material = _rayMaterial;
            _gunBeam.startWidth = 0.1f;
            _gunBeam.endWidth = 0.1f;
            _gunBeam.useWorldSpace = false;
            _gunBeam.SetPosition(0, _rayOriginMark.transform.position);
        }

        protected Selectable GrabItem()
        {
            // Ray = _currentItem is null
            //           ? Camera.main.ScreenPointToRay(Input.mousePosition)
            //           : new Ray(transform.position, _currentItem.transform.position);

            Ray = _currentItem is null
                      ? new Ray(_rayOriginMark.transform.position, _rayOriginMark.transform.forward)
                      : new Ray(transform.position, _currentItem.transform.position);

            _leftMouseClick = Input.GetAxisRaw("Fire1");
            _vertical = Input.GetAxisRaw("Vertical");
            _horizontal = Input.GetAxisRaw("Horizontal");
            
            if (_leftMouseClick > 0 && _energy.value != 0)
            {
                if (_currentItem is not null)
                {
                    Cursor.visible = false;
                    
                    ShowBeam(_currentItem);
                    _beam.SetActive(true);
                    
                    _energy.value -= _energyValue;
                    
                    FollowCharacter(_currentItem);
                    
                    DisableGravity(_currentItem);
                    
                    MoveObject(_currentItem);
                }
                
                if (_currentItem is null && Physics.Raycast(Ray, out Hit, MaxGrabDistance * 100f))
                {
                    if (Hit.collider.gameObject.TryGetComponent(out Selectable item))
                    {
                        ShowBeam(Hit);
                        _beam.SetActive(true);

                        _energy.value -= _energyValue;

                        FollowCharacter(item);

                        DisableGravity(item);

                        MoveObject(item);

                        _currentItem = item;
                        _item = item;
                    }
                }
            }

            if (_energy.value == 0)
            {
                _beam.SetActive(false);
            }

            if (_leftMouseClick == 0)
            {
                Cursor.visible = true;
                
                _energy.value += _energyValue;
                _beam.SetActive(false);
                _currentItem = null;
            }

            return _item;
        }

        private void MoveObject(Selectable item)
        {
            float x = Input.GetAxisRaw("Mouse ScrollWheel") * 5f / ItemRigidbody.mass;
            float y = Input.GetAxisRaw("Mouse Y") / 2f / ItemRigidbody.mass;
            float z = Input.GetAxisRaw("Mouse X") / 2f / ItemRigidbody.mass;

            item.transform.parent.Translate(-x * 1.2f, y * 1.2f, z * 1.2f);
        }

        private void DisableGravity(Selectable item)
        {
            if (item.TryGetComponent(out ItemRigidbody))
            {
                _rightMouseClick = Input.GetAxisRaw("Fire2");

                if (_rightMouseClick > 0) ItemRigidbody.constraints = RigidbodyConstraints.FreezeAll;

                if (Input.GetKey(KeyCode.T)) ItemRigidbody.constraints = RigidbodyConstraints.None;
            }
        }

        private void FollowCharacter(Selectable item)
        {
            float x = 0.15f;
            float y = 0;
            float z = 0.15f;
            
            if (_horizontal > 0) item.transform.parent.Translate(0, y, z);

            if (_horizontal < 0) item.transform.parent.Translate(0, y, -z);

            if (_vertical > 0) item.transform.parent.Translate(-x, y, 0);

            if (_vertical < 0) item.transform.parent.Translate(x, y, 0);
        }

        private void ShowBeam(RaycastHit hit)
        {
            _gunBeam.SetPosition(0, _rayOriginMark.transform.position);
            _gunBeam.SetPosition(1, hit.point);
        }
        
        private void ShowBeam(Selectable item)
        {
            _gunBeam.SetPosition(0, _rayOriginMark.transform.position);
            _gunBeam.SetPosition(1, item.transform.position);
        }
    }
}
