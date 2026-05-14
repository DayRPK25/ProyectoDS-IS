using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ProyectoDS_IS
{
    public partial class Form2 : Form
    {
        bool guardado = false;
        string ruta_archivo = "";
        public Form2()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (guardado == false)
            {
                SaveFileDialog saveFile = new SaveFileDialog();

                saveFile.Title = "Guardar";
                saveFile.Filter = "Archivo Python (*.py)|*.py|Texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
                saveFile.DefaultExt = ".py";
                saveFile.FileName = "Default1.py";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFile.FileName, fastColoredTextBox1.Text);
                    richTextBox1.AppendText("Archivo guardado en:\n" + saveFile.FileName + "\n");
                    guardado = true;
                    ruta_archivo = saveFile.FileName;
                }
            }
            try
            {
                richTextBox1.Clear();
                string rutaArchivo = Path.Combine(Application.StartupPath, "temp.py");

                File.WriteAllText(rutaArchivo, fastColoredTextBox1.Text);

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "python";
                psi.Arguments = $"\"{rutaArchivo}\"";
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;

                Process proceso = new Process();
                proceso.StartInfo = psi;
                proceso.Start();

                string salida = proceso.StandardOutput.ReadToEnd();
                string errores = proceso.StandardError.ReadToEnd();

                proceso.WaitForExit();

                richTextBox1.Text = salida + errores;
            }
            catch (Exception ex)
            {
                richTextBox1.Text = "Error: " + ex.Message;
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (guardado == false)
            {
                SaveFileDialog saveFile = new SaveFileDialog();

                saveFile.Title = "Guardar";
                saveFile.Filter = "Archivo Python (*.py)|*.py|Texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
                saveFile.DefaultExt = ".py";
                saveFile.FileName = "Default1.py";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFile.FileName, fastColoredTextBox1.Text);
                    richTextBox1.AppendText("Archivo guardado en:\n" + saveFile.FileName + "\n");
                    guardado = true;
                    ruta_archivo = saveFile.FileName;
                }
            }
            else
            {
                File.WriteAllText(ruta_archivo, fastColoredTextBox1.Text);
            }
            
        }

        private void abrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Abrir archivo";
            openFileDialog.Filter = "Archivo Python (*.py)|*.py|Texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.InitialDirectory = documents;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fastColoredTextBox1.Text = File.ReadAllText(openFileDialog.FileName);
                guardado = true;
                ruta_archivo = openFileDialog.FileName;


            }
        }
    }
}
