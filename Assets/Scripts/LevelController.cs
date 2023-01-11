using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTranform;
    [SerializeField] private List<Transform> _interactionPoints = new List<Transform>();

    void Update()
    {

    }

    private void UpdateIneractiveObjectsStatus()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cameraTranform.position);
    }

}
