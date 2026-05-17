<?php

namespace model\Auth;

use model\Editor\EditorCodigo;

/**
 * Abstract Factory para la creación de usuarios y sus componentes asociados.
 */
abstract class UsuarioFactory
{
    abstract public function crearUsuario(string $correo, string $nombre, string $contrasena): Usuario;
    abstract public function crearEditor(): EditorCodigo;
    abstract public function crearInterfaz(): string;
}
//Traducción muy directa, el único punto a destacar:
//
//- **`abstract`** en métodos: En C# los métodos abstractos se declaran con `public abstract TipoRetorno Metodo()`.
// En PHP el orden cambia ligeramente a `abstract public function metodo(): TipoRetorno`, 
// pero el comportamiento es idéntico — obliga a todas las clases hijas a implementar estos tres métodos.