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

    [Header("TAGS")]
    [SerializeField] private string _movePoint;

    private Vector2 _firstCrosPoint;


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

            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform,
                                                                     mousePosition,
                                                                     _canvas.worldCamera,
                                                                     out pos);
            _firstCrosPoint = pos;
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
    }
}
