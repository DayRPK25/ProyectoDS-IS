<?php

namespace model\Tareas;

use model\Auth\Estudiante;

class GrupoTrabajo
{
    private int $id;
    private string $nombre = '';
    private ?Tarea $tarea  = null;
    private array $miembros = [];

    public function getId(): int { return $this->id; }
    public function setId(int $id): void { $this->id = $id; }

    public function getNombre(): string { return $this->nombre; }
    public function setNombre(string $nombre): void { $this->nombre = $nombre; }

    public function getTarea(): ?Tarea { return $this->tarea; }
    public function setTarea(?Tarea $tarea): void { $this->tarea = $tarea; }

    public function agregarMiembro(Estudiante $estudiante): void
    {
        if (!$this->tieneMiembro($estudiante)) {
            $this->miembros[] = $estudiante;
        }
    }

    public function quitarMiembro(Estudiante $estudiante): void
    {
        $this->miembros = array_values(array_filter(
            $this->miembros,
            fn(Estudiante $m) => $m !== $estudiante
        ));
    }

    public function tieneMiembro(Estudiante $estudiante): bool
    {
        foreach ($this->miembros as $miembro) {
            if ($miembro === $estudiante) {
                return true;
            }
        }
        return false;
    }

    public function obtenerMiembros(): array
    {
        return $this->miembros;
    }
}
// Los puntos nuevos en esta traducción:
// 
// - **`List<T>.Contains()`**: PHP no tiene búsqueda de referencias en arrays de forma nativa, por lo que `tieneMiembro()` 
// se implementa con un `foreach` comparando por referencia con `===`, que en PHP verifica que ambas variables apunten exactamente 
// al mismo objeto.
// - **`List<T>.Remove()`**: PHP tampoco tiene un método directo para eliminar un elemento por referencia. Se combina `array_filter()` 
// con `!==` para excluir el estudiante buscado, y `array_values()` para reindexar el array resultante y evitar huecos en los índices.
// - **`new List<Estudiante>(miembros)`**: En C# crea una copia defensiva de la lista. En PHP los arrays se copian por valor al asignarse, 
// por lo que `obtenerMiembros()` puede retornar `$this->miembros` directamente sin riesgo de que el llamador modifique la lista interna.