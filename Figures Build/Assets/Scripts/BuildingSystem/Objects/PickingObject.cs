using UnityEngine;

namespace Assets.Scripts.BuildingSystem.Objects
{
    internal abstract class PickingObject : MonoBehaviour
    {
        private float _distanceToSetUp;
        private float _SetUpOfDownDistanceOffset = 0.01f;
        private float _distanceOfCheckSurface = 10;
        private MeshRenderer _meshColor;
        private Color _redColor = new Color(1, 0, 0, 0.5f);
        private Color _greenColor = new Color(0, 1, 0, 0.5f);
        private Color _whiteColor = new Color(1, 1, 1, 1f);

        protected float _heightOffset = 2;
       
        public bool IsPickedUp { get; private set; }

        public bool IsAvailableToSet {  get; private set; }
       
        private void Awake()
        {
            _meshColor = GetComponentInChildren<MeshRenderer>();
            SetDistanceToSetUp();
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
            IsAvailableToSet = false;
            _meshColor.material.color = _whiteColor;
        }

        public virtual void ChangePosition(Transform parent, RaycastHit hitInfo, float timeToTranslate)
        {
        }

        public void CheckAvailableToSet()
        {
            if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hitInfo, _distanceOfCheckSurface))
            {
                if (hitInfo.distance <= _distanceToSetUp)
                    IsAvailableToSet = true;
                else
                    IsAvailableToSet = false;
            }
        }

        public bool ChekUpperObject()
        {
            if (Physics.Raycast(transform.position, transform.up, _distanceOfCheckSurface))
                return false;
            else
                return true;
        }

        private void ChangeColor()  
        { 
            if(transform.parent == null && IsAvailableToSet) 
                _meshColor.material.color = _greenColor;
            else
                _meshColor.material.color = _redColor;
        }

        private void SetDistanceToSetUp()
        {
            _distanceToSetUp = transform.localScale.y / _heightOffset + _SetUpOfDownDistanceOffset;
        }
    }
}