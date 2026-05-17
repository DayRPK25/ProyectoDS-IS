<?php

namespace SistemaEntregas\Bitacora;

use DateTime;
use SistemaEntregas\Auth\Estudiante;
use SistemaEntregas\Tareas\Tarea;
use SistemaEntregas\Tareas\Entrega;

class Bitacora
{
    private array $entradas = [];

    public function registrar(Entrega $entrega): void
    {
        $registro = new RegistroEntrega();
        $registro->id           = count($this->entradas) + 1;
        $registro->fechaHora    = new DateTime();
        $registro->version      = $entrega->getVersion();
        $registro->idEntrega    = $entrega->getId();
        $registro->firmaArchivo = $entrega->obtenerFirma();

        $this->entradas[] = $registro;
    }

    public function obtenerHistorial(Tarea $tarea, Estudiante $estudiante): array
    {
        // Filtrar registros por tarea y estudiante
        $entregasTarea = array_map(
            fn(Entrega $e) => $e->getId(),
            array_filter(
                $tarea->obtenerEntregas(),
                fn(Entrega $e) => $e->getEstudiante() === $estudiante
            )
        );

        $idsEntregas = array_flip($entregasTarea); // Equivalente a ToHashSet() para búsqueda O(1)

        return array_values(array_filter(
            $this->entradas,
            fn(RegistroEntrega $r) => isset($idsEntregas[$r->idEntrega])
        ));
    }

    public function obtenerUltimaVersion(Tarea $tarea, Estudiante $estudiante): ?RegistroEntrega
    {
        $historial = $this->obtenerHistorial($tarea, $estudiante);

        if (empty($historial)) {
            return null;
        }

        usort($historial, fn($a, $b) => $b->version <=> $a->version);

        return $historial[0];
    }
}
// Los puntos nuevos en esta traducción:
// 
// - **LINQ (`.Where()`, `.Select()`, `.OrderByDescending()`, `.FirstOrDefault()`)**: PHP no tiene LINQ, por lo que cada operación se traduce con sus equivalentes funcionales: `array_filter()` para filtrar, `array_map()` para proyectar, y `usort()` con el operador *spaceship* (`<=>`) para ordenar.
// - **`.ToHashSet()`**: Se simula con `array_flip()`, que convierte los valores en claves del array, permitiendo búsquedas en tiempo constante O(1) con `isset()` en lugar de recorrer el array completo.
// - **`FirstOrDefault()!`**: Se traduce como retornar `null` si el historial está vacío, usando `?RegistroEntrega` como tipo de retorno nullable, equivalente al operador `!` de C# que suprime advertencias de nulabilidad.
// - **`new List<T>()`**: Se reemplaza con un `array` vacío `[]`, ya que PHP no tiene colecciones genéricas tipadas.