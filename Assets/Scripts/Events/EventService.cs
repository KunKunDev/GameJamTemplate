using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Event Service script made to use the Observer pattern 
/// with a Singleton on a MonoBehaviour object on the scene
/// You can either fire instantaneous events or queue them so that the LateUpdate triggers them at the end
/// </summary>
public class EventService : MonoBehaviour
{
    #region Inspector Variables
    public bool m_limitQueueProcesing = false;
    public float m_queueProcessTime = 0.0f;
    #endregion

    #region Variables
    private Queue m_eventQueue = new Queue();
    private Dictionary<System.Type, EventDelegate> m_delegates = new Dictionary<System.Type, EventDelegate>();
    private Dictionary<System.Delegate, EventDelegate> m_delegateLookup = new Dictionary<System.Delegate, EventDelegate>();
    private Dictionary<System.Delegate, System.Delegate> m_onceLookups = new Dictionary<System.Delegate, System.Delegate>();

    public delegate void EventDelegate<T>(T e) where T : IGameEvent;
    private delegate void EventDelegate(IGameEvent e);
    #endregion

    #region Singleton
    private static EventService s_Instance = null;

    // override so we don't have the typecast the object
    public static EventService Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = GameObject.FindObjectOfType(typeof(EventService)) as EventService;
            }
            return s_Instance;
        }
    }
    #endregion

    #region MonoBehaviour Methods
    //Every update cycle the queue is processed, if the queue processing is limited,
    //a maximum processing time per update can be set after which the events will have
    //to be processed next update loop.
    void LateUpdate()
    {
        float timer = 0.0f;
        while (m_eventQueue.Count > 0)
        {
            if (m_limitQueueProcesing)
            {
                if (timer > m_queueProcessTime)
                    return;
            }

            IGameEvent evt = m_eventQueue.Dequeue() as IGameEvent;
            TriggerEvent(evt);

            if (m_limitQueueProcesing)
                timer += Time.deltaTime;
        }
    }

    /// <summary>
    /// Clean up at the end of the application.
    /// All delegates are removed, removing the need to call remove listenenr manually in other scripts.
    /// </summary>
    public void OnApplicationQuit()
    {
        RemoveAll();
        m_eventQueue.Clear();
        s_Instance = null;
    }
    #endregion

    #region Class Methods
    private EventDelegate AddDelegate<T>(EventDelegate<T> del) where T : IGameEvent
    {
        // Early-out if we've already registered this delegate
        if (m_delegateLookup.ContainsKey(del))
            return null;

        // Create a new non-generic delegate which calls our generic one.
        // This is the delegate we actually invoke.
        EventDelegate internalDelegate = (e) => del((T)e);
        m_delegateLookup[del] = internalDelegate;

        EventDelegate tempDel;
        if (m_delegates.TryGetValue(typeof(T), out tempDel))
        {
            m_delegates[typeof(T)] = tempDel += internalDelegate;
        }
        else
        {
            m_delegates[typeof(T)] = internalDelegate;
        }

        return internalDelegate;
    }

    public void AddListener<T>(EventDelegate<T> del) where T : IGameEvent
    {
        AddDelegate<T>(del);
    }

    public void AddListenerOnce<T>(EventDelegate<T> del) where T : IGameEvent
    {
        EventDelegate result = AddDelegate<T>(del);

        if (result != null)
        {
            // remember this is only called once
            m_onceLookups[result] = del;
        }
    }

    public void RemoveListener<T>(EventDelegate<T> del) where T : IGameEvent
    {
        EventDelegate internalDelegate;
        if (m_delegateLookup.TryGetValue(del, out internalDelegate))
        {
            EventDelegate tempDel;
            if (m_delegates.TryGetValue(typeof(T), out tempDel))
            {
                tempDel -= internalDelegate;
                if (tempDel == null)
                {
                    m_delegates.Remove(typeof(T));
                }
                else
                {
                    m_delegates[typeof(T)] = tempDel;
                }
            }

            m_delegateLookup.Remove(del);
        }
    }

    /// <summary>
    /// Clear all 3 dictionnaries
    /// </summary>
    public void RemoveAll()
    {
        m_delegates.Clear();
        m_delegateLookup.Clear();
        m_onceLookups.Clear();
    }

    public bool HasListener<T>(EventDelegate<T> del) where T : IGameEvent
    {
        return m_delegateLookup.ContainsKey(del);
    }

    public void TriggerEvent(IGameEvent e)
    {
        EventDelegate del;
        if (m_delegates.TryGetValue(e.GetType(), out del))
        {
            del.Invoke(e);

            // remove listeners which should only be called once
            foreach (EventDelegate k in m_delegates[e.GetType()].GetInvocationList())
            {
                if (m_onceLookups.ContainsKey(k))
                {
                    m_delegates[e.GetType()] -= k;

                    if (m_delegates[e.GetType()] == null)
                    {
                        m_delegates.Remove(e.GetType());
                    }

                    m_delegateLookup.Remove(m_onceLookups[k]);
                    m_onceLookups.Remove(k);
                }
            }
        }
        else
        {
            Debug.LogWarning("Event: " + e.GetType() + " has no listeners");
        }
    }

    //Inserts the event into the current queue.
    public bool QueueEvent(IGameEvent evt)
    {
        if (!m_delegates.ContainsKey(evt.GetType()))
        {
            Debug.LogWarning("EventManager: QueueEvent failed due to no listeners for event: " + evt.GetType());
            return false;
        }

        m_eventQueue.Enqueue(evt);
        return true;
    }
    #endregion

}