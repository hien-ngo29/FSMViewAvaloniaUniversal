namespace FSMExpress.Common.Document;
public class FsmDocument(string name)
{
    public string Name { get; set; } = name;
    public List<FsmDocumentNode> Nodes { get; set; } = [];
}
