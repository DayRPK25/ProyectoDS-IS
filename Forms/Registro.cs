using ProyectoDS_IS.Models;
using ProyectoDS_IS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoDS_IS.Forms
{
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox5.Text;
            //Nombre de usuario
            if (textBox1.Text.Length < 5)
            {
                MessageBox.Show("El nombre de usuario debe tener al menos 5 caracteres", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            //------------------------------------------------------------------------------------------------------------------------------------------

            //Correo:

            string pattern = @"^[a-zA-Z._%+-]+@estudiantec\.cr$";
            if (!Regex.IsMatch(textBox2.Text, pattern, RegexOptions.IgnoreCase))
            {
                MessageBox.Show("El correo debe tener extensión @estudiantec.cr", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //------------------------------------------------------------------------------------------------------------------------------------------

            //Carnet:





            //------------------------------------------------------------------------------------------------------------------------------------------
            //Contraseña:

            if (textBox4.Text.Length < 8)
            {
                MessageBox.Show("La contraseña debe contener al menos 8 caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //-----------------------------------------------------------------------------------------------------------------------
            string json = await ApiService.Instance.Registro(textBox2.Text, textBox5.Text, textBox1.Text, textBox4.Text);
            JsonDocument doc = JsonDocument.Parse(json);
            bool success = doc.RootElement.GetProperty("success").GetBoolean();
            string message = "";
            if (!success)
            {
                message = doc.RootElement.GetProperty("error").GetString();
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            message = doc.RootElement.GetProperty("estado").GetString();
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
            LoginF login = new LoginF();
            login.Show();
            return;



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginF login = new LoginF();
            login.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
