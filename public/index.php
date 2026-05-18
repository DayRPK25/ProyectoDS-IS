<?php
// ============================================================
// public/index.php - Front Controller
// Punto de entrada unico de la aplicacion.
// 1. Carga config y autoloader
// 2. Registra rutas
// 3. Despacha el request al Controller correcto
// ============================================================

// ── 1. Configuracion ──────────────────────────────────────
require_once __DIR__ . '/../config/config.php';

// Headers globales: JSON por defecto, CORS abierto para desarrollo
header('Content-Type: application/json; charset=utf-8');
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: GET, POST, PUT, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type');

// Preflight CORS - el browser pregunta antes de hacer POST cross-origin
if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(204);
    exit;
}

// ── 2. Autoloader ─────────────────────────────────────────
// Carga automaticamente las clases segun convencion de nombres.
// Busca en app/ (plano) y en todos los subdirectorios de model/
spl_autoload_register(function (string $class): void {
    $directorios = [
        __DIR__ . '/../app/core/',
        __DIR__ . '/../app/models/',
        __DIR__ . '/../app/controllers/',
        __DIR__ . '/../app/services/',
        // subdirectorios de model/
        __DIR__ . '/../model/Auth/',
        __DIR__ . '/../model/Core/',
        __DIR__ . '/../model/Tareas/',
        __DIR__ . '/../model/Archivos/',
        __DIR__ . '/../model/Editor/',
        __DIR__ . '/../model/Bitacora/',
    ];

    foreach ($directorios as $dir) {
        $archivo = $dir . $class . '.php';
        if (file_exists($archivo)) {
            require_once $archivo;
            return;
        }
    }
    throw new RuntimeException("Clase no encontrada: {$class}");
});

// ── 3. Parseo de la URI ────────────────────────────────────
// Quitamos el BASE_PATH para que el Router trabaje con rutas relativas
// Ejemplo: /url-shortener/api/urls → /api/urls
$uri = parse_url($_SERVER['REQUEST_URI'], PHP_URL_PATH);

// Normalizamos el BASE_PATH (puede estar vacio en instalacion en root)
$basePath = rtrim(BASE_PATH, '/');
if ($basePath !== '' && str_starts_with($uri, $basePath)) {
    $uri = substr($uri, strlen($basePath));
}

// Aseguramos que siempre empiece con /
$uri = '/' . ltrim($uri, '/');

$method = $_SERVER['REQUEST_METHOD'];

// ── 4. Registro de rutas ───────────────────────────────────
$router = new Router();

// IMPORTANTE: el orden importa - las rutas especificas de /api PRIMERO
// para que no sean atrapadas por el catch-all /{shortCode}

// API para el Backend
$router->add('POST', '/api/auth/login', function () {
    header('Content-Type: application/json; charset=utf-8');
    $data = json_decode(file_get_contents('php://input'), true);

    if (empty($data['correo']) || empty($data['contrasena'])) {
        http_response_code(400);
        echo json_encode(['success' => false, 'error' => 'correo y contrasena requeridos']);
        exit;
    }

    $dsn = "mysql:host=localhost;dbname=sistema_entregas;charset=utf8mb4";
    $pdo = new PDO($dsn, "admin", "password");

    // busca el usuario por correo
    $stmt = $pdo->prepare("SELECT * FROM Usuario WHERE correo = ?");
    $stmt->execute([(string) $data['correo']]);
    $usuario = $stmt->fetch();

    if (!$usuario || !password_verify($data['contrasena'], $usuario['contrasena'])) {
        http_response_code(401);
        echo json_encode(['success' => false, 'error' => 'credenciales invalidas']);
        exit;
    }

    $response = [
        'success'   => true,
        'idUsuario' => $usuario['idUsuario'],
        'nombre'    => $usuario['nombre'],
        'rol'       => $usuario['rol'],
        // token simple por ahora: en produccion reemplazar por JWT
        'token'     => bin2hex(random_bytes(16)),
    ];

    http_response_code(200);
    echo json_encode($response, JSON_UNESCAPED_UNICODE | JSON_UNESCAPED_SLASHES);
    exit;
});

$router->add('POST', '/api/auth/register', function () {
    $data = json_decode(file_get_contents('php://input'), true);

    $campos = ['correo', 'nombre', 'nombreUsuario', 'contrasena', 'rol'];
    foreach ($campos as $campo) {
        if (empty($data[$campo])) {
            http_response_code(400);
            header('Content-Type: application/json; charset=utf-8');
            echo json_encode(['success' => false, 'error' => "campo requerido: {$campo}"]);
            exit;
        }
    }

    $rol = strtoupper($data['rol']);
    if (!in_array($rol, ['ESTUDIANTE', 'PROFESOR'], true)) {
        http_response_code(400);
        header('Content-Type: application/json; charset=utf-8');
        echo json_encode(['success' => false, 'error' => 'rol invalido, debe ser ESTUDIANTE o PROFESOR']);
        exit;
    }

    $hash = password_hash($data['contrasena'], PASSWORD_BCRYPT);

    try {
        $dsn = "mysql:host=localhost;dbname=sistema_entregas;charset=utf8mb4";
        $pdo = new PDO($dsn, "admin", "password");
        
        // insertar usuario base
        $stmt = $pdo->prepare(
            "INSERT INTO Usuario (correo, nombre, nombreUsuario, contrasena, rol)
             VALUES (?, ?, ?, ?, ?)"
        );
        $stmt->execute([
            (string) $data['correo'],
            (string) $data['nombre'],
            (string) $data['nombreUsuario'],
            (string) $hash,
            (string) $rol
        ]);
        //$idUsuario = (int) $pdo->lastInsertId();
        //
        //// insertar en tabla de rol correspondiente
        //if ($rol === 'PROFESOR') {
        //    $codigo = $data['carnet'] ?? 'P' . $idUsuario;
        //    $stmt2  = $pdo->prepare("INSERT INTO Profesor (codigoProfesor, idUsuario) VALUES (?, ?)");
        //    $stmt2->execute([(string) $codigo, (string) $idUsuario]);
        //} else {
        //    $codigo = $data['carnet'] ?? 'E' . $idUsuario;
        //    $stmt2  = $pdo->prepare("INSERT INTO Estudiante (codigoEstudiante, idUsuario) VALUES (?, ?)");
        //    $stmt2->execute([(string) $codigo, (string) $idUsuario]);
        //}

    } catch (Exception $e) {
        // correo o nombreUsuario duplicado
        if ($e->getCode() === '23000') {
            http_response_code(409);
            header('Content-Type: application/json; charset=utf-8');
            echo json_encode(['success' => false, 'error' => 'correo o nombre de usuario ya existe']);
            exit;
        }
        http_response_code(500);
        header('Content-Type: application/json; charset=utf-8');
        echo json_encode(['success' => false, 'error' => 'error interno al registrar']);
        exit;
    }

    http_response_code(201);
    header('Content-Type: application/json; charset=utf-8');
    echo json_encode([
        'success'   => true,
        'idUsuario' => 0,
        'estado'    => 'registrado',
    ], JSON_UNESCAPED_UNICODE | JSON_UNESCAPED_SLASHES);
    exit;
});

