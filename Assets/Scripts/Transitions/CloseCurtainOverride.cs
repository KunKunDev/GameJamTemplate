using UnityEngine;

public class CloseCurtainOverride : MonoBehaviour
{
    public float RateMultiplier = 1.0f;

    bool Initialized = false;

    Transition Stage;
    CloseCurtainLens CurrentLens = null;

    void Initialize()
    {
        if (!Initialized)
        {
            Initialized = true;
            Stage = Transition.GetOrCreateInstance();
        }
    }

    void OnEnable()
    {
        Initialize();

        if (CurrentLens != null)
        {
            CurrentLens.Finish();
            CurrentLens = null;
        }

        CurrentLens = Stage.RequestCloseCurtainLens();
        UpdateLens();
    }

    void OnDisable()
    {
        if (CurrentLens != null)
        {
            CurrentLens.Finish();
            CurrentLens = null;
        }
    }

    void Update()
    {
        UpdateLens();
    }

    void UpdateLens()
    {
        CurrentLens.CurtainRate = RateMultiplier;
    }
}