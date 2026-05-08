using System;

namespace SistemaEntregas.Editor
{
    public class ResultadoEjecucion
    {
        private string salidaStd;
        private string errores;
        private int codigoSalida;
        private TimeSpan duracion;

        public string SalidaStd { get => salidaStd; set => salidaStd = value; }
        public string Errores { get => errores; set => errores = value; }
        public int CodigoSalida { get => codigoSalida; set => codigoSalida = value; }
        public TimeSpan Duracion { get => duracion; set => duracion = value; }

        public bool FueExitoso()
        {
            return codigoSalida == 0 && string.IsNullOrEmpty(errores);
        }

        public override string ToString()
        {
            return $"[Código: {codigoSalida}] Duración: {duracion.TotalMilliseconds}ms\n" +
                   $"Salida: {salidaStd}\n" +
                   (string.IsNullOrEmpty(errores) ? "" : $"Errores: {errores}");
        }
    }
}
