using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmFunctionCall
{
    public string FunctionName;
    public string ParameterType;
    public FsmBool? BoolParameter;
    public FsmFloat? FloatParameter;
    public FsmInt? IntParameter;
    public FsmGameObject? GameObjectParameter;
    public FsmObject? ObjectParameter;
    public FsmString? StringParameter;
    public FsmVector2? Vector2Parameter;
    public FsmVector3? Vector3Parameter;
    public FsmRect? RectParamater;
    public FsmQuaternion? QuaternionParameter;
    public FsmObject? MaterialParameter;
    public FsmObject? TextureParameter;
    public FsmColor? ColorParameter;
    public FsmEnum? EnumParameter;
    public FsmArray? ArrayParameter;

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
