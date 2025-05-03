using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmVector2 : NamedVariable
{
    public Vector2 Value { get; set; }

    public FsmVector2()
    {
        Value = new Vector2();
    }

    public FsmVector2(IAssetField field) : base(field)
    {
        Value = new Vector2(field.GetField("value"));
    }
}

public class Vector2
{
    public float X { get; set; }
    public float Y { get; set; }

    public Vector2()
    {
        X = 0;
        Y = 0;
    }

    public Vector2(IAssetField field)
    {
        X = field.GetValue<float>("x");
        Y = field.GetValue<float>("y");
    }
}