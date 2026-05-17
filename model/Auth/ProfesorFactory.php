<?php

use model\Editor\EditorCodigo;

/**
 * Concrete Factory que crea instancias de Profesor y sus componentes.
 */
class ProfesorFactory extends UsuarioFactory
{
    public function crearUsuario(string $correo, string $nombre, string $contrasena): Profesor
    {
        $profesor = new Profesor();
        $profesor->registrar($correo, $nombre, $contrasena, 'PROFESOR');
        return $profesor;
    }

    public function crearEditor(): EditorCodigo
    {
        $editor = new EditorCodigo();
        $editor->copyPasteHabilitado = true; // Profesores tienen acceso completo
        return $editor;
    }

    public function crearInterfaz(): string
    {
        return 'InterfazProfesor';
    }
}

// Esta traducción es prácticamente idéntica a `EstudianteFactory`, 
// con las únicas diferencias propias de la lógica del original: 
// el tipo de retorno de `crearUsuario()` es `Profesor` en lugar de `Estudiante`, 
// se pasa `'PROFESOR'` al método `registrar()`, y `copyPasteHabilitado` se establece en `true` en lugar de `false`.