using FSMExpress.Common.Document;

namespace FSMExpress.PlayMaker;
public interface IFsmPlaymakerValuePreviewer
{
    // this interface will probably have more fields than this in the future
    public FsmDocumentNodeDataFieldKind FieldKind { get; }
}
