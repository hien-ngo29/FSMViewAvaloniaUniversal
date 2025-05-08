using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmColor : NamedVariable, IFsmPlaymakerValuePreviewer
{
    public Color Value { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Object;

    public FsmColor()
    {
        Value = new Color();
    }

    public FsmColor(IAssetField field) : base(field)
    {
        Value = new Color(field.GetField("value"));
    }

    public override string ToString()
    {
        return $"[R = {Value.R}, G = {Value.G}, B = {Value.B}, A = {Value.A}]";
    }
}

public class Color
{
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; }

    public Color()
    {
        R = 0;
        G = 0;
        B = 0;
        A = 0;
    }

    public Color(IAssetField field)
    {
        R = field.GetValue<float>("r");
        G = field.GetValue<float>("g");
        B = field.GetValue<float>("b");
        A = field.GetValue<float>("a");
    }

    public override string ToString()
    {
        return $"[R = {R}, G = {G}, B = {B}, A = {A}]";
    }
}
