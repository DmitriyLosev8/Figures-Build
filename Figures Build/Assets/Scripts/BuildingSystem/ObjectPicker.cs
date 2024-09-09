using System;
using UnityEngine;
using Assets.Scripts.BuildingSystem.Objects;

namespace Assets.Scripts.BuildingSystem
{
    internal class ObjectPicker : MonoBehaviour
    {
        [SerializeField] private float _takeDistance;
        [SerializeField] private Transform _spotOfObject;

        private PlayerInput _playerInput;

        public event Action ObjectPicked;

        public PickingObject CurrentPickingObject { get; private set; }
       
        public Transform SpotOfObject => _spotOfObject;
       
        public bool IsPicked { get; private set; }

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Player.Pickup.performed += ctx => PickUp();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        public void PickUp()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, _takeDistance))
            {
                if (hitInfo.collider.TryGetComponent(out PickingObject pickingObject) == false)
                    return;
                else
                {
                    CurrentPickingObject = pickingObject;
                    CurrentPickingObject.TakeParent(_spotOfObject);
                    ObjectPicked?.Invoke();
                    IsPicked = true;
                }
            }
        }
    }
}