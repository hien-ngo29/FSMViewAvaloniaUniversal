using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmString : NamedVariable
{
    public string Value { get; set; }

    public FsmString()
    {
        Value = string.Empty;
    }

    public FsmString(IAssetField field) : base(field)
    {
        Value = field.GetValue<string>("value");
    }
}