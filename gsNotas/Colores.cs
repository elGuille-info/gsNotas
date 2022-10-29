//--------------------------------------------------------------/---------------
//
// Colores                                                          (21/oct/22)
// Clase para manejar los colores usados en gsNotas.
//
//
// (c) Guillermo Som (elGuille), 2020-2022
//-----------------------------------------------------------------------------

using System;
// Para acceder a Dictionary y List
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Para la serialización con Json
using System.Text.Json.Serialization;
using System.Text.Json;
// Para el tipo Color
using System.Drawing;
// Para acceder a Application
using System.Windows.Forms;
// Para acceder a las clases Stream y Path
using System.IO;

namespace gsNotas
{
    public class Colores
    {
        // Las propiedades a serializar deben tener el atributo JsonPropertyName con el nombre a usar en el fichero .json
        // Si alguna propiedad no se quiere serizalizar, usar el atributo JsonIgnore.

        // Guardar los colores en formato hexadecimal, (25/oct/22)
        // ya que la serialiación parece que no funciona con los colores directamente.

        /// <summary>
        /// Diccionario para los colores almacenados en formato hexadecimal: AARRGGBB. 
        /// </summary>
        /// <remarks>La clave será el número del grupo de colores y el valor los colores asignados a ese grupo.</remarks>
        [JsonPropertyName("colores")]
        public Dictionary<string, List<string>> LosColores { get; set; } = new();
        //public Dictionary<string, List<Color>> LosColores { get; set; } = new();

        /// <summary>
        /// Convierte una colección de colores en formato hexadecimal en el equivalente del color.
        /// </summary>
        /// <param name="losColores">Colección de los colores en formato hexadecimal.</param>
        /// <remarks>Cada color debe estar en formato AARRGGBB con varlores hexadecimales.</remarks>
        public static List<Color> ColoresFromHex(List<string> losColores)
        {
            List<Color> list = new List<Color>();
            for (int i=0; i < losColores.Count; i++)
            {
                //int alpha = Convert.ToInt32(losColores[i].Substring(0, 2), 16);
                //int r = Convert.ToInt32(losColores[i].Substring(2, 2), 16);
                //int g = Convert.ToInt32(losColores[i].Substring(4, 2), 16);
                //int b = Convert.ToInt32(losColores[i].Substring(6, 2), 16);
                //Color col = Color.FromArgb(alpha, r, g, b);
                //list.Add(col);
                
                list.Add(ColorFromHex(losColores[i]));
            }

            return list;
        }

        // Convertir un color en formato hexadecimal en color. (29/oct/22 15.15)

        /// <summary>
        /// Convierte una cadena en formato hexadecimal (AARRGGBB) en color.
        /// </summary>
        /// <param name="elColor">La cadena del color en formato hexadecimal AARRGGBB.</param>
        /// <returns>El color resultante.</returns>
        public static Color ColorFromHex(string elColor)
        {
            int alpha = Convert.ToInt32(elColor.Substring(0, 2), 16);
            int r = Convert.ToInt32(elColor.Substring(2, 2), 16);
            int g = Convert.ToInt32(elColor.Substring(4, 2), 16);
            int b = Convert.ToInt32(elColor.Substring(6, 2), 16);
            Color col = Color.FromArgb(alpha, r, g, b);

            return col;
        }

        /// <summary>
        /// Convierte una colección de colores en otra en formato hexadecimnal.
        /// </summary>
        /// <param name="losColores">Colección de colores.</param>
        /// <returns></returns>
        public static List<string> ColoresToHex(List<Color> losColores)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < losColores.Count; i++)
            {
                //string s = losColores[i].ToArgb().ToString("x");
                //list.Add(s);

                list.Add(ColorToHex(losColores[i]);
            }
            return list;
        }

        // Convertir un color al formato hexadecimal. (29/oct/22 15.20)

