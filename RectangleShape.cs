namespace GraphicEditor
{
    // Klasa dla prostokąta
    [Serializable]
    public class RectangleShape : Shape
    {
        public override void Draw(Graphics g)
        {
            var rect = new Rectangle(Math.Min(StartPoint.X, EndPoint.X), Math.Min(StartPoint.Y, EndPoint.Y), Math.Abs(StartPoint.X - EndPoint.X), Math.Abs(StartPoint.Y - EndPoint.Y));
            g.DrawRectangle(Pens.Black, rect);
        }

        public override bool Contains(Point p)
        {
            var rect = new Rectangle(Math.Min(StartPoint.X, EndPoint.X), Math.Min(StartPoint.Y, EndPoint.Y), Math.Abs(StartPoint.X - EndPoint.X), Math.Abs(StartPoint.Y - EndPoint.Y));
            return rect.Contains(p);
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
