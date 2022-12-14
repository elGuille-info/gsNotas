//-----------------------------------------------------------------------------
// FormSeleccionarColor                                             (20/oct/22)
//
// Basado en la versión para .NET Framework:
// fPickColor                                                       (25/Nov/20)
// Formulario para seleccionar un color
//
// Código de Hannes DuPreez
//
// (c) Guillermo (elGuille) Som, 2020, 2022
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Seleccionar_Colores.WellPanel;

namespace Seleccionar_Colores
{
    public partial class FormSeleccionarColor : Form
    {
        // Usar el último valor del orden de los colores. (27/oct/22 14.38)

        /// <summary>
        /// El orden de los colores a mostrar.
        /// </summary>
        public OrderES OrdenColores
        {
            get { return (OrderES)duSort.SelectedIndex; }
            set
            {
                duSort.SelectedIndex = (int)value;
            }
        }

        /// <summary>
        /// El color seleccionado.
        /// </summary>
        public Color ElColor
        {
            get { return wellPanel1.Color; }
            set
            { 
                wellPanel1.Color = value;
                Utilities.SetBackColor(lblBackColor, value, true);
                Utilities.SetForeColor(lblForeColor, value, true);
            }
        }

        public FormSeleccionarColor()
        {
            InitializeComponent();

            duSort.Items.AddRange(Enum.GetValues(typeof(OrderES)));
            duSort.SelectedIndex = (int)Order.Brightness;
        }

        private void FormSeleccionarColor_Load(object sender, EventArgs e)
        {
            //duSort.Items.AddRange(Enum.GetValues(typeof(OrderES)));
            ////duSort.SelectedIndex = 0;
            //duSort.SelectedIndex = (int)Order.Brightness;

            //Utilities.SetBackColor(lblBackColor, wellPanel1.Color, true);
            //Utilities.SetForeColor(lblForeColor, wellPanel1.Color, true);
        }

        private void duSort_SelectedItemChanged(object sender, EventArgs e)
        {
            wellPanel1.SortOrder = (Order)duSort.SelectedItem;
        }

        private void wellPanel1_ColorChanged(object sender, ColorChangedEventArgs e)
        {
            Utilities.SetBackColor(lblBackColor, e.Color, true);
            Utilities.SetForeColor(lblForeColor, e.Color, true);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
