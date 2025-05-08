using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmProperty : IFsmPlaymakerValuePreviewer
{
    public FsmObject TargetObject { get; set; }
    public string TargetTypeName { get; set; }
    public string PropertyName { get; set; }
    public FsmBool BoolParameter { get; set; }
    public FsmFloat FloatParameter { get; set; }
    public FsmInt IntParameter { get; set; }
    public FsmGameObject GameObjectParameter { get; set; }
    public FsmString StringParameter { get; set; }
    public FsmVector2 Vector2Parameter { get; set; }
    public FsmVector3 Vector3Parameter { get; set; }
    public FsmRect RectParamater { get; set; }
    public FsmQuaternion QuaternionParameter { get; set; }
    public FsmObject ObjectParameter { get; set; }
    public FsmObject MaterialParameter { get; set; }
    public FsmObject TextureParameter { get; set; }
    public FsmColor ColorParameter { get; set; }
    public FsmEnum EnumParameter { get; set; }
    public FsmArray ArrayParameter { get; set; }
    public bool SetProperty { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Object;

    public FsmProperty()
    {
        TargetObject = new FsmObject();
        TargetTypeName = string.Empty;
        PropertyName = string.Empty;
        BoolParameter = new FsmBool();
        FloatParameter = new FsmFloat();
        IntParameter = new FsmInt();
        GameObjectParameter = new FsmGameObject();
        StringParameter = new FsmString();
        Vector2Parameter = new FsmVector2();
        Vector3Parameter = new FsmVector3();
        RectParamater = new FsmRect();
        QuaternionParameter = new FsmQuaternion();
        ObjectParameter = new FsmObject();
        MaterialParameter = new FsmObject();
        TextureParameter = new FsmObject();
        ColorParameter = new FsmColor();
        EnumParameter = new FsmEnum();
        ArrayParameter = new FsmArray();
        SetProperty = false;
    }

    public FsmProperty(IAssetField field)
    {
        TargetObject = new FsmObject(field.GetField("TargetObject"));
        TargetTypeName = field.GetValue<string>("TargetTypeName");
        PropertyName = field.GetValue<string>("PropertyName");
        BoolParameter = new FsmBool(field.GetField("BoolParameter"));
        FloatParameter = new FsmFloat(field.GetField("FloatParameter"));
        IntParameter = new FsmInt(field.GetField("IntParameter"));
        GameObjectParameter = new FsmGameObject(field.GetField("GameObjectParameter"));
        ObjectParameter = new FsmObject(field.GetField("ObjectParameter"));
        StringParameter = new FsmString(field.GetField("StringParameter"));
        Vector2Parameter = new FsmVector2(field.GetField("Vector2Parameter"));
        Vector3Parameter = new FsmVector3(field.GetField("Vector3Parameter"));
        RectParamater = new FsmRect(field.GetField("RectParamater"));
        QuaternionParameter = new FsmQuaternion(field.GetField("QuaternionParameter"));
        MaterialParameter = new FsmObject(field.GetField("MaterialParameter"));
        TextureParameter = new FsmObject(field.GetField("TextureParameter"));
        ColorParameter = new FsmColor(field.GetField("ColorParameter"));
        EnumParameter = new FsmEnum(field.GetField("EnumParameter"));
        ArrayParameter = new FsmArray(field.GetField("ArrayParameter"));
        SetProperty = field.GetValue<bool>("setProperty");
    }
}
