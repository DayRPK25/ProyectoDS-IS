<?php
require_once __DIR__ . '/Core/Database.php';

class GrupoModel
{
    private PDO $pdo;

    public function __construct()
    {
        $this->pdo = Database::getInstance()->getConnection();
    }

    // Lista los grupos de una tarea con sus miembros
    public function listarPorTarea(int $idTarea): array
    {
        // Primero traemos los grupos de esa tarea
        $stmt = $this->pdo->prepare("
            SELECT g.idGrupoTrabajo, g.nombreGrupoTrabajo
            FROM GrupoTrabajo g
            JOIN Tarea t ON g.idCurso = t.idCurso
            WHERE t.idTarea = ?
        ");
        $stmt->execute([$idTarea]);
        $grupos = $stmt->fetchAll(PDO::FETCH_ASSOC);

        // Para cada grupo traemos sus miembros
        foreach ($grupos as &$grupo) {
            $stmt2 = $this->pdo->prepare("
                SELECT u.idUsuario, u.nombreUsuario AS carnet, u.nombre
                FROM EstudianteXGrupoTrabajo eg
                JOIN Usuario u ON eg.idUsuario = u.idUsuario
                WHERE eg.idGrupoTrabajo = ?
            ");
            $stmt2->execute([$grupo['idGrupoTrabajo']]);
            $grupo['miembros'] = $stmt2->fetchAll(PDO::FETCH_ASSOC);
        }

        return $grupos;
    }

    public function crear(int $idUsuario, int $idCurso, string $nombre): int
    {
        $stmt = $this->pdo->prepare("
            INSERT INTO GrupoTrabajo (idUsuario, idCurso, nombreGrupoTrabajo)
            VALUES (?, ?, ?)
        ");
        $stmt->execute([$idUsuario, $idCurso, $nombre]);
        return (int) $this->pdo->lastInsertId();
    }

    public function actualizar(int $idGrupo, string $nombre): void
    {
        $stmt = $this->pdo->prepare("
            UPDATE GrupoTrabajo SET nombreGrupoTrabajo = ? WHERE idGrupoTrabajo = ?
        ");
        $stmt->execute([$nombre, $idGrupo]);
    }

    public function eliminar(int $idGrupo): void
    {
        // Primero eliminar miembros por FK
        $stmt = $this->pdo->prepare("DELETE FROM EstudianteXGrupoTrabajo WHERE idGrupoTrabajo = ?");
        $stmt->execute([$idGrupo]);

        $stmt2 = $this->pdo->prepare("DELETE FROM GrupoTrabajo WHERE idGrupoTrabajo = ?");
        $stmt2->execute([$idGrupo]);
    }

    public function agregarMiembro(int $idGrupo, int $idUsuario): void
    {
        $stmt = $this->pdo->prepare("
            INSERT IGNORE INTO EstudianteXGrupoTrabajo (idUsuario, idGrupoTrabajo) VALUES (?, ?)
        ");
        $stmt->execute([$idUsuario, $idGrupo]);
    }

    public function quitarMiembro(int $idGrupo, int $idUsuario): void
    {
        $stmt = $this->pdo->prepare("
            DELETE FROM EstudianteXGrupoTrabajo WHERE idGrupoTrabajo = ? AND idUsuario = ?
        ");
        $stmt->execute([$idGrupo, $idUsuario]);
    }

    // Busca un estudiante por nombreUsuario para poder agregarlo al grupo
    public function buscarEstudiantePorCarnet(string $carnet): ?array
    {
        $stmt = $this->pdo->prepare("
            SELECT idUsuario, nombreUsuario AS carnet, nombre
            FROM Usuario WHERE nombreUsuario = ? AND rol = 'ESTUDIANTE'
        ");
        $stmt->execute([$carnet]);
        return $stmt->fetch(PDO::FETCH_ASSOC) ?: null;
    }

    public function obtenerIdCursoDeTarea(int $idTarea): int
    {
        $stmt = $this->pdo->prepare("SELECT idCurso FROM Tarea WHERE idTarea = ?");
        $stmt->execute([$idTarea]);
        $row = $stmt->fetch(PDO::FETCH_ASSOC);
        if (!$row) throw new \RuntimeException("Tarea no encontrada");
        return (int) $row['idCurso'];
    }
}