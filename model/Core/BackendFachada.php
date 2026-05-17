<?php

namespace model\Core;

use DateTime;
use model\Auth\Estudiante;
use model\Auth\EstudianteFactory;
use model\Auth\ProfesorFactory;
use model\Auth\Usuario;
use model\Auth\UsuarioFactory;
use model\Archivos\ArchivoP;
use model\Bitacora\Bitacora;
use model\Bitacora\RegistroEntrega;
use model\Editor\Terminal;
use model\Tareas\Entrega;
use model\Tareas\GrupoTrabajo;
use model\Tareas\ResultadoEjecucion;
use model\Tareas\Tarea;

/**
 * Fachada del sistema. Implementa el patrón Singleton para garantizar una única instancia.
 * Centraliza el acceso a todos los subsistemas.
 */
class BackendFachada
{
    private static ?BackendFachada $instancia = null;
    private static bool $inicializado = false;

    // Subsistemas internos
    private array $usuarios  = [];
    private array $tareas    = [];
    private array $entregas  = [];
    private array $archivos  = [];
    private array $grupos    = [];
    private Bitacora $bitacora;

    private int $contadorUsuarios  = 1;
    private int $contadorTareas    = 1;
    private int $contadorEntregas  = 1;
    private int $contadorArchivos  = 1;
    private int $contadorGrupos    = 1;

    /**
     * Constructor privado — patrón Singleton.
     */
    private function __construct()
    {
        $this->bitacora = new Bitacora();
    }

    // Prevenir clonación e instanciación por unserialize
    private function __clone() {}
    public function __wakeup() {}

    public static function getInstancia(): self
    {
        if (self::$instancia === null) {
            self::$instancia  = new self();
            self::$inicializado = true;
        }
        return self::$instancia;
    }

    // ── Auth ──────────────────────────────────────────────

    public function login(string $correo, string $contrasena): string
    {
        foreach ($this->usuarios as $usuario) {
            if ($usuario->iniciarSesion($correo, $contrasena)) {
                return "TOKEN_{$usuario->getId()}_{$usuario->getRol()->name}";
            }
        }
        return '';
    }

    public function registrar(string $correo, string $nombre, string $contrasena, string $rol): int
    {
        $factory = strtoupper($rol) === 'PROFESOR'
            ? new ProfesorFactory()
            : new EstudianteFactory();

        $usuario = $factory->crearUsuario($correo, $nombre, $contrasena);
        $id = $this->contadorUsuarios++;
        $this->usuarios[$id] = $usuario;
        return $id;
    }

    // ── Tareas ────────────────────────────────────────────

    public function getTareas(int $usuarioId, string $rol): array
    {
        return array_values($this->tareas);
    }

    public function crearTarea(
        string $nombre,
        string $descripcion,
        DateTime $fechaEntrega,
        bool $esGrupal,
        int $profesorId
    ): int {
        $tarea = new Tarea();
        $tarea->setId($this->contadorTareas);
        $tarea->setNombre($nombre);
        $tarea->setDescripcion($descripcion);
        $tarea->setFechaEntrega($fechaEntrega);
        $tarea->setEsGrupal($esGrupal);
        $tarea->setFechaCreacion(new DateTime());

        $this->tareas[$this->contadorTareas] = $tarea;
        return $this->contadorTareas++;
    }

    public function editarTarea(int $idTarea, Tarea $datos): void
    {
        if (isset($this->tareas[$idTarea])) {
            $tarea = $this->tareas[$idTarea];
            $tarea->setNombre($datos->getNombre());
            $tarea->setDescripcion($datos->getDescripcion());
            $tarea->setFechaEntrega($datos->getFechaEntrega());
            $tarea->setEsGrupal($datos->getEsGrupal());
        }
    }

    public function eliminarTarea(int $idTarea): void
    {
        unset($this->tareas[$idTarea]);
    }

    // ── Entregas ──────────────────────────────────────────

    public function getEntregas(int $idTarea): array
    {
        return isset($this->tareas[$idTarea])
            ? $this->tareas[$idTarea]->obtenerEntregas()
            : [];
    }

    public function registrarEntrega(Entrega $entrega): void
    {
        $id = $this->contadorEntregas++;
        $entrega->setId($id);
        $this->entregas[$id] = $entrega;
        $entrega->registrar();
        $this->bitacora->registrar($entrega);
    }

    public function asignarNota(int $idEntrega, float $nota): void
    {
        if (isset($this->entregas[$idEntrega])) {
            $this->entregas[$idEntrega]->setNota($nota);
        }
    }

    // ── Archivos ──────────────────────────────────────────

    public function crearArchivo(int $estudianteId, string $nombre, string $contenido): int
    {
        $archivo = new ArchivoP();
        $archivo->setId($this->contadorArchivos);
        $archivo->setNombre($nombre);
        $archivo->setContenido($contenido);
        $archivo->setFechaCreacion(new DateTime());

        $this->archivos[$this->contadorArchivos] = $archivo;
        return $this->contadorArchivos++;
    }

    public function ejecutarArchivo(int $idArchivo): ResultadoEjecucion
    {
        if (isset($this->archivos[$idArchivo])) {
            $terminal = new Terminal();
            return $terminal->ejecutar($this->archivos[$idArchivo]);
        }

        $resultado = new ResultadoEjecucion();
        $resultado->setErrores('Archivo no encontrado');
        $resultado->setCodigoSalida(-1);
        return $resultado;
    }

    // ── Grupos ────────────────────────────────────────────

    public function crearGrupo(int $idTarea, string $nombre, array $miembros): int
    {
        if (!isset($this->tareas[$idTarea])) {
            return -1;
        }

        $grupo = new GrupoTrabajo();
        $grupo->setId($this->contadorGrupos);
        $grupo->setNombre($nombre);
        $grupo->setTarea($this->tareas[$idTarea]);

        foreach ($miembros as $idEstudiante) {
            if (isset($this->usuarios[$idEstudiante]) && $this->usuarios[$idEstudiante] instanceof Estudiante) {
                $grupo->agregarMiembro($this->usuarios[$idEstudiante]);
            }
        }

        $this->grupos[$this->contadorGrupos] = $grupo;
        return $this->contadorGrupos++;
    }

    // ── Bitácora ──────────────────────────────────────────

    public function getHistorial(int $idTarea, int $idEstudiante): array
    {
        if (!isset($this->tareas[$idTarea])) return [];
        if (!isset($this->usuarios[$idEstudiante]) || !($this->usuarios[$idEstudiante] instanceof Estudiante)) return [];

        return $this->bitacora->obtenerHistorial(
            $this->tareas[$idTarea],
            $this->usuarios[$idEstudiante]
        );
    }
}
// Los puntos nuevos en esta traducción:
// 
// - **Singleton**: En PHP se refuerza bloqueando también `__clone()` y `__wakeup()` para evitar que se creen instancias adicionales por clonación o deserialización, lo cual no es necesario en C# pero es una buena práctica en PHP.
// - **`Dictionary<K,V>`**: Se reemplaza con `array` asociativo de PHP, usando los mismos IDs enteros como claves.
// - **`TryGetValue()`**: Se sustituye con `isset()` para verificar existencia, equivalente directo y más idiomático en PHP.
// - **`u is not Estudiante`**: El pattern matching de C# se traduce como `!($u instanceof Estudiante)` en PHP.
// - **`usuario.Rol`**: Como `Rol` es un enum de PHP 8.1, se accede a su nombre como string mediante la propiedad `->name`.
// - **`tareas.Remove()`**: Se traduce como `unset()`, su equivalente directo en PHP.