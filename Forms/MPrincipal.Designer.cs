namespace ProyectoDS_IS.Forms
{
    partial class MPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MPrincipal));
            panel1 = new Panel();
            panel3 = new Panel();
            label1 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel2 = new Panel();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            button2 = new Button();
            button1 = new Button();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkSlateBlue;
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1084, 591);
            panel1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Lavender;
            panel3.Controls.Add(label1);
            panel3.Controls.Add(flowLayoutPanel1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(200, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(884, 591);
            panel3.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Semibold", 30F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(383, 0);
            label1.Name = "label1";
            label1.Size = new Size(144, 54);
            label1.TabIndex = 0;
            label1.Text = "Cursos";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Dock = DockStyle.Bottom;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 57);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(884, 534);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Controls.Add(label2);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button1);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(200, 591);
            panel2.TabIndex = 0;
            panel2.Paint += panel2_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 14.75F, FontStyle.Bold);
            label2.Location = new Point(62, 20);
            label2.Name = "label2";
            label2.Size = new Size(53, 28);
            label2.TabIndex = 0;
            label2.Text = "User";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(44, 42);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.IndianRed;
            button2.Font = new Font("Segoe UI Semibold", 13.75F, FontStyle.Bold);
            button2.ForeColor = Color.White;
            button2.Location = new Point(30, 526);
            button2.Name = "button2";
            button2.Size = new Size(139, 53);
            button2.TabIndex = 2;
            button2.Text = "Cerrar Sesión";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Gray;
            button1.Font = new Font("Segoe UI Semibold", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(30, 112);
            button1.Name = "button1";
            button1.Size = new Size(139, 53);
            button1.TabIndex = 1;
            button1.Text = "IDE";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // MPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 591);
            Controls.Add(panel1);
            Name = "MPrincipal";
            Text = "MPrincipal";
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Label label1;
        private Button button2;
        private Button button1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label2;
    }
}