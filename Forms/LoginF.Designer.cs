namespace ProyectoDS_IS.Forms
{
    partial class LoginF
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
            panel1 = new Panel();
            panel2 = new Panel();
            button1 = new Button();
            linkLabel2 = new LinkLabel();
            linkLabel1 = new LinkLabel();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label3 = new Label();
            label2 = new Label();
            panel3 = new Panel();
            label1 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkSlateBlue;
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1084, 591);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.None;
            panel2.BackColor = Color.Lavender;
            panel2.Controls.Add(button1);
            panel2.Controls.Add(linkLabel2);
            panel2.Controls.Add(linkLabel1);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(panel3);
            panel2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
            panel2.ForeColor = Color.Purple;
            panel2.Location = new Point(340, 122);
            panel2.Name = "panel2";
            panel2.Size = new Size(400, 468);
            panel2.TabIndex = 0;
            // 
            // button1
            // 
            button1.BackColor = Color.LightGreen;
            button1.ForeColor = Color.White;
            button1.Location = new Point(279, 401);
            button1.Name = "button1";
            button1.Size = new Size(102, 45);
            button1.TabIndex = 1;
            button1.Text = "Ingresar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            linkLabel2.LinkColor = Color.SteelBlue;
            linkLabel2.Location = new Point(19, 349);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(73, 15);
            linkLabel2.TabIndex = 7;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Crear cuenta";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            linkLabel1.LinkColor = Color.SteelBlue;
            linkLabel1.Location = new Point(19, 316);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(119, 15);
            linkLabel1.TabIndex = 6;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Olvidé mi contraseña";
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            textBox2.Location = new Point(19, 279);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(208, 25);
            textBox2.TabIndex = 5;
            textBox2.UseSystemPasswordChar = true;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            textBox1.Location = new Point(19, 180);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(208, 25);
            textBox1.TabIndex = 4;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(19, 246);
            label3.Name = "label3";
            label3.Size = new Size(92, 21);
            label3.TabIndex = 3;
            label3.Text = "Contraseña";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(19, 149);
            label2.Name = "label2";
            label2.Size = new Size(169, 21);
            label2.TabIndex = 2;
            label2.Text = "Nombre de Usuario....";
            label2.Click += label2_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.MediumSlateBlue;
            panel3.Controls.Add(label1);
            panel3.Location = new Point(99, 15);
            panel3.Name = "panel3";
            panel3.Size = new Size(200, 100);
            panel3.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(29, 0);
            label1.Name = "label1";
            label1.Size = new Size(143, 90);
            label1.TabIndex = 0;
            label1.Text = "Inicio de\r\nSesión";
            label1.Click += label1_Click;
            // 
            // LoginF
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 591);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LoginF";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label label1;
        private Button button1;
        private LinkLabel linkLabel2;
        private LinkLabel linkLabel1;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label label3;
        private Label label2;
    }
}