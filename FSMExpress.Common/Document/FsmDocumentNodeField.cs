using AssetsTools.NET;
using OneOf;

namespace FSMExpress.Common.Document;
public class FsmDocumentNodeClassField(AssetTypeReference typeRef, bool isEnabled)
{
    public AssetTypeReference TypeRef { get; set; } = typeRef;
    public bool IsEnabled { get; set; } = isEnabled;
}

public class FsmDocumentNodeDataField(string key, IFsmDocumentNodeDataFieldValue value)
{
    public string Key { get; set; } = key;
    public IFsmDocumentNodeDataFieldValue Value { get; set; } = value;
}

[GenerateOneOf]
public partial class FsmDocumentNodeField : OneOfBase<FsmDocumentNodeClassField, FsmDocumentNodeDataField> { }