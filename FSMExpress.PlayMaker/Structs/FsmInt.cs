using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmInt : NamedVariable
{
    public int Value { get; set; }

    public FsmInt()
    {
        Value = 0;
    }

    public FsmInt(IAssetField field) : base(field)
    {
        Value = field.GetValue<int>("value");
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}