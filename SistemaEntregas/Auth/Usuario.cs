using System;

namespace SistemaEntregas.Auth
{
    public abstract class Usuario
    {
        protected int id;
        protected string correo;
        protected string nombreUsuario;
        protected string contrasena;
        protected RolUsuario rol;
        protected DateTime fechaCreacion;

        public int Id => id;
        public string Correo => correo;
        public string NombreUsuario => nombreUsuario;
        public RolUsuario Rol => rol;
        public DateTime FechaCreacion => fechaCreacion;

        public virtual void Registrar(string correo, string nombre, string contrasena, string rol)
        {
            this.correo = correo;
            this.nombreUsuario = nombre;
            this.contrasena = contrasena;
            this.rol = Enum.Parse<RolUsuario>(rol, true);
            this.fechaCreacion = DateTime.Now;
        }

        public virtual bool IniciarSesion(string usuario, string contrasena)
        {
            return this.correo == usuario && this.contrasena == contrasena;
        }

        public virtual void CerrarSesion()
        {
            // Limpiar estado de sesión
        }

        public virtual bool ValidarCredenciales()
        {
            return !string.IsNullOrEmpty(correo) && !string.IsNullOrEmpty(contrasena);
        }

        public virtual bool ValidarFormato()
        {
            return correo.Contains("@") && contrasena.Length >= 6;
        }
    }
}
