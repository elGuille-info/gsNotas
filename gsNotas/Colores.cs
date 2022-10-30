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
        //
        // Funciones de apoyo y de uso general.
        //

        //
        // Estos métodos estaban en el formulario principal.
        //

        // Crear una copia de los colores para que sean independientes. (30/oct/22 14.01)

        public static Color[] CopiarColores(Color[] losColores)
        {
            List<Color> colorList = new List<Color>();
            foreach (var c in losColores)
            {
                colorList.Add(c);
            }
            return colorList.ToArray();
        }

        /// <summary>
        /// Crear un color de forma aleatoria.
        /// </summary>
        /// <param name="red">Si no se indica o se indica el valor cero, se asignará un valor aleatorio para el rojo.</param>
        /// <param name="green">Si no se indica o se indica el valor cero, se asignará un valor aleatorio para el verde.</param>
        /// <param name="blue">Si no se indica o se indica el valor cero, se asignará un valor aleatorio para el azul.</param>
        public static Color GetRandomColor(byte red = 0, byte green = 0, byte blue = 0)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            byte r = red == 0 ? (byte)random.Next(0, 255) : red;
            byte g = green == 0 ? (byte)random.Next(0, 255) : green;
            byte b = blue == 0 ? (byte)random.Next(0, 255) : blue;
            if (random.Next(0, 2) == 0)
                return Color.FromArgb(255, r, g, b);
            else
                return Color.FromArgb(255, b, r, g);
        }

        /// <summary>
        /// Asignar el BackColor al control indicado y un ForeColor según la luminosidad.
        /// </summary>
        /// <param name="ctrl">El control al que asignar el BackColor y el ForeColor.</param>
        /// <param name="col">El Color a asignar.</param>
        public static void SetBackColor(Control ctrl, Color col)
        {
            ctrl.BackColor = col;
            ctrl.ForeColor = (col.GetBrightness() < 0.6) ? Color.White : Color.Black;
        }

        /// <summary>
        /// Añade un nuevo color aleatorio a la colección indicada.
        /// </summary>
        /// <param name="colores">Colección con los colores que hay para no repetir.</param>
        public static void AñadirNuevoColor(List<Color> colores)
        {
            Color col2;
            do
            {
                col2 = Colores.GetRandomColor();
            } while (colores.Contains(col2));

            colores.Add(col2);
        }

        //
        // Estos métodos se usan en la clase para serializar los colores.
        //

        /// <summary>
        /// Convierte una colección de colores en formato hexadecimal en el equivalente del color.
        /// </summary>
        /// <param name="losColores">Colección de los colores en formato hexadecimal.</param>
        /// <remarks>Cada color debe estar en formato AARRGGBB con varlores hexadecimales.</remarks>
        public static List<Color> ColoresFromHex(List<string> losColores)
        {
            List<Color> list = new List<Color>();
            for (int i = 0; i < losColores.Count; i++)
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
        /// <remarks>Permite el formato con [#][AA]RRGGBB</remarks>
        public static Color ColorFromHex(string elColor)
        {
            // Tener en cuenta la longitud de la cadena. (29/oct/22 15.34)
            //  De forma que se permita sin el valor Alpha (AA)
            int alpha = 255;
            int pos = 0;

            // Si empieza con # convertir a partir de la siguiente posición.
            if (elColor.StartsWith("#"))
            {
                //pos = 1;
                elColor = elColor.Substring(1);
            }
            // Si la longitud es 8, es que se incluye el canal alpha (luminosidad).
            if (elColor.Length == 8)
            {
                alpha = Convert.ToInt32(elColor.Substring(pos, 2), 16);
            }
            else
            {
                pos = -2;
            }
            //alpha = Convert.ToInt32(elColor.Substring(pos, 2), 16);
            int r = Convert.ToInt32(elColor.Substring(pos + 2, 2), 16);
            int g = Convert.ToInt32(elColor.Substring(pos + 4, 2), 16);
            int b = Convert.ToInt32(elColor.Substring(pos + 6, 2), 16);
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

                list.Add(ColorToHex(losColores[i]));
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

        //
        // </Funciones de apoyo y de uso general.
        //


        //
        // Métodos y demás para serializar los colores de los grupos.
        //

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
