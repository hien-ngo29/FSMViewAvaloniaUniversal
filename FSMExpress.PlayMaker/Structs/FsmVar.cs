using FSMExpress.Common.Assets;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmVar
{
    public string VariableName { get; set; }
    public string ObjectType { get; set; }
    public bool UseVariable { get; set; }
    public VariableType VarType { get; set; }
    public float? FloatValue { get; set; }
    public int? IntValue { get; set; }
    public bool? BoolValue { get; set; }
    public string? StringValue { get; set; }
    public Vector4? Vector4Value { get; set; }
    public NamedAssetPPtr? ObjectReference { get; set; }
    public FsmArray? ArrayValue { get; set; }

    public FsmVar()
    {
        VariableName = string.Empty;
        ObjectType = string.Empty;
        UseVariable = false;
        VarType = VariableType.Unknown;
        FloatValue = 0f;
        BoolValue = false;
        StringValue = string.Empty;
        Vector4Value = new Vector4();
        ObjectReference = new NamedAssetPPtr();
        ArrayValue = new FsmArray();
    }

    public FsmVar(IAssetField field)
    {
        VariableName = field.GetValue<string>("variableName");
        ObjectType = field.GetValue<string>("objectType");
        UseVariable = field.GetValue<bool>("useVariable");
        VarType = field.GetValue<VariableType>("type");
        switch (VarType)
        {
            case VariableType.Float:
                FloatValue = field.GetValue<float>("floatValue");
                break;
            case VariableType.Int:
                IntValue = field.GetValue<int>("intValue");
                break;
            case VariableType.Bool:
                BoolValue = field.GetValue<bool>("boolValue");
                break;
            case VariableType.String:
                StringValue = field.GetValue<string>("stringValue");
                break;
            case VariableType.Vector2:
            case VariableType.Vector3:
            case VariableType.Color:
            case VariableType.Rect:
            case VariableType.Quaternion:
                Vector4Value = new Vector4(field.GetField("vector4Value"));
                break;
            case VariableType.Object:
            case VariableType.GameObject:
            case VariableType.Material:
            case VariableType.Texture:
                ObjectReference = field.GetValue<NamedAssetPPtr>("objectReference");
                break;
            case VariableType.Array:
                ArrayValue = new FsmArray(field.GetField("arrayValue"));
                break;
        }
    }
}
