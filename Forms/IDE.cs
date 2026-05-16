using ProyectoDS_IS.Forms;
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
    public partial class IDE : Form
    {
        bool guardado = false;
        string ruta_archivo = "";
        public IDE()
        {
            InitializeComponent();
        }

        private void CargarCarpeta(string rutaCarpeta, TreeNode nodoPadre)
        {
            try
            {
                string[] carpetas = Directory.GetDirectories(rutaCarpeta);

                foreach (string carpeta in carpetas)
                {
                    TreeNode nodoCarpeta = new TreeNode(Path.GetFileName(carpeta));
                    nodoCarpeta.Tag = carpeta;

                    CargarCarpeta(carpeta, nodoCarpeta);

                    nodoPadre.Nodes.Add(nodoCarpeta);
                }

                string[] archivos = Directory.GetFiles(rutaCarpeta);

                foreach (string archivo in archivos)
                {

                    TreeNode nodoArchivo = new TreeNode(Path.GetFileName(archivo));
                    nodoArchivo.Tag = archivo;

                    nodoPadre.Nodes.Add(nodoArchivo);

                }
            }
            catch
            {
                // Evita errores por carpetas sin permisos
            }
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

                File.WriteAllText(ruta_archivo, fastColoredTextBox1.Text);

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "python";
                psi.Arguments = $"\"{ruta_archivo}\"";
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
                if (Path.GetExtension(openFileDialog.FileName) == ".py")
                {
                    fastColoredTextBox1.Text = File.ReadAllText(openFileDialog.FileName);
                    label1.Text = Path.GetFileName(openFileDialog.FileName);
                    guardado = true;
                    ruta_archivo = openFileDialog.FileName;
                }
                else
                {
                    MessageBox.Show("SOLO SE PUEDEN ABRIR ARCHIVOS .py", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void abrirCarpetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            folderDialog.InitialDirectory = documents;
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                string carpeta = folderDialog.SelectedPath;

                treeView1.Nodes.Clear();

                TreeNode nodoRaiz = new TreeNode(Path.GetFileName(carpeta));
                nodoRaiz.Tag = carpeta;

                CargarCarpeta(carpeta, nodoRaiz);

                treeView1.Nodes.Add(nodoRaiz);
                nodoRaiz.Expand();
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string ruta = e.Node.Tag.ToString();

            if (File.Exists(ruta))
            {
                if (Path.GetExtension(ruta) == ".py")
                {
                    fastColoredTextBox1.Text = File.ReadAllText(ruta);
                    label1.Text = Path.GetFileName(ruta);
                    guardado = true;
                    ruta_archivo = ruta;
                }
                else
                {
                    MessageBox.Show("SOLO SE PUEDEN ABRIR ARCHIVOS .py", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e)
        {
            MPrincipal principal = new MPrincipal();
            principal.Show();
            this.Hide();
        }
    }
}
