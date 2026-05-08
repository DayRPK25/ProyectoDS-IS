using System;

namespace SistemaEntregas.Bitacora
{
    public class RegistroEntrega
    {
        private int id;
        private DateTime fechaHora;
        private int version;
        private int idEntrega;
        private string firmaArchivo;

        public int Id { get => id; set => id = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public int Version { get => version; set => version = value; }
        public int IdEntrega { get => idEntrega; set => idEntrega = value; }
        public string FirmaArchivo { get => firmaArchivo; set => firmaArchivo = value; }

        public override string ToString()
        {
            return $"[v{version}] {fechaHora:dd/MM/yyyy HH:mm:ss} | Entrega#{idEntrega} | Firma: {firmaArchivo}";
        }
    }
}
