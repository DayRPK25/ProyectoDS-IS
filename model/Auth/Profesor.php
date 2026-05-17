<?php

namespace model\Auth;

use DateTime;
use model\Tareas\Tarea;
use model\Tareas\Entrega;
use model\Tareas\GrupoTrabajo;

class Profesor extends Usuario
{
    private int $codigoProfesor;

    public function getCodigoProfesor(): int
    {
        return $this->codigoProfesor;
    }

    public function __construct(int $codigo = 0)
    {
        $this->codigoProfesor = $codigo;
        $this->rol = RolUsuario::PROFESOR;
    }

    public function crearTarea(
        string $nombre,
        string $descripcion,
        DateTime $fechaEntrega,
        bool $esGrupal
    ): Tarea {
        $tarea = new Tarea();
        $tarea->nombre = $nombre;
        $tarea->descripcion = $descripcion;
        $tarea->fechaEntrega = $fechaEntrega;
        $tarea->esGrupal = $esGrupal;
        $tarea->fechaCreacion = new DateTime();
        return $tarea;
    }

    public function editarTarea(Tarea $tarea): void
    {
        // Modifica los datos de la tarea
    }

    public function eliminarTarea(int $id): void
    {
        // Elimina la tarea con el id indicado
    }

    public function asignarGrupo(Tarea $tarea, array $estudiantes): GrupoTrabajo
    {
        $grupo = new GrupoTrabajo();
        $grupo->tarea = $tarea;
        foreach ($estudiantes as $estudiante) {
            $grupo->agregarMiembro($estudiante);
        }
        return $grupo;
    }

    public function consultarEntregas(Tarea $tarea): array
    {
        return $tarea->obtenerEntregas();
    }

    public function revisarEntrega(Entrega $entrega): void
    {
        // Lógica de revisión de entrega
    }

    public function asignarNota(Entrega $entrega, float $nota): void
    {
        $entrega->nota = $nota;
    }
}
// El único punto nuevo respecto a las traducciones anteriores:
// 
// - **`List<Estudiante>`**: Se traduce como `array` en PHP, ya que PHP no tiene colecciones genéricas tipadas. 
// Si se quisiera mayor seguridad de tipos, se podría validar cada elemento dentro del método con `instanceof Estudiante`, pero para mantener la equivalencia directa con el original basta con `array`.