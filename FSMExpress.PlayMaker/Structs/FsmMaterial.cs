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

    public FsmMaterial(FsmObject obj)
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
