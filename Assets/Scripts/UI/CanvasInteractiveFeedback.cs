using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasInteractiveFeedback : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _textResult;
    [SerializeField] private Color _colorOfCompletedText;

    public void SetParamsOfTheCanvasFeedback(Sprite sprite, int currentValue, int desiredValue)
    {
        _image.sprite = sprite;
        _textResult.text = currentValue + "/" + desiredValue;

        if(currentValue == desiredValue)
        {
            _textResult.color = _colorOfCompletedText;
        }

    }
}
