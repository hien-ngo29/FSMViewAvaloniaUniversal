namespace FSMExpress.Common.Document;
public class FsmDocumentEvent(string name)
{
    public string Name { get; set; } = name;
    public bool IsSystem { get; set; } = false;
    public bool IsGlobal { get; set; } = false;
}
