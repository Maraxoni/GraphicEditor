namespace GraphicEditor
{
    // Klasa dla okręgu
    [Serializable]
    public class CircleShape : Shape
    {
        private const int PointRadius = 5;

        public CircleShape()
        {

        }

        public override void Draw(Graphics g)
        {
            int radius = (int)Math.Sqrt(Math.Pow(EndPoint.X - StartPoint.X, 2) + Math.Pow(EndPoint.Y - StartPoint.Y, 2));
            var rect = new Rectangle(StartPoint.X - radius, StartPoint.Y - radius, radius * 2, radius * 2);
            g.DrawEllipse(Pens.Black, rect);
        }

        // Draw small circles around the center and the edge of the circle
        public override void DrawPointsBorder(Graphics g)
        {
            // Draw circle at the center (StartPoint)
            g.DrawEllipse(Pens.Red, StartPoint.X - PointRadius, StartPoint.Y - PointRadius, PointRadius * 2, PointRadius * 2);

            // Draw circle at EndPoint (one edge of the circle)
            g.DrawEllipse(Pens.Red, EndPoint.X - PointRadius, EndPoint.Y - PointRadius, PointRadius * 2, PointRadius * 2);
        }

        public override bool Contains(Point p)
        {
            int radius = (int)Math.Sqrt(Math.Pow(EndPoint.X - StartPoint.X, 2) + Math.Pow(EndPoint.Y - StartPoint.Y, 2));
            int dx = p.X - StartPoint.X;
            int dy = p.Y - StartPoint.Y;
            // Sprawdzanie, czy punkt kliknięcia znajduje się blisko StartPoint lub EndPoint
            if (IsNearPoint(p, StartPoint) || IsNearPoint(p, EndPoint))
            {
                return true; // Kliknięcie blisko punktów charakterystycznych traktujemy jako "zawierające"
            }
            return dx * dx + dy * dy <= radius * radius;
        }
        // Sprawdzanie, czy kliknięcie jest blisko danego punktu (StartPoint lub EndPoint)
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