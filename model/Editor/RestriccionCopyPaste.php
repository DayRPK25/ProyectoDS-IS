<?php

namespace SistemaEntregas\Editor;

class RestriccionCopyPaste
{
    private bool $activa;

    public function getActiva(): bool { return $this->activa; }

    public function __construct(bool $activa = true)
    {
        $this->activa = $activa;
    }

    public function interceptarCtrl(mixed $evento): void
    {
        if ($this->activa) {
            $this->mostrarMensajeBloqueo();
            // Cancelar el evento de teclado (Ctrl+C, Ctrl+V, etc.)
        }
    }

    public function interceptarClicD(mixed $evento): void
    {
        if ($this->activa) {
            $this->mostrarMensajeBloqueo();
            // Cancelar el menú contextual del clic derecho
        }
    }

    public function interceptarMenuContextual(): void
    {
        if ($this->activa) {
            $this->mostrarMensajeBloqueo();
        }
    }

    public function mostrarMensajeBloqueo(): void
    {
        echo "⚠ Acción bloqueada: el copy-paste está deshabilitado en este editor." . PHP_EOL;
    }
}
// Traducción muy directa, el único punto a destacar:
// 
// - **`object evento`**: El tipo `object` de C# es la clase base de todos los tipos. En PHP su equivalente más fiel es `mixed`, 
// disponible desde PHP 8.0, que acepta cualquier tipo de valor incluyendo `null`, objetos, primitivos, etc. Esto refleja correctamente 
// la intención original de aceptar cualquier objeto de evento sin restricción de tipo.