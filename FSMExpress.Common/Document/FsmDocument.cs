using System.Numerics;

namespace FSMExpress.Common.Document;
public class FsmDocument(string name, string sourceName)
{
    public string Name { get; set; } = name;
    public string SourceName { get; set; } = sourceName;
    public List<FsmDocumentNode> Nodes { get; set; } = [];
    public List<FsmDocumentEvent> Events { get; set; } = [];
    public List<FsmDocumentNodeField> Variables { get; set; } = [];
    public Matrix4x4 ViewTransform { get; set; } = Matrix4x4.Identity;
}
