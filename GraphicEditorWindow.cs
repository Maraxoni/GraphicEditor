using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace GraphicEditor
{
    public partial class GraphicEditorWindow : Form
    {
        private List<Shape> shapes = new List<Shape>(); // Lista kszta³tów
        private List<Point> paintbrushPoints = new List<Point>(); // Lista punktów dla paintbrusha
        private ToolType selectedToolType = ToolType.None;
        private Point startPoint;   // Punkt pocz¹tkowy kszta³tu
        private Point dragStartPoint;  // Punkt pocz¹tkowy przeci¹gania
        private Shape currentShape; // Aktualnie rysowany kszta³t
        private Shape selectedShape = null; // Kszta³t wybrany do przesuwania
        private bool isPainting = false;  // Czy paintbrush jest w u¿yciu
        private bool isDragging = false;  // Czy u¿ytkownik przesuwa kszta³t
        private bool isResizingStartPoint = false; // Czy u¿ytkownik przeci¹ga punkt pocz¹tkowy kszta³tu
        private bool isResizingEndPoint = false;   // Czy u¿ytkownik przeci¹ga punkt koñcowy kszta³tu

        // Typy narzêdzi
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

        // Obs³uga przycisku Menu
        private void MenuButton_Click(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
        }

        // Wybór paintbrusha
        private void PaintbrushButton_Click(object sender, EventArgs e)
        {
            selectedToolType = ToolType.Paintbrush;
            CurrentlySelected.Text = "Paintbrush"; // Aktualizacja labelu
        }

        // Wybór linii
        private void LineButton_Click(object sender, EventArgs e)
        {
            selectedToolType = ToolType.Line;
            CurrentlySelected.Text = "Line"; // Aktualizacja labelu
        }

        // Wybór prostok¹ta
        private void RectangleButton_Click(object sender, EventArgs e)
        {
            selectedToolType = ToolType.Rectangle;
            CurrentlySelected.Text = "Rectangle"; // Aktualizacja labelu
        }

        // Wybór ko³a
        private void CircleButton_Click(object sender, EventArgs e)
        {
            selectedToolType = ToolType.Circle;
            CurrentlySelected.Text = "Circle"; // Aktualizacja labelu
        }

        // Wybór narzêdzia zaznaczania
        private void SelectButton_Click(object sender, EventArgs e)
        {
            selectedToolType = ToolType.Select;
            CurrentlySelected.Text = "Select"; // Aktualizacja labelu
        }

        // Rysowanie na podstawie wspó³rzêdnych z textboxów
        private void DrawButton_Click(object sender, EventArgs e)
        {
            try
            {
                Point p1 = new Point(int.Parse(First_Point_x_textBox.Text), int.Parse(First_Point_y_textBox.Text));
                Point p2 = new Point(int.Parse(Second_Point_x_textBox.Text), int.Parse(Second_Point_y_textBox.Text));
                currentShape = CreateShapeFromPoints(p1, p2);
                shapes.Add(currentShape);
                currentShape = null;  // Clear after placing the shape
                pictureBox1.Invalidate(); // Odœwie¿ PictureBox
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input in the coordinates fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Funkcja tworz¹ca kszta³t na podstawie dwóch punktów
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
            if (selectedToolType == ToolType.Select)
            {
                // Sprawdzanie, czy u¿ytkownik wybra³ kszta³t
                selectedShape = shapes.Find(shape => shape.Contains(e.Location));
                if (selectedShape != null)
                {
                    if (IsNearPoint(e.Location, selectedShape.StartPoint))
                    {
                        isResizingStartPoint = true; // Start dragging the start point
                    }
                    else if (IsNearPoint(e.Location, selectedShape.EndPoint))
                    {
                        isResizingEndPoint = true; // Start dragging the end point
                    }
                    else
                    {
                        dragStartPoint = e.Location;
                        isDragging = true;
                    }
                    UpdateCoordinateInputs(selectedShape);
                    pictureBox1.Invalidate();  // Refresh PictureBox
                }
                else
                {
                    pictureBox1.Invalidate();  // Refresh PictureBox
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
                pictureBox1.Invalidate(); // Refresh PictureBox
            }
            else if (isPainting && selectedToolType != ToolType.Select)
            {
                // Live drawing of the shape while mouse is moving
                currentShape = CreateShapeFromPoints(startPoint, e.Location);
                pictureBox1.Invalidate(); // Refresh PictureBox
            }
            else if (isDragging && selectedShape != null)
            {
                // Dragging the entire shape
                var delta = new Point(e.X - dragStartPoint.X, e.Y - dragStartPoint.Y);
                selectedShape.Move(delta);  // Adjust the shape's position
                dragStartPoint = e.Location;  // Update the drag start point
                pictureBox1.Invalidate();  // Refresh PictureBox
            }
            else if (isResizingStartPoint && selectedShape != null)
            {
                // Dragging the start point of the shape
                selectedShape.DragStartPoint(e.Location);  // Update start point
                pictureBox1.Invalidate();
            }
            else if (isResizingEndPoint && selectedShape != null)
            {
                // Dragging the end point of the shape
                selectedShape.DragEndPoint(e.Location);  // Update end point
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
                isResizingStartPoint = false; // Stop dragging start point
            }
            else if (isResizingEndPoint)
            {
                isResizingEndPoint = false; // Stop dragging end point
            }
        }

        // Rysowanie kszta³tów i paintbrusha na PictureBox
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

            // Draw the shape currently being created
            if (currentShape != null)
            {
                currentShape.Draw(e.Graphics);
            }

            // Rysowanie Paintbrusha jako punkty
            foreach (var point in paintbrushPoints)
            {
                e.Graphics.FillEllipse(Pens.Black.Brush, point.X - 2, point.Y - 2, 4, 4); // Rysowanie ma³ego okrêgu (punktu)
            }
        }

        // Czyszczenie wszystkich kszta³tów i paintbrusha
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

        // Update SaveShapes and LoadShapes methods to handle shape types correctly
        private void SaveShapes(string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, Converters = { new JsonShapeConverter() } };
            string jsonString = JsonSerializer.Serialize(shapes, options);
            File.WriteAllText(filePath, jsonString);
        }

        private void LoadShapes(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, Converters = { new JsonShapeConverter() } };
                shapes = JsonSerializer.Deserialize<List<Shape>>(jsonString, options);
                pictureBox1.Invalidate(); // Refresh the PictureBox
            }
        }

        // Aktualizacja wspó³rzêdnych kszta³tu na podstawie textboxów
        private void UpdateCoordinateInputs(Shape shape)
        {
            First_Point_x_textBox.Text = shape.StartPoint.X.ToString();
            First_Point_y_textBox.Text = shape.StartPoint.Y.ToString();
            Second_Point_x_textBox.Text = shape.EndPoint.X.ToString();
            Second_Point_y_textBox.Text = shape.EndPoint.Y.ToString();
        }

        private bool IsNearPoint(Point p1, Point p2)
        {
            const int proximityRange = 5; // Defining a proximity range for easier dragging
            return Math.Abs(p1.X - p2.X) <= proximityRange && Math.Abs(p1.Y - p2.Y) <= proximityRange;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Your code for panel paint (if necessary)
        }

        // Obs³uguje zmiany w polach tekstowych i aktualizuje wybrany kszta³t
        private void First_Point_x_textBox_TextChanged(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                try
                {
                    // Aktualizacja X punktu pocz¹tkowego kszta³tu
                    selectedShape.StartPoint = new Point(int.Parse(First_Point_x_textBox.Text), selectedShape.StartPoint.Y);
                    pictureBox1.Invalidate();  // Odœwie¿enie widoku
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
                    // Aktualizacja Y punktu pocz¹tkowego kszta³tu
                    selectedShape.StartPoint = new Point(selectedShape.StartPoint.X, int.Parse(First_Point_y_textBox.Text));
                    pictureBox1.Invalidate();  // Odœwie¿enie widoku
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
                    // Aktualizacja X punktu koñcowego kszta³tu
                    selectedShape.EndPoint = new Point(int.Parse(Second_Point_x_textBox.Text), selectedShape.EndPoint.Y);
                    pictureBox1.Invalidate();  // Odœwie¿enie widoku
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
                    // Aktualizacja Y punktu koñcowego kszta³tu
                    selectedShape.EndPoint = new Point(selectedShape.EndPoint.X, int.Parse(Second_Point_y_textBox.Text));
                    pictureBox1.Invalidate();  // Odœwie¿enie widoku
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input in Y coordinate field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
