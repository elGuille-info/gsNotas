//-----------------------------------------------------------------------------
// Utilities                                                        (25/Nov/20)
//
// Código de Hannes DuPreez 
//
//
// (c) Guillermo (elGuille) Som, 2020
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace Seleccionar_Colores
{
    public class Utilities
    {
        private Utilities()
        {
        }

        /// <summary>
        /// Comprueba si el nombre corresponde con un valor de la enumeración.
        /// </summary>
        /// <param name="arg">El nombre de la enumeración</param>
        /// <param name="val">El valor de la enumeración</param>
        /// <param name="class">La enumeración que define el valor</param>
        /// <param name="exception">true si no se lanza una excepción y se devuelve false</param>
        /// <returns>
        /// Un valor true si el valor está definido en la enumeración.
        /// Si no está definido y exception es true lanza una excepción, en otro caso devuelve false.
        /// </returns>
        public static bool CheckValidEnumValue(string arg, object val, Type @class, bool exception = true)
        {
            if (!Enum.IsDefined(@class, val))
            {
                if(exception)
                    throw new InvalidEnumArgumentException(arg, (int)val, @class);
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Asigna el color de fondo al control y muestra el nombre y valor del color.
        /// </summary>
        /// <param name="ctrl">El control a asignar el color</param>
        /// <param name="col">El color a asignar</param>
        /// <param name="conCrLf">Si se debe cambiar de línea entre el nombre y el valor del color</param>
        public static void SetBackColor(Control ctrl, Color col, bool conCrLf = false)
        {
            ctrl.BackColor = col;

            var s = conCrLf ? string.Format("{0}\r\n{1:X}", col.Name, col.ToArgb()) 
                            : string.Format("{0} {1:X}", col.Name, col.ToArgb());
            ctrl.Text = s;
            ctrl.ForeColor = (col.GetBrightness() < 0.6) ? (Color.White) : (Color.Black);
        }

        /// <summary>
        /// Asigna el color del texto al control y muestra el nombre y valor del color.
        /// </summary>
        /// <param name="ctrl">El control a asignar el color</param>
        /// <param name="col">El color a asignar</param>
        /// <param name="conCrLf">Si se debe cambiar de línea entre el nombre y el valor del color</param>
        public static void SetForeColor(Control ctrl, Color col, bool conCrLf = false)
        {
            ctrl.ForeColor = col;

            string s; 
            if(conCrLf)
                s = string.Format("{0}\r\n{1:X}", col.Name, col.ToArgb());
            else 
                s = string.Format("{0} {1:X}", col.Name, col.ToArgb());

            ctrl.Text = s;

            ctrl.BackColor = (col.GetBrightness() < 0.6) ? (Color.White) : (Color.Black);
        }

    }
}
