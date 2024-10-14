using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.AxHost;

namespace GraphicEditor
{
    public partial class GraphicEditorWindow : Form
    {
        private List<Shape> shapes = new List<Shape>(); // Shape List
        private List<Point> paintbrushPoints = new List<Point>(); // Point List
        private ToolType selectedToolType = ToolType.None;
        private Point startPoint;
        private Point dragStartPoint;
        private Shape currentShape;
        private Shape selectedShape = null;
        private bool isPainting = false;
        private bool isDragging = false;
        private bool isResizingStartPoint = false;
        private bool isResizingEndPoint = false;

        // Types
        private enum ToolType
        {
            None,
            Select,
            Paintbrush,
            Line,
            Rectangle,
            Circle
        }

        public GraphicEditorWindow()
        {
            InitializeComponent();
            pictureBox1.Paint += PictureBox1_Paint;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            panel1.Visible = false;
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
        }

        private void PaintbrushButton_Click(object sender, EventArgs e)
        {
            selectedToolType = ToolType.Paintbrush;
            CurrentlySelected.Text = "Paintbrush"; // Aktualizacja labelu
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            selectedToolType = ToolType.Line;
            CurrentlySelected.Text = "Line";
        }

        private void RectangleButton_Click(object sender, EventArgs e)
        {
            selectedToolType = ToolType.Rectangle;
            CurrentlySelected.Text = "Rectangle";
        }

        private void CircleButton_Click(object sender, EventArgs e)
        {
            selectedToolType = ToolType.Circle;
            CurrentlySelected.Text = "Circle";
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            selectedToolType = ToolType.Select;
            CurrentlySelected.Text = "Select";
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            try
            {
                Point p1 = new Point(int.Parse(First_Point_x_textBox.Text), int.Parse(First_Point_y_textBox.Text));
                Point p2 = new Point(int.Parse(Second_Point_x_textBox.Text), int.Parse(Second_Point_y_textBox.Text));
                currentShape = CreateShapeFromPoints(p1, p2);
                shapes.Add(currentShape);
                currentShape = null;
                pictureBox1.Invalidate();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input in the coordinates fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Shape CreateShapeFromPoints(Point p1, Point p2)
        {
            switch (selectedToolType)
            {
                case ToolType.Line:
                    return new LineShape { StartPoint = p1, EndPoint = p2 };
                case ToolType.Rectangle:
                    return new RectangleShape { StartPoint = p1, EndPoint = p2 };
                case ToolType.Circle:
                    return new CircleShape { StartPoint = p1, EndPoint = p2 };
                default:
                    return null;
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (selectedToolType == ToolType.None)
            {

            }
            else if (selectedToolType == ToolType.Select)
            {
                selectedShape = shapes.Find(shape => shape.Contains(e.Location));
                if (selectedShape != null)
                {
                    if (IsNearPoint(e.Location, selectedShape.StartPoint))
                    {
                        isResizingStartPoint = true;
                    }
                    else if (IsNearPoint(e.Location, selectedShape.EndPoint))
                    {
                        isResizingEndPoint = true;
                    }
                    else
                    {
                        dragStartPoint = e.Location;
                        isDragging = true;
                    }
                    UpdateCoordinateInputs(selectedShape);
                    pictureBox1.Invalidate();
                }
                else
                {
                    pictureBox1.Invalidate();
                }
            }
            else if (selectedToolType == ToolType.Paintbrush)
            {
                isPainting = true;
                paintbrushPoints.Add(e.Location);
            }
            else
            {
                startPoint = e.Location;
                isPainting = true;
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPainting && selectedToolType == ToolType.Paintbrush)
            {
                paintbrushPoints.Add(e.Location);
                pictureBox1.Invalidate();
            }
            else if (isPainting && selectedToolType != ToolType.Select)
            {
                currentShape = CreateShapeFromPoints(startPoint, e.Location);
                pictureBox1.Invalidate();
            }
            else if (isDragging && selectedShape != null)
            {
                var delta = new Point(e.X - dragStartPoint.X, e.Y - dragStartPoint.Y);
                selectedShape.Move(delta);
                dragStartPoint = e.Location;
                UpdateCoordinateInputs(selectedShape);
                pictureBox1.Invalidate();
            }
            else if (isResizingStartPoint && selectedShape != null)
            {
                selectedShape.DragStartPoint(e.Location);
                UpdateCoordinateInputs(selectedShape);
                pictureBox1.Invalidate();
            }
            else if (isResizingEndPoint && selectedShape != null)
            {
                selectedShape.DragEndPoint(e.Location);
                UpdateCoordinateInputs(selectedShape);
                pictureBox1.Invalidate();
            }
        }


        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isPainting && selectedToolType == ToolType.Paintbrush)
            {
                isPainting = false;
                pictureBox1.Invalidate();
            }
            else if (isPainting)
            {
                shapes.Add(currentShape);
                currentShape = null;
                isPainting = false;
                pictureBox1.Invalidate();
            }
            else if (isDragging)
            {
                isDragging = false;
            }
            else if (isResizingStartPoint)
            {
                isResizingStartPoint = false;
            }
            else if (isResizingEndPoint)
            {
                isResizingEndPoint = false;
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var shape in shapes)
            {
                if(shape != null)
                {
                    shape.Draw(e.Graphics);
                    if (shape == selectedShape)
                    {
                        selectedShape.DrawPointsBorder(e.Graphics);
                    }
                }   
            }

            if (currentShape != null)
            {
                currentShape.Draw(e.Graphics);
            }

            foreach (var point in paintbrushPoints)
            {
                e.Graphics.FillEllipse(Pens.Black.Brush, point.X - 2, point.Y - 2, 4, 4);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            shapes.Clear();
            paintbrushPoints.Clear();
            pictureBox1.Invalidate();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON Files|*.json";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveShapes(saveFileDialog.FileName);
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files|*.json";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadShapes(openFileDialog.FileName);
            }
        }


        private void SaveShapes(string filePath)

        {

            var options = new JsonSerializerOptions { WriteIndented = true };

            string jsonString = JsonSerializer.Serialize(shapes, options);

            File.WriteAllText(filePath, jsonString);

        }




        private void LoadShapes(string filePath)

        {

            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                shapes = JsonSerializer.Deserialize<List<Shape>>(jsonString, options);
                pictureBox1.Invalidate();
            }

        }


