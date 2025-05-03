using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmAnimationCurve : NamedVariable
{
    public AnimationCurve Value { get; set; }

    public FsmAnimationCurve()
    {
        Value = new AnimationCurve();
    }

    public FsmAnimationCurve(IAssetField field) : base(field)
    {
        Value = new AnimationCurve(field.GetField("value"));
    }
}

public class AnimationCurve
{
    public List<Keyframe> Curve { get; set; }
    public int PreInfinity { get; set; }
    public int PostInfinity { get; set; }
    public int RotationOrder { get; set; }

    public AnimationCurve()
    {
        Curve = [];
        PreInfinity = 0;
        PostInfinity = 0;
        RotationOrder = 0;
    }

    public AnimationCurve(IAssetField field)
    {
        Curve = field.GetValueArray("m_Curve", x => new Keyframe(x));
        PreInfinity = field.GetValue<int>("m_PreInfinity");
        PostInfinity = field.GetValue<int>("m_PostInfinity");
        RotationOrder = field.GetValue<int>("m_RotationOrder");
    }
}

public class Keyframe
{
    public float Time { get; set; }
    public float Value { get; set; }
    public float InSlope { get; set; }
    public float OutSlope { get; set; }

    // new in 2019
    public int WeightedMode { get; set; }
    public float InWeight { get; set; }
    public float OutWeight { get; set; }

    public Keyframe()
    {
        Time = 0;
        Value = 0;
        InSlope = 0;
        OutSlope = 0;

        WeightedMode = 0;
        InWeight = 0;
        OutWeight = 0;
    }

    public Keyframe(IAssetField field)
    {
        Time = field.GetValue<float>("time");
        Value = field.GetValue<float>("value");
        InSlope = field.GetValue<float>("inSlope");
        OutSlope = field.GetValue<float>("outSlope");

        if (field.Exists("weightedMode"))
            WeightedMode = field.GetValue<int>("weightedMode");
        if (field.Exists("inWeight"))
            InWeight = field.GetValue<float>("inWeight");
        if (field.Exists("outWeight"))
            OutWeight = field.GetValue<float>("outWeight");
    }
}