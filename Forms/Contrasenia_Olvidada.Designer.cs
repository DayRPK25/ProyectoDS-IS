namespace ProyectoDS_IS.Forms
{
    partial class Contrasenia_Olvidada
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
            panel3 = new Panel();
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
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
            // 
            // panel2
            // 
            panel2.BackColor = Color.Lavender;
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(panel3);
            panel2.Location = new Point(340, 122);
            panel2.Name = "panel2";
            panel2.Size = new Size(400, 468);
            panel2.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.BackColor = Color.DarkSlateBlue;
            panel3.Controls.Add(label1);
            panel3.Location = new Point(79, 13);
            panel3.Name = "panel3";
            panel3.Size = new Size(234, 107);
            panel3.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(23, 9);
            label1.Name = "label1";
            label1.Size = new Size(186, 90);
            label1.TabIndex = 0;
            label1.Text = "Olvidé mi\r\nContraseña";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(41, 248);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(193, 23);
            textBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(19, 158);
            label2.Name = "label2";
            label2.Size = new Size(226, 34);
            label2.TabIndex = 2;
            label2.Text = "Ingrese el nombre de usuario\r\nde la cuenta a la que desea ingresar";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(41, 224);
            label3.Name = "label3";
            label3.Size = new Size(151, 21);
            label3.TabIndex = 3;
            label3.Text = "Nombre de usuario";
            label3.Click += label3_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.LightGreen;
            button1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(271, 412);
            button1.Name = "button1";
            button1.Size = new Size(109, 45);
            button1.TabIndex = 1;
            button1.Text = "Continuar";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.IndianRed;
            button2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(19, 412);
            button2.Name = "button2";
            button2.Size = new Size(109, 45);
            button2.TabIndex = 4;
            button2.Text = "Salir";
            button2.UseVisualStyleBackColor = false;
            // 
            // Contrasenia_Olvidada
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 591);
            Controls.Add(panel1);
            Name = "Contrasenia_Olvidada";
            Text = "Contrasenia_Olvidada";
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
        private TextBox textBox1;
        private Label label2;
        private Label label3;
        private Button button1;
        private Button button2;
    }
}