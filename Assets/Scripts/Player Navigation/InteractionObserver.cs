using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObserver : MonoBehaviour
{
    [SerializeField] private List<InteractiveObject> _interactioveObjects = new List<InteractiveObject>();
    [SerializeField] private float _hitDistance;
    [SerializeField] private LayerMask _interactiovePointsLayer;

    public void FindNearestsInteractioveObjects(GameObject currentPoint)
    {
        _interactioveObjects.Clear();
        Collider[] interactioveObjectsColloders = Physics.OverlapSphere(transform.position, _hitDistance, _interactiovePointsLayer);
        foreach (Collider collider in interactioveObjectsColloders)
        {
            if (currentPoint != collider.gameObject)
            {
                _interactioveObjects.Add(collider.GetComponent<InteractiveObject>());
                _interactioveObjects[_interactioveObjects.Count - 1].SetObjectInteractiveStatus();
            }
        }
    }

    public void FindNearestsInteractioveObjects()
    {
        _interactioveObjects.Clear();
        Collider[] interactioveObjectsColloders = Physics.OverlapSphere(transform.position, _hitDistance, _interactiovePointsLayer);
        foreach (Collider collider in interactioveObjectsColloders)
        {
            _interactioveObjects.Add(collider.GetComponent<InteractiveObject>());
            _interactioveObjects[_interactioveObjects.Count - 1].SetObjectInteractiveStatus();
        }
    }

    public void UpdateToNullInteractioveObjectsArray()
    {
        foreach (InteractiveObject interactiveObject in _interactioveObjects)
        {
            interactiveObject.CloseInteractiveObject();
        }
    }
}
