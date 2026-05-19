using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using ProyectoDS_IS.Models;
using ProyectoDS_IS.Services;
using System.Diagnostics;

namespace ProyectoDS_IS.Forms
{
    public partial class MPrincipal : Form
    {

        private async void CargarCursos()
        {
            flowLayoutPanel1.Controls.Clear();

            string json = await ApiService.Instance.cargarCursos(ApiService.Instance.idUsuario);

            JsonDocument doc = JsonDocument.Parse(json);

            JsonElement cursos = doc.RootElement.GetProperty("courses");

            bool success = doc.RootElement.GetProperty("success").GetBoolean();

            if (!success)
            {
                MessageBox.Show("No se pudieron cargar los cursos.");
                return;
            }

            foreach (JsonElement curso in cursos.EnumerateArray())
            {
                string nombre = curso.GetProperty("nombreCurso").GetString();
                Debug.WriteLine(nombre);
                string profesor = curso.GetProperty("nombre").GetString();
                Debug.WriteLine(profesor);
                Panel tarjeta = new Panel();
                tarjeta.Width = flowLayoutPanel1.Width - 30;
                tarjeta.Height = 90;
                tarjeta.BackColor = Color.Thistle;
                tarjeta.Margin = new Padding(10);

                Label lblNombre = new Label();
                lblNombre.Text = nombre;
                lblNombre.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                lblNombre.Location = new Point(15, 15);
                lblNombre.AutoSize = true;

                Label lblProfesor = new Label();
                lblProfesor.Text = "Profesor: " + profesor;
                lblProfesor.Location = new Point(15, 45);
                lblProfesor.AutoSize = true;

                Button btnVer = new Button();
                btnVer.Text = "Ver";
                btnVer.Width = 80;
                btnVer.Height = 35;
                btnVer.Location = new Point(tarjeta.Width - 100, 25);
                int idCurso = curso.GetProperty("idCurso").GetInt32();
                Debug.WriteLine(idCurso);
                btnVer.Click += (sender, e) =>
                {
                    CursoT curso = new CursoT(idCurso, nombre);
                    curso.Show();
                    this.Hide();
                };

                tarjeta.Controls.Add(lblNombre);
                tarjeta.Controls.Add(lblProfesor);
                tarjeta.Controls.Add(btnVer);

                flowLayoutPanel1.Controls.Add(tarjeta);
            }
        }
        public MPrincipal()
        {
            InitializeComponent();
            CargarCursos();
            label2.Text = ApiService.Instance.CurrentUser;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginF login = new LoginF();
            login.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IDE ide = new IDE();
            ide.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
