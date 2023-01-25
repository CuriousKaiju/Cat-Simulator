using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
    public int _id;

    public GameObject _interactionPoint;
    public GameObject _interactionParticles;
    public GameObject _pickParticles;
    public GameObject _visualInteractionPoint;

    public Collider _collider;

    public string _interactionTag;

    public QuestsThing _questThing;

    public GameObject[] _differentTypesOfObject;

    private GameObject _currentSkin;

    private void Start()
    {
        if(_differentTypesOfObject.Length > 0)
        {
            ChooseObjectVisual();
        }
    }
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
        /*
        var newCanvasFeedback = Instantiate(_canvasFeedback, _canvas.transform);
        newCanvasFeedback.transform.position = _canvas.transform.TransformPoint(canvasFeedbackPosition);
        */

        _questThing._pickedStatus = true;
        _questThing._activeStatus = false;
        GameEvents.CallOnPickNewItem(_id, canvasFeedbackPosition);
        _pickParticles.SetActive(true);
        _pickParticles.transform.SetParent(null);
        ClearObject();
    }
    public virtual void IncludeObjectInGame()
    {
        _collider.enabled = true;
    }
    public virtual void ChooseObjectVisual()
    {
        int randomId = Random.Range(0, _differentTypesOfObject.Length);

        for (int i = 0; i < _differentTypesOfObject.Length; i++)
        {
            _differentTypesOfObject[i].SetActive(false);

            if (i == randomId)
            {
                _differentTypesOfObject[i].SetActive(true);
                _currentSkin = _differentTypesOfObject[i];
            }
        }
    }
    public virtual void ClearObject()
    {
        _currentSkin.SetActive(false);
        _collider.enabled = false;
        _interactionPoint.SetActive(false);
        _interactionParticles.SetActive(false);
    }
}
