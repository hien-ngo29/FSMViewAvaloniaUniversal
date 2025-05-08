using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmQuaternion : NamedVariable, IFsmPlaymakerValuePreviewer
{
    public Quaternion Value { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Object;

    public FsmQuaternion()
    {
        Value = new Quaternion();
    }

    public FsmQuaternion(IAssetField field) : base(field)
    {
        Value = new Quaternion(field.GetField("value"));
    }

    public override string ToString()
    {
        return $"[X = {Value.X}, Y = {Value.Y}, Z = {Value.Z}, W = {Value.W}]";
    }
}

public class Quaternion
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float W { get; set; }

    public Quaternion()
    {
        X = 0;
        Y = 0;
        Z = 0;
        W = 0;
    }

    public Quaternion(IAssetField field)
    {
        X = field.GetValue<float>("x");
        Y = field.GetValue<float>("y");
        Z = field.GetValue<float>("z");
        W = field.GetValue<float>("w");
    }

    public override string ToString()
    {
        return $"[X = {X}, Y = {Y}, Z = {Z}, W = {W}]";
    }
}
