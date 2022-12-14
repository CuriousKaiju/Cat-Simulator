using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private PoolObject prefab;
    [SerializeField] private Transform container; //Where we created a bullet
    [SerializeField] private int minCopacity;
    [SerializeField] private int maxCopacity;
    [SerializeField] private bool isExpand;
    [SerializeField] private Canvas _canvas;
    private List<PoolObject> pool;

    private void Start()
    {
        CreatePool();
    }
    private void OnValidate()
    {
        if (isExpand)
        {
            maxCopacity = int.MaxValue;
        }
    }
    private void CreatePool()
    {
        pool = new List<PoolObject>(minCopacity);

        for (int i = 0; i < minCopacity; i++)
        {
            CreateElement();
        }
    }
    private PoolObject CreateElement(bool isActioveByDefault = false)
    {
        var createdObject = Instantiate(prefab, container);
        createdObject.gameObject.SetActive(isActioveByDefault);

        pool.Add(createdObject);

        return createdObject;
    }

    public bool TryGetElement(out PoolObject element)
    {
        foreach (var item in pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                //item.gameObject.SetActive(true);


                return true;
            }
        }

        element = null;
        return false;
    }

    public PoolObject GetFreeElement()
    {
        if (TryGetElement(out var element))
        {
            return element;
        }

        if (isExpand)
        {
            return CreateElement(true);
        }

        if (pool.Count < maxCopacity)
        {
            return CreateElement(true);
        }

        return null;
        //throw new System.Exception("Pool is over");
    }

    public Transform GetFreeElementObject()
    {
        var element = GetFreeElement();
        return element.transform;
    }
}
