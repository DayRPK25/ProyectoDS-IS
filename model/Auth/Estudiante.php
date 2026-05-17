<?php

namespace model\Auth;

use DateTime;
use model\Archivos\ArchivoP;
use model\Editor\Terminal;
use model\Tareas\Tarea;
use model\Tareas\Entrega;
use model\Tareas\ResultadoEjecucion;

class Estudiante extends Usuario
{
    private int $codigoEstudiante;

    public function getCodigoEstudiante(): int
    {
        return $this->codigoEstudiante;
    }

    public function __construct(int $codigo = 0)
    {
        $this->codigoEstudiante = $codigo;
        $this->rol = RolUsuario::ESTUDIANTE;
    }

    public function crearArchivo(string $nombre): ArchivoP
    {
        $archivo = new ArchivoP();
        $archivo->nombre = $nombre;
        $archivo->fechaCreacion = new DateTime();
        return $archivo;
    }

    public function abrirArchivo(string $ruta): ArchivoP
    {
        $archivo = new ArchivoP();
        $archivo->ruta = $ruta;
        return $archivo;
    }

    public function editarContenido(): void
    {
        // Abre el editor de código para editar el archivo actual
    }

    public function ejecutarCodigo(): ResultadoEjecucion
    {
        $terminal = new Terminal();
        // Se ejecuta el archivo actual del estudiante
        return new ResultadoEjecucion();
    }

    public function entregarTarea(Tarea $tarea): Entrega
    {
        $entrega = new Entrega();
        $entrega->tarea = $tarea;
        $entrega->estudiante = $this;
        $entrega->fechaHora = new DateTime();
        $entrega->version = 1;
        $entrega->registrar();
        return $entrega;
    }

    public function consultarBitacora(Tarea $tarea): array
    {
        return $tarea->obtenerEntregas();
    }
}

// Algunos puntos clave sobre la traducción:
// 
// - **Constructor**: PHP usa `__construct()`. Se fusionaron los dos constructores de C# en uno solo con un parámetro opcional (`$codigo = 0`).
// - **Propiedad de solo lectura (`=>`)**: En C# se usaba una propiedad con getter automático. En PHP se reemplaza con un método `getCodigoEstudiante()` siguiendo la convención de getters.
// - **Tipos escalares**: `int`, `string`, `void` y `array` se mantienen gracias al tipado de PHP 7+.
// - **`DateTime`**: Ambos lenguajes tienen una clase `DateTime`, por lo que la lógica es equivalente.
// - **Inicialización de objetos**: C# permite inicializar propiedades en línea con `{ Prop = valor }`. En PHP se asignan línea por línea tras instanciar con `new`.
// - **Convención de nombres**: Se respetó `camelCase` para métodos y propiedades, estándar en PHP.