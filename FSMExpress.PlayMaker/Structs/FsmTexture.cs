using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmTexture : FsmObject, IFsmPlaymakerValuePreviewer
{
    public FsmTexture()
    {
    }

    public FsmTexture(IAssetField field) : base(field)
    {
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