        private void UpdateCoordinateInputs(Shape shape)
        {
            First_Point_x_textBox.Text = shape.StartPoint.X.ToString();
            First_Point_y_textBox.Text = shape.StartPoint.Y.ToString();
            Second_Point_x_textBox.Text = shape.EndPoint.X.ToString();
            Second_Point_y_textBox.Text = shape.EndPoint.Y.ToString();
        }

        private bool IsNearPoint(Point p1, Point p2)
        {
            const int proximityRange = 5;
            return Math.Abs(p1.X - p2.X) <= proximityRange && Math.Abs(p1.Y - p2.Y) <= proximityRange;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Placeholder
        }

        private void First_Point_x_textBox_TextChanged(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                try
                {
                    selectedShape.StartPoint = new Point(int.Parse(First_Point_x_textBox.Text), selectedShape.StartPoint.Y);
                    pictureBox1.Invalidate();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input in X coordinate field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void First_Point_y_textBox_TextChanged(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                try
                {
                    selectedShape.StartPoint = new Point(selectedShape.StartPoint.X, int.Parse(First_Point_y_textBox.Text));
                    pictureBox1.Invalidate();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input in Y coordinate field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Second_Point_x_textBox_TextChanged(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                try
                {
                    selectedShape.EndPoint = new Point(int.Parse(Second_Point_x_textBox.Text), selectedShape.EndPoint.Y);
                    pictureBox1.Invalidate();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input in X coordinate field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Second_Point_y_textBox_TextChanged(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                try
                {
                    selectedShape.EndPoint = new Point(selectedShape.EndPoint.X, int.Parse(Second_Point_y_textBox.Text));
                    pictureBox1.Invalidate();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input in Y coordinate field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
