using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmVector3 : NamedVariable
{
    public Vector3 Value { get; set; }

    public FsmVector3()
    {
        Value = new Vector3();
    }

    public FsmVector3(IAssetField field) : base(field)
    {
        Value = new Vector3(field.GetField("value"));
    }
}

public class Vector3
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public Vector3()
    {
        X = 0;
        Y = 0;
        Z = 0;
    }

    public Vector3(IAssetField field)
    {
        X = field.GetValue<float>("x");
        Y = field.GetValue<float>("y");
        Z = field.GetValue<float>("z");
    }
}