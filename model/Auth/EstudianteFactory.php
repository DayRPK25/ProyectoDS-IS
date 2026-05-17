<?php

namespace model\Auth;

use model\Editor\EditorCodigo;

/**
 * Concrete Factory que crea instancias de Estudiante y sus componentes.
 */
class EstudianteFactory extends UsuarioFactory
{
    public function crearUsuario(string $correo, string $nombre, string $contrasena): Estudiante
    {
        $estudiante = new Estudiante();
        $estudiante->registrar($correo, $nombre, $contrasena, 'ESTUDIANTE');
        return $estudiante;
    }

    public function crearEditor(): EditorCodigo
    {
        $editor = new EditorCodigo();
        $editor->copyPasteHabilitado = false; // Estudiantes tienen copy-paste restringido
        return $editor;
    }

    public function crearInterfaz(): string
    {
        return 'InterfazEstudiante';
    }
}

// Los cambios principales respecto a la traducción anterior:
// 
// - **`override`**: C# requiere declarar explícitamente que un método sobreescribe al padre. En PHP simplemente se redeclara el método, ya que `override` no existe como palabra clave (aunque PHP 8.3+ añadió el atributo `#[\Override]` si se quisiera ser explícito).
// - **Comentario `<summary>`**: El bloque XML de documentación de C# se convirtió a un comentario `/** */` estándar de PHP (PHPDoc).
// - **Inicialización en línea**: Al igual que en la traducción anterior, `new EditorCodigo { CopyPasteHabilitado = false }` se separa en instanciación y asignación de propiedad.