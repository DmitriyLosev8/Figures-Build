using Assets.Scripts.BuildingSystem.Objects;
using UnityEngine;
using UnityEngine.UIElements;

internal class Cube : PickingObject
{
    [SerializeField] private float _height = 1f;

    public override void ChangePosition(Transform parent, RaycastHit hitInfo, float timeToTranslate)
    {
        if (hitInfo.collider.gameObject.layer == 7)
        {
            Vector3 newPosition = new Vector3(hitInfo.point.x, hitInfo.point.y + _height / 2, hitInfo.point.z);
            transform.parent = null;
            transform.position = Vector3.Lerp(transform.position, newPosition, timeToTranslate * Time.deltaTime);

        }  
        else if (hitInfo.collider.TryGetComponent(out PickingObject pickingObject) && hitInfo.collider.gameObject.layer == 6)  
        {
           if (!pickingObject.IsPickedUp) // && hitInfo.normal == Vector3.up
            {
             transform.parent = null;
             transform.position = pickingObject.transform.position + new Vector3(0,_height, 0);
             transform.rotation = pickingObject.transform.rotation;
            }
        }
        else
            TakeParent(parent);   
    }   
}
