using System.Collections.Generic;
using SistemaEntregas.Auth;

namespace SistemaEntregas.Tareas
{
    public class GrupoTrabajo
    {
        private int id;
        private string nombre;
        private Tarea tarea;
        private readonly List<Estudiante> miembros = new();

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public Tarea Tarea { get => tarea; set => tarea = value; }

        public void AgregarMiembro(Estudiante estudiante)
        {
            if (!miembros.Contains(estudiante))
                miembros.Add(estudiante);
        }

        public void QuitarMiembro(Estudiante estudiante)
        {
            miembros.Remove(estudiante);
        }

        public bool TieneMiembro(Estudiante estudiante)
        {
            return miembros.Contains(estudiante);
        }

        public List<Estudiante> ObtenerMiembros()
        {
            return new List<Estudiante>(miembros);
        }
    }
}
