using System.Security.Cryptography;
using System.Text;
using SistemaEntregas.Archivos;

namespace SistemaEntregas.Archivos
{
    public class GestorFirma
    {
        private string claveSecreta;
        private string algoritmo;

        public GestorFirma(string claveSecreta = "clave-secreta-default", string algoritmo = "SHA256")
        {
            this.claveSecreta = claveSecreta;
            this.algoritmo = algoritmo;
        }

        public string GenerarFirma(ArchivoP archivo)
        {
            var datos = archivo.Contenido + claveSecreta;
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(claveSecreta));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(datos));
            return Convert.ToHexString(hash);
        }

        public bool ValidarFirma(ArchivoP archivo, string firma)
        {
            var firmaEsperada = GenerarFirma(archivo);
            return firmaEsperada == firma;
        }

        public void InsertarComentarioFirma(ArchivoP archivo)
        {
            var firma = GenerarFirma(archivo);
            archivo.Escribir($"# FIRMA: {firma}\n" + archivo.Leer());
        }

        public string ExtraerFirmaDeContenido(string contenido)
        {
            var lineas = contenido.Split('\n');
            foreach (var linea in lineas)
            {
                if (linea.StartsWith("# FIRMA: "))
                    return linea.Replace("# FIRMA: ", "").Trim();
            }
            return string.Empty;
        }
    }
}
