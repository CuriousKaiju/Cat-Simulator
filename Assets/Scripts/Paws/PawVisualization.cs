using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawVisualization : MonoBehaviour
{
    [Header("ROTATION")]
    [SerializeField] private Vector3 _rotationDirection;
    [SerializeField] private Transform _rotationPaw;
    [SerializeField] private float _rotationSpeed;


    void Update()
    {
        RotatePaw();
    }
    private void RotatePaw()
    {
        _rotationPaw.Rotate(_rotationDirection * _rotationSpeed * Time.deltaTime);
    }
}
