<?php

namespace model\Tareas;

use DateTime;

class Tarea
{
    private int $id;
    private string $nombre      = '';
    private string $descripcion = '';
    private DateTime $fechaCreacion;
    private DateTime $fechaEntrega;
    private bool $esGrupal      = false;
    private array $grupos       = [];
    private array $entregas     = [];

    public function getId(): int { return $this->id; }
    public function setId(int $id): void { $this->id = $id; }

    public function getNombre(): string { return $this->nombre; }
    public function setNombre(string $nombre): void { $this->nombre = $nombre; }

    public function getDescripcion(): string { return $this->descripcion; }
    public function setDescripcion(string $descripcion): void { $this->descripcion = $descripcion; }

    public function getFechaCreacion(): DateTime { return $this->fechaCreacion; }
    public function setFechaCreacion(DateTime $fechaCreacion): void { $this->fechaCreacion = $fechaCreacion; }

    public function getFechaEntrega(): DateTime { return $this->fechaEntrega; }
    public function setFechaEntrega(DateTime $fechaEntrega): void { $this->fechaEntrega = $fechaEntrega; }

    public function getEsGrupal(): bool { return $this->esGrupal; }
    public function setEsGrupal(bool $esGrupal): void { $this->esGrupal = $esGrupal; }

    public function publicar(): void
    {
        // Marca la tarea como publicada y disponible para los estudiantes
        echo "Tarea '{$this->nombre}' publicada." . PHP_EOL;
    }

    public function cerrar(): void
    {
        $this->fechaEntrega = new DateTime();
        echo "Tarea '{$this->nombre}' cerrada." . PHP_EOL;
    }

    public function agregarGrupo(string $grupo): void
    {
        $nuevoGrupo = new GrupoTrabajo();
        $nuevoGrupo->setNombre($grupo);
        $nuevoGrupo->setTarea($this);
        $this->grupos[] = $nuevoGrupo;
    }

    public function obtenerEntregas(): array
    {
        return $this->entregas;
    }

    public function estaVigente(): bool
    {
        return new DateTime() <= $this->fechaEntrega;
    }

    public function agregarEntrega(Entrega $entrega): void
    {
        $this->entregas[] = $entrega;
    }
}
// Traducción muy directa sin puntos nuevos relevantes, todos los patrones usados aquí ya aparecieron en clases anteriores: `array` 
// en lugar de `List<T>`, `echo` en lugar de `Console.WriteLine`, `new DateTime()` en lugar de `DateTime.Now`, inicialización en línea 
// reemplazada por asignación de propiedades, y getters/setters explícitos en lugar de propiedades de C#.