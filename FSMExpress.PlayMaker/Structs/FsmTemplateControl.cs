using FSMExpress.Common.Assets;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmTemplateControl
{
    public NamedAssetPPtr FsmTemplate { get; set; }
    public List<FsmVarOverride> FsmVarOverrides { get; set; }

    public FsmTemplateControl()
    {
        FsmTemplate = new NamedAssetPPtr();
        FsmVarOverrides = [];
    }

    public FsmTemplateControl(IAssetField field)
    {
        FsmTemplate = field.GetValue<NamedAssetPPtr>("fsmTemplate");
        FsmVarOverrides = field.GetValueArray("fsmVarOverrides", x => new FsmVarOverride(x));
    }
}
