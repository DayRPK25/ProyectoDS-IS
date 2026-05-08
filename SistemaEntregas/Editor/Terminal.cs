using System;
using System.Diagnostics;
using SistemaEntregas.Archivos;

namespace SistemaEntregas.Editor
{
    public class Terminal
    {
        private string salida;
        private string proceso;

        public string Salida => salida;
        public string Proceso => proceso;

        public ResultadoEjecucion Ejecutar(ArchivoP archivo)
        {
            var resultado = new ResultadoEjecucion();
            var cronometro = Stopwatch.StartNew();

            try
            {
                proceso = archivo.Nombre;
                var psi = new ProcessStartInfo
                {
                    FileName = "python3",
                    Arguments = $"\"{archivo.Ruta}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var proc = Process.Start(psi)!;
                resultado.SalidaStd = proc.StandardOutput.ReadToEnd();
                resultado.Errores = proc.StandardError.ReadToEnd();
                proc.WaitForExit();
                resultado.CodigoSalida = proc.ExitCode;
            }
            catch (Exception ex)
            {
                resultado.Errores = ex.Message;
                resultado.CodigoSalida = -1;
            }

            cronometro.Stop();
            resultado.Duracion = cronometro.Elapsed;
            salida = resultado.SalidaStd;
            return resultado;
        }

        public void MostrarSalida(string salida)
        {
            this.salida = salida;
            Console.WriteLine(salida);
        }

        public void Limpiar()
        {
            salida = string.Empty;
            proceso = string.Empty;
        }

        public void DetenerEjecucion()
        {
            // Implementar cancelación del proceso en ejecución
        }
    }
}