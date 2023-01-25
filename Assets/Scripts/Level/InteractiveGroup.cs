using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGroup : MonoBehaviour
{
    public int _groupID;
    [SerializeField] private InteractiveObject[] _groupOfInteractiveObjects;
    [SerializeField] private QuestsThingGroup _questThingsGroup;
    public InteractiveObject[] ReturnGroupOfInteractiveObjects()
    {
        return _groupOfInteractiveObjects;
    }
    public void IncludeGroupInTheGame(int groupID)
    {
        _groupID = groupID;
        _questThingsGroup._id = _groupID;
        foreach (InteractiveObject intObject in _groupOfInteractiveObjects)
        {
            intObject.IncludeObjectInGame();
            intObject._id = _groupID;
        }
    }
    public void ReturnProgress(out int desiredCount, out int currentCount)
    {
        currentCount = 0;

        foreach (InteractiveObject intObject in _groupOfInteractiveObjects)
        {
            if (intObject._questThing._pickedStatus)
            {
                currentCount += 1;
            }
        }

        desiredCount = _groupOfInteractiveObjects.Length;
    }
}
