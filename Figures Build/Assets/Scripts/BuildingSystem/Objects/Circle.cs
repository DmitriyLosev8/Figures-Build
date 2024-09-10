using UnityEngine;

namespace Assets.Scripts.BuildingSystem.Objects
{
    internal class Circle : PickingObject
    {
        public override void ChangePosition(Transform parent, RaycastHit hitInfo, float timeToTranslate)
        {
            if (hitInfo.collider.gameObject.layer == LayersHash.WallLayer)
            {
                transform.parent = null;
                transform.position = Vector3.Lerp(transform.position, hitInfo.point, timeToTranslate * Time.deltaTime);
                transform.rotation = Quaternion.FromToRotation(transform.up, hitInfo.normal);
            }
            else if (hitInfo.collider.TryGetComponent(out PickingObject pickingObject) && hitInfo.collider.gameObject.layer == LayersHash.CircleLayer)
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