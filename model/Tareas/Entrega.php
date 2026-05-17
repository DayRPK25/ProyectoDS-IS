<?php

namespace SistemaEntregas\Tareas;

use DateTime;
use SistemaEntregas\Archivos\ArchivoP;
use SistemaEntregas\Auth\Estudiante;
use SistemaEntregas\Core\IPrototipo;

class Entrega implements IPrototipo
{
    private int $id;
    private DateTime $fechaHora;
    private int $version          = 1;
    private ?ArchivoP $archivo    = null;
    private ?Estudiante $estudiante = null;
    private ?Tarea $tarea         = null;
    private float $nota           = 0.0;
    private string $comentarioProfesor = '';

    public function getId(): int { return $this->id; }
    public function setId(int $id): void { $this->id = $id; }

    public function getFechaHora(): DateTime { return $this->fechaHora; }
    public function setFechaHora(DateTime $fechaHora): void { $this->fechaHora = $fechaHora; }

    public function getVersion(): int { return $this->version; }
    public function setVersion(int $version): void { $this->version = $version; }

    public function getArchivo(): ?ArchivoP { return $this->archivo; }
    public function setArchivo(?ArchivoP $archivo): void { $this->archivo = $archivo; }

    public function getEstudiante(): ?Estudiante { return $this->estudiante; }
    public function setEstudiante(?Estudiante $estudiante): void { $this->estudiante = $estudiante; }

    public function getTarea(): ?Tarea { return $this->tarea; }
    public function setTarea(?Tarea $tarea): void { $this->tarea = $tarea; }

    public function getNota(): float { return $this->nota; }
    public function setNota(float $nota): void { $this->nota = $nota; }

    public function getComentarioProfesor(): string { return $this->comentarioProfesor; }
    public function setComentarioProfesor(string $comentario): void { $this->comentarioProfesor = $comentario; }

    public function registrar(): void
    {
        $this->fechaHora = new DateTime();
        $this->tarea?->agregarEntrega($this);
    }

    public function generarCopia(): CopiaEntrega
    {
        $copia = new CopiaEntrega();
        $copia->setFechaHora(new DateTime());
        $copia->setNombreArchivo($this->archivo?->getNombre() ?? '');
        $copia->setContenido($this->archivo?->leer() ?? '');
        $copia->setMarcaTemporal((new DateTime())->format('YmdHis'));
        $copia->setChecksumFirma($this->archivo?->getFirma() ?? '');
        return $copia;
    }

    public function obtenerFirma(): string
    {
        return $this->archivo?->getFirma() ?? '';
    }

    public function clonar(): static
    {
        return clone $this;
    }
}
// Los puntos nuevos en esta traducción:
// 
// - **Propiedades nullable**: Las referencias a objetos como `ArchivoP`, `Estudiante` y `Tarea` se declaran con `?` (ej. `?ArchivoP`) 
// ya que pueden no estar asignadas, reflejando que en C# son tipos de referencia que admiten `null` por defecto.
// - **`archivo?.Nombre`**: El operador de navegación segura `?.` de C# tiene su equivalente exacto en PHP 8.0 con `?->`, 
// aplicado aquí en `generarCopia()` y `obtenerFirma()`.
// - **`DateTime.Now.ToString("yyyyMMddHHmmss")`**: Se traduce como `(new DateTime())->format('YmdHis')`, 
// usando el formato de `DateTime::format()` de PHP donde `Y` = año 4 dígitos, `m` = mes, `d` = día, `H` = hora, `i` = minutos, `s` = segundos.