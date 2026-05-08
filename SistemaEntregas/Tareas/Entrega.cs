using System;
using SistemaEntregas.Archivos;
using SistemaEntregas.Auth;
using SistemaEntregas.Core;

namespace SistemaEntregas.Tareas
{
    public class Entrega : IPrototipo
    {
        private int id;
        private DateTime fechaHora;
        private int version;
        private ArchivoP archivo;
        private Estudiante estudiante;
        private Tarea tarea;
        private float nota;
        private string comentarioProfesor;

        public int Id { get => id; set => id = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public int Version { get => version; set => version = value; }
        public ArchivoP Archivo { get => archivo; set => archivo = value; }
        public Estudiante Estudiante { get => estudiante; set => estudiante = value; }
        public Tarea Tarea { get => tarea; set => tarea = value; }
        public float Nota { get => nota; set => nota = value; }
        public string ComentarioProfesor { get => comentarioProfesor; set => comentarioProfesor = value; }

        public void Registrar()
        {
            fechaHora = DateTime.Now;
            tarea?.AgregarEntrega(this);
        }

        public CopiaEntrega GenerarCopia()
        {
            return new CopiaEntrega
            {
                FechaHora = DateTime.Now,
                NombreArchivo = archivo?.Nombre ?? string.Empty,
                Contenido = archivo?.Leer() ?? string.Empty,
                MarcaTemporal = DateTime.Now.ToString("yyyyMMddHHmmss"),
                ChecksumFirma = archivo?.Firma ?? string.Empty
            };
        }

        public string ObtenerFirma()
        {
            return archivo?.Firma ?? string.Empty;
        }

        public IPrototipo Clonar()
        {
            return (Entrega)this.MemberwiseClone();
        }
    }
}
