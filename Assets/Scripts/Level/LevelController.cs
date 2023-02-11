using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [Header("NEXT LEVEL")]
    [SerializeField] private string _nextLevel;

    [Header("INTERACTIONS POINTS")]
    [SerializeField] private Transform _cameraTranform;
    [SerializeField] private List<Transform> _interactionPoints = new List<Transform>();
    [SerializeField] private List<QuestElement> _questElementsList = new List<QuestElement>();

    [SerializeField] private InteractiveGroup[] _interactionGroups;
    [SerializeField] private Sprite[] _imagesForQuestGroups;
    [SerializeField] private Transform _questContent;

    [SerializeField] private GameObject _questPrefab;
    [SerializeField] private PlayerProgress _playerProgress;

    [Header("FEEDBACK")]
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _canvasFeedback;

    private void Awake()
    {
        GameEvents.OnPickNewItem += ChangeQuestsStatus;
    }
    private void OnDestroy()
    {
        GameEvents.OnPickNewItem -= ChangeQuestsStatus;
    }

    public void GetParamsAboutTheThings()
    {
        int currentCountOfThings = 0;
        int desiredCountOfThings = 0;

        foreach (InteractiveGroup intGroup in _interactionGroups)
        {
            int current = 0;
            int desired = 0;
            intGroup.ReturnProgress(out desired, out current);
            currentCountOfThings += current;
            desiredCountOfThings += desired;
        }
        _playerProgress.SetTotalCountOfItems(desiredCountOfThings);
    }
    private void ChangeQuestsStatus(int id, Vector2 tapPosition)
    {
        _questElementsList[id].SetBasicParams(1);
        SpawnFeedbackOnTheCanvas(tapPosition, _imagesForQuestGroups[id], id);
        _playerProgress.PlusThing();
    }

    private void Start()
    {
        InitLevel();
        GetParamsAboutTheThings();

        if (PlayerPrefs.HasKey("Current Level"))
        {
            TinySauce.OnGameStarted(PlayerPrefs.GetString("Current Level"));
        }
        else
        {
            TinySauce.OnGameStarted("Level_1");
        }
    }

    private void InitLevel()
    {
        for (int i = 0; i < _interactionGroups.Length; i++)
        {
            _interactionGroups[i].IncludeGroupInTheGame(i);
            var newQuest = Instantiate(_questPrefab, _questContent).GetComponent<QuestElement>(); ;
            newQuest.SetBasicParams(_imagesForQuestGroups[i], _interactionGroups[i].ReturnGroupOfInteractiveObjects().Length, 0, 0);
            _questElementsList.Add(newQuest);
        }
    }

    public void SpawnFeedbackOnTheCanvas(Vector2 canvasFeedBackPosition, Sprite sprite, int id)
    {
        int desiredValue = 0;
        int currentValue = 0;
        var newCanvasFeedback = Instantiate(_canvasFeedback, _canvas.transform);
        newCanvasFeedback.transform.position = _canvas.transform.TransformPoint(canvasFeedBackPosition);
        var newCanasInteractiveFeedback = newCanvasFeedback.GetComponent<CanvasInteractiveFeedback>();
        _interactionGroups[id].ReturnProgress(out desiredValue, out currentValue);
        newCanasInteractiveFeedback.SetParamsOfTheCanvasFeedback(sprite, currentValue, desiredValue);
    }

    public void StartNextLevel()
    {
        if (PlayerPrefs.HasKey("Current Level"))
        {
            TinySauce.OnGameFinished(true, 0, PlayerPrefs.GetString("Current Level"));
        }
        else
        {
            TinySauce.OnGameFinished(true, 0, "Level_1");
        }

        PlayerPrefs.SetString("Current Level", _nextLevel);
        SceneManager.LoadScene(_nextLevel);
    }

}

[System.Serializable]
public class QuestsThing
{
    public bool _pickedStatus = false;
    public bool _activeStatus = true;
}
[System.Serializable]
public class QuestsThingGroup
{
    public int _id = 0;
    public QuestsThing[] _questThingsGroup;
    public QuestsThingGroup(int questThingsLenth)
    {
        _questThingsGroup = new QuestsThing[questThingsLenth];
    }
}
[System.Serializable]
public class QuestsThingGroupsArray
{
    
    public QuestsThingGroup[] _questsThingGroupsArray;
    public QuestsThingGroupsArray(int questsThingGroupsArray)
    {
        _questsThingGroupsArray = new QuestsThingGroup[questsThingGroupsArray];
    }
}

