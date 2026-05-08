using System.Collections.Generic;
using SistemaEntregas.Tareas;

namespace SistemaEntregas.Auth
{
    public class Profesor : Usuario
    {
        private int codigoProfesor;

        public int CodigoProfesor => codigoProfesor;

        public Profesor()
        {
            rol = RolUsuario.PROFESOR;
        }

        public Profesor(int codigo)
        {
            codigoProfesor = codigo;
            rol = RolUsuario.PROFESOR;
        }

        public Tarea CrearTarea(string nombre, string descripcion, System.DateTime fechaEntrega, bool esGrupal)
        {
            var tarea = new Tarea
            {
                Nombre = nombre,
                Descripcion = descripcion,
                FechaEntrega = fechaEntrega,
                EsGrupal = esGrupal,
                FechaCreacion = System.DateTime.Now
            };
            return tarea;
        }

        public void EditarTarea(Tarea tarea)
        {
            // Modifica los datos de la tarea
        }

        public void EliminarTarea(int id)
        {
            // Elimina la tarea con el id indicado
        }

        public GrupoTrabajo AsignarGrupo(Tarea tarea, List<Estudiante> estudiantes)
        {
            var grupo = new GrupoTrabajo { Tarea = tarea };
            foreach (var estudiante in estudiantes)
                grupo.AgregarMiembro(estudiante);
            return grupo;
        }

        public List<Entrega> ConsultarEntregas(Tarea tarea)
        {
            return tarea.ObtenerEntregas();
        }

        public void RevisarEntrega(Entrega entrega)
        {
            // Lógica de revisión de entrega
        }

        public void AsignarNota(Entrega entrega, float nota)
        {
            entrega.Nota = nota;
        }
    }
}
