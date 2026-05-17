<?php

namespace model\Archivos;

use DateTime;

class ArchivoP
{
    private int $id;
    /** El nombre debe incluir la extensión .py (ej: "solucion.py") */
    private string $nombre;
    private string $ruta;
    private string $contenido;
    private DateTime $fechaCreacion;
    private DateTime $fechaModificacion;
    private string $firma;

    // Getters y Setters
    public function getId(): int { return $this->id; }
    public function setId(int $id): void { $this->id = $id; }

    public function getNombre(): string { return $this->nombre; }
    public function setNombre(string $nombre): void { $this->nombre = $nombre; }

    public function getRuta(): string { return $this->ruta; }
    public function setRuta(string $ruta): void { $this->ruta = $ruta; }

    public function getContenido(): string { return $this->contenido; }
    public function setContenido(string $contenido): void { $this->contenido = $contenido; }

    public function getFechaCreacion(): DateTime { return $this->fechaCreacion; }
    public function setFechaCreacion(DateTime $fecha): void { $this->fechaCreacion = $fecha; }

    public function getFechaModificacion(): DateTime { return $this->fechaModificacion; }
    public function setFechaModificacion(DateTime $fecha): void { $this->fechaModificacion = $fecha; }

    public function getFirma(): string { return $this->firma; }
    public function setFirma(string $firma): void { $this->firma = $firma; }

    public function leer(): string
    {
        return $this->contenido ?? '';
    }

    public function escribir(string $nuevoContenido): void
    {
        $this->contenido = $nuevoContenido;
        $this->fechaModificacion = new DateTime();
    }

    public function guardar(): void
    {
        if (!empty($this->ruta)) {
            file_put_contents($this->ruta, $this->contenido);
        }
        $this->fechaModificacion = new DateTime();
    }

    public function obtenerHash(): string
    {
        return strtoupper(hash('sha256', $this->contenido ?? ''));
    }

    public function establecerIntegridad(): bool
    {
        $this->firma = $this->obtenerHash();
        return !empty($this->firma);
    }
}
?>
//Los puntos nuevos en esta traducción:
//
//- **Propiedades con getter y setter**: C# usa propiedades con `get` y `set` en una sola declaración. En PHP se separan en métodos `get*()` y `set*()` individuales.
//- **`System.IO.File.WriteAllText()`**: Se traduce como `file_put_contents()`, su equivalente directo en PHP.
//- **`SHA256` + `Convert.ToHexString()`**: PHP no requiere librerías externas para esto. La función nativa `hash('sha256', $texto)` devuelve directamente el hash en hexadecimal, y `strtoupper()` lo convierte a mayúsculas para mantener el mismo formato que `Convert.ToHexString()` de C#.
//- **`string.Empty`**: Se reemplaza con `''`, cadena vacía estándar en PHP.