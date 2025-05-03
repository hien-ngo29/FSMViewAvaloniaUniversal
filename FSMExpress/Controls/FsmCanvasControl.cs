using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using FSMExpress.Common.Document;

namespace FSMExpress.Controls;
public class FsmCanvasControl : Grid
{
    private static readonly Color BG_LIGHT_THEME_COLOR = Color.FromRgb(140, 140, 140);
    private static readonly Color BLACK_GRAY_COLOR = Color.FromRgb(32, 32, 32);
    private static readonly Color BLACK_SOFT_COLOR = Color.FromRgb(50, 50, 50);
    private static readonly Color WHITE_GRAY_COLOR = Color.FromRgb(222, 222, 222);
    private static readonly FontFamily DEFAULT_FONT = new("Segoe UI Bold");

    private readonly Canvas _can;
    private readonly MatrixTransform _mt;
    private Point _lastPosition = new(0, 0);
    private bool _beingDragged = false;

    private FsmDocument? _document;

    public static readonly DirectProperty<FsmCanvasControl, FsmDocument?> DocumentProperty =
        AvaloniaProperty.RegisterDirect<FsmCanvasControl, FsmDocument?>(nameof(Document), o => o.Document, (o, v) => o.Document = v);

    public FsmDocument? Document
    {
        get => _document;
        set
        {
            SetAndRaise(DocumentProperty, ref _document, value);
            RebuildGraph();
        }
    }

    public FsmCanvasControl() : base()
    {
        _mt = new MatrixTransform();
        _can = new Canvas()
        {
            RenderTransform = _mt
        };

        Children.Add(_can);
        ClipToBounds = true;

        Background = new SolidColorBrush(BG_LIGHT_THEME_COLOR);

        PointerPressed += MouseDownCanvas;
        PointerReleased += MouseUpCanvas;
        PointerMoved += MouseMoveCanvas;
        PointerWheelChanged += MouseScrollCanvas;
    }

    private void MouseDownCanvas(object? sender, PointerPressedEventArgs e)
    {
        if (!e.GetCurrentPoint(this).Properties.IsRightButtonPressed)
            return;

        _lastPosition = e.GetPosition(this);
        _beingDragged = true;
        Cursor = new Cursor(StandardCursorType.Hand);
    }

    private void MouseUpCanvas(object? sender, PointerReleasedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsRightButtonPressed)
            return;

        _beingDragged = false;
        Cursor = new Cursor(StandardCursorType.Arrow);
    }

    private void MouseMoveCanvas(object? sender, PointerEventArgs e)
    {
        if (!_beingDragged)
            return;

        var curPos = e.GetPosition(this);
        _mt.Matrix *= Matrix.CreateTranslation(curPos.X - _lastPosition.X, curPos.Y - _lastPosition.Y);
        _lastPosition = curPos;
    }

    private void MouseScrollCanvas(object? sender, PointerWheelEventArgs e)
    {
        var scale = 1 + (e.Delta.Y / 10);

        var halfWidth = _can.Bounds.Width / 2;
        var halfHeight = _can.Bounds.Height / 2;

        var curPos = e.GetPosition(this);
        var zoomX = curPos.X - halfWidth;
        var zoomY = curPos.Y - halfHeight;

        _mt.Matrix *= Matrix.CreateTranslation(-zoomX, -zoomY);
        _mt.Matrix *= Matrix.CreateScale(scale, scale);
        _mt.Matrix *= Matrix.CreateTranslation(zoomX, zoomY);
    }

    private void RebuildGraph()
    {
        _can.Children.Clear();
        if (Document is null)
            return;

        foreach (var node in Document.Nodes)
        {
            var bounds = node.Bounds;
            var nodeBrush = new SolidColorBrush(node.IsStart ? Colors.Gold : BLACK_SOFT_COLOR);
            var titleBrush = new SolidColorBrush(node.IsGlobal ? BLACK_GRAY_COLOR : DrawingToAvaloniaColor(node.NodeColor));
            var transBrush = new SolidColorBrush(DrawingToAvaloniaColor(node.TransitionColor));
            var globalTransBrush = new SolidColorBrush(WHITE_GRAY_COLOR);

            var stackPanel = new StackPanel();

            stackPanel.Children.Add(new TextBlock
            {
                Foreground = Brushes.White,
                Background = titleBrush,
                Text = node.Name,
                FontFamily = DEFAULT_FONT,
                FontWeight = FontWeight.Bold,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                TextAlignment = TextAlignment.Center
            });

            if (!node.IsGlobal)
            {
                // this is yucky. maybe we should calculate it from the control itself?
                float ypos = 27f;

                foreach (var transition in node.Transitions)
                {
                    stackPanel.Children.Add(new TextBlock
                    {
                        Foreground = Brushes.DimGray,
                        Background = transBrush,
                        Text = transition.Name,
                        FontFamily = DEFAULT_FONT,
                        HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                        TextAlignment = TextAlignment.Center
                    });

                    if (transition.ToNode is not null)
                    {
                        var arrow = FsmCanvasArrow.CreateLineBetweenNodes(node, transition.ToNode, transBrush, ypos);
                        _can.Children.Add(arrow);
                    }

                    ypos += 16;
                }
            }
            else
            {
                if (node.Transitions.Count > 0)
                {
                    var transition = node.Transitions[0];
                    if (transition.ToNode is not null)
                    {
                        var arrow = FsmCanvasArrow.CreateLineBetweenNodes(node, transition.ToNode, globalTransBrush, 0);
                        _can.Children.Add(arrow);
                    }
                }
            }

            var innerBorder = new Border()
            {
                Background = nodeBrush,
                CornerRadius = new CornerRadius(10.5)
            };

            stackPanel.OpacityMask = new VisualBrush(innerBorder);

            var innerBorderGrid = new Grid();
            innerBorderGrid.Children.Add(innerBorder);
            innerBorderGrid.Children.Add(stackPanel);

            var border = new Border()
            {
                Child = innerBorderGrid,
                Background = nodeBrush,
                BorderBrush = nodeBrush,
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(0),
                Width = node.Bounds.Width,
            };

            Canvas.SetLeft(border, bounds.X);
            Canvas.SetTop(border, bounds.Y);
            _can.Children.Add(border);
        }
    }

    private static Color DrawingToAvaloniaColor(System.Drawing.Color drawingColor)
    {
        if (drawingColor.A == 0)
            return Colors.Gray;

        return Color.FromArgb(
            drawingColor.A,
            drawingColor.R,
            drawingColor.G,
            drawingColor.B
        );
    }
}
