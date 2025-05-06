using Avalonia.Media;
using FSMExpress.Common.Document;

namespace FSMExpress.Controls.FsmCanvas;
public class FsmCanvasNodeInfo(FsmDocumentNode node, IBrush borderBrush)
{
    public FsmDocumentNode Node { get; set; } = node;
    public IBrush BorderBrush { get; set; } = borderBrush;
}
