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

    public FsmTexture(FsmObject obj)
    {
        Value = obj.Value;
        UseVariable = obj.UseVariable;
        Name = obj.Name;
        Tooltip = obj.Tooltip;
        ShowInInspector = obj.ShowInInspector;
        NetworkSync = obj.NetworkSync;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
