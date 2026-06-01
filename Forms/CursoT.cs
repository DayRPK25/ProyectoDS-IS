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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoDS_IS.Forms
{
    public partial class CursoT : Form
    {

        private async void CargarTareas(int idCurso, string nameCurso)
        {
            flowLayoutPanel1.Controls.Clear();

            string json = await ApiService.Instance.cargarTareas(idCurso);

            JsonDocument doc = JsonDocument.Parse(json);

            JsonElement tareas = doc.RootElement.GetProperty("tareas");

            bool success = doc.RootElement.GetProperty("success").GetBoolean();

            if (!success)
            {
                MessageBox.Show("No se pudieron cargar los cursos.");
                return;
            }

            foreach (JsonElement tarea in tareas.EnumerateArray())
            {
                string titulo = tarea.GetProperty("nombreTarea").GetString();
                string fechaC = tarea.GetProperty("fechaCreacion").GetString();
                string fechaE = tarea.GetProperty("fechaEntrega").GetString();
                string instr = tarea.GetProperty("descripcion").GetString();



                Panel tarjeta = new Panel();
                tarjeta.Width = flowLayoutPanel1.Width - 30;
                tarjeta.Height = 100;
                tarjeta.BackColor = Color.Thistle;
                tarjeta.Margin = new Padding(10);

                Label lblTitulo = new Label();
                lblTitulo.Text = titulo;
                lblTitulo.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                lblTitulo.Location = new Point(15, 15);
                lblTitulo.AutoSize = true;

                Label lblFechaCreacion = new Label();
                lblFechaCreacion.Text = "Fecha de creación: " + fechaC;
                lblFechaCreacion.Location = new Point(15, 45);
                lblFechaCreacion.AutoSize = true;

                Label lblFechaEntrega = new Label();
                lblFechaEntrega.Text = "Fecha de entrega: " + fechaE;
                lblFechaEntrega.Location = new Point(15, 67);
                lblFechaEntrega.AutoSize = true;

                Button btnVer = new Button();
                btnVer.Text = "Ver";
                btnVer.Width = 80;
                btnVer.Height = 30;
                btnVer.Location = new Point(tarjeta.Width - 100, 55);

                int idTarea = tarea.GetProperty("idTarea").GetInt32();

                btnVer.Click += (sender, e) =>
                {
                    EspcT espcT = new EspcT(titulo, nameCurso, fechaC, fechaE, instr, idTarea);
                    espcT.Show();
                    this.Hide();
                };

                tarjeta.Controls.Add(lblTitulo);
                tarjeta.Controls.Add(lblFechaCreacion);
                tarjeta.Controls.Add(lblFechaEntrega);
                tarjeta.Controls.Add(btnVer);

                flowLayoutPanel1.Controls.Add(tarjeta);
            }
        }

        public CursoT(int idCurso, string nameCurso)
        {
            InitializeComponent();
            CargarTareas(idCurso, nameCurso);
            label2.Text = nameCurso;
            label1.Text = ApiService.Instance.CurrentUser;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MPrincipal principal = new MPrincipal();
            principal.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
