using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace GraphicEditor
{
    [Serializable]
    [JsonConverter(typeof(ShapeConverter))]
    public abstract class Shape
    {
        public String Type { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        // Abstract methods that must be implemented by derived shapes
        public abstract void Draw(Graphics g);
        public abstract void DrawPointsBorder(Graphics g); // Added to base class
        public abstract bool Contains(Point p);
        public abstract void Move(Point delta);
        public abstract void DragStartPoint(Point newStartPoint);
        public abstract void DragEndPoint(Point newEndPoint);
    }

}
