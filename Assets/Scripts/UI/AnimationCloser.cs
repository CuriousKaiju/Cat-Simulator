using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCloser : MonoBehaviour
{
    public void CloseObject()
    {
        Destroy(gameObject);
    }
}
