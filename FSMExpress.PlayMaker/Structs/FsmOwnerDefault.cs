using FSMExpress.Common.Interfaces;

namespace FSMExpress.PlayMaker.Structs;
public class FsmOwnerDefault
{
    public OwnerDefaultOption OwnerOption { get; set; }
    public FsmGameObject? GameObject { get; set; }

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
}

public enum OwnerDefaultOption
{
    UseOwner,
    SpecifyGameObject
}