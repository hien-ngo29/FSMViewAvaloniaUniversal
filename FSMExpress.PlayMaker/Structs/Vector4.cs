using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class Vector4
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float W { get; set; }

    public Vector4()
    {
        X = 0;
        Y = 0;
        Z = 0;
        W = 0;
    }

    public Vector4(IAssetField field)
    {
        X = field.GetValue<float>("x");
        Y = field.GetValue<float>("y");
        Z = field.GetValue<float>("z");
        W = field.GetValue<float>("w");
    }
}