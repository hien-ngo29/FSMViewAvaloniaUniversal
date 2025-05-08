using FSMExpress.Common.Document;
using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmOwnerDefault : IFsmPlaymakerValuePreviewer
{
    public OwnerDefaultOption OwnerOption { get; set; }
    public FsmGameObject? GameObject { get; set; }

    public FsmDocumentNodeDataFieldKind FieldKind => FsmDocumentNodeDataFieldKind.Object;

    public FsmOwnerDefault()
    {
        OwnerOption = OwnerDefaultOption.UseOwner;
        GameObject = null;
    }

    public FsmOwnerDefault(IAssetField field)
    {
        OwnerOption = field.GetValue<OwnerDefaultOption>("ownerOption");
        GameObject = OwnerOption == OwnerDefaultOption.SpecifyGameObject
            ? new FsmGameObject(field.GetField("gameObject"))
            : null;
    }

    public override string ToString()
    {
        return OwnerOption == OwnerDefaultOption.UseOwner
            ? "[UseOwner]"
            : GameObject?.ToString() ?? string.Empty;
    }
}

public enum OwnerDefaultOption
{
    UseOwner,
    SpecifyGameObject
}