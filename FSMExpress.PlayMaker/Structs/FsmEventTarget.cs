using FSMExpress.Common.Assets;
using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;
using System.Text;

namespace FSMExpress.PlayMaker.Structs;
public class FsmEventTarget : IFsmPlaymakerValuePreviewer
{
    public EventTarget Target { get; set; }
    public FsmBool ExcludeSelf { get; set; }
    public FsmOwnerDefault GameObject { get; set; }
    public FsmString FsmName { get; set; }
    public FsmBool SendToChildren { get; set; }
    public NamedAssetPPtr FsmComponent { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Object;

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
        var flags = new StringBuilder();

        if (ExcludeSelf.Value)
            flags.Append("ExcludeSelf,");

        if (SendToChildren.Value)
            flags.Append("SendToChildren,");

        if (!string.IsNullOrEmpty(FsmName.Value))
            flags.Append($"SendToFSM: {FsmName.Value},");

        var flagsStr = flags.ToString();
        if (flagsStr.Length > 0)
            flagsStr = $"[{flagsStr[..^1]}]";

        return $"EventTarget({Target}){flagsStr}:{GameObject}";
    }
}
