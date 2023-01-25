using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameEvents
{
    public static Action<int, Vector2> OnPickNewItem;

    public static void CallOnPickNewItem(int itemID, Vector2 tapPosition)
    {
        OnPickNewItem?.Invoke(itemID, tapPosition);
    }
}
