using System.Numerics;

namespace FSMExpress.Common.Document;
public class FsmDocument(string name)
{
    public string Name { get; set; } = name;
    public List<FsmDocumentNode> Nodes { get; set; } = [];
    public Matrix4x4 ViewTransform { get; set; } = Matrix4x4.Identity;
}
