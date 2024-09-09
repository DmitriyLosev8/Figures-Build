using UnityEngine;

namespace Assets.Scripts.BuildingSystem.Objects
{
    internal class Circle : PickingObject
    {
        public override void ChangePosition(Transform parent, RaycastHit hitInfo, float timeToTranslate)
        {
            if (hitInfo.collider.gameObject.layer == 8)
            {
                transform.parent = null;
                transform.position = Vector3.Lerp(transform.position, hitInfo.point, timeToTranslate * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, hitInfo.normal);
            }
            else if (hitInfo.transform.TryGetComponent(out PickingObject pickingObject) && hitInfo.collider.gameObject.layer == 9)
            {
                if (!pickingObject.IsPickedUp)
                    TakeParent(parent);       
                else
                    return;
            }
            else
                TakeParent(parent);
        }
    }
}