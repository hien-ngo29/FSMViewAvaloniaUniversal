using FSMExpress.Common.Assets;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmObject : NamedVariable
{
    public NamedAssetPPtr Value { get; set; }

    public FsmObject()
    {
        Value = new NamedAssetPPtr();
    }

    public FsmObject(IAssetField field) : base(field)
    {
        Value = field.GetValue<NamedAssetPPtr>("value");
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}