using SistemaEntregas.Editor;

namespace SistemaEntregas.Auth
{
    /// <summary>
    /// Concrete Factory que crea instancias de Estudiante y sus componentes.
    /// </summary>
    public class EstudianteFactory : UsuarioFactory
    {
        public override Estudiante CrearUsuario(string correo, string nombre, string contrasena)
        {
            var estudiante = new Estudiante();
            estudiante.Registrar(correo, nombre, contrasena, "ESTUDIANTE");
            return estudiante;
        }

        public override EditorCodigo CrearEditor()
        {
            return new EditorCodigo
            {
                CopyPasteHabilitado = false // Estudiantes tienen copy-paste restringido
            };
        }

        public override string CrearInterfaz()
        {
            return "InterfazEstudiante";
        }
    }
}
