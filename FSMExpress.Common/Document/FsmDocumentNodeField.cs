using AssetsTools.NET;

namespace FSMExpress.Common.Document;
public abstract class FsmDocumentNodeField { }

public class FsmDocumentNodeClassField(AssetTypeReference typeRef, bool isEnabled) : FsmDocumentNodeField
{
    public AssetTypeReference TypeRef { get; set; } = typeRef;
    public bool IsEnabled { get; set; } = isEnabled;
}

public class FsmDocumentNodeDataField(string key, FsmDocumentNodeFieldValue value) : FsmDocumentNodeField
{
    public string Key { get; set; } = key;
    public FsmDocumentNodeFieldValue Value { get; set; } = value;
}