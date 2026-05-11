using System;
using System.Collections.Generic;
using SistemaEntregas.Archivos;
using SistemaEntregas.Auth;
using SistemaEntregas.Bitacora;
using SistemaEntregas.Editor;
using SistemaEntregas.Tareas;

namespace SistemaEntregas.Core
{
    /// <summary>
    /// Fachada del sistema. Implementa el patrón Singleton para garantizar una única instancia.
    /// Centraliza el acceso a todos los subsistemas.
    /// </summary>
    public class BackendFachada
    {
        private static BackendFachada instancia;
        private static bool inicializado = false;

        // Subsistemas internos
        private readonly Dictionary<int, Usuario> usuarios = new();
        private readonly Dictionary<int, Tarea> tareas = new();
        private readonly Dictionary<int, Entrega> entregas = new();
        private readonly Dictionary<int, ArchivoP> archivos = new();
        private readonly Dictionary<int, GrupoTrabajo> grupos = new();
        private readonly Bitacora.Bitacora bitacora = new();

        private int contadorUsuarios = 1;
        private int contadorTareas = 1;
        private int contadorEntregas = 1;
        private int contadorArchivos = 1;
        private int contadorGrupos = 1;

        /// <summary>
        /// Constructor privado — patrón Singleton.
        /// </summary>
        private BackendFachada()
        {
        }

        public static BackendFachada GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new BackendFachada();
                inicializado = true;
            }
            return instancia;
        }

        // ── Auth ──────────────────────────────────────────────

        public string Login(string correo, string contrasena)
        {
            foreach (var usuario in usuarios.Values)
            {
                if (usuario.IniciarSesion(correo, contrasena))
                    return $"TOKEN_{usuario.Id}_{usuario.Rol}";
            }
            return string.Empty;
        }

        public int Registrar(string correo, string nombre, string contrasena, string rol)
        {
            UsuarioFactory factory = rol.ToUpper() == "PROFESOR"
                ? new ProfesorFactory()
                : new EstudianteFactory();

            var usuario = factory.CrearUsuario(correo, nombre, contrasena);
            var id = contadorUsuarios++;
            usuarios[id] = usuario;
            return id;
        }

        // ── Tareas ────────────────────────────────────────────

        public List<Tarea> GetTareas(int usuarioId, string rol)
        {
            return new List<Tarea>(tareas.Values);
        }

        public int CrearTarea(string nombre, string descripcion, DateTime fechaEntrega, bool esGrupal, int profesorId)
        {
            var tarea = new Tarea
            {
                Id = contadorTareas,
                Nombre = nombre,
                Descripcion = descripcion,
                FechaEntrega = fechaEntrega,
                EsGrupal = esGrupal,
                FechaCreacion = DateTime.Now
            };
            tareas[contadorTareas] = tarea;
            return contadorTareas++;
        }

        public void EditarTarea(int idTarea, Tarea datos)
        {
            if (tareas.TryGetValue(idTarea, out var tarea))
            {
                tarea.Nombre = datos.Nombre;
                tarea.Descripcion = datos.Descripcion;
                tarea.FechaEntrega = datos.FechaEntrega;
                tarea.EsGrupal = datos.EsGrupal;
            }
        }

        public void EliminarTarea(int idTarea)
        {
            tareas.Remove(idTarea);
        }

        // ── Entregas ──────────────────────────────────────────

        public List<Entrega> GetEntregas(int idTarea)
        {
            return tareas.TryGetValue(idTarea, out var tarea)
                ? tarea.ObtenerEntregas()
                : new List<Entrega>();
        }

        public void RegistrarEntrega(Entrega entrega)
        {
            var id = contadorEntregas++;
            entrega.Id = id;
            entregas[id] = entrega;
            entrega.Registrar();
            bitacora.Registrar(entrega);
        }

        public void AsignarNota(int idEntrega, float nota)
        {
            if (entregas.TryGetValue(idEntrega, out var entrega))
                entrega.Nota = nota;
        }

        // ── Archivos ──────────────────────────────────────────

        public int CrearArchivo(int estudianteId, string nombre, string contenido)
        {
            var archivo = new ArchivoP
            {
                Id = contadorArchivos,
                Nombre = nombre,
                Contenido = contenido,
                FechaCreacion = DateTime.Now
            };
            archivos[contadorArchivos] = archivo;
            return contadorArchivos++;
        }

        public ResultadoEjecucion EjecutarArchivo(int idArchivo)
        {
            if (archivos.TryGetValue(idArchivo, out var archivo))
            {
                var terminal = new Terminal();
                return terminal.Ejecutar(archivo);
            }
            return new ResultadoEjecucion { Errores = "Archivo no encontrado", CodigoSalida = -1 };
        }

        // ── Grupos ────────────────────────────────────────────

        public int CrearGrupo(int idTarea, string nombre, List<int> miembros)
        {
            if (!tareas.TryGetValue(idTarea, out var tarea))
                return -1;

            var grupo = new GrupoTrabajo
            {
                Id = contadorGrupos,
                Nombre = nombre,
                Tarea = tarea
            };

            foreach (var idEstudiante in miembros)
            {
                if (usuarios.TryGetValue(idEstudiante, out var u) && u is Estudiante estudiante)
                    grupo.AgregarMiembro(estudiante);
            }

            grupos[contadorGrupos] = grupo;
            return contadorGrupos++;
        }

        // ── Bitácora ──────────────────────────────────────────

        public List<RegistroEntrega> GetHistorial(int idTarea, int idEstudiante)
        {
            if (!tareas.TryGetValue(idTarea, out var tarea)) return new List<RegistroEntrega>();
            if (!usuarios.TryGetValue(idEstudiante, out var u) || u is not Estudiante estudiante) return new List<RegistroEntrega>();

            return bitacora.ObtenerHistorial(tarea, estudiante);
        }
    }
}
