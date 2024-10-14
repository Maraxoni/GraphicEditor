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
        public String Type { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Shape()
        {
            // Możesz zainicjalizować wartości domyślne, jeśli to konieczne
        }


        // Abstract methods that must be implemented by derived shapes
        public abstract void Draw(Graphics g);
        public abstract void DrawPointsBorder(Graphics g); // Added to base class
        public abstract bool Contains(Point p);
        public abstract void Move(Point delta);
        public abstract void DragStartPoint(Point newStartPoint);
        public abstract void DragEndPoint(Point newEndPoint);
    }

}
