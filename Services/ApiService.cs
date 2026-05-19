using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using SistemaEntregas.Auth;

namespace ProyectoDS_IS.Services
{
    internal class ApiService
    {
        private readonly HttpClient client;
        public static ApiService Instance = new ApiService();
        public string CurrentUser { get; set; }
        public string Token { get; set; }

        public int idUsuario { get; set; }

        public ApiService()
        {
            client = new HttpClient();

            client.BaseAddress = new Uri("http://170.9.26.131:80");
        }

        public async Task<string> Login(
            string correo,
            string contrasena
            )
        {
            var data = new { correo = correo, contrasena = contrasena };
            string json = JsonSerializer.Serialize(data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await client.PostAsync(
                    "api/auth/login",
                    content
                    );
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Registro(
            string correo,
            string nombre,
            string nombreUsuario,
            string contrasena,
            string carnet,
            string rol = "Estudiante"
            
            )
        {
            var data = new { correo = correo, nombre = nombre, nombreUsuario = nombreUsuario, contrasena = contrasena, rol = rol , carnet = carnet};
            string json = JsonSerializer.Serialize(data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await client.PostAsync(
                    "api/auth/register",
                    content
                    );

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }


        public async Task<string> cargarCursos
            (
            int idUsuario

            )
        {
            var data = new {idUsuario = idUsuario};
            string json = JsonSerializer.Serialize(data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await client.PostAsync(
                    "api/cursos",
                    content
                    );

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }


    }
}
