<?php

namespace SistemaEntregas\Editor;

class ResultadoEjecucion
{
    private string $salidaStd = '';
    private string $errores   = '';
    private int $codigoSalida = 0;
    private float $duracion   = 0.0; // Duración en milisegundos

    public function getSalidaStd(): string { return $this->salidaStd; }
    public function setSalidaStd(string $salidaStd): void { $this->salidaStd = $salidaStd; }

    public function getErrores(): string { return $this->errores; }
    public function setErrores(string $errores): void { $this->errores = $errores; }

    public function getCodigoSalida(): int { return $this->codigoSalida; }
    public function setCodigoSalida(int $codigoSalida): void { $this->codigoSalida = $codigoSalida; }

    public function getDuracion(): float { return $this->duracion; }
    public function setDuracion(float $duracion): void { $this->duracion = $duracion; }

    public function fueExitoso(): bool
    {
        return $this->codigoSalida === 0 && empty($this->errores);
    }

    public function __toString(): string
    {
        return sprintf(
            "[Código: %d] Duración: %.2fms\nSalida: %s\n%s",
            $this->codigoSalida,
            $this->duracion,
            $this->salidaStd,
            empty($this->errores) ? '' : "Errores: {$this->errores}"
        );
    }
}
// El único punto nuevo en esta traducción:
// 
// - **`TimeSpan`**: PHP no tiene una clase equivalente a `TimeSpan` de C#. Dado que el único uso en la clase es `duracion.TotalMilliseconds`, 
// se simplifica directamente a un `float` que representa la duración en milisegundos, eliminando la necesidad de una clase contenedora. 
// Si el proyecto requiriera operaciones más complejas con tiempos, se podría usar `DateInterval` de PHP, 
// pero para este caso sería innecesariamente complejo.