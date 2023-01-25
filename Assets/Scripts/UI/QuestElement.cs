using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestElement : MonoBehaviour
{
    [Header("ELEMENTS")]
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _countOfThingsText;
    [SerializeField] private Color _completeColor;
    [SerializeField] private GameObject _checkMarker;

    private int _desiredCountOfThings;
    private int _currentCountOfThings;

    public void SetBasicParams(Sprite sprite, int desiredCountOfThings, int currentCountOfThings, int arrayId)
    {
        _image.sprite = sprite;
        _desiredCountOfThings = desiredCountOfThings;
        _currentCountOfThings = currentCountOfThings;
        CompleteCheck();
        transform.SetSiblingIndex(arrayId);
    }
    public void SetBasicParams(Sprite sprite, int desiredCountOfThings, int currentCountOfThings)
    {
        _image.sprite = sprite;
        _desiredCountOfThings = desiredCountOfThings;
        _currentCountOfThings = currentCountOfThings;
        CompleteCheck();
    }
    public void SetBasicParams(int desiredCountOfThings, int currentCountOfThings)
    {
        _desiredCountOfThings = desiredCountOfThings;
        _currentCountOfThings = currentCountOfThings;
        CompleteCheck();
    }
    public void SetBasicParams(int plusOneThing)
    {
        _currentCountOfThings += plusOneThing;
        CompleteCheck();
    }

    private void CompleteCheck()
    {
        if (_currentCountOfThings == _desiredCountOfThings)
        {
            _countOfThingsText.gameObject.SetActive(false);
            _checkMarker.SetActive(true);
            _image.color = _completeColor;
            transform.SetSiblingIndex(transform.parent.childCount - 2);
        }
        else
        {
            _countOfThingsText.text = _currentCountOfThings + "/" + _desiredCountOfThings;
        }
    }

}
