using FSMExpress.Common.Assets;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmArray : NamedVariable
{
    public VariableType VarType { get; set; }
    public string ObjectTypeName { get; set; }
    public List<float> FloatValues { get; set; }
    public List<int> IntValues { get; set; }
    public List<bool> BoolValues { get; set; }
    public List<string> StringValues { get; set; }
    public List<Vector4> Vector4Values { get; set; }
    public List<NamedAssetPPtr> ObjectReferences { get; set; }

    public FsmArray()
    {
        VarType = VariableType.Unknown;
        ObjectTypeName = string.Empty;
        FloatValues = [];
        IntValues = [];
        BoolValues = [];
        StringValues = [];
        Vector4Values = [];
        ObjectReferences = [];
    }

    public FsmArray(IAssetField field) : base(field)
    {
        VarType = field.GetValue<VariableType>("type");
        ObjectTypeName = field.GetValue<string>("objectTypeName");
        FloatValues = field.GetValue<List<float>>("floatValues");
        IntValues = field.GetValue<List<int>>("intValues");
        BoolValues = field.GetValue<List<bool>>("boolValues");
        StringValues = field.GetValue<List<string>>("stringValues");
        Vector4Values = field.GetValueArray("vector4Values", x => new Vector4(x));
        ObjectReferences = field.GetValue<List<NamedAssetPPtr>>("objectReferences");
    }
}
