<?php

namespace SistemaEntregas\Editor;

use SistemaEntregas\Archivos\ArchivoP;

class Terminal
{
    private string $salida  = '';
    private string $proceso = '';

    public function getSalida(): string { return $this->salida; }
    public function getProceso(): string { return $this->proceso; }

    public function ejecutar(ArchivoP $archivo): ResultadoEjecucion
    {
        $resultado = new ResultadoEjecucion();
        $inicio    = microtime(true);

        try {
            $this->proceso = $archivo->getNombre();

            $descriptors = [
                1 => ['pipe', 'w'], // stdout
                2 => ['pipe', 'w'], // stderr
            ];

            $proc = proc_open(
                ['python3', $archivo->getRuta()],
                $descriptors,
                $pipes
            );

            if (!is_resource($proc)) {
                throw new \RuntimeException('No se pudo iniciar el proceso.');
            }

            $resultado->setSalidaStd(stream_get_contents($pipes[1]));
            $resultado->setErrores(stream_get_contents($pipes[2]));

            fclose($pipes[1]);
            fclose($pipes[2]);

            $resultado->setCodigoSalida(proc_close($proc));

        } catch (\Exception $ex) {
            $resultado->setErrores($ex->getMessage());
            $resultado->setCodigoSalida(-1);
        }

        $resultado->setDuracion((microtime(true) - $inicio) * 1000); // Convertir a ms
        $this->salida = $resultado->getSalidaStd();
        return $resultado;
    }

    public function mostrarSalida(string $salida): void
    {
        $this->salida = $salida;
        echo $salida . PHP_EOL;
    }

    public function limpiar(): void
    {
        $this->salida  = '';
        $this->proceso = '';
    }

    public function detenerEjecucion(): void
    {
        // Implementar cancelación del proceso en ejecución
    }
}
// Los puntos nuevos en esta traducción:
// 
// - **`Stopwatch`**: Se reemplaza con `microtime(true)`, que retorna el tiempo actual en segundos con microsegundos. La diferencia entre inicio y fin se multiplica por `1000` para convertir a milisegundos, equivalente a `Elapsed.TotalMilliseconds`.
// - **`ProcessStartInfo` + `Process.Start()`**: Se reemplaza con `proc_open()`, que es el equivalente más completo en PHP para lanzar procesos externos con control sobre stdin, stdout y stderr. Se pasa un array en lugar de una cadena para evitar problemas de escapado de argumentos, equivalente a `ArgumentList` en C#.
// - **`RedirectStandardOutput/Error`**: Se logra con el array de descriptores `$descriptors`, mapeando los pipes de salida y error del proceso.
// - **`proc.StandardOutput.ReadToEnd()`**: Se traduce como `stream_get_contents($pipes[1])`, que lee todo el contenido del pipe de stdout.
// - **`proc.WaitForExit()` + `proc.ExitCode`**: Se combinan en `proc_close($proc)`, que espera a que el proceso termine y retorna su código de salida.