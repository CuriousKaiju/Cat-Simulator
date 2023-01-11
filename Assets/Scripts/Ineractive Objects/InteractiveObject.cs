using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    public GameObject _interactionPoint;
    public GameObject _interactionParticles;
    public GameObject _pickParticles;
    public GameObject _visualInteractionPoint;

    public GameObject _canvasFeedback;
    public GameObject _canvas;

    public string _interactionTag;
    public virtual void SetObjectInteractiveStatus()
    {
        _interactionPoint.SetActive(true);
        _interactionParticles.SetActive(true);
        gameObject.tag = _interactionTag;
    }
    public virtual void CloseInteractiveObject()
    {
        _interactionPoint.SetActive(false);
        _interactionParticles.SetActive(false);
        gameObject.tag = "Untagged";
    }

    public virtual void TakeInteractiveObject(Vector2 canvasFeedbackPosition)
    {
        var newCanvasFeedback = Instantiate(_canvasFeedback, _canvas.transform);
        newCanvasFeedback.transform.position = _canvas.transform.TransformPoint(canvasFeedbackPosition);
        
        _pickParticles.SetActive(true);
        _pickParticles.transform.SetParent(null);
        gameObject.SetActive(false);
    }
}
