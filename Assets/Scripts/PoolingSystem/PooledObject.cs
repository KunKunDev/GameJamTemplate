using UnityEngine;

/// <summary>
/// Poolable MonoBehaviour objects 
/// </summary>
public class PooledObject : MonoBehaviour
{
    #region Variables
    [System.NonSerialized]
    private ObjectPool m_poolInstanceForPrefab;
    private ObjectPool m_pool;
    #endregion

    #region Class Methods
    public void ReturnToPool()
    {
        if (m_pool)
        {
            m_pool.AddObject(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public T GetPooledInstance<T>() where T : PooledObject
    {
        if (!m_poolInstanceForPrefab)
        {
            m_poolInstanceForPrefab = ObjectPool.GetPool(this);
        }
        return (T)m_poolInstanceForPrefab.GetObject();
    }
    #endregion

    #region Getter and Setters
    public void SetPool(ObjectPool pool)
    {
        m_pool = pool;
    }

    public ObjectPool GetPool()
    {
        return m_pool;
    }
    #endregion

}