using ProyectoDS_IS.Forms;
using ProyectoDS_IS.Models;
using ProyectoDS_IS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic;



namespace ProyectoDS_IS
{
    public partial class IDE : Form
    {
        private string directorioActual = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private int inicioComando = 0;
        private int idArchivo = 0;

        private string CalcularSHA256(string rutaArchivo)
        {
            using (SHA256 sha256 = SHA256.Create())
            using (FileStream stream = File.OpenRead(rutaArchivo))
            {
                byte[] hashBytes = sha256.ComputeHash(stream);
                return Convert.ToHexString(hashBytes);
            }
        }
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Evitar borrar el prompt
            if ((e.KeyCode == Keys.Back || e.KeyCode == Keys.Left) &&
                richTextBox1.SelectionStart <= inicioComando)
            {
                e.SuppressKeyPress = true;
                return;
            }

            // Evitar escribir antes del prompt
            if (richTextBox1.SelectionStart < inicioComando)
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
            }

            // Ejecutar comando
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string texto = richTextBox1.Text;
                int index = texto.LastIndexOf(">>> ");

                string comando = texto.Substring(index + 4).Trim();

                EjecutarComandoTerminal(comando);
            }
        }

        private void MostrarPrompt()
        {
            richTextBox1.AppendText(directorioActual + " >>> ");
            inicioComando = richTextBox1.TextLength;
            richTextBox1.SelectionStart = richTextBox1.TextLength;
        }

        private void EjecutarComandoReal(string comando)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.Arguments = "/c " + comando;
                psi.WorkingDirectory = directorioActual;
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

                richTextBox1.AppendText(salida);
                richTextBox1.AppendText(errores);
                MostrarPrompt();
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText("Error: " + ex.Message);
                MostrarPrompt();
            }
        }

        private void CambiarDirectorio(string nuevaRuta)
        {
            nuevaRuta = nuevaRuta.Trim().Trim('"');

            string rutaFinal;

            if (Path.IsPathRooted(nuevaRuta))
            {
                rutaFinal = nuevaRuta;
            }
            else
            {
                rutaFinal = Path.Combine(directorioActual, nuevaRuta);
            }

            rutaFinal = Path.GetFullPath(rutaFinal);

            if (Directory.Exists(rutaFinal))
            {
                directorioActual = rutaFinal;
            }
            else
            {
                richTextBox1.AppendText("El sistema no puede encontrar la ruta especificada.\n");
            }
        }


        private void EjecutarComandoTerminal(string comando)
        {
            richTextBox1.AppendText("\n");

            if (comando.Trim() == "")
            {
                MostrarPrompt();
                return;
            }

            if (comando == "clear" || comando == "cls")
            {
                richTextBox1.Clear();
                MostrarPrompt();
                return;
            }

            if (comando.StartsWith("cd "))
            {
                CambiarDirectorio(comando.Substring(3).Trim());
                MostrarPrompt();
                return;
            }

            EjecutarComandoReal(comando);
        }


        private void fastColoredTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control &&
                (e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }


        bool guardado = false;
        string ruta_archivo = "";
        string rutaProyectoActual = "";
        private void CargarProyectoEnTreeView(string rutaProyecto)
        {
            treeView1.Nodes.Clear();

            TreeNode nodoRaiz = new TreeNode(Path.GetFileName(rutaProyecto));
            nodoRaiz.Tag = rutaProyecto;

            CargarCarpeta(rutaProyecto, nodoRaiz);

            treeView1.Nodes.Add(nodoRaiz);
            nodoRaiz.Expand();

            rutaProyectoActual = rutaProyecto;
            directorioActual = rutaProyecto;
        }
        public IDE()
        {
            InitializeComponent();
            fastColoredTextBox1.ContextMenuStrip = null;
            fastColoredTextBox1.KeyDown += fastColoredTextBox1_KeyDown;
            fastColoredTextBox1.AllowDrop = false;
            fastColoredTextBox1.DragEnter += (s, e) =>
            {
                e.Effect = DragDropEffects.None;
            };
            richTextBox1.KeyDown += richTextBox1_KeyDown;
            richTextBox1.Text = directorioActual + " >>> ";
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

        private async void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
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

                        richTextBox1.AppendText(
                            "Archivo guardado en:\n" + saveFile.FileName + "\n"
                        );

                        guardado = true;
                        ruta_archivo = saveFile.FileName;

                        string nombreArchivo = Path.GetFileName(saveFile.FileName);

                        DateTime fechaCreacion =
                            File.GetCreationTime(saveFile.FileName);



                        string fechaCreacionF =
                            fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss");



                        string firma = CalcularSHA256(saveFile.FileName);

                        string contenido =
                            File.ReadAllText(ruta_archivo);

                        string respuesta =
                            await ApiService.Instance.guardarArchivoP(
                                nombreArchivo,
                                saveFile.FileName,
                                contenido,
                                fechaCreacionF,
                                fechaCreacionF,
                                firma
                            );
                        JsonDocument doc = JsonDocument.Parse(respuesta);
                        bool success = doc.RootElement.GetProperty("success").GetBoolean();
                        if (!success)
                        {
                            string message = doc.RootElement.GetProperty("error").GetString();
                            MessageBox.Show("No se pudo guardar el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

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

                string salida =
                    proceso.StandardOutput.ReadToEnd();

                string errores =
                    proceso.StandardError.ReadToEnd();

                proceso.WaitForExit();

                richTextBox1.AppendText(salida);
                richTextBox1.AppendText(errores);
                richTextBox1.AppendText("\n>>> ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Error completo"
                );
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

        private async void toolStripButton2_Click(object sender, EventArgs e)
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
                    string nombreArchivo = Path.GetFileName(saveFile.FileName);
                    DateTime fechaCreacion = File.GetCreationTime(saveFile.FileName);
                    DateTime fechaModificacion = File.GetLastWriteTime(saveFile.FileName);
                    string fechaCreacionF = fechaCreacion.ToString(("yyyy-MM-dd HH:mm:ss"));
                    string firma = CalcularSHA256(saveFile.FileName);
                    string contenido = File.ReadAllText(ruta_archivo);
                    string respuesta = await ApiService.Instance.guardarArchivoP(nombreArchivo, saveFile.FileName, contenido, fechaCreacionF, fechaCreacionF, firma);
                    JsonDocument doc = JsonDocument.Parse(respuesta);
                    bool success = doc.RootElement.GetProperty("success").GetBoolean();
                    if (!success)
                    {
                        string message = doc.RootElement.GetProperty("error").GetString();
                        MessageBox.Show("No se pudo guardar el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else
            {
                DateTime fechaModificacionActual = File.GetLastWriteTime(ruta_archivo);
                File.WriteAllText(ruta_archivo, fastColoredTextBox1.Text);
                string nombreArchivoP = Path.GetFileName(ruta_archivo);
                DateTime fechaCreacion = File.GetCreationTime(ruta_archivo);
                DateTime fechaModificacionNueva = File.GetLastWriteTime(ruta_archivo);
                string fechaCreacionF = fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss");
                string fechaModificacionActualF = fechaModificacionActual.ToString("yyyy-MM-dd HH:mm:ss");
                string fechaModificacionNuevaF = fechaModificacionNueva.ToString("yyyy-MM-dd HH:mm:ss");
                string firma = CalcularSHA256(ruta_archivo);
                string contenido = File.ReadAllText(ruta_archivo);
                string respuesta = await ApiService.Instance.actualizarArchivoP(nombreArchivoP, ruta_archivo, contenido, fechaModificacionNuevaF, firma);
                JsonDocument doc = JsonDocument.Parse(respuesta);
                bool success = doc.RootElement.GetProperty("success").GetBoolean();
                if (!success)
                {
                    string message = doc.RootElement.GetProperty("error").GetString();
                    MessageBox.Show("No se pudo actualizar el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

        }

        private async void abrirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
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
                    string firma = CalcularSHA256(openFileDialog.FileName);
                    string json = await ApiService.Instance.verificarArchivoP(
                         Path.GetFileName(openFileDialog.FileName), firma, openFileDialog.FileName
                     );

                    JsonDocument doc = JsonDocument.Parse(json);
                    bool success = doc.RootElement.GetProperty("success").GetBoolean();

                    if (!success)
                    {
                        string message = doc.RootElement.GetProperty("error").GetString();
                        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
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
                CargarProyectoEnTreeView(folderDialog.SelectedPath);
            }
        }

        private async void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string ruta = e.Node.Tag.ToString();

            if (File.Exists(ruta))
            {
                if (Path.GetExtension(ruta) == ".py")
                {
                    string firma = CalcularSHA256(ruta);
                    string json = await ApiService.Instance.verificarArchivoP(
                        Path.GetFileName(ruta), firma, ruta

                     );

                    JsonDocument doc = JsonDocument.Parse(json);
                    bool success = doc.RootElement.GetProperty("success").GetBoolean();
                    if (!success)
                    {
                        string msg = doc.RootElement.GetProperty("error").GetString();
                        MessageBox.Show("Este archivo fue editado fuera del IDE.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
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

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void crearProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nombreProyecto = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese el nombre del proyecto:",
                "Crear proyecto",
                "NuevoProyecto"
            );

            if (string.IsNullOrWhiteSpace(nombreProyecto))
            {
                MessageBox.Show("Debe ingresar un nombre para el proyecto.");
                return;
            }

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                if (nombreProyecto.Contains(c))
                {
                    MessageBox.Show("El nombre del proyecto contiene caracteres inválidos.");
                    return;
                }
            }

            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Seleccione dónde guardar el proyecto";
            folderDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                string rutaProyecto = Path.Combine(folderDialog.SelectedPath, nombreProyecto);

                if (Directory.Exists(rutaProyecto))
                {
                    MessageBox.Show("Ya existe una carpeta con ese nombre.");
                    return;
                }

                Directory.CreateDirectory(rutaProyecto);

                CargarProyectoEnTreeView(rutaProyecto);

                richTextBox1.Clear();
                MostrarPrompt();

                MessageBox.Show("Proyecto creado correctamente.");
            }
        }

        private async void crearArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rutaProyectoActual))
            {
                MessageBox.Show("Primero debe abrir o crear un proyecto.");
                return;
            }

            TreeNode nodoSeleccionado = treeView1.SelectedNode;

            if (nodoSeleccionado == null)
            {
                MessageBox.Show("Seleccione una carpeta dentro del proyecto.");
                return;
            }

            string rutaSeleccionada = nodoSeleccionado.Tag.ToString();
            string carpetaDestino;

            if (Directory.Exists(rutaSeleccionada))
            {
                carpetaDestino = rutaSeleccionada;
            }
            else
            {
                carpetaDestino = Path.GetDirectoryName(rutaSeleccionada);
            }

            string nombreArchivo = Microsoft.VisualBasic.Interaction.InputBox(
                "Ingrese el nombre del archivo:",
                "Crear archivo",
                "main.py"
            );

            if (string.IsNullOrWhiteSpace(nombreArchivo))
            {
                MessageBox.Show("Debe ingresar un nombre para el archivo.");
                return;
            }

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                if (nombreArchivo.Contains(c))
                {
                    MessageBox.Show("El nombre del archivo contiene caracteres inválidos.");
                    return;
                }
            }

            if (Path.GetExtension(nombreArchivo) == "")
            {
                nombreArchivo += ".py";
            }

            if (Path.GetExtension(nombreArchivo).ToLower() != ".py")
            {
                MessageBox.Show("Solo se pueden crear archivos .py");
                return;
            }

            string rutaArchivoNuevo = Path.Combine(carpetaDestino, nombreArchivo);

            if (File.Exists(rutaArchivoNuevo))
            {
                MessageBox.Show("Ya existe un archivo con ese nombre.");
                return;
            }

            File.WriteAllText(rutaArchivoNuevo, "");
            string nombreArchivoP = Path.GetFileName(rutaArchivoNuevo);

            DateTime fechaCreacion = File.GetCreationTime(rutaArchivoNuevo);
            DateTime fechaModificacion = File.GetLastWriteTime(rutaArchivoNuevo);

            string fechaCreacionF = fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss");
            string fechaModificacionF = fechaModificacion.ToString("yyyy-MM-dd HH:mm:ss");

            string firma = CalcularSHA256(rutaArchivoNuevo);
            string contenido = "";

            string respuesta = await ApiService.Instance.guardarArchivoP(
                nombreArchivoP,
                rutaArchivoNuevo,
                contenido,
                fechaCreacionF,
                fechaModificacionF,
                firma
            );

            JsonDocument doc = JsonDocument.Parse(respuesta);
            bool success = doc.RootElement.GetProperty("success").GetBoolean();

            if (!success)
            {
                string error = "";

                if (doc.RootElement.TryGetProperty("error", out JsonElement errorElement))
                {
                    error = errorElement.GetString();
                }

                MessageBox.Show(
                    "No se pudo registrar el archivo en la base de datos.\n" + error,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                File.Delete(rutaArchivoNuevo);
                return;
            }

            CargarProyectoEnTreeView(rutaProyectoActual);

            fastColoredTextBox1.Text = "";
            label1.Text = nombreArchivo;
            guardado = true;
            ruta_archivo = rutaArchivoNuevo;

            MessageBox.Show("Archivo creado correctamente.");
        }
    }
}
