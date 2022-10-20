//-----------------------------------------------------------------------------
// Para definir los delegados                                       (09/Dic/20)
// Y otras cosas, como enumeraciones, métodos de extensión, etc.
//
/* 
    Para poder usar el C# 9.0 (por defecto en .NET Framework se usa el 7.3)
    hay que añadir esto en el fichero del proyecto:
    <PropertyGroup>
        <LangVersion>latest</LangVersion>
    </PropertyGroup>

    Y para usar init en las propiedades, añadir esto después del espacio de nombres normal
    o mejor en clase aparte.
    Si se añade antes de la definición del namespace del formulario,
    el diseñador de Windows Forms se hace un lío y define ese espacio de nombres en Form1.Designer.cs
    en lugar del espacio de nombres del proyecto.

    namespace System.Runtime.CompilerServices
    {
        public class IsExternalInit { }
    }
*/
//
// (c) Guillermo Som (elGuille), 2020. 2022
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace gsNotas
{
    /// <summary>
    /// Delegado para avisar del cambio de tema.
    /// </summary>
    /// <param name="tema">El tema asignado.</param>
    public delegate void TemaCambiado(Temas tema);

    /// <summary>
    /// Delegado para manejar el reemplazo de una nota.
    /// </summary>
    /// <param name="grupo">El grupo.</param>
    /// <param name="texto">El texto de la nota.</param>
    /// <param name="index">El índice del elemento que se edita.</param>
    public delegate void ReemplazarNota(string grupo, string texto, int index);

    /// <summary>
    /// Delegado para manejar los errores y otros mensajes de esta clase.
    /// </summary>
    /// <param name="mensaje">El texto a devolver en el evento.</param>
    public delegate void MensajeDelegate(string mensaje);

    /// <summary>
    /// Delegado para los eventos de seleccionar grupo y nota.
    /// </summary>
    /// <param name="nota">El texto del elemento seleccionado.</param>
    /// <param name="index">El índice del elemento seleccionado.</param>
    public delegate void TextoModificado(string nota, int index);

    /// <summary>
    /// Enumeración para los temas a usar.
    /// Light/Claro, Dark/Oscuro.
    /// </summary>
    [Browsable(false)]
    [Description("Enumeración para los temas a usar. Light/Claro, Dark/Oscuro.")]
    [Category("Colores")]
    public enum Temas
    {
        /// <summary>
        /// Tema claro (0)
        /// </summary>
        Claro = 0,
        /// <summary>
        /// Tema oscuro (1)
        /// </summary>
        Oscuro = 1,
        // En inglés...
        Light = Claro,
        Dark = Oscuro
    }

    //// Enumeración para los colores a usar en los grupos.
    //public enum ColorGrupo
    //{
    //    Aleatorio = 0,
    //    Predeterminado = 1,
    //    Color1 = 2,
    //    Color2 = 3,
    //    Color3 = 4
    //}
}

/* 
Para evitar el error al usar init:
Error CS0518 Predefined type ‘System.Runtime.CompilerServices.IsExternalInit’ is not defined or imported
*/
namespace System.Runtime.CompilerServices
{
    public class IsExternalInit { }
}

