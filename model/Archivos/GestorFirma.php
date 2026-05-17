<?php

namespace model\Archivos;

class GestorFirma
{
    private string $claveSecreta;
    private string $algoritmo;

    public function __construct(
        string $claveSecreta = 'clave-secreta-default',
        string $algoritmo = 'SHA256'
    ) {
        $this->claveSecreta = $claveSecreta;
        $this->algoritmo    = $algoritmo;
    }

    public function generarFirma(ArchivoP $archivo): string
    {
        $datos = $archivo->getContenido() . $this->claveSecreta;
        return strtoupper(hash_hmac('sha256', $datos, $this->claveSecreta));
    }

    public function validarFirma(ArchivoP $archivo, string $firma): bool
    {
        $firmaEsperada = $this->generarFirma($archivo);
        return hash_equals($firmaEsperada, $firma); // hash_equals previene timing attacks
    }

    public function insertarComentarioFirma(ArchivoP $archivo): void
    {
        $firma = $this->generarFirma($archivo);
        $archivo->escribir("# FIRMA: {$firma}\n" . $archivo->leer());
    }

    public function extraerFirmaDeContenido(string $contenido): string
    {
        $lineas = explode("\n", $contenido);
        foreach ($lineas as $linea) {
            if (str_starts_with($linea, '# FIRMA: ')) {
                return trim(str_replace('# FIRMA: ', '', $linea));
            }
        }
        return '';
    }
}

// Los puntos nuevos en esta traducción:
// 
// - **`HMACSHA256`**: Se reemplaza con la función nativa `hash_hmac('sha256', $datos, $clave)` de PHP, que hace exactamente lo mismo sin necesidad de instanciar ningún objeto.
// - **`firmaEsperada == firma`**: Se sustituye por `hash_equals()`, que compara cadenas en tiempo constante y previene ataques de temporización (*timing attacks*), siendo la práctica recomendada al comparar hashes o firmas criptográficas en PHP.
// - **`contenido.Split('\n')`**: Se traduce como `explode("\n", $contenido)`, su equivalente directo en PHP.
// - **`linea.StartsWith()`**: Se traduce como `str_starts_with()`, disponible desde PHP 8.0.
// - **`linea.Replace()`**: Se traduce como `str_replace()`, función estándar de PHP.