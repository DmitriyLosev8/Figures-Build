using UnityEngine;

namespace Assets.Scripts.BuildingSystem.Objects
{
    internal class Cube : PickingObject
    {
        private bool _canBeSeted;

        public override void ChangePosition(Transform parent, RaycastHit hitInfo, float timeToTranslate)
        {
            if (hitInfo.collider.gameObject.layer == LayersHash.GroundLayer)
            {
                Vector3 newPosition = new Vector3(hitInfo.point.x, hitInfo.point.y + transform.localScale.y / _heightOffset, hitInfo.point.z);
                transform.parent = null;
                transform.position = Vector3.Lerp(transform.position, newPosition, timeToTranslate * Time.deltaTime);

            }
            else if (hitInfo.collider.TryGetComponent(out PickingObject pickingObject) && hitInfo.collider.gameObject.layer == LayersHash.CubeLayer)
            {
                if (!pickingObject.IsPickedUp)
                {
                    _canBeSeted = pickingObject.ChekUpperObject();

                    if (_canBeSeted)
                    {
                        transform.parent = null;
                        transform.position = pickingObject.transform.position + new Vector3(0, pickingObject.transform.localScale.y, 0);
                        transform.rotation = pickingObject.transform.rotation;
                    }
                }
            }
            else
                TakeParent(parent);
        }
    }
}