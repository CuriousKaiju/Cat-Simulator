using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerProgress : MonoBehaviour
{
    [SerializeField] private Image _progressCircle;
    [SerializeField] private TextMeshProUGUI _progressText;

    private int _totalItemsCount;
    private int _playersProgress;


    public void SetTotalCountOfItems(int totalCount)
    {
        _totalItemsCount = totalCount;
        Visualization();
    }
    public void PlusThing()
    {
        _playersProgress += 1;
        Visualization();
    }
    private void Visualization()
    {
        float percentage = (float)_playersProgress / (float)_totalItemsCount;
        _progressCircle.fillAmount = percentage;

        _progressText.text = _playersProgress + "/" + _totalItemsCount;
    }
}
