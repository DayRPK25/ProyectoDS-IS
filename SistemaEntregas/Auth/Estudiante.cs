using System;
using System.Collections.Generic;
using SistemaEntregas.Archivos;
using SistemaEntregas.Editor;
using SistemaEntregas.Tareas;

namespace SistemaEntregas.Auth
{
    public class Estudiante : Usuario
    {
        private int codigoEstudiante;

        public int CodigoEstudiante => codigoEstudiante;

        public Estudiante()
        {
            rol = RolUsuario.ESTUDIANTE;
        }

        public Estudiante(int codigo)
        {
            codigoEstudiante = codigo;
            rol = RolUsuario.ESTUDIANTE;
        }

        public ArchivoP CrearArchivo(string nombre)
        {
            return new ArchivoP
            {
                Nombre = nombre,
                FechaCreacion = DateTime.Now
            };
        }

        public ArchivoP AbrirArchivo(string ruta)
        {
            var archivo = new ArchivoP { Ruta = ruta };
            return archivo;
        }

        public void EditarContenido()
        {
            // Abre el editor de código para editar el archivo actual
        }

        public ResultadoEjecucion EjecutarCodigo()
        {
            var terminal = new Terminal();
            // Se ejecuta el archivo actual del estudiante
            return new ResultadoEjecucion();
        }

        public Entrega EntregarTarea(Tarea tarea)
        {
            var entrega = new Entrega
            {
                Tarea = tarea,
                Estudiante = this,
                FechaHora = DateTime.Now,
                Version = 1
            };
            entrega.Registrar();
            return entrega;
        }

        public List<Entrega> ConsultarBitacora(Tarea tarea)
        {
            return tarea.ObtenerEntregas();
        }
    }
}
