using System;
using SistemaEntregas.Archivos;

namespace SistemaEntregas.Editor
{
    public class EditorCodigo
    {
        private string contenido;
        private ArchivoP archivoActual;
        private bool copyPasteHabilitado;
        private int cursorPosicion;

        private RestriccionCopyPaste restriccion;
        private Terminal terminal;

        public string Contenido => contenido;
        public ArchivoP ArchivoActual => archivoActual;
        public bool CopyPasteHabilitado
        {
            get => copyPasteHabilitado;
            set
            {
                copyPasteHabilitado = value;
                restriccion = new RestriccionCopyPaste(!value);
            }
        }
        public int CursorPosicion => cursorPosicion;

        public EditorCodigo()
        {
            terminal = new Terminal();
            restriccion = new RestriccionCopyPaste(false);
        }

        public void AbrirArchivo(ArchivoP archivo)
        {
            archivoActual = archivo;
            contenido = archivo.Leer();
            cursorPosicion = 0;
        }

        public void GuardarArchivo()
        {
            archivoActual?.Escribir(contenido);
            archivoActual?.Guardar();
        }

        public void EscribirCaracter(char c)
        {
            if (cursorPosicion <= contenido.Length)
            {
                contenido = contenido.Insert(cursorPosicion, c.ToString());
                cursorPosicion++;
            }
        }

        public bool InterceptarAtajoEvento()
        {
            if (!copyPasteHabilitado)
            {
                restriccion.InterceptarCtrl(null!);
                return true; // Evento interceptado
            }
            return false;
        }

        public void BloquearCopyPaste()
        {
            copyPasteHabilitado = false;
            restriccion = new RestriccionCopyPaste(true);
        }

        public void MostrarResultadoSintaxis()
        {
            if (archivoActual != null)
            {
                var resultado = terminal.Ejecutar(archivoActual);
                Console.WriteLine(resultado);
            }
        }
    }
}
