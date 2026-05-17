<?php

// Alternativa para PHP < 8.1
namespace SistemaEntregas\Auth;

class RolUsuario
{
    const ESTUDIANTE = 'ESTUDIANTE';
    const PROFESOR   = 'PROFESOR';
}

// Se recomienda la primera opción siempre que el proyecto use PHP 8.1 o superior, ya que es más fiel al diseño original y 
// ofrece mayor seguridad de tipos.