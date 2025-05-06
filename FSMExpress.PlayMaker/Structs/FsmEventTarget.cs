using FSMExpress.Common.Assets;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmEventTarget
{
    public EventTarget Target { get; set; }
    public FsmBool ExcludeSelf { get; set; }
    public FsmOwnerDefault GameObject { get; set; }
    public FsmString FsmName { get; set; }
    public FsmBool SendToChildren { get; set; }
    public NamedAssetPPtr FsmComponent { get; set; }

    public FsmEventTarget()
    {
        Target = EventTarget.Self;
        ExcludeSelf = new FsmBool();
        GameObject = new FsmOwnerDefault();
        FsmName = new FsmString();
        SendToChildren = new FsmBool();
        FsmComponent = new NamedAssetPPtr();
    }

    public FsmEventTarget(IAssetField field)
    {
        Target = field.GetValue<EventTarget>("target");
        ExcludeSelf = new FsmBool(field.GetField("excludeSelf"));
        GameObject = new FsmOwnerDefault(field.GetField("gameObject"));
        FsmName = new FsmString(field.GetField("fsmName"));
        SendToChildren = new FsmBool(field.GetField("sendToChildren"));
        FsmComponent = field.GetValue<NamedAssetPPtr>("fsmComponent");
    }

    public override string ToString()
    {
        return $"[Target = {Target}, ExcludeSelf = {ExcludeSelf}, GameObject = {GameObject}, FsmName = {FsmName}, SendToChildren = {SendToChildren}, FsmComponent = {FsmComponent}]";
    }
}
