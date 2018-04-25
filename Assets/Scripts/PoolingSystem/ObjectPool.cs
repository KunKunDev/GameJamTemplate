using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Pooling system for MonoBehaviour
/// </summary>
public class ObjectPool : MonoBehaviour
{
    #region Variables
    private PooledObject m_pooledPrefab;
    private List<PooledObject> m_availableObjects = new List<PooledObject>();
    #endregion

    #region Class Methods
    public PooledObject GetObject()
    {
        PooledObject obj;
        int lastAvailableIndex = m_availableObjects.Count - 1;
        if (lastAvailableIndex >= 0)
        {
            obj = m_availableObjects[lastAvailableIndex];
            m_availableObjects.RemoveAt(lastAvailableIndex);
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = Instantiate<PooledObject>(m_pooledPrefab);
            obj.transform.SetParent(transform, false);
            obj.SetPool(this);
        }
        return obj;
    }

    public void AddObject(PooledObject obj)
    {
        obj.gameObject.SetActive(false);
        m_availableObjects.Add(obj);
    }

    public static ObjectPool GetPool(PooledObject prefab)
    {
        GameObject obj;
        ObjectPool pool;

        if (Application.isEditor)
        {
            obj = GameObject.Find(prefab.name + " Pool");
            DontDestroyOnLoad(obj);
            if (obj)
            {
                pool = obj.GetComponent<ObjectPool>();
                if (pool)
                {
                    return pool;
                }
            }
        }

        obj = new GameObject(prefab.name + " Pool");
        pool = obj.AddComponent<ObjectPool>();
        pool.m_pooledPrefab = prefab;

        return pool;
    }
    #endregion
}