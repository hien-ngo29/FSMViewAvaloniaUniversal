using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmVarOverride
{
    public NamedVariable Variable { get; set; }
    public FsmVar Var { get; set; }
    public bool IsEdited { get; set; }

    public FsmVarOverride()
    {
        Variable = new NamedVariable();
        Var = new FsmVar();
        IsEdited = false;
    }

    public FsmVarOverride(IAssetField field)
    {
        Variable = new NamedVariable(field.GetField("variable"));
        Var = new FsmVar(field.GetField("fsmVar"));
        IsEdited = field.GetValue<bool>("isEdited");
    }
}