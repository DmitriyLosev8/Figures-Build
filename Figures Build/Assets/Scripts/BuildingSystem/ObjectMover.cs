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

        public void StartMove(PickingObject currentObject, Transform parent)
        {
            _isObjectSeted = false;

            if (currentObject != null)
                StartCoroutine(Move(currentObject, parent));
        }
      
        public void StoptMove()
        {
            _isObjectSeted = true;
            StopCoroutine(Move(_object, _spotOfObject));
        }

        private IEnumerator Move(PickingObject currentObject, Transform parent)
        {
            _object = currentObject;
            _spotOfObject = parent;

            while (!_isObjectSeted)
            {  
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, _setUpDistance))                  
                    _object.ChangePosition(_spotOfObject, hitInfo, _timeToTranslate);
                else
                    _object.TakeParent(_spotOfObject);

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
    }
}