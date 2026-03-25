using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BaseDatos
{
    // Id (4) + Nombre (50) bytes (ANSI) + Edad (4) = 58 bytes per record
    public struct Ciudadano
    {
        public int Id;
        public string Nombre;
        public int Edad;

        public Ciudadano(int id, string nombre, int edad)
        {
            Id = id;
            Nombre = nombre;
            Edad = edad;
        }

        public static int Size
        {
            get { return 4 + 50 + 4; }
        }
    }

    public class GestorArchivos
    {
        private readonly string _path = "datos_ciudadanos.dat";

      

public void GuardarCiudadano(Ciudadano c, int posicion)
        {
            // 1. Abrimos el flujo de archivo (usando 'using' clásico para compatibilidad C# 7.3)
            using (var fs = new FileStream(_path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                // 2. NIVEL 1: Cálculo del Offset (n * Tamaño)
                long offset = (long)posicion * Ciudadano.Size;

                // 3. Posicionamos el "cabezal" del disco en el lugar exacto
                fs.Seek(offset, SeekOrigin.Begin);

                // 4. Escribimos los datos binarios
                using (var writer = new BinaryWriter(fs, Encoding.ASCII, leaveOpen: true))
                {
                    writer.Write(c.Id);

                    // Aseguramos que el nombre ocupe exactamente 50 bytes
                    var nameFixed = (c.Nombre ?? string.Empty).PadRight(50).Substring(0, 50);
                    writer.Write(Encoding.ASCII.GetBytes(nameFixed));

                    writer.Write(c.Edad);

                    // Forzamos la escritura en el disco
                    writer.Flush();
                }
            }
        }

        public Ciudadano? LeerCiudadano(int posicion)
        {
            if (!File.Exists(_path)) return null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read);
                long offset = (long)posicion * Ciudadano.Size;
                if (offset + Ciudadano.Size > fs.Length) return null;
                fs.Seek(offset, SeekOrigin.Begin);
                var reader = new BinaryReader(fs, Encoding.ASCII);
                int id = reader.ReadInt32();
                byte[] nameBytes = reader.ReadBytes(50);
                string nombre = Encoding.ASCII.GetString(nameBytes).TrimEnd();
                int edad = reader.ReadInt32();
                return new Ciudadano(id, nombre, edad);
            }
            finally
            {
                if (fs != null) fs.Dispose();
            }
        }

        // NIVEL 2: Índice simple en archivo .idx que guarda pares (Id:int, Posicion:int)
        public void CrearIndice(string indicePath = null)
        {
            if (indicePath == null) indicePath = _path + ".idx";
            var dict = new Dictionary<int, int>();
            if (!File.Exists(_path))
            {
                // crear archivo índice vacío
                using (var fsEmpty = new FileStream(indicePath, FileMode.Create, FileAccess.Write))
                {
                    // nothing
                }
                return;
            }

            FileStream fs = null;
            try
            {
                fs = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read);
                int posicion = 0;
                var reader = new BinaryReader(fs, Encoding.ASCII);
                while (fs.Position + Ciudadano.Size <= fs.Length)
                {
                    try
                    {
                        int id = reader.ReadInt32();
                        reader.ReadBytes(50);
                        reader.ReadInt32();
                        if (!dict.ContainsKey(id)) dict[id] = posicion;
                        posicion++;
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            finally
            {
                if (fs != null) fs.Dispose();
            }

            using (var idxFs = new FileStream(indicePath, FileMode.Create, FileAccess.Write))
            using (var idxWriter = new BinaryWriter(idxFs))
            {
                foreach (var kv in dict)
                {
                    idxWriter.Write(kv.Key);
                    idxWriter.Write(kv.Value);
                }
            }
        }

        public Dictionary<int, int> CargarIndice(string indicePath = null)
        {
            if (indicePath == null) indicePath = _path + ".idx";
            var dict = new Dictionary<int, int>();
            if (!File.Exists(indicePath)) return dict;
            using (var fs = new FileStream(indicePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new BinaryReader(fs))
            {
                while (fs.Position + 8 <= fs.Length)
                {
                    int id = reader.ReadInt32();
                    int pos = reader.ReadInt32();
                    if (!dict.ContainsKey(id)) dict[id] = pos;
                }
            }
            return dict;
        }

        public Ciudadano? BuscarPorId(int id)
        {
            var idx = CargarIndice();
            if (!idx.TryGetValue(id, out int posicion)) return null;
            return LeerCiudadano(posicion);
        }

        public IEnumerable<Ciudadano> LeerTodos()
        {
            if (!File.Exists(_path)) yield break;
            using (var fs = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new BinaryReader(fs, Encoding.ASCII))
            {
                while (fs.Position + Ciudadano.Size <= fs.Length)
                {
                    int id = reader.ReadInt32();
                    var nameBytes = reader.ReadBytes(50);
                    string nombre = Encoding.ASCII.GetString(nameBytes).TrimEnd();
                    int edad = reader.ReadInt32();
                    yield return new Ciudadano(id, nombre, edad);
                }
            }
        }
    }
}
