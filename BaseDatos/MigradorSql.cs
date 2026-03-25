using System;
using System.IO;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BaseDatos
{
    public class MigradorSql
    {
        private readonly string _connectionString;
        public MigradorSql(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task MigrarDesdeArchivo(string archivoPath)
        {
            if (!File.Exists(archivoPath)) throw new FileNotFoundException("Archivo no encontrado");

            // Reemplazo de declaraciones using por bloques using tradicionales
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("Conexión establecida. Iniciando migración...");

                using (var fs = new FileStream(archivoPath, FileMode.Open, FileAccess.Read))
                using (var reader = new BinaryReader(fs, System.Text.Encoding.ASCII))
                {
                    while (fs.Position + BaseDatos.Ciudadano.Size <= fs.Length)
                    {
                        int id = reader.ReadInt32();
                        var nameBytes = reader.ReadBytes(50);
                        string nombre = System.Text.Encoding.ASCII.GetString(nameBytes).TrimEnd();
                        int edad = reader.ReadInt32();

                        using (var cmd = new SqlCommand("INSERT INTO Ciudadanos (Id, Nombre, Edad) VALUES (@Id, @Nombre, @Edad)", connection))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.Parameters.AddWithValue("@Nombre", nombre);
                            cmd.Parameters.AddWithValue("@Edad", edad);

                            try
                            {
                                await cmd.ExecuteNonQueryAsync();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error en Id={id}: {ex.Message}");
                            }
                        }
                    }
                }
                Console.WriteLine("Migración completada.");
            }
        }
    }
}