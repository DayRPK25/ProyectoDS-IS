using MongoDB.Bson;
using MongoDB.Driver;

namespace ProyectoDS_IS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Prueba de conexión
            try
            {
                Console.WriteLine("Realizando conexión con la base de MongoDB");
                var client = new MongoClient("mongodb://100.66.38.10:27017/tec_digitalito?directConnection=true");
                Console.WriteLine("Conectado al cliente con las siguientes especificaciones:\n" + client.ToString());
                var database = client.GetDatabase("tec_digitalito");
                Console.WriteLine("Conectado a la base de datos con las siguientes especificaciones:\n" + database.ToString());
                var collection = database.GetCollection<IConvertibleToBsonDocument>("evaluations");
                Console.WriteLine("Colección recuperada:\n" + collection.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Hubo un problema");
                Console.WriteLine(ex.Message);
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}