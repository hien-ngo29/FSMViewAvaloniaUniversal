using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmMaterial : FsmObject, IFsmPlaymakerValuePreviewer
{
    public FsmMaterial()
    {
    }

    public FsmMaterial(IAssetField field) : base(field)
    {
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
