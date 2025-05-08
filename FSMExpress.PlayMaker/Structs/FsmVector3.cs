using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmVector3 : NamedVariable, IFsmPlaymakerValuePreviewer
{
    public Vector3 Value { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Object;

    public FsmVector3()
    {
        Value = new Vector3();
    }

    public FsmVector3(IAssetField field) : base(field)
    {
        Value = new Vector3(field.GetField("value"));
    }

    public override string ToString()
    {
        return $"[X = {Value.X}, Y = {Value.Y}, Z = {Value.Z}]";
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

    public override string ToString()
    {
        return $"[X = {X}, Y = {Y}, Z = {Z}]";
    }
}