using MongoDB.Bson;
using MongoDB.Driver;

namespace ProyectoDS_IS
{
    public partial class Form1 : Form
    {
        public Form1()
        {   
            InitializeComponent();

            label1.Text = "";
            label2.Text = "";
            label3.Text = "";

            // Prueba de conexión
            try
            {
                // Esto prueba la conexión
                label1.Text = "Realizando conexión con MongoDB";
                var client = new MongoClient("mongodb://100.66.38.10:27017/tec_digitalito?directConnection=true");
                label1.Text = "Conectado al cliente con las siguientes especificaciones: " + client.ToString();
                
                // Esto prueba si puede acceder a alguna base
                var database = client.GetDatabase("tec_digitalito");
                label2.Text = "Conectado a la base de datos con las siguientes especificaciones: " + database.ToString();
                
                // Esto prueba si puede acceder a una colección
                var collection = database.GetCollection<IConvertibleToBsonDocument>("evaluations");
                label3.Text = "Colección recuperada: " + collection.ToString();
            }
            catch (Exception ex)
            {
                label1.Text = "Error: Hubo un problema con la base de datos";
                label2.Text = "";
                label3.Text = "";
                Console.WriteLine(ex.Message);
            }
        }
    }
}
