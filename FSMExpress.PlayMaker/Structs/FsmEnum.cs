using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmEnum : NamedVariable
{
    public string EnumName { get; set; }
    public int IntValue { get; set; }

    public FsmEnum()
    {
        EnumName = string.Empty;
        IntValue = 0;
    }

    public FsmEnum(IAssetField field) : base(field)
    {
        EnumName = field.GetValue<string>("enumName");
        IntValue = field.GetValue<int>("intValue");
    }
}