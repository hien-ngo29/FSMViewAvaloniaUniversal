using Avalonia;
using Avalonia.Input;
using Avalonia.Media;
using FSMExpress.Common.Document;
using System;
using AvaloniaPath = Avalonia.Controls.Shapes.Path;

namespace FSMExpress.Controls;
public static class FsmCanvasArrow
{
    private static Point ComputeLocation(FsmDocumentNode node1, FsmDocumentNode node2, float yPos, out bool isLeft)
    {
        var nodetfm1 = node1.Bounds;
        var nodetfm2 = node2.Bounds;
        var midx1 = nodetfm1.X + (nodetfm1.Width / 2);
        var midx2 = nodetfm2.X + (nodetfm2.Width / 2);
        var midy1 = nodetfm1.Y + (nodetfm1.Height / 2);
        var midy2 = nodetfm2.Y + (nodetfm2.Height / 2);

        var loc = new Point(
            nodetfm1.X + (nodetfm1.Width / 2),
            nodetfm1.Y + yPos
        );

        if (midx1 == midx2)
            isLeft = true;
        else if (Math.Abs(midx1 - midx2) * 2 >= nodetfm1.Width + nodetfm2.Width || midy2 > midy1)
            isLeft = midx1 < midx2;
        else
            isLeft = midx1 > midx2;

        loc = isLeft
            ? new Point(loc.X + (nodetfm1.Width / 2), loc.Y)
            : new Point(loc.X - (nodetfm1.Width / 2), loc.Y);

        return loc;
    }

    private static PathFigure BezierFromIntersection(Point startPt, Point int1, Point int2, Point endPt)
    {
        var pathFig = new PathFigure()
        {
            StartPoint = startPt,
            Segments = [] // to satify null check
        };

        pathFig.Segments.Add(new BezierSegment { Point1 = int1, Point2 = int2, Point3 = endPt });
        pathFig.IsClosed = false;
        return pathFig;
    }

    private static PathFigure ArrowPoint(Point endPt, FsmCanvasArrowDirection arrowDir)
    {
        var pathFig = new PathFigure()
        {
            StartPoint = endPt,
            Segments = [] // to satify null check
        };

        if (arrowDir == FsmCanvasArrowDirection.Down)
        {
            pathFig.Segments.Add(new LineSegment { Point = new Point(endPt.X - 3, endPt.Y - 5) });
            pathFig.Segments.Add(new LineSegment { Point = new Point(endPt.X + 3, endPt.Y - 5) });
        }
        else
        {
            var offX = arrowDir == FsmCanvasArrowDirection.Right ? -5 : 5;
            pathFig.Segments.Add(new LineSegment { Point = new Point(endPt.X + offX, endPt.Y - 3) });
            pathFig.Segments.Add(new LineSegment { Point = new Point(endPt.X + offX, endPt.Y + 3) });
        }

        pathFig.IsClosed = true;
        return pathFig;
    }

    private static AvaloniaPath CreateLine(
        Point startPt, Point int1, Point int2, Point endPt, FsmCanvasArrowDirection arrowDir, SolidColorBrush brush)
    {
        PathGeometry pathGeometry = new()
        {
            Figures = [
                BezierFromIntersection(startPt, int1, int2, endPt),
                ArrowPoint(endPt, arrowDir)
            ]
        };

        return new AvaloniaPath()
        {
            Data = pathGeometry,
            Stroke = brush,
            StrokeThickness = 3
        };
    }

    public static AvaloniaPath CreateLineBetweenNodes(FsmDocumentNode startNode, FsmDocumentNode endNode, SolidColorBrush brush, float offY)
    {
        Point start, end, startMiddle, endMiddle;
        FsmCanvasArrowDirection arrowDir;
        if (!startNode.IsGlobal)
        {
            start = ComputeLocation(startNode, endNode, offY, out bool isLeftStart);
            end = ComputeLocation(endNode, startNode, 10, out bool isLeftEnd);

            // more padding on left direction due to border
            var newStartX = isLeftStart ? start.X : start.X;
            var newEndX = isLeftEnd ? (end.X + 3) : (end.X - 3);
            start = new Point(newStartX, start.Y);
            end = new Point(newEndX, end.Y);

            double dist = (isLeftStart == isLeftEnd) ? 50 : 40;
            startMiddle = !isLeftStart ? new Point(start.X - dist, start.Y) : new Point(start.X + dist, start.Y);
            endMiddle = !isLeftEnd ? new Point(end.X - dist, end.Y) : new Point(end.X + dist, end.Y);

            arrowDir = isLeftEnd ? FsmCanvasArrowDirection.Left : FsmCanvasArrowDirection.Right;
        }
        else
        {
            start = new Point(
                startNode.Bounds.X + (startNode.Bounds.Width / 2),
                startNode.Bounds.Y + startNode.Bounds.Height
            );
            end = new Point(
                endNode.Bounds.X + (endNode.Bounds.Width / 2),
                endNode.Bounds.Y
            );
            startMiddle = new Point(start.X, start.Y + 1);
            endMiddle = new Point(end.X, end.Y - 1);

            arrowDir = FsmCanvasArrowDirection.Down;
        }

        var line = CreateLine(start, startMiddle, endMiddle, end, arrowDir, brush);
        line.PointerMoved += (object? sender, PointerEventArgs e) =>
        {
            line.Stroke = Brushes.Black;
            line.ZIndex = 1;
        };
        line.PointerExited += (object? sender, PointerEventArgs e) =>
        {
            line.Stroke = brush;
            line.ZIndex = -1;
        };
        line.ZIndex = -1;

        return line;
    }
}

public enum FsmCanvasArrowDirection
{
    Left,
    Right,
    Down
}
