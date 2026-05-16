namespace ProyectoDS_IS.Forms
{
    partial class Cambio_Contraseña
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
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            label4 = new Label();
            label5 = new Label();
            textBox2 = new TextBox();
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
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(25, 163);
            label2.Name = "label2";
            label2.Size = new Size(176, 17);
            label2.TabIndex = 1;
            label2.Text = "Escriba la nueva contraseña";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(25, 207);
            label3.Name = "label3";
            label3.Size = new Size(92, 21);
            label3.TabIndex = 2;
            label3.Text = "Contraseña";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(25, 231);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(193, 23);
            textBox1.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(25, 257);
            label4.Name = "label4";
            label4.Size = new Size(264, 13);
            label4.TabIndex = 4;
            label4.Text = "La contraseña debe contener al menos 8 caracteres";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(25, 301);
            label5.Name = "label5";
            label5.Size = new Size(167, 21);
            label5.TabIndex = 5;
            label5.Text = "Confirmar contraseña";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(25, 325);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(193, 23);
            textBox2.TabIndex = 6;
            // 
            // button1
            // 
            button1.BackColor = Color.LightGreen;
            button1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(271, 412);
            button1.Name = "button1";
            button1.Size = new Size(102, 45);
            button1.TabIndex = 7;
            button1.Text = "Cambiar";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.IndianRed;
            button2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(25, 412);
            button2.Name = "button2";
            button2.Size = new Size(102, 45);
            button2.TabIndex = 8;
            button2.Text = "Cancelar";
            button2.UseVisualStyleBackColor = false;
            // 
            // Cambio_Contraseña
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 591);
            Controls.Add(panel1);
            Name = "Cambio_Contraseña";
            Text = "Cambio_Contraseña";
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
        private Label label2;
        private Panel panel3;
        private Label label1;
        private TextBox textBox1;
        private Label label3;
        private Label label5;
        private Label label4;
        private Button button2;
        private Button button1;
        private TextBox textBox2;
    }
}