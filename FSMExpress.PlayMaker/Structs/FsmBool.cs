using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmBool : NamedVariable
{
    public bool Value { get; set; }

    public FsmBool()
    {
        Value = false;
    }

    public FsmBool(IAssetField field) : base(field)
    {
        Value = field.GetValue<bool>("value");
    }

    public override string ToString()
    {
        return Value.ToString().ToLowerInvariant();
    }
}