<?php

namespace model\Auth;

use DateTime;

abstract class Usuario
{
    protected int $id;
    protected string $correo;
    protected string $nombreUsuario;
    protected string $contrasena;
    protected RolUsuario $rol;
    protected DateTime $fechaCreacion;

    public function getId(): int
    {
        return $this->id;
    }

    public function getCorreo(): string
    {
        return $this->correo;
    }

    public function getNombreUsuario(): string
    {
        return $this->nombreUsuario;
    }

    public function getRol(): RolUsuario
    {
        return $this->rol;
    }

    public function getFechaCreacion(): DateTime
    {
        return $this->fechaCreacion;
    }

    public function registrar(string $correo, string $nombre, string $contrasena, string $rol): void
    {
        $this->correo       = $correo;
        $this->nombreUsuario = $nombre;
        $this->contrasena   = $contrasena;
        $this->rol          = RolUsuario::from(strtoupper($rol)); // Equivalente a Enum.Parse
        $this->fechaCreacion = new DateTime();
    }

    public function iniciarSesion(string $correo, string $contrasena): bool
    {
        return $this->correo === $correo && $this->contrasena === $contrasena;
    }

    public function cerrarSesion(): void
    {
        // Limpiar estado de sesión
    }

    public function validarCredenciales(): bool
    {
        return !empty($this->correo) && !empty($this->contrasena);
    }

    public function validarFormato(): bool
    {
        return str_contains($this->correo, '@') && strlen($this->contrasena) >= 6;
    }
}
// Los puntos nuevos respecto a traducciones anteriores:
// 
// - **`abstract class`**: Se mantiene igual en PHP.
// - **`virtual`**: En C# indica que el método puede ser sobreescrito. En PHP todos los métodos son virtuales por defecto, así que simplemente se omite.
// - **`Enum.Parse<RolUsuario>(rol, true)`**: Se traduce como `RolUsuario::from(strtoupper($rol))`. El `strtoupper()` reemplaza el flag `true` que en C# hacía la búsqueda insensible a mayúsculas, y `::from()` es el método nativo de los enums de PHP 8.1 para obtener un caso a partir de su nombre.
// - **`string.IsNullOrEmpty()`**: Se traduce como `empty()`, que en PHP evalúa tanto `null` como cadena vacía `""`.
// - **`string.Contains()`**: Se traduce como `str_contains()`, disponible desde PHP 8.0.