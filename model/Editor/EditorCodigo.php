<?php

namespace model\Editor;

use model\Archivos\ArchivoP;

class EditorCodigo
{
    private string $contenido = '';
    private ?ArchivoP $archivoActual = null;
    private bool $copyPasteHabilitado;
    private int $cursorPosicion = 0;
    private RestriccionCopyPaste $restriccion;
    private Terminal $terminal;

    public function getContenido(): string { return $this->contenido; }
    public function getArchivoActual(): ?ArchivoP { return $this->archivoActual; }
    public function getCursorPosicion(): int { return $this->cursorPosicion; }

    public function getCopyPasteHabilitado(): bool { return $this->copyPasteHabilitado; }
    public function setCopyPasteHabilitado(bool $value): void
    {
        $this->copyPasteHabilitado = $value;
        $this->restriccion = new RestriccionCopyPaste(!$value);
    }

    public function __construct()
    {
        $this->terminal  = new Terminal();
        $this->restriccion = new RestriccionCopyPaste(false);
    }

    public function abrirArchivo(ArchivoP $archivo): void
    {
        $this->archivoActual  = $archivo;
        $this->contenido      = $archivo->leer();
        $this->cursorPosicion = 0;
    }

    public function guardarArchivo(): void
    {
        $this->archivoActual?->escribir($this->contenido);
        $this->archivoActual?->guardar();
    }

    public function escribirCaracter(string $c): void
    {
        if ($this->cursorPosicion <= strlen($this->contenido)) {
            $this->contenido = substr_replace(
                $this->contenido,
                $c,
                $this->cursorPosicion,
                0
            );
            $this->cursorPosicion++;
        }
    }

    public function interceptarAtajoEvento(): bool
    {
        if (!$this->copyPasteHabilitado) {
            $this->restriccion->interceptarCtrl(null);
            return true; // Evento interceptado
        }
        return false;
    }

    public function bloquearCopyPaste(): void
    {
        $this->copyPasteHabilitado = false;
        $this->restriccion = new RestriccionCopyPaste(true);
    }

    public function mostrarResultadoSintaxis(): void
    {
        if ($this->archivoActual !== null) {
            $resultado = $this->terminal->ejecutar($this->archivoActual);
            echo $resultado . PHP_EOL;
        }
    }
}

// Los puntos nuevos en esta traducción:
// 
// - **`CopyPasteHabilitado` con lógica en el setter**: C# permite incluir lógica dentro del `set`. En PHP se separa en `getCopyPasteHabilitado()` y `setCopyPasteHabilitado()`, colocando la lógica dentro del setter.
// - **`string.Insert(pos, str)`**: Se traduce como `substr_replace($cadena, $insercion, $posicion, 0)`. El `0` indica que no se reemplaza ningún carácter, solo se inserta.
// - **`char`**: PHP no tiene tipo `char`, por lo que el parámetro se recibe como `string $c`, asumiendo que siempre será un solo carácter.
// - **`archivoActual?.Escribir()`**: El operador de navegación segura `?.` de C# tiene su equivalente exacto en PHP 8.0 con `?->`.
// - **`Console.WriteLine()`**: Se traduce como `echo $resultado . PHP_EOL`, usando la constante `PHP_EOL` para el salto de línea multiplataforma.