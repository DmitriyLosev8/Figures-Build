using System;
using System.Collections;
using UnityEngine;
using Assets.Scripts.BuildingSystem.Objects;

namespace Assets.Scripts.BuildingSystem
{
    internal class ObjectMover : MonoBehaviour
    {
        [SerializeField] private float _setUpDistance;

        private Transform _spotOfObject;
        private PickingObject _object;
        private PlayerInput _playerInput;
        private bool _isObjectSeted = false;
        private float valueOfRotate = 45;
        private float _timeToTranslate = 6;

        public event Action ObjectSeted;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Player.Build.performed += ctx => SetUp();
            _playerInput.Player.TurnRight.performed += ctx => RotateRight();
            _playerInput.Player.TurnLeft.performed += ctx => RotateLeft();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        public void StartMove(PickingObject currentObject, Transform spot)
        {
            _isObjectSeted = false;

            if (currentObject != null)
                StartCoroutine(Move(currentObject, spot));
        }
      
        public void StoptMove()
        {
            _isObjectSeted = true;
            StopCoroutine(Move(_object, _spotOfObject));
        }

        private IEnumerator Move(PickingObject currentObject, Transform spotOfObject)
        {
            _object = currentObject;
            _spotOfObject = spotOfObject;

            while (!_isObjectSeted)
            {
               
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, _setUpDistance))   //Raycast
                {                  
                    //if(hitInfo.collider.gameObject.layer == 0)
                    //    _object.TakeParent(_spotOfObject);
                    //else
                        _object.ChangePosition(_spotOfObject, hitInfo, _timeToTranslate);
                    
                }
                else
                {
                    _object.TakeParent(_spotOfObject);
                }



                //if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, _setUpDistance))
                //{
                //    if (hitInfo.transform.TryGetComponent(out Ground ground))
                //    {
                //        _object.ChangePosition(hitInfo, _timeToTranslate);
                //        //_object.transform.parent = null;
                //        //_object.transform.position = Vector3.Lerp(_object.transform.position, hitInfo.point, _timeToTranslate * Time.deltaTime);
                //    }
                //    else if (hitInfo.transform.TryGetComponent(out PickingObject pickingObject) == false)
                //    {
                //        _object.TakeParent(_spotOfObject);
                //    }
                //}
                //else
                //{
                //    _object.TakeParent(_spotOfObject);
                //}
                yield return null;
            }
        }
                 
        private void RotateRight()
        {
            if (_object != null)
            {
                _object.transform.Rotate(0, valueOfRotate, 0);
            }
        }

        private void RotateLeft()
        {
            if (_object != null)
            {
                _object.transform.Rotate(0, -valueOfRotate, 0);
            }
        }

        private void SetUp()
        {
            if (_object != null)
            {
                if (_object.transform.parent == null && _object.IsAvailableToSet)
                {
                    StoptMove();
                    _object.SetPosition();
                    ObjectSeted?.Invoke();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * _setUpDistance);
        }
    }
}