-- Base de datos para acortador de URLs
CREATE DATABASE IF NOT EXISTS sistema_entregas
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE sistema_entregas;

-- Tabla Usuarios
CREATE TABLE IF NOT EXISTS Usuario (
    idUsuario       INT             AUTO_INCREMENT,
    correo          VARCHAR(255)    NOT NULL UNIQUE,
    nombreUsuario   VARCHAR(100)    NOT NULL UNIQUE,
    contrasena      VARCHAR(255)    NOT NULL,
    fechaCreacion   DATETIME        NOT NULL DEFAULT NOW(),
    PRIMARY KEY (idUsuario)
    CONSTRAINT chk_correo CHECK (correo REGEXP '^[a-zA-Z0-9._%+\\-]+@[a-zA-Z0-9.\\-]+\\.[a-zA-Z]{2,}$')
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla de Profesor
CREATE TABLE IF NOT EXISTS Profesor (
    codigoProfesor  VARCHAR(20),
    idUsuario       INT             NOT NULL UNIQUE,
    PRIMARY KEY (codigoProfesor),
    CONSTRAINT fk_Profesor_idUser FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla de Estudiante
CREATE TABLE IF NOT EXISTS Estudiante (
    codigoEstudiante    VARCHAR(20),
    idUsuario           INT             NOT NULL UNIQUE,
    PRIMARY KEY (codigoEstudiante),
    CONSTRAINT fk_Estudiante_idUsuario FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla Tarea
CREATE TABLE IF NOT EXISTS Tarea (
    idTarea         INT             NOT NULL AUTO_INCREMENT,
    codigoProfesor  VARCHAR(20)     NOT NULL,
    nombre          VARCHAR(255)    NOT NULL,
    descripcion     TEXT            NULL,
    fechaCreacion   DATETIME        NOT NULL DEFAULT NOW(),
    fechaEntrega    DATETIME        NULL,
    esGrupal        TINYINT(1)      NOT NULL,
    PRIMARY KEY (idTarea),
    CONSTRAINT fk_tarea_profesor FOREIGN KEY (codigoProfesor) REFERENCES Profesor(codigoProfesor),
    CONSTRAINT chk_fechaEntrega CHECK (fechaEntrega > fechaCreacion),
    CONSTRAINT chk_esGrupal CHECK (esGrupal IN (0, 1))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla GrupoTrabajo
CREATE TABLE IF NOT EXISTS GrupoTrabajo (
    idGrupoTrabajo  INT             NOT NULL AUTO_INCREMENT,
    codigoProfesor  VARCHAR(20)     NOT NULL,
    idTarea         INT             NOT NULL,
    nombre          VARCHAR(255)    NOT NULL,
    PRIMARY KEY (idGrupoTrabajo),
    CONSTRAINT fk_profesor FOREIGN KEY (codigoProfesor) REFERENCES Profesor(codigoProfesor),
    CONSTRAINT fk_tarea FOREIGN KEY (idTarea) REFERENCES Tarea(idTarea)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla ArchivoP
CREATE TABLE IF NOT EXISTS ArchivoP (
    idArchivoP          INT             NOT NULL AUTO_INCREMENT,
    nombre              VARCHAR(255)    NOT NULL,
    ruta                VARCHAR(500)    NOT NULL,
    contenido           LONGTEXT        NULL,
    fechaCreacion       DATETIME        NOT NULL DEFAULT NOW(),
    fechaModificacion   DATETIME        NULL,
    firma               VARCHAR(64)     NULL,
    PRIMARY KEY (idArchivoP),
    CONSTRAINT chk_nombre CHECK (nombre LIKE '%.py'),
    CONSTRAINT chk_fechaModificacion CHECK (fechaModificacion > fechaCreacion),
    CONSTRAINT chk_firma CHECK (firma REGEXP '^[a-fA-F0-9]{64}$')
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla Entrega
CREATE TABLE IF NOT EXISTS Entrega (
    idEntrega           INT             NOT NULL AUTO_INCREMENT,
    idTarea             INT             NOT NULL,
    idArchivoP          INT             NOT NULL,
    fechaCreacion       DATETIME        NOT NULL DEFAULT NOW(),
    version             INT             NOT NULL,
    nota                DECIMAL(5,2)    NULL,
    comentarioProfesor  TEXT            NULL,
    PRIMARY KEY (idEntrega),
    CONSTRAINT fk_entrega_tarea FOREIGN KEY (idTarea) REFERENCES Tarea(idTarea),
    CONSTRAINT fk_entrega_archivo FOREIGN KEY (idArchivoP) REFERENCES ArchivoP(idArchivoP),
    CONSTRAINT chk_version CHECK (version >= 1),
    CONSTRAINT chk_nota CHECK (nota BETWEEN 0 AND 100)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla EstudianteXGrupoTrabajo
CREATE TABLE IF NOT EXISTS EstudianteXGrupoTrabajo (
    idEstXGrpTrb        INT         NOT NULL AUTO_INCREMENT,
    codigoEstudiante    VARCHAR(20) NOT NULL,
    idGrupoTrabajo      INT         NOT NULL,
    PRIMARY KEY (idEstXGrpTrb),
    CONSTRAINT fk_exg_estudiante FOREIGN KEY (codigoEstudiante) REFERENCES Estudiante(codigoEstudiante),
    CONSTRAINT fk_exg_grupo FOREIGN KEY (idGrupoTrabajo) REFERENCES GrupoTrabajo(idGrupoTrabajo),
    CONSTRAINT uq_estudiante_grupo UNIQUE (codigoEstudiante, idGrupoTrabajo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabla EstudianteXEntrega
CREATE TABLE IF NOT EXISTS EstudianteXEntrega (
    idEstXEntrega       INT         NOT NULL AUTO_INCREMENT,
    codigoEstudiante    VARCHAR(20) NOT NULL,
    idEntrega           INT         NOT NULL,
    PRIMARY KEY (idEstXEntrega),
    CONSTRAINT fk_exe_estudiante FOREIGN KEY (codigoEstudiante) REFERENCES Estudiante(codigoEstudiante),
    CONSTRAINT fk_exe_entrega FOREIGN KEY (idEntrega) REFERENCES Entrega(idEntrega),
    CONSTRAINT uq_estudiante_entrega UNIQUE (codigoEstudiante, idEntrega)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Datos de ejemplo para asegurar que todo ta bien
-- INSERT IGNORE INTO urls (short_code, original_url, click_count) VALUES
--     ('google1', 'https://www.google.com',         3),
--     ('github1', 'https://github.com',             1),
--     ('wiki001', 'https://es.wikipedia.org/wiki/LAMP', 0);

-- Clicks de ejemplo (id=1)
-- INSERT IGNORE INTO clicks (url_id, ip_address, country, country_code) VALUES
--     (1, '187.216.1.1',  'Mexico',        'MX'),
--     (1, '200.91.1.1',   'Argentina',     'AR'),
--     (1, '200.91.1.2',   'Argentina',     'AR'),
--     (2, '8.8.8.8',      'United States', 'US');