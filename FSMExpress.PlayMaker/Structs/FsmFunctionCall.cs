using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmFunctionCall : IFsmPlaymakerValuePreviewer
{
    public string FunctionName { get; set; }
    public string ParameterType { get; set; }
    public FsmBool? BoolParameter { get; set; }
    public FsmFloat? FloatParameter { get; set; }
    public FsmInt? IntParameter { get; set; }
    public FsmGameObject? GameObjectParameter { get; set; }
    public FsmObject? ObjectParameter { get; set; }
    public FsmString? StringParameter { get; set; }
    public FsmVector2? Vector2Parameter { get; set; }
    public FsmVector3? Vector3Parameter { get; set; }
    public FsmRect? RectParamater { get; set; }
    public FsmQuaternion? QuaternionParameter { get; set; }
    public FsmObject? MaterialParameter { get; set; }
    public FsmObject? TextureParameter { get; set; }
    public FsmColor? ColorParameter { get; set; }
    public FsmEnum? EnumParameter { get; set; }
    public FsmArray? ArrayParameter { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Object;

    public FsmFunctionCall()
    {
        FunctionName = "";
        ParameterType = "void";
    }

    public FsmFunctionCall(IAssetField field)
    {
        FunctionName = field.GetValue<string>("FunctionName");
        ParameterType = field.GetValue<string>("parameterType");
        switch (ParameterType)
        {
            case "bool":
                BoolParameter = new FsmBool(field.GetField("BoolParameter"));
                break;
            case "float":
                FloatParameter = new FsmFloat(field.GetField("FloatParameter"));
                break;
            case "int":
                IntParameter = new FsmInt(field.GetField("IntParameter"));
                break;
            case "GameObject":
                GameObjectParameter = new FsmGameObject(field.GetField("GameObjectParameter"));
                break;
            case "Object":
                ObjectParameter = new FsmObject(field.GetField("ObjectParameter"));
                break;
            case "string":
                StringParameter = new FsmString(field.GetField("StringParameter"));
                break;
            case "Vector2":
                Vector2Parameter = new FsmVector2(field.GetField("Vector2Parameter"));
                break;
            case "Vector3":
                Vector3Parameter = new FsmVector3(field.GetField("Vector3Parameter"));
                break;
            case "Rect":
                RectParamater = new FsmRect(field.GetField("RectParameter"));
                break;
            case "Quaternion":
                QuaternionParameter = new FsmQuaternion(field.GetField("QuaternionParameter"));
                break;
            case "Material":
                MaterialParameter = new FsmObject(field.GetField("MaterialParameter"));
                break;
            case "Texture":
                TextureParameter = new FsmObject(field.GetField("TextureParameter"));
                break;
            case "Color":
                ColorParameter = new FsmColor(field.GetField("ColorParameter"));
                break;
            case "Enum":
                EnumParameter = new FsmEnum(field.GetField("EnumParameter"));
                break;
            case "Array":
                ArrayParameter = new FsmArray(field.GetField("ArrayParameter"));
                break;
            default:
                // do nothing since we can't handle it
                break;
        }
    }
}
