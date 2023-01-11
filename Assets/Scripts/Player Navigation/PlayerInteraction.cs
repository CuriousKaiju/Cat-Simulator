using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteraction : MonoBehaviour
{

    [Header("COMPONENTS")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Pool _crossPool;
    [SerializeField] private Camera _camera;
    [SerializeField] private Player _player;

    [Header("TAGS")]
    [SerializeField] private string _movePointTag;
    [SerializeField] private string _interactionObjectTag;

    private Vector2 _firstCrosPoint;
    private GameObject _firstPawPoint;
    private GameObject _firstInteractiveObject;


    public void TouchHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                InterectionHandlerDown();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            InterectionHandlerUp();
        }
    }

    private void InterectionHandlerDown()
    {
        var mousePosition = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            var hittedObject = hit.collider.gameObject;

            if (hittedObject.CompareTag(_movePointTag))
            {
                _firstPawPoint = hittedObject;
                var tappedPlatform = _firstPawPoint.GetComponent<PawPoint>();
                tappedPlatform.SetPressedState();
            }
            else if(hittedObject.CompareTag(_interactionObjectTag))
            {
                _firstInteractiveObject = hittedObject;
            }
            else
            {
                Vector2 pos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform,
                                                                         mousePosition,
                                                                         _canvas.worldCamera,
                                                                         out pos);
                _firstCrosPoint = pos;
            }
        }

    }

    private void InterectionHandlerUp()
    {
        var mousePosition = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            var hittedObject = hit.collider.gameObject;

            if (hittedObject.CompareTag(_movePointTag))
            {
                if (_firstPawPoint == hittedObject)
                {
                    var selectedPawPoint = hittedObject.GetComponent<PawPoint>();
                    selectedPawPoint.ClosePoint();

                    _player.MoveTo(selectedPawPoint.transform);
                }
            }
            else if (hittedObject.CompareTag(_interactionObjectTag))
            {
                if(_firstInteractiveObject == hittedObject)
                {
                    Vector2 pos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform,
                                                                             mousePosition,
                                                                             _canvas.worldCamera,
                                                                             out pos);

                    _player.RotationForInteraction(_firstInteractiveObject.transform, _firstInteractiveObject.GetComponent<InteractiveObject>(), pos);
                }
            }
            else
            {
                Vector2 pos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform,
                                                                         mousePosition,
                                                                         _canvas.worldCamera,
                                                                         out pos);
                if (_firstCrosPoint == pos)
                {
                    Transform newCros = _crossPool.GetFreeElementObject();
                    newCros.position = _canvas.transform.TransformPoint(pos);
                    newCros.gameObject.SetActive(true);
                }
            }

            FreshAllFirstClicks();
        }
    }

    private void FreshAllFirstClicks()
    {
        if (_firstPawPoint != null)
        {
            _firstPawPoint.GetComponent<PawPoint>().SetUnpressedState();
            _firstPawPoint = null;
        }

        if(_firstInteractiveObject != null)
        {
            _firstInteractiveObject = null;
        }
    }

}
