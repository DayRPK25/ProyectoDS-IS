using SistemaEntregas.Editor;

namespace SistemaEntregas.Auth
{
    /// <summary>
    /// Abstract Factory para la creación de usuarios y sus componentes asociados.
    /// </summary>
    public abstract class UsuarioFactory
    {
        public abstract Usuario CrearUsuario(string correo, string nombre, string contrasena);

        public abstract EditorCodigo CrearEditor();

        public abstract string CrearInterfaz();
    }
}
