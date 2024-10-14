namespace GraphicEditor
{
    [Serializable]
    public class LineShape : Shape
    {
        private const int PointRadius = 5;
        public LineShape() 
        {
            Type = "LineShape";
        }

        public override void Draw(Graphics g)
        {
            g.DrawLine(Pens.Black, StartPoint, EndPoint);
        }

        public override void DrawPointsBorder(Graphics g)
        {
            g.DrawEllipse(Pens.Red, StartPoint.X - PointRadius, StartPoint.Y - PointRadius, PointRadius * 2, PointRadius * 2);

            g.DrawEllipse(Pens.Red, EndPoint.X - PointRadius, EndPoint.Y - PointRadius, PointRadius * 2, PointRadius * 2);
        }

        public override bool Contains(Point p)
        {
            var rect = new Rectangle(Math.Min(StartPoint.X, EndPoint.X), Math.Min(StartPoint.Y, EndPoint.Y), Math.Abs(StartPoint.X - EndPoint.X), Math.Abs(StartPoint.Y - EndPoint.Y));
            if (IsNearPoint(p, StartPoint) || IsNearPoint(p, EndPoint))
            {
                return true;
            }
            return rect.Contains(p);
        }
        private bool IsNearPoint(Point p, Point point)
        {
            const int proximityRange = PointRadius;
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
