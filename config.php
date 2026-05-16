<?php
// --- URL base de la aplicacion (sin trailing slash) ---
// Cambia BASE_PATH si la app vive en una subcarpeta de Apache
define('BASE_URL',  'http://localhost/ProyectoDS-IS');
define('BASE_PATH', '/ProyectoDS-IS');

// --- API externa de geolocalizacion ---
define('IP_API_BASE', 'http://ip-api.com/json/');

// --- Configuracion del codigo corto ---
define('SHORT_CODE_LENGTH', 6);

// --- Entorno ---
define('APP_ENV', 'development');
?>