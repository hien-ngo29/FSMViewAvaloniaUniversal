using CommunityToolkit.Mvvm.ComponentModel;
using System.Drawing;

namespace FSMExpress.Common.Document;
public partial class FsmDocumentNode(string name) : ObservableObject
{
    public string Name { get; set; } = name;
    public bool IsStart { get; set; } = false;
    public bool IsGlobal { get; set; } = false;
    public RectangleF Bounds { get; set; } = new RectangleF(0f, 0f, 100f, 100f);
    public Color NodeColor { get; set; } = Color.Transparent;
    public Color TransitionColor { get; set; } = Color.Transparent;
    public List<FsmDocumentNodeTransition> Transitions { get; set; } = [];
    public List<FsmDocumentNodeField> Fields { get; set; } = [];

    // add all properties that can change here.
    // we might just make all fields changable in the future.
    [ObservableProperty]
    public bool _isSelected = false;
}
