using System;
using System.Collections.Generic;

public class Lens
{
    public bool IsFinished { get; private set; }

    Queue<Action> Callbacks = new Queue<Action>();

    public void OnFinished(Action callback)
    {
        EnqueueCallback(callback);
    }

    internal void EnqueueCallback(Action c)
    {
        Callbacks.Enqueue(c);

        if (IsFinished)
        {
            FlushCallbacks();
        }
    }

    public void Finish()
    {
        if (!IsFinished)
        {
            IsFinished = true;
            FlushCallbacks();
        }
    }

    protected virtual void FlushCallbacks()
    {
        while (Callbacks.Count > 0)
        {
            var a = Callbacks.Dequeue();
            a();
        }
    }
}

public class LensManager<T> where T : Lens, new()
{
    List<T> LensList = new List<T>();
    IList<T> ReadOnlyLenses;

    public IList<T> Lenses
    {
        get
        {
            return ReadOnlyLenses;
        }
    }

    public T CreateLens()
    {
        var lens = new T();

        LensList.Add(lens);
        lens.OnFinished(() => LensList.Remove(lens));

        return lens;
    }

    public bool AnyLensesExist
    {
        get
        {
            return LensList.Count > 0;
        }
    }

    public LensManager()
    {
        ReadOnlyLenses = LensList.AsReadOnly();
    }
}