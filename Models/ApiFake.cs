using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace ProyectoDS_IS.Models
{
    internal class ApiFake
    {
        // Instancia única
        private static ApiFake instance;

        // Constructor privado
        private ApiFake()
        {

        }

        // Propiedad pública Singleton
        public static ApiFake Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApiFake();
                }

                return instance;
            }
        }

        // Variables globales futuras
        public string Token { get; set; } = "";
        public string CurrentUser { get; set; } = "";


        public bool convertirJsonBool(string json)
        {
            JsonDocument doc = JsonDocument.Parse(json);
            bool success = doc.RootElement.GetProperty("success").GetBoolean();
            string message = doc.RootElement.GetProperty("message").GetString();
            return success;
        }

        public string convertirJsonStr(string json)
        {
            JsonDocument doc = JsonDocument.Parse(json);
            bool success = doc.RootElement.GetProperty("success").GetBoolean();
            string message = doc.RootElement.GetProperty("message").GetString();
            return message;
        }


        // Login falso temporal
        public string Login(string email, string password)
        {
            if (email == "demo@estudiantec.cr" &&
                password == "12345678")
            {
                return @"{
                    ""success"": true,
                    ""message"": ""Login correcto"",
                    ""token"": ""fake-token-123"",
                    ""user"": {
                        ""id"": 1,
                        ""name"": ""demo"",
                        ""email"": ""demo@estudiantec.cr""
                    }
                }";
            }

            return @"{
                ""success"": false,
                ""message"": ""Correo o contraseña incorrectos"",
                ""token"": null,
                ""user"": null
            }";
        }

        public string UsernameExists(string username)
        {
            string[] usernames =
            {
        "admin",
        "usuario1",
        "marcelo",
        "demo"
    };

            bool exists = usernames.Any(user =>
                user.Equals(username, StringComparison.OrdinalIgnoreCase)
            );

            if (exists)
            {
                return @"{
            ""success"": false,
            ""exists"": true,
            ""message"": ""El nombre de usuario ya está registrado.""
        }";
            }

            return @"{
        ""success"": true,
        ""exists"": false,
        ""message"": ""Nombre de usuario disponible.""
    }";
        }

        public string EmailExists(string email)
        {
            string[] emails =
            {
        "admin@estudiantec.cr",
        "demo@estudiantec.cr",
        "marcelo@estudiantec.cr"
    };

            bool exists = emails.Any(existingEmail =>
                existingEmail.Equals(email, StringComparison.OrdinalIgnoreCase)
            );

            if (exists)
            {
                return @"{
            ""success"": false,
            ""exists"": true,
            ""message"": ""El correo ya está registrado.""
        }";
            }

            return @"{
        ""success"": true,
        ""exists"": false,
        ""message"": ""Correo disponible.""
    }";
        }

        public string CarnetExists(string carnet)
        {
            string[] carnets =
            {
        "2023000001",
        "2023000002",
        "2023000003"
    };

            bool exists = carnets.Any(existingCarnet =>
                existingCarnet == carnet
            );

            if (exists)
            {
                return @"{
            ""success"": false,
            ""exists"": true,
            ""message"": ""El carnet ya está registrado.""
        }";
            }

            return @"{
        ""success"": true,
        ""exists"": false,
        ""message"": ""Carnet disponible.""
    }";
        }


        public string Register(
            string username,
            string email,
            string carnet,
            string password
        )
            {
                // Verificar username
                if (!convertirJsonBool(UsernameExists(username)))
                {
                    return @"{
                ""success"": false,
                ""field"": ""username"",
                ""message"": ""El nombre de usuario ya está registrado.""
            }";
                }

                // Verificar email
                if (!convertirJsonBool(EmailExists(email)))
                {
                    return @"{
                ""success"": false,
                ""field"": ""email"",
                ""message"": ""El correo ya está registrado.""
            }";
                }

                // Verificar carnet
                if (!convertirJsonBool(CarnetExists(carnet)))
                {
                    return @"{
                ""success"": false,
                ""field"": ""carnet"",
                ""message"": ""El carnet ya está registrado.""
            }";
                }

                // Registro exitoso
                return @"{
            ""success"": true,
            ""message"": ""Usuario registrado correctamente."",
            ""token"": ""fake-jwt-token"",
            ""user"": {
                ""username"": """ + username + @""",
                ""email"": """ + email + @""",
                ""carnet"": """ + carnet + @"""
            }
        }";
            }


        public string GetCursos(string username)
        {
            if (username == "demo")
            {
                return @"{
            ""success"": true,
            ""courses"": [
                {
                    ""id"": 1,
                    ""name"": ""Administración de Sistemas"",
                    ""teacher"": ""Profesor Admin""
                },
                {
                    ""id"": 2,
                    ""name"": ""Seguridad Informática"",
                    ""teacher"": ""Profesor Seguridad""
                }
            ]
        }";
            }

            if (username == "marcelo")
            {
                return @"{
            ""success"": true,
            ""courses"": [
                {
                    ""id"": 3,
                    ""name"": ""Diseño de Software"",
                    ""teacher"": ""Marcelo Gómez Cordero""
                },
                {
                    ""id"": 4,
                    ""name"": ""Taller de Programación"",
                    ""teacher"": ""Marcelo Gómez Cordero""
                }
            ]
        }";
            }

            // Usuario genérico
            return @"{
        ""success"": true,
        ""courses"": [
            {
                ""id"": 5,
                ""name"": ""Introducción a la Programación"",
                ""teacher"": ""Profesor Demo""
            }
        ]
    }";
        }


        public string GetTareas(int courseId)
        {
            if (courseId == 1)
            {
                return @"{
            ""success"": true,
            ""courseName"": ""Introducción a la programación"",
            ""tasks"": [
                {
                    ""id"": 1,
                    ""title"": ""Número par o impar"",
                    ""createdAt"": ""07/05/26"",
                    ""dueDate"": ""14/05/26"",
                    ""instructions"": ""Crear un programa en Python que determine si un número ingresado por el usuario es par o impar.""
                },
                {
                    ""id"": 2,
                    ""title"": ""Calculadora"",
                    ""createdAt"": ""11/05/26"",
                    ""dueDate"": ""18/05/26"",
                    ""instructions"": ""Desarrollar una calculadora básica en Python que permita sumar, restar, multiplicar y dividir.""
                },
                {
                    ""id"": 3,
                    ""title"": ""Fibonacci"",
                    ""createdAt"": ""11/05/26"",
                    ""dueDate"": ""25/05/26"",
                    ""instructions"": ""Generar la secuencia de Fibonacci hasta la cantidad de números indicada por el usuario.""
                }
            ]
        }";
            }

            if (courseId == 2)
            {
                return @"{
            ""success"": true,
            ""courseName"": ""Diseño de Software"",
            ""tasks"": [
                {
                    ""id"": 4,
                    ""title"": ""Diagrama UML"",
                    ""createdAt"": ""01/06/26"",
                    ""dueDate"": ""08/06/26"",
                    ""instructions"": ""Crear un diagrama UML de clases para el sistema propuesto.""
                },
                {
                    ""id"": 5,
                    ""title"": ""Prototipo de interfaz"",
                    ""createdAt"": ""03/06/26"",
                    ""dueDate"": ""10/06/26"",
                    ""instructions"": ""Diseñar un prototipo visual de la interfaz principal del sistema.""
                }
            ]
        }";
            }

            return @"{
        ""success"": true,
        ""courseName"": ""Curso sin tareas"",
        ""tasks"": []
    }";
        }

    }
}
