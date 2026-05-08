using System;
using System.IO;
using SistemaEntregas.Core;

namespace SistemaEntregas.Tareas
{
    public class CopiaEntrega : IPrototipo
    {
        private int id;
        private DateTime fechaHora;
        private string nombreArchivo;
        private string contenido;
        private string marcaTemporal;
        private string checksumFirma;

        public int Id { get => id; set => id = value; }
        public DateTime FechaHora { get => fechaHora; set => fechaHora = value; }
        public string NombreArchivo { get => nombreArchivo; set => nombreArchivo = value; }
        public string Contenido { get => contenido; set => contenido = value; }
        public string MarcaTemporal { get => marcaTemporal; set => marcaTemporal = value; }
        public string ChecksumFirma { get => checksumFirma; set => checksumFirma = value; }

        public void Guardar()
        {
            var ruta = ObtenerRuta();
            Directory.CreateDirectory(Path.GetDirectoryName(ruta)!);
            File.WriteAllText(ruta, contenido);
        }

        public string ObtenerRuta()
        {
            return Path.Combine("copias", marcaTemporal, nombreArchivo);
        }

        public IPrototipo Clonar()
        {
            return (CopiaEntrega)this.MemberwiseClone();
        }
    }
}
