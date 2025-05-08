using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmRect : NamedVariable, IFsmPlaymakerValuePreviewer
{
    public Rect Value { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Object;

    public FsmRect()
    {
        Value = new Rect();
    }

    public FsmRect(IAssetField field) : base(field)
    {
        Value = new Rect(field.GetField("value"));
    }

    public override string ToString()
    {
        return $"[X = {Value.X}, Y = {Value.Y}, Width = {Value.Width}, Height = {Value.Height}]";
    }
}

public class Rect
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }

    public Rect()
    {
        X = 0;
        Y = 0;
        Width = 0;
        Height = 0;
    }

    public Rect(IAssetField field)
    {
        X = field.GetValue<float>("x");
        Y = field.GetValue<float>("y");
        Width = field.GetValue<float>("width");
        Height = field.GetValue<float>("height");
    }

    public override string ToString()
    {
        return $"[X = {X}, Y = {Y}, Width = {Width}, Height = {Height}]";
    }
}