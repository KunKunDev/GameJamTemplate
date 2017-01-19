using UnityEngine;

// A lens for spotlighting a particular segment of the stage!
public class SpotlightCurtainLens : Lens
{
    public Vector3 WorldCenter;
    public float HorizontalRadius;
    public float CurtainRate = 1.0f;
}

// A lens for just closing the damn curtain!
public class CloseCurtainLens : Lens
{
    public float CurtainRate = 1.0f;
}

// A relatively small class that uses the above lenses to solve several really complicated problems!
[RequireComponent(typeof(RectTransform))]
public class Transition : MonoBehaviour
{
    // our lens managers!
    public LensManager<CloseCurtainLens> CloseLensManager = new LensManager<CloseCurtainLens>();

    public CloseCurtainLens RequestCloseCurtainLens()
    {
        return CloseLensManager.CreateLens();
    }

    // how to tell whether the curtain fully closed
    public bool CurtainIsClosed
    {
        get
        {
            ThisRect.GetLocalCorners(CornersCache);
            return CurrentLeftCurtainWidth + CurrentRightCurtainWidth + 0.5f > CornersCache[2].x - CornersCache[0].x;
        }
    }

    // the parameters we need to do fancy animation work
    public RectTransform LeftCurtain;
    public RectTransform RightCurtain;

    public float BaseCurtainSmoothTime = 0.1f;
    public float MinCurtainRate = 0.1f;
    public float MaxCurtainVelocity = 800f;

    RectTransform ThisRect;

    float CurrentLeftCurtainWidth;
    float CurrentLeftCurtainVelocity;

    float CurrentRightCurtainWidth;
    float CurrentRightCurtainVelocity;

    Vector3[] CornersCache = new Vector3[4];

    void Awake()
    {
        ThisRect = GetComponent<RectTransform>();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        ThisRect.GetLocalCorners(CornersCache);

        CurrentLeftCurtainWidth = CurrentRightCurtainWidth = (CornersCache[2].x - CornersCache[0].x) * 0.5f;
    }

    // a den of stinking evil...
    void LateUpdate()
    {
        ThisRect.GetLocalCorners(CornersCache);

        var totalWidth = CornersCache[2].x - CornersCache[0].x;

        var targetFocalX = Mathf.Lerp(CornersCache[2].x, CornersCache[0].x, 0.5f);
        var targetRate = 1.0f;
        var targetFocalWidth = totalWidth;

        if (CloseLensManager.AnyLensesExist)
        {
            foreach (var l in CloseLensManager.Lenses)
            {
                targetRate *= l.CurtainRate;
            }

            targetFocalX = Mathf.Lerp(CornersCache[0].x + CurrentLeftCurtainWidth, CornersCache[2].x - CurrentRightCurtainWidth, 0.5f);
            targetFocalWidth = 0;
        }

        targetRate = Mathf.Max(MinCurtainRate, targetRate);

        CurrentLeftCurtainWidth = Mathf.SmoothDamp(CurrentLeftCurtainWidth, Mathf.Min(totalWidth, Mathf.Max(0, (targetFocalX - targetFocalWidth / 2.0f) - CornersCache[0].x)), ref CurrentLeftCurtainVelocity, BaseCurtainSmoothTime / targetRate, MaxCurtainVelocity);
        CurrentRightCurtainWidth = Mathf.SmoothDamp(CurrentRightCurtainWidth, Mathf.Min(totalWidth, Mathf.Max(0, CornersCache[2].x - (targetFocalX + targetFocalWidth / 2.0f))), ref CurrentRightCurtainVelocity, BaseCurtainSmoothTime / targetRate, MaxCurtainVelocity);

        LeftCurtain.sizeDelta = new Vector2(CurrentLeftCurtainWidth, LeftCurtain.sizeDelta.y);
        RightCurtain.sizeDelta = new Vector2(CurrentRightCurtainWidth, RightCurtain.sizeDelta.y);
    }

    // a weird little singleton-type thing
    public static Transition GetOrCreateInstance()
    {
        var stage = FindObjectOfType<Transition>();
        if (stage == null)
        {
            stage = Instantiate(Resources.Load<Transition>("Transition/Transition"));
        }
        return stage;
    }
}