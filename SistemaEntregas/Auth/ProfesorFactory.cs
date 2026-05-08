using SistemaEntregas.Editor;

namespace SistemaEntregas.Auth
{
    /// <summary>
    /// Concrete Factory que crea instancias de Profesor y sus componentes.
    /// </summary>
    public class ProfesorFactory : UsuarioFactory
    {
        public override Profesor CrearUsuario(string correo, string nombre, string contrasena)
        {
            var profesor = new Profesor();
            profesor.Registrar(correo, nombre, contrasena, "PROFESOR");
            return profesor;
        }

        public override EditorCodigo CrearEditor()
        {
            return new EditorCodigo
            {
                CopyPasteHabilitado = true // Profesores tienen acceso completo
            };
        }

        public override string CrearInterfaz()
        {
            return "InterfazProfesor";
        }
    }
}
