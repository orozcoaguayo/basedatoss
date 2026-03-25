using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace BaseDatos
{
    public partial class Form1 : Form
    {
        private readonly GestorArchivos _gestor = new GestorArchivos();

        public Form1()
        {
            InitializeComponent();
            // Default connection string example for local SQLEXPRESS
            txtConnectionString.Text = "Server=10.12.13.153\\SQLEXPRESS;Database=MiBaseDatos;User Id=sa;Password=1234;TrustServerCertificate=True;";

            // Ensure sample data (2000 ciudadanos) exists on startup
            try
            {
                EnsureSampleData(2000);
            }
            catch (Exception ex)
            {
                Log("Error creando datos de ejemplo: " + ex.Message);
            }
        }

        private void Log(string message)
        {
            txtLog.AppendText($"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}");
        }

        private void EnsureSampleData(int total)
        {
            int existingCount = 0;
            int maxId = 0;
            try
            {
                foreach (var c in _gestor.LeerTodos())
                {
                    existingCount++;
                    if (c.Id > maxId) maxId = c.Id;
                }
            }
            catch (Exception ex)
            {
                Log("Error leyendo archivo al verificar datos existentes: " + ex.Message);
                existingCount = 0;
                maxId = 0;
            }

            if (existingCount >= total)
            {
                Log($"Ya existen {existingCount} registros. No se crearán datos de ejemplo.");
                return;
            }

            var rnd = new Random();
            Log($"Creando {total - existingCount} registros de ejemplo (total objetivo: {total})...");
            for (int pos = existingCount; pos < total; pos++)
            {
                int id = maxId + 1;
                string nombre = string.Format("Ciudadano {0}", id);
                int edad = rnd.Next(18, 90);
                var ciudadano = new Ciudadano(id, nombre, edad);
                try
                {
                    _gestor.GuardarCiudadano(ciudadano, pos);
                    Log($"Creado Id={id} en posición {pos}");
                }
                catch (Exception ex)
                {
                    Log($"Error creando Id={id} en posición {pos}: {ex.Message}");
                }
                maxId++;
            }
            Log("Creación de datos de ejemplo finalizada.");
        }

        private void btnGuardarBinario_Click(object sender, EventArgs e)
        {
            int id;
            int edad;
            int posicion = (int)numericPos.Value;
            if (!int.TryParse(txtId.Text, out id))
            {
                MessageBox.Show("Id inválido");
                return;
            }
            if (!int.TryParse(txtEdad.Text, out edad))
            {
                MessageBox.Show("Edad inválida");
                return;
            }

            var ciudadano = new Ciudadano(id, txtNombre.Text ?? string.Empty, edad);
            try
            {
                _gestor.GuardarCiudadano(ciudadano, posicion);
                Log($"Guardado Id={id} en posición {posicion}");
            }
            catch (Exception ex)
            {
                Log("Error guardando: " + ex.Message);
            }
        }

        private void btnGenerarIndice_Click(object sender, EventArgs e)
        {
            try
            {
                _gestor.CrearIndice();
                Log("Índice generado");
            }
            catch (Exception ex)
            {
                Log("Error generando índice: " + ex.Message);
            }
        }

        private void btnCargarDesdeBinario_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                foreach (var c in _gestor.LeerTodos())
                {
                    Log($"Registro: Id={c.Id}, Nombre={c.Nombre}, Edad={c.Edad}");
                    count++;
                }
                Log($"Total registros leídos: {count}");
            }
            catch (Exception ex)
            {
                Log("Error leyendo archivo: " + ex.Message);
            }
        }

        private void btnBuscarSecuencial_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtBuscarId.Text, out int targetId))
            {
                MessageBox.Show("Id inválido");
                return;
            }

            string path = "datos_ciudadanos.dat";
            if (!File.Exists(path))
            {
                Log($"Archivo '{path}' no encontrado.");
                MessageBox.Show("Archivo de datos no encontrado.");
                return;
            }

            var sw = Stopwatch.StartNew();
            bool found = false;
            int posicion = 0;
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var reader = new BinaryReader(fs, System.Text.Encoding.ASCII);
                while (fs.Position + Ciudadano.Size <= fs.Length)
                {
                    int id = reader.ReadInt32();
                    var nameBytes = reader.ReadBytes(50);
                    string nombre = System.Text.Encoding.ASCII.GetString(nameBytes).TrimEnd();
                    int edad = reader.ReadInt32();
                    if (id == targetId)
                    {
                        sw.Stop();
                        labelTiempoSecuencial.Text = $"Tiempo secuencial: {sw.ElapsedMilliseconds} ms";
                        Log($"Búsqueda secuencial: encontrado Id={id} en posición {posicion} (Nombre={nombre}, Edad={edad}) en {sw.ElapsedMilliseconds} ms");
                        found = true;
                        break;
                    }
                    posicion++;
                }
            }
            catch (Exception ex)
            {
                Log("Error durante búsqueda secuencial: " + ex.Message);
            }
            finally
            {
                if (fs != null) fs.Dispose();
            }

            if (!found)
            {
                sw.Stop();
                labelTiempoSecuencial.Text = $"Tiempo secuencial: {sw.ElapsedMilliseconds} ms";
                Log($"Búsqueda secuencial: Id={targetId} no encontrado. Tiempo: {sw.ElapsedMilliseconds} ms");
            }
        }

        private void btnBuscarIndexado_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtBuscarId.Text, out int targetId))
            {
                MessageBox.Show("Id inválido");
                return;
            }

            string path = "datos_ciudadanos.dat";
            if (!File.Exists(path))
            {
                Log($"Archivo '{path}' no encontrado.");
                MessageBox.Show("Archivo de datos no encontrado.");
                return;
            }

            var sw = Stopwatch.StartNew();
            try
            {
                var indice = _gestor.CargarIndice();
                if (!indice.TryGetValue(targetId, out int posicion))
                {
                    sw.Stop();
                    labelTiempoIndexado.Text = $"Tiempo indexado: {sw.ElapsedMilliseconds} ms";
                    Log($"Búsqueda indexada: Id={targetId} no encontrado. Tiempo: {sw.ElapsedMilliseconds} ms");
                    return;
                }

                var ciudadano = _gestor.LeerCiudadano(posicion);
                sw.Stop();
                labelTiempoIndexado.Text = $"Tiempo indexado: {sw.ElapsedMilliseconds} ms";
                if (ciudadano != null)
                {
                    Log($"Búsqueda indexada: encontrado Id={ciudadano.Value.Id} en posición {posicion} (Nombre={ciudadano.Value.Nombre}, Edad={ciudadano.Value.Edad}) en {sw.ElapsedMilliseconds} ms");
                }
                else
                {
                    Log($"Búsqueda indexada: Id={targetId} no encontrado al intentar leer posición {posicion}. Tiempo: {sw.ElapsedMilliseconds} ms");
                }
            }
            catch (Exception ex)
            {
                sw.Stop();
                labelTiempoIndexado.Text = $"Tiempo indexado: {sw.ElapsedMilliseconds} ms";
                Log("Error durante búsqueda indexada: " + ex.Message);
            }
        }

        private async void btnMigrarSql_Click(object sender, EventArgs e)
        {
            string cnx = txtConnectionString.Text;
            string nombreArchivo = "datos_ciudadanos.dat";

            // 1. Validar cadena de conexión
            if (string.IsNullOrWhiteSpace(cnx))
            {
                MessageBox.Show("Proporcione la cadena de conexión");
                return;
            }

            // 2. Validar si el archivo existe físicamente antes de intentar migrar
            if (!File.Exists(nombreArchivo))
            {
                Log($"Error: El archivo '{nombreArchivo}' no existe en {AppDomain.CurrentDomain.BaseDirectory}");
                MessageBox.Show("Primero debes guardar al menos un ciudadano en el Nivel 1.");
                return;
            }

            try
            {
                Log("Iniciando migración...");
                var migrador = new MigradorSql(cnx);

                // Ejecutamos la migración
                await migrador.MigrarDesdeArchivo(nombreArchivo);

                Log("Migración finalizada con éxito.");
                MessageBox.Show("Datos migrados a SQL Server correctamente.");
            }
            catch (Exception ex)
            {
                Log("Error migrando: " + ex.Message);
            }
        }
    }
}
