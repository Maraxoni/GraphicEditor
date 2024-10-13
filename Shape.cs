using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicEditor
{
    // Klasa bazowa dla wszystkich kształtów
    [Serializable]
    public abstract class Shape
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public abstract void Draw(Graphics g);
        public abstract bool Contains(Point p);
        public abstract void Move(Point delta);
        public abstract void Resize(Point newEndPoint);
    }

}
