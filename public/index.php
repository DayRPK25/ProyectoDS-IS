<?php

$router = new Router();

router->add('GET', '/', function () {
    require_once __DIR__ . '/../views/home.php';
    exit;
});
?>