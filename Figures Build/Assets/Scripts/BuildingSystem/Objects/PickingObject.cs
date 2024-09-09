using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem.Objects
{
    internal abstract class PickingObject : MonoBehaviour
    {
        [SerializeField] private float _distanceToSetUp;

        private MeshRenderer _meshColor;
        protected Rigidbody _rigidbody;
        private Color _redColor = new Color(1, 0, 0, 0.5f);
        private Color _greenColor = new Color(0, 1, 0, 0.5f);
        private Color _whiteColor = new Color(1, 1, 1, 1f);
        
        private float _distanceOfCheckSurface = 10;

        public bool IsPickedUp { get; private set; }

        public bool IsAvailableToSet;
       
        public Collision CurrentCollision { get; private set; }

        private void Awake()
        {
            _meshColor = GetComponentInChildren<MeshRenderer>();
        }

        private void Update()
        {
            if (IsPickedUp)
            {
                ChangeColor();
                CheckAvailableToSet();
            }   
        }

        public void TakeParent(Transform parent)
        {
            IsPickedUp = true;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
        }

        public void SetPosition()
        {
            IsPickedUp = false;
            transform.parent = null;
            _meshColor.material.color = _whiteColor;
        }

        public virtual void ChangePosition(Transform parent, RaycastHit hitInfo, float timeToTranslate)
        {
        }

        public void CheckAvailableToSet()
        {
            if (Physics.Raycast(transform.position,-transform.up, out RaycastHit hitInfo, _distanceOfCheckSurface))
            {
              if(hitInfo.distance <= _distanceToSetUp)
                    IsAvailableToSet = true;
                else
                    IsAvailableToSet = false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, -transform.up * _distanceOfCheckSurface);
        }



        private void ChangeColor()  //ChangeAvailableToSet
        { 
            if(transform.parent == null && IsAvailableToSet) 
                _meshColor.material.color = _greenColor;
            else
                _meshColor.material.color = _redColor;
        }
    }
}