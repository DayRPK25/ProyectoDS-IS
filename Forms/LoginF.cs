using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text.Json;
using ProyectoDS_IS.Models;
using System.Windows.Forms.Design;

namespace ProyectoDS_IS.Forms
{
    public partial class LoginF : Form
    {
        public LoginF()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pattern = @"^[a-zA-Z._%+-]+@estudiantec\.cr$";
            if (!Regex.IsMatch(textBox1.Text, pattern, RegexOptions.IgnoreCase))
            {
                MessageBox.Show("El correo debe tener extensión @estudiantec.cr", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (textBox2.Text.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string json = ApiFake.Instance.Login(
                textBox1.Text,
                textBox2.Text
            );

            JsonDocument doc = JsonDocument.Parse(json);
            bool success = doc.RootElement.GetProperty("success").GetBoolean();
            string message = doc.RootElement.GetProperty("message").GetString();

            // Si login falla
            if (!success)
            {
                MessageBox.Show(
                    message,
                    "Login incorrecto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            string token = doc.RootElement.GetProperty("token").GetString();
            string userName = doc.RootElement.GetProperty("user").GetProperty("name").GetString();

            ApiFake.Instance.Token = token;
            ApiFake.Instance.CurrentUser = userName;

            MPrincipal principal = new MPrincipal();
            principal.Show();
            this.Hide();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Registro registro = new Registro();
            registro.Show();
        }
    }
}
