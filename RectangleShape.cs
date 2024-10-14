namespace GraphicEditor
{
    // Klasa dla prostokąta
    [Serializable]
    public class RectangleShape : Shape
    {
        private const int PointRadius = 5;

        public RectangleShape()
        {

        }

        public override void Draw(Graphics g)
        {
            var rect = new Rectangle(Math.Min(StartPoint.X, EndPoint.X), Math.Min(StartPoint.Y, EndPoint.Y), Math.Abs(StartPoint.X - EndPoint.X), Math.Abs(StartPoint.Y - EndPoint.Y));
            g.DrawRectangle(Pens.Black, rect);
        }

        // Draw small circles around the corners of the rectangle
        public override void DrawPointsBorder(Graphics g)
        {
            // Top-left corner
            g.DrawEllipse(Pens.Red, StartPoint.X - PointRadius, StartPoint.Y - PointRadius, PointRadius * 2, PointRadius * 2);

            // Bottom-right corner
            g.DrawEllipse(Pens.Red, EndPoint.X - PointRadius, EndPoint.Y - PointRadius, PointRadius * 2, PointRadius * 2);
        }

        public override bool Contains(Point p)
        {
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
