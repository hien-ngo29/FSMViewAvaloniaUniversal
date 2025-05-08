namespace FSMExpress.Common.Document;
public class FsmDocumentNodeFieldBooleanValue(bool value, int indent = 0) : FsmDocumentNodeFieldValue
{
    public override int DisplayIndent => indent;
    public override string DisplayType => "bool";
    public override string DisplayString => value ? "true" : "false";
    public override FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Boolean;
}

public class FsmDocumentNodeFieldIntegerValue(int value, int indent = 0) : FsmDocumentNodeFieldValue
{
    public override int DisplayIndent => indent;
    public override string DisplayType => "int";
    public override string DisplayString => value.ToString();
    public override FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Integer;
}

public class FsmDocumentNodeFieldFloatValue(float value, int indent = 0) : FsmDocumentNodeFieldValue
{
    public override int DisplayIndent => indent;
    public override string DisplayType => "float";
    public override string DisplayString => value.ToString() + "f";
    public override FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Float;
}

public class FsmDocumentNodeFieldStringValue(string value, int indent = 0) : FsmDocumentNodeFieldValue
{
    public override int DisplayIndent => indent;
    public override string DisplayType => "string";
    public override string DisplayString => $"\"{value}\"";
    public override FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.String;
}

public class FsmDocumentNodeFieldArrayValue(string typeName, int count, int indent = 0) : FsmDocumentNodeFieldValue
{
    public override int DisplayIndent => indent;
    public override string DisplayType => typeName + "[]";
    public override string DisplayString => $"[{count} length]";
    public override FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Array;
}

public class FsmDocumentNodeFieldFallbackValue(object value, int indent = 0) : FsmDocumentNodeFieldValue
{
    public override int DisplayIndent => indent;
    public override string DisplayType => value.GetType().Name;
    public override string DisplayString => value.ToString() ?? string.Empty;
    public override FsmDocumentNodeDataFieldKind FieldKind
    {
        get
        {
            var typeName = value.GetType().Name;
            if (typeName.Contains("bool", StringComparison.InvariantCultureIgnoreCase))
                return FsmDocumentNodeDataFieldKind.Boolean;
            else if (typeName.Contains("int", StringComparison.InvariantCultureIgnoreCase))
                return FsmDocumentNodeDataFieldKind.Integer;
            else if (typeName.Contains("float", StringComparison.InvariantCultureIgnoreCase))
                return FsmDocumentNodeDataFieldKind.Float;
            else if (typeName.Contains("string", StringComparison.InvariantCultureIgnoreCase))
                return FsmDocumentNodeDataFieldKind.String;
            else
                return FsmDocumentNodeDataFieldKind.Object;
        }
    }
}

public abstract class FsmDocumentNodeFieldValue
{
    abstract public int DisplayIndent { get; }
    abstract public string DisplayType { get; }
    abstract public string DisplayString { get; }
    abstract public FsmDocumentNodeDataFieldKind FieldKind { get; }
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
