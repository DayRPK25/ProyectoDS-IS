<?php
require_once __DIR__ . '/../config.php';

$router = new Router();

router->add('GET', '/', function () {
    header('Content-Type: text/html; charset=utf-8');
    require_once __DIR__ . '/../views/home.php';
    exit;
});
?>