$router->add('GET', '/api/tareas', function () {
    $data = json_decode(file_get_contents('php://input'), true);
    
});

$router->add('POST', '/api/tareas', function () {});

$router->add('PUT', '/api/tareas/{idTarea}', function (int $idTarea) {});

$router->add('POST', '/api/tareas/{idTarea}/grupos', function (int $idTarea) {});

$router->add('GET', '/api/tareas/{idTarea}/entregas', function (int $idTarea) {});

$router->add('POST', '/api/archivos', function () {});

$router->add('GET', '/api/archivos/{idArchivo}', function (int $idArchivo) {});

$router->add('PUT', '/api/tareas/{idArchivo}', function (int $idArchivo) {});

$router->add('POST', '/api/archivos/{idArchivo}', function (int $idArchivo) {});

$router->add('POST', '/api/ejecucion', function () {});

$router->add('GET', '/api/entregas/{idEntrega}/firma', function (int $idEntrega) {});

$router->add('GET', '/api/entregas/{idEntrega}', function (int $idEntrega) {});

$router->add('GET', '/api/bitacora', function () {});

$router->add('GET', '/api/bitacora/ultima-version', function () {});

// API para la Base de Datos
$router->add('GET', '/data/usuarios/{id}', function (int $id) {});

$router->add('POST', '/data/usuarios', function () {});

$router->add('GET', '/data/tareas', function () {});

$router->add('POST', '/data/tareas', function () {});

$router->add('POST', '/data/grupos', function () {});

$router->add('POST', '/data/archivos', function () {});

$router->add('GET', '/data/archivos/{idArchivo}', function (int $idArchivo) {});

$router->add('PUT', '/data/archivos/{idArchivo}', function (int $idArchivo) {});

$router->add('POST', '/data/entregas', function () {});

$router->add('GET', '/data/entregas', function () {});

$router->add('POST', '/data/bitacora', function () {});

$router->add('GET', '/data/bitacora', function () {});

$router->add('GET', '/data/usuarios', function () {});

$router->add('PUT', '/data/tareas/{idTarea}', function (int $idTarea) {});

$router->add('PUT', '/data/archivos/{idArchivo}/firma', function (int $idArchivo) {});

$router->add('GET', '/data/entregas/{idEntrega}/firma', function (int $idEntrega) {});

$router->add('GET', '/data/bitacora/ultima-version', function () {});

// API para Git
$router->add('POST', '/git/repos', function () {});

$router->add('POST', '/git/commits', function () {});

$router->add('POST', '/api/urls', function () {
    (new UrlController())->create();
});

// API de rutas base
$router->add('GET', '/', function () {
    // Para el HTML desactivamos el Content-Type JSON que pusimos arriba
    header('Content-Type: text/html; charset=utf-8');
    require_once __DIR__ . '/../views/home.html';
    exit;
});

// Dirige a la página de Inicio de Sesión
$router->add('GET', '/login', function () {
    // Para el HTML desactivamos el Content-Type JSON que pusimos arriba
    header('Content-Type: text/html; charset=utf-8');
    require_once __DIR__ . '/../views/login.html';
    exit;
});

// Dirige a la página de Registro de Cuenta
$router->add('GET', '/register', function () {
    // Para el HTML desactivamos el Content-Type JSON que pusimos arriba
    header('Content-Type: text/html; charset=utf-8');
    require_once __DIR__ . '/../views/register.html';
    exit;
});

// Dirige a la página de menuEstudiante
$router->add('GET', '/menuEstudiante', function () {
    // Para el HTML desactivamos el Content-Type JSON que pusimos arriba
    header('Content-Type: text/html; charset=utf-8');
    require_once __DIR__ . '/../views/menuEstudiante.html';
    exit;
});

$router->add('GET', '/api/urls', function () {
    (new UrlController())->index();
});

// Estadisticas - ANTES del catch-all /{shortCode}
$router->add('GET', '/api/urls/{shortCode}/stats', function (string $shortCode) {
    (new StatsController())->show($shortCode);
});

// Redireccion - catch-all de codigos cortos
$router->add('GET', '/{shortCode}', function (string $shortCode) {
    (new UrlController())->redirect($shortCode);
});

// ── 5. Despachar ──────────────────────────────────────────
$router->dispatch($method, $uri);
