<?php
$dsn = "mysql:host=localhost;dbname=sistema_entregas;charset=utf8mb4";
$pdo = new PDO($dsn, "admin", "password");
echo "Conexión hecha";

try {
    $stmt = $pdo->prepare(
        "INSERT INTO Usuario (correo, nombre, nombreUsuario, contrasena, rol)
         VALUES (?, ?, ?, ?, ?)"
    );

    $stmt->execute([
        'javi0409@estudiantec.cr',
        'Javier',
        'javi0409',
        'fsdjkfhdsjkfhkjs',
        'ESTUDIANTE'
    ]);
}
catch (Exception $e) {
    echo $e;
}

