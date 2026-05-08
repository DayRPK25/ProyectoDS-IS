using System;
using System.Collections.Generic;

namespace SistemaEntregas.Tareas
{
    public class Tarea
    {
        private int id;
        private string nombre;
        private string descripcion;
        private DateTime fechaCreacion;
        private DateTime fechaEntrega;
        private bool esGrupal;

        private readonly List<GrupoTrabajo> grupos = new();
        private readonly List<Entrega> entregas = new();

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
        public DateTime FechaEntrega { get => fechaEntrega; set => fechaEntrega = value; }
        public bool EsGrupal { get => esGrupal; set => esGrupal = value; }

        public void Publicar()
        {
            // Marca la tarea como publicada y disponible para los estudiantes
            Console.WriteLine($"Tarea '{nombre}' publicada.");
        }

        public void Cerrar()
        {
            fechaEntrega = DateTime.Now;
            Console.WriteLine($"Tarea '{nombre}' cerrada.");
        }

        public void AgregarGrupo(string grupo)
        {
            grupos.Add(new GrupoTrabajo { Nombre = grupo, Tarea = this });
        }

        public List<Entrega> ObtenerEntregas()
        {
            return entregas;
        }

        public bool EstaVigente()
        {
            return DateTime.Now <= fechaEntrega;
        }

        public void AgregarEntrega(Entrega entrega)
        {
            entregas.Add(entrega);
        }
    }
}
