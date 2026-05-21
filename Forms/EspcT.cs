using FastColoredTextBoxNS;
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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;

namespace ProyectoDS_IS.Forms
{
    public partial class EspcT : Form
    {
        private int idTarea2;
        private string CalcularSHA256(string rutaArchivo)
        {
            using (SHA256 sha256 = SHA256.Create())
            using (FileStream stream = File.OpenRead(rutaArchivo))
            {
                byte[] hashBytes = sha256.ComputeHash(stream);
                return Convert.ToHexString(hashBytes);
            }
        }

        public EspcT(string titulo, string nomCurso, string fechaC, string fechaE, string instrucciones, int idTarea)
        {
            InitializeComponent();
            label1.Text = ApiFake.Instance.CurrentUser;
            label2.Text = titulo;
            label4.Text = nomCurso;
            label9.Text = fechaC;
            label10.Text = fechaE;
            richTextBox1.Text = instrucciones;
            idTarea2 = idTarea;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar archivo";
            openFileDialog.Filter = "Archivo Python (*.py)|*.py|Texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.InitialDirectory = documents;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialog.FileName) == ".py")
                {
                    string firma = CalcularSHA256(openFileDialog.FileName);
                    string json = await ApiService.Instance.verificarArchivoP(
                         Path.GetFileName(openFileDialog.FileName), File.GetCreationTime(openFileDialog.FileName).ToString("yyyy-MM-dd HH:mm:ss"), File.GetLastWriteTime(openFileDialog.FileName).ToString("yyyy-MM-dd HH:mm:ss"), firma
                     );

                    JsonDocument doc = JsonDocument.Parse(json);
                    bool success = doc.RootElement.GetProperty("success").GetBoolean();
                    string message = doc.RootElement.GetProperty("message").GetString();
                    if (!success)
                    {
                        MessageBox.Show("Este archivo fue editado fuera del IDE.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    int idArchivoP = doc.RootElement.GetProperty("idArchivoP").GetInt32();
                    string ruta_archivo = openFileDialog.FileName;
                    
                    json= await ApiService.Instance.crearEntrega(idArchivoP, File.GetCreationTime(openFileDialog.FileName), Path.GetFileName(openFileDialog.FileName), firma, openFileDialog.FileName, idTarea2, "20", "Nice");
                    doc = JsonDocument.Parse(json);
                    success = doc.RootElement.GetProperty("success").GetBoolean();
                    
                    if (!success)
                    {
                        message = doc.RootElement.GetProperty("error").GetString();
                        MessageBox.Show("No se pudo entregar el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("El archivo se entrgó correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("SOLO SE PUEDEN ENTREGAR ARCHIVOS .py", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
