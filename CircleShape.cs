namespace GraphicEditor
{
    // Klasa dla okręgu
    [Serializable]
    public class CircleShape : Shape
    {
        public override void Draw(Graphics g)
        {
            int radius = (int)Math.Sqrt(Math.Pow(EndPoint.X - StartPoint.X, 2) + Math.Pow(EndPoint.Y - StartPoint.Y, 2));
            var rect = new Rectangle(StartPoint.X - radius, StartPoint.Y - radius, radius * 2, radius * 2);
            g.DrawEllipse(Pens.Black, rect);
        }

        public override bool Contains(Point p)
        {
            int radius = (int)Math.Sqrt(Math.Pow(EndPoint.X - StartPoint.X, 2) + Math.Pow(EndPoint.Y - StartPoint.Y, 2));
            int dx = p.X - StartPoint.X;
            int dy = p.Y - StartPoint.Y;
            return dx * dx + dy * dy <= radius * radius;
        }

        public override void Move(Point delta)
        {
            StartPoint = new Point(StartPoint.X + delta.X, StartPoint.Y + delta.Y);
            EndPoint = new Point(EndPoint.X + delta.X, EndPoint.Y + delta.Y);
        }

        public override void Resize(Point newEndPoint)
        {
            EndPoint = newEndPoint;
        }
    }
}