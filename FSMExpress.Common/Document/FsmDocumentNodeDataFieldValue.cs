namespace FSMExpress.Common.Document;
public class FsmDocumentNodeDataFieldBooleanValue(bool value) : IFsmDocumentNodeDataFieldValue
{
    public string GetDisplayString()
        => value ? "true" : "false";

    public FsmDocumentNodeDataFieldKind GetFieldKind()
        => FsmDocumentNodeDataFieldKind.Boolean;
}

public class FsmDocumentNodeDataFieldIntegerValue(int value) : IFsmDocumentNodeDataFieldValue
{
    public string GetDisplayString()
        => value.ToString();

    public FsmDocumentNodeDataFieldKind GetFieldKind()
        => FsmDocumentNodeDataFieldKind.Integer;
}

public class FsmDocumentNodeDataFieldFloatValue(float value) : IFsmDocumentNodeDataFieldValue
{
    public string GetDisplayString()
        => value.ToString() + "f";

    public FsmDocumentNodeDataFieldKind GetFieldKind()
        => FsmDocumentNodeDataFieldKind.Float;
}

public class FsmDocumentNodeDataFieldStringValue(string value) : IFsmDocumentNodeDataFieldValue
{
    public string GetDisplayString()
        => value;

    public FsmDocumentNodeDataFieldKind GetFieldKind()
        => FsmDocumentNodeDataFieldKind.Float;
}

public class FsmDocumentNodeDataFieldArrayValue(List<IFsmDocumentNodeDataFieldValue> value) : IFsmDocumentNodeDataFieldValue
{
    public string GetDisplayString()
        => $"[Array, {value.Count} length]";

    public FsmDocumentNodeDataFieldKind GetFieldKind()
        => FsmDocumentNodeDataFieldKind.Array;
}

public interface IFsmDocumentNodeDataFieldValue
{
    public FsmDocumentNodeDataFieldKind GetFieldKind();
    public string GetDisplayString();
}

public enum FsmDocumentNodeDataFieldKind
{
    Boolean,
    Integer,
    Float,
    String,
    Array,
    Object
}
