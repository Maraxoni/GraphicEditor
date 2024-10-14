namespace GraphicEditor
{
    // Klasa dla linii
    [Serializable]
    public class LineShape : Shape
    {
        // Size of the circle drawn around points
        private const int PointRadius = 5;
        public LineShape() 
        { 

        }

        public override void Draw(Graphics g)
        {
            g.DrawLine(Pens.Black, StartPoint, EndPoint);
        }

        public override void DrawPointsBorder(Graphics g)
        {
            // Draw a small circle around StartPoint
            g.DrawEllipse(Pens.Red, StartPoint.X - PointRadius, StartPoint.Y - PointRadius, PointRadius * 2, PointRadius * 2);

            // Draw a small circle around EndPoint
            g.DrawEllipse(Pens.Red, EndPoint.X - PointRadius, EndPoint.Y - PointRadius, PointRadius * 2, PointRadius * 2);
        }

        public override bool Contains(Point p)
        {
            // Simple method to detect if a point is on the line
            var rect = new Rectangle(Math.Min(StartPoint.X, EndPoint.X), Math.Min(StartPoint.Y, EndPoint.Y), Math.Abs(StartPoint.X - EndPoint.X), Math.Abs(StartPoint.Y - EndPoint.Y));
            // Sprawdzanie, czy punkt kliknięcia znajduje się blisko StartPoint lub EndPoint
            if (IsNearPoint(p, StartPoint) || IsNearPoint(p, EndPoint))
            {
                return true; // Kliknięcie blisko punktów charakterystycznych traktujemy jako "zawierające"
            }
            return rect.Contains(p);
        }
        private bool IsNearPoint(Point p, Point point)
        {
            const int proximityRange = PointRadius;  // Tolerancja w promieniu punktu
            return Math.Abs(p.X - point.X) <= proximityRange && Math.Abs(p.Y - point.Y) <= proximityRange;
        }

        public override void Move(Point delta)
        {
            StartPoint = new Point(StartPoint.X + delta.X, StartPoint.Y + delta.Y);
            EndPoint = new Point(EndPoint.X + delta.X, EndPoint.Y + delta.Y);
        }

        public override void DragStartPoint(Point newStartPoint)
        {
            StartPoint = newStartPoint;
        }

        public override void DragEndPoint(Point newEndPoint)
        {
            EndPoint = newEndPoint;
        }
    }
}
