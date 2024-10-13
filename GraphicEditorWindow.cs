using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace GraphicEditor
{
    public partial class GraphicEditorWindow : Form
    {
        private List<Shape> shapes = new List<Shape>(); // Lista kszta³tów
        private List<Point> paintbrushStroke = new List<Point>(); // Lista punktów dla paintbrusha
        private ToolType selectedToolType = ToolType.None;
        private Point startPoint;   // Punkt pocz¹tkowy kszta³tu
        private Point dragStartPoint;  // Punkt pocz¹tkowy przeci¹gania
        private Shape currentShape; // Aktualnie rysowany kszta³t
        private Shape selectedShape = null; // Kszta³t wybrany do przesuwania
        private bool isPainting = false;  // Czy paintbrush jest w u¿yciu
        private bool isDragging = false;  // Czy u¿ytkownik przesuwa kszta³t

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
                    dragStartPoint = e.Location;
                    isDragging = true;
                    UpdateCoordinateInputs(selectedShape);
                }
            }
            else if (selectedToolType == ToolType.Paintbrush)
            {
                isPainting = true;
                paintbrushStroke.Add(e.Location);
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
                paintbrushStroke.Add(e.Location);
                pictureBox1.Invalidate(); // Odœwie¿anie PictureBox
            }
            else if (isPainting && selectedToolType != ToolType.Select)
            {
                currentShape = CreateShapeFromPoints(startPoint, e.Location);
                pictureBox1.Invalidate(); // Odœwie¿anie PictureBox
            }
            else if (isDragging && selectedShape != null)
            {
                var delta = new Point(e.X - dragStartPoint.X, e.Y - dragStartPoint.Y);
                selectedShape.Move(delta);
                dragStartPoint = e.Location;
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
        }

        // Rysowanie kszta³tów i paintbrusha na PictureBox
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var shape in shapes)
            {
                shape.Draw(e.Graphics);
            }

            // Rysowanie paintbrusha
            for (int i = 0; i < paintbrushStroke.Count - 1; i++)
            {
                e.Graphics.DrawLine(Pens.Black, paintbrushStroke[i], paintbrushStroke[i + 1]);
            }
        }

        // Czyszczenie wszystkich kszta³tów i paintbrusha
        private void ClearButton_Click(object sender, EventArgs e)
        {
            shapes.Clear();
            paintbrushStroke.Clear();
            pictureBox1.Invalidate();
        }

        // Zapis kszta³tów i paintbrusha do pliku JSON
        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON Files|*.json";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveData(saveFileDialog.FileName);
            }
        }

        // Wczytanie kszta³tów i paintbrusha z pliku JSON
        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files|*.json";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadData(openFileDialog.FileName);
            }
        }

        // Serializacja kszta³tów i paintbrusha do pliku
        private void SaveData(string filePath)
        {
            var data = new { shapes, paintbrushStroke };
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, jsonString);
        }

        // Deserializacja kszta³tów i paintbrusha z pliku JSON
        private void LoadData(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                var data = JsonSerializer.Deserialize<dynamic>(jsonString);
                shapes = JsonSerializer.Deserialize<List<Shape>>(data["shapes"].ToString());
                paintbrushStroke = JsonSerializer.Deserialize<List<Point>>(data["paintbrushStroke"].ToString());
                pictureBox1.Invalidate();
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
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
