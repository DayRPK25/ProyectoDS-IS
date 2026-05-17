<?php

namespace SistemaEntregas\Tareas;

use DateTime;
use SistemaEntregas\Core\IPrototipo;

class CopiaEntrega implements IPrototipo
{
    private int $id;
    private DateTime $fechaHora;
    private string $nombreArchivo = '';
    private string $contenido     = '';
    private string $marcaTemporal = '';
    private string $checksumFirma = '';

    public function getId(): int { return $this->id; }
    public function setId(int $id): void { $this->id = $id; }

    public function getFechaHora(): DateTime { return $this->fechaHora; }
    public function setFechaHora(DateTime $fechaHora): void { $this->fechaHora = $fechaHora; }

    public function getNombreArchivo(): string { return $this->nombreArchivo; }
    public function setNombreArchivo(string $nombreArchivo): void { $this->nombreArchivo = $nombreArchivo; }

    public function getContenido(): string { return $this->contenido; }
    public function setContenido(string $contenido): void { $this->contenido = $contenido; }

    public function getMarcaTemporal(): string { return $this->marcaTemporal; }
    public function setMarcaTemporal(string $marcaTemporal): void { $this->marcaTemporal = $marcaTemporal; }

    public function getChecksumFirma(): string { return $this->checksumFirma; }
    public function setChecksumFirma(string $checksumFirma): void { $this->checksumFirma = $checksumFirma; }

    public function guardar(): void
    {
        $ruta = $this->obtenerRuta();
        $directorio = dirname($ruta);

        if (!is_dir($directorio)) {
            mkdir($directorio, 0755, true); // true = creación recursiva
        }

        file_put_contents($ruta, $this->contenido);
    }

    public function obtenerRuta(): string
    {
        return implode(DIRECTORY_SEPARATOR, ['copias', $this->marcaTemporal, $this->nombreArchivo]);
    }

    public function clonar(): static
    {
        return clone $this;
    }
}
// Los puntos nuevos en esta traducción:
// 
// - **`MemberwiseClone()`**: Realiza una copia superficial (*shallow copy*) del objeto en C#. En PHP su equivalente directo es la palabra clave `clone`, que también produce una copia superficial del objeto.
// - **`Directory.CreateDirectory()` + `Path.GetDirectoryName()`**: Se reemplaza con la combinación de `dirname()` para obtener el directorio padre y `mkdir($dir, 0755, true)` para crearlo de forma recursiva, equivalente al comportamiento de `CreateDirectory` que crea todos los directorios intermedios necesarios.
// - **`Path.Combine()`**: Se traduce como `implode(DIRECTORY_SEPARATOR, [...])`, usando la constante `DIRECTORY_SEPARATOR` para garantizar compatibilidad entre sistemas operativos, igual que hace `Path.Combine()` en C#.
// - **`File.WriteAllText()`**: Se traduce nuevamente como `file_put_contents()`, su equivalente directo en PHP.