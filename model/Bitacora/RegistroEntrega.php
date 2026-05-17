<?php

namespace SistemaEntregas\Bitacora;

use DateTime;

class RegistroEntrega
{
    private int $id;
    private DateTime $fechaHora;
    private int $version;
    private int $idEntrega;
    private string $firmaArchivo;

    public function getId(): int { return $this->id; }
    public function setId(int $id): void { $this->id = $id; }

    public function getFechaHora(): DateTime { return $this->fechaHora; }
    public function setFechaHora(DateTime $fechaHora): void { $this->fechaHora = $fechaHora; }

    public function getVersion(): int { return $this->version; }
    public function setVersion(int $version): void { $this->version = $version; }

    public function getIdEntrega(): int { return $this->idEntrega; }
    public function setIdEntrega(int $idEntrega): void { $this->idEntrega = $idEntrega; }

    public function getFirmaArchivo(): string { return $this->firmaArchivo; }
    public function setFirmaArchivo(string $firmaArchivo): void { $this->firmaArchivo = $firmaArchivo; }

    public function __toString(): string
    {
        return sprintf(
            '[v%d] %s | Entrega#%d | Firma: %s',
            $this->version,
            $this->fechaHora->format('d/m/Y H:i:s'),
            $this->idEntrega,
            $this->firmaArchivo
        );
    }
}

// El único punto nuevo en esta traducción:
// 
// - **`ToString()` con interpolación de cadenas**: C# usa `$"..."` con especificadores de formato como `{fechaHora:dd/MM/yyyy HH:mm:ss}`. 
// En PHP se reemplaza con `sprintf()` para los valores enteros y de texto, y con `$this->fechaHora->format('d/m/Y H:i:s')` 
// para formatear el `DateTime`, que es el equivalente directo al especificador de formato de C#. El método se llama `__toString()` 
// en PHP (con doble guión bajo), y es invocado automáticamente cuando el objeto se usa en un contexto de cadena.