        /// <summary>
        /// Convierte un color en formato hexadecimal (AARRGGBB).
        /// </summary>
        /// <param name="elColor">El color a convertir en cadena hexadecimal.</param>
        /// <returns>El color en formato hexadecimal resultante.</returns>
        public static string ColorToHex(Color elColor)
        {
            return elColor.ToArgb().ToString("x");
        }

        // El directorio de configuración en documentos.
        // Si no se indica, usar el de inicio de la aplicación.

        private static readonly string FicheroColores = "ColoresGrupos.json";

        private static string _DirectorioConfiguracion = null;

        /// <summary>
        /// El directorio de configuración donde se guardará el fichero .json con los colores.
        /// </summary>
        public static string DirectorioConfiguracion
        {
            get
            {
                // Si no se indica el nombre, usar el del directorio de la aplicación.
                if (string.IsNullOrWhiteSpace(_DirectorioConfiguracion))
                {
                    _DirectorioConfiguracion = Application.StartupPath;
                }
                return _DirectorioConfiguracion;
            }
            set
            {
                _DirectorioConfiguracion = value;
                // Leer el fichero al asignar el directorio.
                _ColoresGrupos = Leer();
            }
        }

        // Para leer los colores.

        private static Colores _ColoresGrupos = null;

        /// <summary>
        /// Los colores de los grupos.
        /// </summary>
        public static Colores ColoresGrupos
        {
            get
            {
                if (_ColoresGrupos == null)
                {
                    _ColoresGrupos = Leer();
                    _ColoresGrupos ??= new();
                }
                return _ColoresGrupos;
            }
        }

        /// <summary>
        /// Guardar los datos de los colores de los grupos.
        /// </summary>
        /// <param name="losColores">Los colores a guardar.</param>
        public static void Guardar(Colores losColores)
        {
            var fic = Path.Combine(DirectorioConfiguracion, FicheroColores);
            Save(losColores, fic);
        }

        /// <summary>
        /// Guardar los colores internos.
        /// </summary>
        public static void Guardar()
        {
            var fic = Path.Combine(DirectorioConfiguracion, FicheroColores);
            Save(ColoresGrupos, fic);
        }

        /// <summary>
        /// Leer los colores de los grupos.
        /// </summary>
        /// <returns></returns>
        public static Colores Leer()
        {
            // El fichero está en la carpeta del ejecutable.
            var fic = Path.Combine(DirectorioConfiguracion, FicheroColores);
            return Load(fic);
        }

        /// <summary>
        /// Carga los valores del fichero indicado.
        /// </summary>
        /// <param name="fileName">El path del fichero a leer y devolver el contenido.</param>
        /// <returns>El objeto leído del fichero indicado.</returns>
        private static Colores Load(string fileName)
        {
            // Abrir el fichero para leer, compartido para lectura y escritura.
            using var stream = new FileStream(fileName,
                                              FileMode.OpenOrCreate,
                                              FileAccess.Read,
                                              FileShare.ReadWrite);
            // Si tiene contenido, deserializarlo, si no, devolver un valor nulo.
            //
            // Da error algunas veces al leer una sección que no existe
            //  y dice que es porque acaba con una llave de más (o algo así).
            //  Por tanto, en caso de que de error, devolver nulo.
            // La solución es leerlo al asignar el path donde se guarda.
            if (stream.Length > 0)
            {
                try
                {
                    return JsonSerializer.Deserialize<Colores>(stream);
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Guarda los datos de tipo AnchoColumnas indicado.
        /// </summary>
        /// <param name="losColores">Los colores a guardar.</param>
        /// <param name="fileName">El path del fichero a leer y devolver el contenido.</param>
        private static void Save(Colores losColores, string fileName)
        {
            // Abrir el fichero para escribir, compartido para lectura y escritura.
            using var stream = new FileStream(fileName,
                                              FileMode.OpenOrCreate,
                                              FileAccess.Write,
                                              FileShare.ReadWrite);
            // Que se indente el contenido.
            var options = new JsonSerializerOptions { WriteIndented = true };
            // Guardar (serializar) el contenido de la clase.
            JsonSerializer.Serialize<Colores>(stream, losColores, options);
        }
    }
}
