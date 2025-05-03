namespace FSMExpress.Common.Document;
public class FsmDocumentNodeTransition(string name)
{
    public string Name { get; set; } = name;
    public FsmDocumentNode? ToNode { get; set; }
}
