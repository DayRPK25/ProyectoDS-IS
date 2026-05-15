using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                        ""name"": ""Usuario Demo"",
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
    }
}
