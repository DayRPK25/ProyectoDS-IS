using ProyectoDS_IS.Models;
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

        private void button1_Click(object sender, EventArgs e)
        {
            //Nombre de usuario
            if (textBox1.Text.Length < 5)
            {
                MessageBox.Show("El nombre de usuario debe tener al menos 5 caracteres", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            string json = ApiFake.Instance.UsernameExists(
                textBox1.Text
            );
            JsonDocument doc = JsonDocument.Parse(json);
            bool success = doc.RootElement.GetProperty("success").GetBoolean();
            string message = doc.RootElement.GetProperty("message").GetString();
            if (!success)
            {
                MessageBox.Show("El nombre de usuario ya está registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            json = ApiFake.Instance.EmailExists(textBox2.Text);
            doc = JsonDocument.Parse(json);
            success = doc.RootElement.GetProperty("success").GetBoolean();
            message = doc.RootElement.GetProperty("message").GetString();

            if (!success)
            {
                MessageBox.Show("El correo ya está registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------

            //Carnet:

            pattern = @"^\d{10}$";
            if (!Regex.IsMatch(textBox3.Text, pattern, RegexOptions.IgnoreCase))
            {
                MessageBox.Show("El carnet debe contener 10 números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            json = ApiFake.Instance.CarnetExists(textBox3.Text);
            doc = JsonDocument.Parse(json);
            success = doc.RootElement.GetProperty("success").GetBoolean();
            message = doc.RootElement.GetProperty("message").GetString();

            if (!success)
            {
                MessageBox.Show("El carnet ya está registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //------------------------------------------------------------------------------------------------------------------------------------------
            //Contraseña:

            if (textBox4.Text.Length < 8)
            {
                MessageBox.Show("La contraseña debe contener al menos 8 caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //-----------------------------------------------------------------------------------------------------------------------
            json = ApiFake.Instance.Register(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            doc = JsonDocument.Parse(json);
            success = doc.RootElement.GetProperty("success").GetBoolean();
            message = doc.RootElement.GetProperty("message").GetString();
            if (!success)
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


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
    }
}
