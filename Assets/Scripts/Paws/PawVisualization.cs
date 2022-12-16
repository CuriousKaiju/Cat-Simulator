using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawVisualization : MonoBehaviour
{
    [Header("ROTATION")]
    [SerializeField] private Vector3 _rotationDirection;
    [SerializeField] private Transform _rotationPaw;
    [SerializeField] private float _rotationSpeed;
    private float _startRotationSpeed;

    [Header("ROTATION")]
    [SerializeField] private Animator _animator;

    [Header("INTERACTIVE")]
    [SerializeField] private GameObject _selectedParticles;
    [SerializeField] private GameObject _tapedParticles;


    private void Start()
    {
        _startRotationSpeed = _rotationSpeed;
    }
    void Update()
    {
        RotatePaw();
    }
    private void RotatePaw()
    {
        _rotationPaw.Rotate(_rotationDirection * _rotationSpeed * Time.deltaTime);
    }
    public void SetParticlesStatus(bool status)
    {
        if (status)
        {
            _selectedParticles.SetActive(status);
            _rotationSpeed = _rotationSpeed * 10;
            _animator.speed = 5;
        }
        else
        {
            _selectedParticles.SetActive(status);
            _rotationSpeed = _startRotationSpeed;
            _animator.speed = 1;
        }
    }
    public void ClosePawPoint()
    {
        _animator.SetTrigger("Close");
        _tapedParticles.SetActive(true);
    }
}
