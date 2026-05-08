using System;

namespace SistemaEntregas.Editor
{
    public class RestriccionCopyPaste
    {
        private bool activa;

        public bool Activa => activa;

        public RestriccionCopyPaste(bool activa = true)
        {
            this.activa = activa;
        }

        public void InterceptarCtrl(object evento)
        {
            if (activa)
            {
                MostrarMensajeBloqueo();
                // Cancelar el evento de teclado (Ctrl+C, Ctrl+V, etc.)
            }
        }

        public void InterceptarClicD(object evento)
        {
            if (activa)
            {
                MostrarMensajeBloqueo();
                // Cancelar el menú contextual del clic derecho
            }
        }

        public void InterceptarMenuContextual()
        {
            if (activa)
            {
                MostrarMensajeBloqueo();
            }
        }

        public void MostrarMensajeBloqueo()
        {
            Console.WriteLine("⚠ Acción bloqueada: el copy-paste está deshabilitado en este editor.");
        }
    }
}
