using System;
using System.Security.Cryptography;
using System.Text;

namespace SistemaEntregas.Archivos
{
    public class ArchivoP
    {
        private int id;
        private string nombre;
        private string ruta;
        private string contenido;
        private DateTime fechaCreacion;
        private DateTime fechaModificacion;
        private string firma;

        public int Id { get => id; set => id = value; }
        /// <summary>El nombre debe incluir la extensión .py (ej: "solucion.py")</summary>
        public string Nombre { get => nombre; set => nombre = value; }
        public string Ruta { get => ruta; set => ruta = value; }
        public string Contenido { get => contenido; set => contenido = value; }
        public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
        public DateTime FechaModificacion { get => fechaModificacion; set => fechaModificacion = value; }
        public string Firma { get => firma; set => firma = value; }

        public string Leer()
        {
            return contenido ?? string.Empty;
        }

        public void Escribir(string nuevoContenido)
        {
            contenido = nuevoContenido;
            fechaModificacion = DateTime.Now;
        }

        public void Guardar()
        {
            if (!string.IsNullOrEmpty(ruta))
                System.IO.File.WriteAllText(ruta, contenido);
            fechaModificacion = DateTime.Now;
        }

        public string ObtenerHash()
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contenido ?? string.Empty));
            return Convert.ToHexString(bytes);
        }

        public bool EstablecerIntegridad()
        {
            firma = ObtenerHash();
            return !string.IsNullOrEmpty(firma);
        }
    }
}
