using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _cameraTranform;
    void Start()
    {
        
    }
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cameraTranform.position);
    }
}
