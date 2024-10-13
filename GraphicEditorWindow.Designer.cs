namespace GraphicEditor
{
    partial class GraphicEditorWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PaintbrushButton = new Button();
            LineButton = new Button();
            RectangleButton = new Button();
            CircleButton = new Button();
            pictureBox1 = new PictureBox();
            First_Point_x_textBox = new TextBox();
            DrawButton = new Button();
            First_Point_label = new Label();
            Second_Point_label = new Label();
            Second_Point_x_textBox = new TextBox();
            First_Point_y_textBox = new TextBox();
            Second_Point_y_textBox = new TextBox();
            First_Point_x_label = new Label();
            First_Point_y_label = new Label();
            Second_Point_x_label = new Label();
            Second_Point_y_label = new Label();
            panel1 = new Panel();
            LoadButton = new Button();
            SaveButton = new Button();
            MenuButton = new Button();
            ClearButton = new Button();
            CurrentlySelected_label = new Label();
            CurrentlySelected = new Label();
            SelectButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // PaintbrushButton
            // 
            PaintbrushButton.Location = new Point(12, 32);
            PaintbrushButton.Name = "PaintbrushButton";
            PaintbrushButton.Size = new Size(94, 29);
            PaintbrushButton.TabIndex = 0;
            PaintbrushButton.Text = "Paintbrush";
            PaintbrushButton.UseVisualStyleBackColor = true;
            PaintbrushButton.Click += PaintbrushButton_Click;
            // 
            // LineButton
            // 
            LineButton.Location = new Point(112, 32);
            LineButton.Name = "LineButton";
            LineButton.Size = new Size(94, 29);
            LineButton.TabIndex = 1;
            LineButton.Text = "Line";
            LineButton.UseVisualStyleBackColor = true;
            LineButton.Click += LineButton_Click;
            // 
            // RectangleButton
            // 
            RectangleButton.Location = new Point(12, 67);
            RectangleButton.Name = "RectangleButton";
            RectangleButton.Size = new Size(94, 29);
            RectangleButton.TabIndex = 2;
            RectangleButton.Text = "Rectangle";
            RectangleButton.UseVisualStyleBackColor = true;
            RectangleButton.Click += RectangleButton_Click;
            // 
            // CircleButton
            // 
            CircleButton.Location = new Point(112, 67);
            CircleButton.Name = "CircleButton";
            CircleButton.Size = new Size(94, 29);
            CircleButton.TabIndex = 3;
            CircleButton.Text = "Circle";
            CircleButton.UseVisualStyleBackColor = true;
            CircleButton.Click += CircleButton_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(212, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(576, 426);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // First_Point_x_textBox
            // 
            First_Point_x_textBox.Location = new Point(34, 122);
            First_Point_x_textBox.Name = "First_Point_x_textBox";
            First_Point_x_textBox.Size = new Size(72, 27);
            First_Point_x_textBox.TabIndex = 5;
            // 
            // DrawButton
            // 
            DrawButton.Location = new Point(12, 225);
            DrawButton.Name = "DrawButton";
            DrawButton.Size = new Size(94, 29);
            DrawButton.TabIndex = 6;
            DrawButton.Text = "Draw";
            DrawButton.UseVisualStyleBackColor = true;
            DrawButton.Click += DrawButton_Click;
            // 
            // First_Point_label
            // 
            First_Point_label.AutoSize = true;
            First_Point_label.Location = new Point(12, 99);
            First_Point_label.Name = "First_Point_label";
            First_Point_label.Size = new Size(73, 20);
            First_Point_label.TabIndex = 7;
            First_Point_label.Text = "First Point";
            // 
            // Second_Point_label
            // 
            Second_Point_label.AutoSize = true;
            Second_Point_label.Location = new Point(12, 149);
            Second_Point_label.Name = "Second_Point_label";
            Second_Point_label.Size = new Size(95, 20);
            Second_Point_label.TabIndex = 8;
            Second_Point_label.Text = "Second Point";
            // 
            // Second_Point_x_textBox
            // 
            Second_Point_x_textBox.Location = new Point(34, 172);
            Second_Point_x_textBox.Name = "Second_Point_x_textBox";
            Second_Point_x_textBox.Size = new Size(73, 27);
            Second_Point_x_textBox.TabIndex = 9;
            // 
            // First_Point_y_textBox
            // 
            First_Point_y_textBox.Location = new Point(134, 122);
            First_Point_y_textBox.Name = "First_Point_y_textBox";
            First_Point_y_textBox.Size = new Size(72, 27);
            First_Point_y_textBox.TabIndex = 10;
            // 
            // Second_Point_y_textBox
            // 
            Second_Point_y_textBox.Location = new Point(134, 172);
            Second_Point_y_textBox.Name = "Second_Point_y_textBox";
            Second_Point_y_textBox.Size = new Size(72, 27);
            Second_Point_y_textBox.TabIndex = 11;
            // 
            // First_Point_x_label
            // 
            First_Point_x_label.AutoSize = true;
            First_Point_x_label.Location = new Point(12, 129);
            First_Point_x_label.Name = "First_Point_x_label";
            First_Point_x_label.Size = new Size(16, 20);
            First_Point_x_label.TabIndex = 12;
            First_Point_x_label.Text = "x";
            // 
            // First_Point_y_label
            // 
            First_Point_y_label.AutoSize = true;
            First_Point_y_label.Location = new Point(112, 122);
            First_Point_y_label.Name = "First_Point_y_label";
            First_Point_y_label.Size = new Size(16, 20);
            First_Point_y_label.TabIndex = 13;
            First_Point_y_label.Text = "y";
            // 
            // Second_Point_x_label
            // 
            Second_Point_x_label.AutoSize = true;
            Second_Point_x_label.Location = new Point(12, 175);
            Second_Point_x_label.Name = "Second_Point_x_label";
            Second_Point_x_label.Size = new Size(16, 20);
            Second_Point_x_label.TabIndex = 14;
            Second_Point_x_label.Text = "x";
            // 
            // Second_Point_y_label
            // 
            Second_Point_y_label.AutoSize = true;
            Second_Point_y_label.Location = new Point(113, 175);
            Second_Point_y_label.Name = "Second_Point_y_label";
            Second_Point_y_label.Size = new Size(16, 20);
            Second_Point_y_label.TabIndex = 15;
            Second_Point_y_label.Text = "y";
            // 
            // panel1
            // 
            panel1.Controls.Add(LoadButton);
            panel1.Controls.Add(SaveButton);
            panel1.Location = new Point(12, 313);
            panel1.Name = "panel1";
            panel1.Size = new Size(194, 125);
            panel1.TabIndex = 16;
            panel1.Paint += panel1_Paint;
            // 
            // LoadButton
            // 
            LoadButton.Location = new Point(97, 0);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(94, 32);
            LoadButton.TabIndex = 1;
            LoadButton.Text = "Load";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(3, 3);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(94, 29);
            SaveButton.TabIndex = 0;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // MenuButton
            // 
            MenuButton.Location = new Point(14, -1);
            MenuButton.Name = "MenuButton";
            MenuButton.Size = new Size(92, 27);
            MenuButton.TabIndex = 17;
            MenuButton.Text = "Menu";
            MenuButton.UseVisualStyleBackColor = true;
            MenuButton.Click += MenuButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(112, 225);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(94, 29);
            ClearButton.TabIndex = 18;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // CurrentlySelected_label
            // 
            CurrentlySelected_label.AutoSize = true;
            CurrentlySelected_label.Location = new Point(12, 202);
            CurrentlySelected_label.Name = "CurrentlySelected_label";
            CurrentlySelected_label.Size = new Size(69, 20);
            CurrentlySelected_label.TabIndex = 19;
            CurrentlySelected_label.Text = "Selected:";
            // 
            // CurrentlySelected
            // 
            CurrentlySelected.AutoSize = true;
            CurrentlySelected.Location = new Point(79, 202);
            CurrentlySelected.Name = "CurrentlySelected";
            CurrentlySelected.Size = new Size(45, 20);
            CurrentlySelected.TabIndex = 20;
            CurrentlySelected.Text = "None";
            // 
            // SelectButton
            // 
            SelectButton.Location = new Point(112, -3);
            SelectButton.Name = "SelectButton";
            SelectButton.Size = new Size(94, 29);
            SelectButton.TabIndex = 21;
            SelectButton.Text = "Select";
            SelectButton.UseVisualStyleBackColor = true;
            SelectButton.Click += SelectButton_Click;
            // 
            // GraphicEditorWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(SelectButton);
            Controls.Add(CurrentlySelected);
            Controls.Add(CurrentlySelected_label);
            Controls.Add(ClearButton);
            Controls.Add(MenuButton);
            Controls.Add(panel1);
            Controls.Add(Second_Point_y_label);
            Controls.Add(Second_Point_x_label);
            Controls.Add(First_Point_y_label);
            Controls.Add(First_Point_x_label);
            Controls.Add(Second_Point_y_textBox);
            Controls.Add(First_Point_y_textBox);
            Controls.Add(Second_Point_x_textBox);
            Controls.Add(Second_Point_label);
            Controls.Add(First_Point_label);
            Controls.Add(DrawButton);
            Controls.Add(First_Point_x_textBox);
            Controls.Add(pictureBox1);
            Controls.Add(CircleButton);
            Controls.Add(RectangleButton);
            Controls.Add(LineButton);
            Controls.Add(PaintbrushButton);
            Name = "GraphicEditorWindow";
            Text = "GraphicEditor";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button PaintbrushButton;
        private Button LineButton;
        private Button RectangleButton;
        private Button CircleButton;
        private PictureBox pictureBox1;
        private TextBox First_Point_x_textBox;
        private Button DrawButton;
        private Label First_Point_label;
        private Label Second_Point_label;
        private TextBox Second_Point_x_textBox;
        private TextBox First_Point_y_textBox;
        private TextBox Second_Point_y_textBox;
        private Label First_Point_x_label;
        private Label First_Point_y_label;
        private Label Second_Point_x_label;
        private Label Second_Point_y_label;
        private Panel panel1;
        private Button MenuButton;
        private Button LoadButton;
        private Button SaveButton;
        private Button ClearButton;
        private Label CurrentlySelected_label;
        private Label CurrentlySelected;
        private Button SelectButton;
    }
}
