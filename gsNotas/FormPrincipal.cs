//-----------------------------------------------------------------------------
// FormNotaUC                                                       (05/Dic/20)
// Formulario para manejar las notas
//
// Usando .NET 6                                                    (19/oct/22)
// Le cambio el nombre a Form2/FormPrincipal                        (20/oct/22)
//  El diseño lo he hecho desde cero basándome en el de gsNotasNETF.
//
// (c) Guillermo Som (elGuille), 2020-2022
//-----------------------------------------------------------------------------
/* Versiones y cambios
Las revisiones anteriores al 19/oct/22 17.38 verlas en el proyecto gsNotasNETF.
    E:\gsCodigo_00\Visual Studio\gsNotasNETF\gsNotasNETF

v2.0.0.0    19-oct-22   Primera versión para .NET 6.
                        A ver si logro que encaje el diseño (y que el diseñador funcione)
            20-oct-22   Todo el diseño finalizado.
                        Creo el repositorio en GitHub (19.30)
                        Añado el paquete Google.Apis.Keep.v1 v1.57.0.2637
                        Añado referencia al proyecto Seleccionar Colores. (23.24)
v2.0.0.1    21-oct-22   Empezar a usar los colores (serializados).
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Seleccionar_Colores;

namespace gsNotas
{
    public partial class FormPrincipal : Form
    {
        /// <summary>
        /// Si se quiere o no mostrar el aviso en caso de error al escribir en el registro.
        /// </summary>
        private bool mostrarAvisoReg { get; set; } = false;

        /// <summary>
        /// Acceso a los datos de configuración.
        /// </summary>
        private Properties.Settings MySettings = Properties.Settings.Default;

        /// <summary>
        /// Para controlar que no re-entre en un método de evento.
        /// </summary>
        private bool iniciando = true;
        /// <summary>
        /// Colección con las notas del grupo seleccionado. A mostrar en la ficha Notas.
        /// </summary>
        private List<Label> CtrlNotas { get; set; } = new List<Label>();
        /// <summary>
        /// Colección con los grupos mostrados en la ficha de Grupos.
        /// </summary>
        private List<Label> CtrlGrupos { get; set; } = new List<Label>();
        /// <summary>
        /// El grupo seleccionado del combo.
        /// </summary>
        private string ElGrupo { get; set; }

        /// <summary>
        /// El índice del grupo seleccionado.
        /// </summary>
        private int ElGrupoIndex { get; set; }

        /// <summary>
        /// El color de grupo a usar.
        /// </summary>
        private int ColorGrupo { get; set; }

        /// <summary>
        /// Colección con los colores a mostrar en cada grupo y notas de cada grupo.
        /// </summary>
        private List<Color> ColoresGrupo;

        /// <summary>
        /// El tamaño normal de las notas y grupos en el panel.
        /// </summary>
        private Size NormalSize { get => Program.SizeNet(180, 80); } // new Size(180, 80);

        /// <summary>
        /// El tamaño de las notas si se elige mostrar en horizontal.
        /// El ancho se asignará al de NotasFlowLayoutPanel.Client.Width - 12
        /// </summary>
        private Size HorizontalSize = new Size(250, 32);  //Program.SizeNet(250, 23); // new Size(250, 23);

        /// <summary>
        /// El tamaño a usar en las notas
        /// </summary>
        private Size NotasSize { get; set; } = Program.SizeNet(180, 80); // new Size(180, 80);

        public FormPrincipal()
        {
            InitializeComponent();

            // Asignar el directorio donde se guardarán los colores. (21/oct/22 08.31)
            Colores.DirectorioConfiguracion = notaUC1.DirNotas;
            // Leer el contenido del fichero.
            Colores.Leer();

            // Ajustar los settings para cuando cambie de versión. (28/oct/22 14.34)
            // El problema es que al usar directamente Upgrade, se usan los valores de la versión anterior. (29/oct/22 17.23)
            // Comprobar la versión por si se debe hacer el upgrade, (29/oct/22 17.35)
            //  (se debe asignar ProductVersion con 3 valores ya que en realidad los settings se cambian al cambiar AssemblyVersion).
            object obj;
            try
            {
                obj = MySettings.GetPreviousVersion("Version");
            }
            catch
            {
                obj = null;
            }

            if (obj == null || obj.ToString() != Application.ProductVersion)
            {
                MySettings.Upgrade();
            }
            MySettings.Version = Application.ProductVersion;
            MySettings.Save();

            notaUC1.ColoresPredeterminados = MySettings.ColorPredeterminadoTema;

            // Poder indicar en configuración que se use aleatorio o el indicado.
            ColorGrupo = MySettings.ColorGrupo;
            OrdenColores = (WellPanel.OrderES)MySettings.OrdenColores;
            AsignarColoresGrupo();

            // Asignar los colores de los temas. (29/oct/22 16.16)
            AsignarColoresTemas();

            // Asignar los colores de copia y configuración después de leer los colores guardados. (29/oct/22 22.45)
            // No asignarlos directamente porque son valores por referencia. (30/oct/22 14.00)
            ColoresClaroCopia = Colores.CopiarColores(notaUC1.ColoresClaro);
            ColoresOscuroCopia = Colores.CopiarColores(notaUC1.ColoresOscuro);
            ColoresClaroConfig = Colores.CopiarColores(ColoresClaroCopia);
            ColoresOscuroConfig = Colores.CopiarColores(ColoresOscuroCopia);
        }

        private void AsignarColoresTemas()
        {
            bool hayQueGuardar = false;

            // Los colores del tema claro.
            string grupoColor = "Tema-Claro";
            if (Colores.ColoresGrupos.LosColores.ContainsKey(grupoColor))
            {
                var colorsHex = Colores.ColoresGrupos.LosColores[grupoColor];
                var colores = Colores.ColoresFromHex(colorsHex).ToArray();
                notaUC1.ColoresClaro = colores;
            }
            else
            {
                // Si no existe en la colección, añadir los predeterminados.
                var colorsHex = Colores.ColoresToHex(NotaUC.ColoresClaroPredeterminado.ToList());
                Colores.ColoresGrupos.LosColores.Add(grupoColor, colorsHex);
                hayQueGuardar = true;
            }

            // Los colores del tema oscuro.
            grupoColor = "Tema-Oscuro";
            if (Colores.ColoresGrupos.LosColores.ContainsKey(grupoColor))
            {
                var colorsHex = Colores.ColoresGrupos.LosColores[grupoColor];
                var colores = Colores.ColoresFromHex(colorsHex).ToArray();
                notaUC1.ColoresOscuro = colores;
            }
            else
            {
                // Si no existe en la colección, añadir los predeterminados.
                var colorsHex = Colores.ColoresToHex(NotaUC.ColoresOscuroPredeterminado.ToList());
                Colores.ColoresGrupos.LosColores.Add(grupoColor, colorsHex);
                hayQueGuardar = true;
            }
            // Guardar los colores si se han asignado.
            if (hayQueGuardar)
            {
                Colores.Guardar();
            }

            notaUC1.AsignarTema(notaUC1.Tema);
        }

        /// <summary>
        /// Asignar los colores del grupo según el valor de ColorGrupo (de la configuración).
        /// </summary>
        private void AsignarColoresGrupo()
        {
            int elColorGrupo = ColorGrupo;

            // Si es aleatorio, usar siempre colores aletorios. (27/oct/22 15.49)
            //if (ColorGrupo < 1)
            //{
            //    Random rnd = new Random();
            //    // Un valor aleatorio entre 1 y 4 (inclusive)
            //    elColorGrupo = rnd.Next(1, 5);
            //}

            ColoresGrupo = ElColorGrupo(elColorGrupo);
        }

        /// <summary>
        /// Devuelve el color del grupo con un número mínimo de colores.
        /// </summary>
        /// <param name="elColorGrupo">El grupo de colores a asignar.</param>
        /// <param name="cuantosColores">Valor predeterminado 15 (para 16 colores).</param>
        private List<Color> ElColorGrupo(int elColorGrupo, int cuantosColores = 15)
        {
            List<Color> colores = null;
            List<string> colorsHex = null;
            bool asignar = false;
            string grupoColor = $"Grupo-{elColorGrupo:00}";

            // Si el color del grupo es 0 (aleatorio) no usar los colores guardados. (27/oct/22 15.44)
            if (elColorGrupo < 1)
            {
                colores = null;
            }
            else
            {
                // Si existe ese grupo de colores, asignarlo. (21/oct/22 08.25)
                if (Colores.ColoresGrupos.LosColores.ContainsKey(grupoColor))
                {
                    // Guardar los colores en formato hexadecimal. (25/oct/22)
                    //colores = Colores.ColoresGrupos.LosColores[grupoColor];
                    colorsHex = Colores.ColoresGrupos.LosColores[grupoColor];
                    colores = Colores.ColoresFromHex(colorsHex);

                    // Comprobar si todos los colores están asignados. (21/oct/22 08.51)
                    var todosSinColor = colores.Where((c) => c.IsEmpty).Count();
                    if (todosSinColor == colores.Count)
                    {
                        asignar = true;
                        Colores.ColoresGrupos.LosColores.Remove(grupoColor);
                    }
                }
                else
                {
                    asignar = true;
                }
                // Si no están los colores asignados o estaban todos vacíos.
                if (asignar)
                {
                    if (elColorGrupo == 2)
                        colores = new List<Color>() {Color.LightSkyBlue, Color.Gold, Color.PaleGreen, Color.MistyRose, Color.LemonChiffon,
                                                     Color.FromArgb(0,99,177), Color.LightGoldenrodYellow, Color.AliceBlue, Color.LightGray, Color.LightPink };
                    else if (elColorGrupo == 3)
                        // Colores pálidos
                        colores = new List<Color>() {Color.AliceBlue, Color.LightGoldenrodYellow, Color.PaleGreen, Color.PaleTurquoise, Color.Moccasin,
                                                     Color.SeaShell, Color.Beige, Color.LightCyan, Color.LemonChiffon, Color.MistyRose };
                    else if (elColorGrupo == 4)
                        colores = new List<Color>() {Color.MistyRose, Color.LightSkyBlue, Color.LightGoldenrodYellow, Color.Gold, Color.Pink,
                                                     Color.PaleGreen, Color.LemonChiffon, Color.LightGray, Color.AliceBlue, Color.FromArgb(0,99,177) };
                    else
                        // Predeterminado (el que estaba asignado al definir ColoresGrupo.
                        colores = new List<Color>() {Color.FromArgb(0,99,177), Color.Gold, Color.PaleGreen, Color.Pink, Color.LemonChiffon,
                                                     Color.LightGray, Color.AliceBlue, Color.LightPink, Color.LightSkyBlue, Color.LightGoldenrodYellow };

                    // Si no existe, asignarlos a la colección LosColores.
                    //Colores.ColoresGrupos.LosColores.Add(grupoColor, colores);
                    // Guardar los colores en formato hexadecimal. (25/oct/22)
                    colorsHex = Colores.ColoresToHex(colores);
                    Colores.ColoresGrupos.LosColores.Add(grupoColor, colorsHex);
                }
            }
            // Comprobación por si no hay colores asignados.
            if (colores == null)
            {
                colores = new();
            }
            // Añadir el resto de colores según se indica en cuantosColores.
            for (int i = colores.Count; i < cuantosColores; i++)
            {
                Colores.AñadirNuevoColor(colores);
            }

            // Solo guardar los colores si no era el aleatorio. (27/oct/22 15.49)
            if (elColorGrupo >0)
            {
                // Asignar los nuevos colores (o puede que los mismos que había).
                // Primero quitar la clave y después asignarla con los colores.
                Colores.ColoresGrupos.LosColores.Remove(grupoColor);
                //Colores.ColoresGrupos.LosColores[grupoColor] = colores;
                // Guardar los colores en formato hexadecimal. (25/oct/22)
                colorsHex = Colores.ColoresToHex(colores);
                Colores.ColoresGrupos.LosColores[grupoColor] = colorsHex;
                Colores.Guardar(Colores.ColoresGrupos);
            }

            return colores;
        }

        private void FormNotasUC_Load(object sender, EventArgs e)
        {
            // Usar un icono de notificación en la barra de tarea
            notifyIcon1.Text = Application.ProductName;
            NotifyMenuRestaurar.Text = "Minimizar";
            notifyIcon1.Icon = this.Icon;
            notifyIcon1.Visible = true;

            // La comprobación de actualizar los settings lo muevo al constructor del formulario. (29/oct/22 17.40)

            var grupoTmp = MySettings.UltimoGrupo;

            HorizontalSize.Width = NotasFlowLayoutPanel.ClientSize.Width - 12;

            AsignarSettings();

            AsignarColores();

            NotasFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            GruposFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;

            NotasFlowLayoutPanel.Controls.Clear();
            GruposFlowLayoutPanel.Controls.Clear();

            lblBuscando.Text = "";
            lstResultadoBuscar.Items.Clear();

            // Hacer la copia antes de llamar a leerNotas
            notaUC1.HacerCopia();

            notaUC1.LeerNotas();

            if (MySettings.MostrarMismoGrupo)
            {
                if (notaUC1.Notas.ContainsKey(grupoTmp))
                {
                    notaUC1.ComboGrupos.Text = grupoTmp;
                }
            }

            MySettings.UltimoGrupo = grupoTmp;

            //
            // Antes estaba en AsignarSettings                      (10/Feb/21)
            // 

            if (MySettings.IniciarMinimizada)
            {
                this.WindowState = FormWindowState.Minimized;
            }

            // Usar un temporizador para ocultar al minimizar       (10/Feb/21)
            // cuando se inicia la aplicación.
            timerInicio.Interval = 300;
            timerInicio.Start();

            iniciando = false;
        }

        private void timerInicio_Tick(object sender, EventArgs e)
        {
            timerInicio.Stop();

            if (this.WindowState == FormWindowState.Minimized)
            {
                NotifyMenuRestaurar.Text = "Restaurar";
                // al minimizar, ocultar el formulario
                this.Hide();
            }
        }

        /// <summary>
        /// Solo asignar true cuando se cierre desde el menú cerrar.
        /// Ya que al cerrar desde la X del formulario puede que queramos minimizar.
        /// </summary>
        private bool NoCerrar = true;

        private void FormNotasUC_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (notaUC1.Modificado)
            {
                if (MessageBox.Show("Los datos están modificados y no se han guardado." + "\n\r" +
                                    "¿Quieres guardarlos?",
                                    "Datos modificados",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    notaUC1.GuardarNotas();
                }
            }
            
            // Que el valor de MySetting.Tema siempre tenga Claro u Oscuro. (28/oct/22 13.46)
            MySettings.Tema = notaUC1.Tema == (int)Temas.Claro ? "Claro" : "Oscuro";
            //if (notaUC1.Tema == Temas.Claro || notaUC1.Tema == Temas.Light)
            //    MySetting.Tema = "Claro";
            //else
            //    MySetting.Tema = "Oscuro";

            // No hace falta asignarlos, tienen los mismos valores
            if (this.WindowState == FormWindowState.Normal)
            {
                MySettings.Left = this.Left;
                MySettings.Top = this.Top;

                // Da igual si está expandido o no, el tamaño es el mismo. (20/oct/22 11.59)
                //if (!OcultarPanelExpanded)
                //{
                //    MySetting.Height = this.Height;
                //    MySetting.Width = this.Width;
                //}
                MySettings.Height = this.Height;
                MySettings.Width = this.Width;
            }
            MySettings.SiempreEncima = notaUC1.SiempreEncima;

            // Guardar los colores. (21/oct/22 08.37)
            Colores.Guardar(Colores.ColoresGrupos);
            MySettings.Save();

            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Solo minimizar si así se indica en MinimizarAlCerrar
                // y no se está cerrando desde el menú cerrar.
                if (NoCerrar && MySettings.MinimizarAlCerrar)
                {
                    // no cerrar, sólo minimizar
                    e.Cancel = true;
                    WindowState = FormWindowState.Minimized;
                }
            }
            else
            {
                try { notifyIcon1.Visible = false; }
                catch { }
            }
        }

        private void FormNotasUC_Resize(object sender, EventArgs e)
        {
            if (iniciando) return;

            if (this.WindowState == FormWindowState.Normal)
            {
                NotifyMenuRestaurar.Text = "Minimizar";
                MySettings.Left = this.Left;
                MySettings.Top = this.Top;

                // Da igual si está expandido o no, el tamaño es el mismo. (20/oct/22 11.59)
                //if (!OcultarPanelExpanded)
                //{
                //    MySetting.Width = this.Width;
                //    MySetting.Height = this.Height;

                //    TabsConfigHeightNormal = tabsConfig.Height;
                //}
                MySettings.Width = this.Width;
                MySettings.Height = this.Height;

                TamApp = (MySettings.Left, MySettings.Top, MySettings.Width, MySettings.Height);

                AsignarAnchors();
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                NotifyMenuRestaurar.Text = "Restaurar";
                // al minimizar, ocultar el formulario
                this.Hide();
            }
        }

        private void FormNotasUC_LocationChanged(object sender, EventArgs e)
        {
            if (iniciando) return;

            if (WindowState == FormWindowState.Normal)
            {
                MySettings.Left = this.Left;
                MySettings.Top = this.Top;
                TamApp.Left = this.Left;
                TamApp.Top = this.Top;
            }
        }

        private void notaUC1_GrupoCambiado(string grupo, int index)
        {
            // al cambiar de grupo crear las notas
            ElGrupo = grupo;
            ElGrupoIndex = index;

            MostrarGrupos(ElGrupo, ElGrupoIndex);

            for (var i = 0; i < CtrlGrupos.Count; i++)
            {
                if (i == index)
                {
                    AsignarValores(CtrlGrupos[i], true, esNota: false);
                }
                else
                {
                    AsignarValores(CtrlGrupos[i], false, esNota: false);
                }
            }
            if (notaUC1.ComboGrupos.Text != ElGrupo)
            {
                notaUC1.ComboGrupos.Text = ElGrupo;
            }

            // mostrar la primera nota
            MostrarNotas(grupo, 0);

            MySettings.UltimoGrupo = ElGrupo;
        }

        private void notaUC1_NotaCambiada(string nota, int index)
        {
            MostrarGrupos(ElGrupo, ElGrupoIndex);
            MostrarNotas(ElGrupo, ElGrupoIndex);

            for (var i = 0; i < CtrlNotas.Count; i++)
            {
                if (i == index)
                {
                    AsignarValores(CtrlNotas[i], true, true);
                }
                else
                {
                    AsignarValores(CtrlNotas[i], false, true);
                }
            }
        }

        /// <summary>
        /// Asignar los valores a la etiqueta
        /// </summary>
        /// <param name="lbl">La etiqueta de la nota o grupo.</param>
        /// <param name="esSeleccionado">Si está seleccionada.</param>
        /// <param name="esNota">True si es nota, false si es grupo.</param>
        private void AsignarValores(Label lbl, bool esSeleccionado, bool esNota)
        {
            // Si está seleccionada.
            if (esSeleccionado)
            {
                lbl.FlatStyle = FlatStyle.Popup;
                lbl.BorderStyle = BorderStyle.Fixed3D;
                lbl.Padding = new Padding(0);
                if (esNota)
                {
                    lbl.Font = new Font(LblNota.Font, FontStyle.Bold);
                    
                    // Asignar el tamaño grande de la nota seleccionada
                    // lo dejo al doble de ancho cuando no se muestra horizontal
                    if (MySettings.VistaNotasHorizontal)
                    {
                        lbl.Width = NotasSize.Width;
                    }
                    else
                    {
                        lbl.Width = NormalSize.Width * 2;
                    }
                }
                else
                {
                    lbl.Font = new Font(LblGrupo.Font, FontStyle.Bold);
                    lbl.Size = NormalSize;
                }
            }
            else
            {
                lbl.FlatStyle = FlatStyle.Flat;
                lbl.BorderStyle = BorderStyle.None;
                lbl.Padding = new Padding(2);
                if (esNota)
                {
                    lbl.Font = new Font(LblNota.Font, FontStyle.Regular);
                    lbl.Size = NotasSize;
                }
                else
                {
                    lbl.Font = new Font(LblGrupo.Font, FontStyle.Regular);
                    lbl.Size = NormalSize;
                }
            }
        }

        /// <summary>
        /// Mostrar las notas en el flowPanel.
        /// </summary>
        /// <param name="grupo">El grupo a mostrar.</param>
        /// <param name="index">Indica la nota a marcar como seleccionada.</param>
        private void MostrarNotas(string grupo, int index)
        {
            //Color col = AsignarColoresGrupos();

            CtrlNotas.Clear();
            NotasFlowLayoutPanel.Controls.Clear();

            // Esto se dará cuando se cree un nuevo grupo
            if (ElGrupoIndex < 0)
                return;

            if (!notaUC1.Notas.ContainsKey(grupo))
                return;

            // Poner el color a cada grupo
            // Asegurarse que todos los grupos tienen color asignado.
            _ = AsignarColoresGrupos();

            // Asignar a las notas el color del grupo.
            Color col = ColoresGrupo[ElGrupoIndex];
            for (var j = 0; j < notaUC1.Notas[grupo].Count; j++)
            {
                var n = notaUC1.Notas[grupo][j];
                var lbl = CrearNota(n, col, true);
                lbl.Click += LblNota_Click;
                lbl.DoubleClick += LblNota_DoubleClick;
                lbl.ContextMenuStrip = contextEditarNota;
                CtrlNotas.Add(lbl);
                NotasFlowLayoutPanel.Controls.Add(lbl);
                lbl.Tag = j;
                if (j == index)
                {
                    AsignarValores(lbl, true, true);
                }
            }
        }

        private bool esEnMenu = false;

        private void contextEditarNota_Opening(object sender, CancelEventArgs e)
        {
            esEnMenu = true;
        }

        private void mnu_EditarEnVentana(object sender, EventArgs e)
        {
            var mnu = sender as ToolStripMenuItem;
            if (mnu == null) return;

            esEnMenu = true;

            int index = notaUC1.ComboNotas.SelectedIndex;
            Label lbl = contextEditarNota.SourceControl as Label;
            if (lbl == null) return;

            index = (int)lbl.Tag;

            FormEditarNotaUC frmEditUC;
            frmEditUC = new FormEditarNotaUC(notaUC1, lbl.Text, index);
            frmEditUC.NotaReemplazada += NotaUC1_NotaReemplazadaAlEditarEnVentana;
            frmEditUC.FormBorderStyle = FormBorderStyle.None;

            frmEditUC.Show();

            // Al mostrar la nota, ponerla encima y un poco a la derecha para que se vea.
            if (this.TopMost)
            {
                frmEditUC.Left = this.Left + 40;
            }
            frmEditUC.BringToFront();
            esEnMenu = false;
        }

        /// <summary>
        /// Mostrar los grupos en el flowPanel
        /// </summary>
        /// <param name="grupo"></param>
        /// <param name="index"></param>
        private void MostrarGrupos(string grupo, int index)
        {
            //Color col = AsignarColoresGrupos();

            CtrlGrupos.Clear();
            GruposFlowLayoutPanel.Controls.Clear();

            // Esto se dará cuando se cree un nuevo grupo
            if (ElGrupoIndex < 0)
                return;

            // Poner color aleatorio a cada grupo
            // Asegurarse que todos los grupos tienen color asignado.
            _ = AsignarColoresGrupos();

            int ind = 0;
            foreach (var n in notaUC1.Notas.Keys)
            {
                // Asignar a cada grupo el color que le corresponde.
                Color col = ColoresGrupo[ind];
                var s = $"{n}\n\rGrupo con {notaUC1.Notas[n].Count} notas.";
                var lbl = CrearNota(s, col, false);
                lbl.Click += LblGrupo_Click;
                //lbl.DoubleClick += LblGrupo_DoubleClick;
                CtrlGrupos.Add(lbl);
                GruposFlowLayoutPanel.Controls.Add(lbl);
                lbl.Tag = ind;
                if (ind == index)
                {
                    AsignarValores(lbl, true, false);
                }
                ind++;
            }
        }

        /// <summary>
        /// Asignar colores para que haya para todos los grupos de notas.
        /// </summary>
        /// <remarks>Solo se añadirán nuevos colores aleatorios si hay más grupos que colores asignados.</remarks>
        /// <returns>El último color asignado.</returns>
        private Color AsignarColoresGrupos()
        {
            Color col = Colores.GetRandomColor();
            var rnd = new Random();

            // No es necesario hacer la comprobación de si no hay datos de colores. (18/oct/22 17.11)
            // Esto hará que se asignen colores a todos los grupos de notas.
            for (var j = ColoresGrupo.Count; j < notaUC1.Notas.Keys.Count; j++)
            {
                var n = rnd.Next(1, 4);
                byte r = col.R, g = col.G, b = col.B;
                if (n == 1)
                    r = 0;
                else if (n == 2)
                    g = 0;
                else if (n == 3)
                    b = 0;
                Color col2;
                do
                {
                    col2 = Colores.GetRandomColor(r, g, b);
                } while (col.Equals(col2));
                col = col2;

                ColoresGrupo.Add(col);
            }
            return col;
        }

        private Label CrearNota(string nota, Color col, bool esNota)
        {
            var lbl = new Label();

            AsignarValores(lbl, false, esNota);
            // Los valores fijos
            lbl.Margin = new Padding(3);
            Colores.SetBackColor(lbl, col);
            lbl.Text = nota;

            return lbl;
        }

        private void LblNota_Click(object sender, EventArgs e)
        {
            if (esEnMenu) return;

            var lbl = sender as Label;
            if (lbl is null)
                return;

            // Si tiene este borde es que está seleccionada. (19/oct/22 14.35)
            // Salir para que al hacer doble-clic reaccione antes.
            if (lbl.BorderStyle == BorderStyle.Fixed3D) return;

            foreach (var l in CtrlNotas)
            {
                AsignarValores(l, false, true);
            }

            AsignarValores(lbl, true, true);

            if (!(lbl.Tag is null))
            {
                notaUC1.Seleccionar((int)lbl.Tag, true);
                lbl.Click -= LblNota_Click;
                lbl.DoubleClick -= LblNota_DoubleClick;
            }
        }

        private void LblNota_DoubleClick(object sender, EventArgs e)
        {
            FormEditarNotaUC frmEditUC;
            frmEditUC = new FormEditarNotaUC(notaUC1, notaUC1.ComboNotas.Text, notaUC1.ComboNotas.SelectedIndex);
            frmEditUC.NotaReemplazada += NotaUC1_NotaReemplazadaAlEditarEnVentana;

            frmEditUC.Show();

            // Al mostrar la nota, ponerla encima y un poco a la izquierda para que se vea.
            if (this.TopMost)
            {
                frmEditUC.Left = this.Left + 40;
            }
            frmEditUC.BringToFront();
        }

        private void NotaUC1_NotaReemplazadaAlEditarEnVentana(string grupo, string texto, int index)
        {
            // Se ha remplazado desde el editor externo
            notaUC1.AsignarNota(grupo, texto, index);
        }

        private void LblGrupo_Click(object sender, EventArgs e)
        {
            foreach (var l in CtrlNotas)
            {
                AsignarValores(l, false, false);
            }
            var lbl = sender as Label;
            if (lbl is null)
                return;

            AsignarValores(lbl, true, false);

            if (!(lbl.Tag is null))
                notaUC1.Seleccionar((int)lbl.Tag, false);

            // Activar la ficha de notas
            tabsConfig.SelectedIndex = 0;

            notaUC1.Seleccionar(0, true);
        }

        private void notaUC1_NotaReemplazada(string grupo, string texto, int index)
        {
            // Se ha reemplazado en esta ventana
            notaUC1.Seleccionar(index, true);
        }

        private void notaUC1_TemaCambiado(Temas tema)
        {
            AsignarColores();
            // Si está mostrada la pestaña de opciones, colorear los grupos. (19/oct/22 11.29)
            if (tabsConfig.SelectedTab.Name == "tabOpciones")
            {
                OpcCboColorGrupo_SelectedIndexChanged(null, null);
            }
        }

        /// <summary>
        /// Asigna los colores de los controles según el tema.
        /// En realidad según los valores de BackColor y ForeColor del control NotasUC.
        /// </summary>
        private void AsignarColores()
        {
            MySettings.InvertirColores = notaUC1.InvertirColores;

            // Asignar el tema. (19/oct/22 05.53)
            // Que el valor de MySetting.Tema siempre tenga Claro u Oscuro. (28/oct/22 13.46)
            MySettings.Tema = notaUC1.Tema == (int)Temas.Claro ? "Claro" : "Oscuro";
            //MySetting.Tema = notaUC1.Tema.ToString();

            this.BackColor = notaUC1.BackColor;
            this.ForeColor = notaUC1.ForeColor;
            AsignarColores(tabsConfig, MySettings.InvertirColores);

            notaUC1.AsignarTema(notaUC1.Tema);

            // Esta forma de asignación múltiple de un valor me gusta :-)
            // Los colores invertidos de las etiquetas.
            lblEdInfo.BackColor = lblResultadoBuscar.BackColor = lblBuscando.BackColor = btnBuscar.BackColor = btnClasificarGrupos.BackColor = btnBorrar.BackColor = btnCambiarNombre.BackColor = btnMoverNota.BackColor = notaUC1.ForeColor;
            lblEdInfo.ForeColor = lblResultadoBuscar.ForeColor = lblBuscando.ForeColor = btnBuscar.ForeColor = btnClasificarGrupos.ForeColor = btnBorrar.ForeColor = btnCambiarNombre.ForeColor = btnMoverNota.ForeColor = notaUC1.BackColor;
        }

        /// <summary>
        /// Asignar recursivamente los colores. Se asignan los colores de NotaUC.
        /// </summary>
        /// <param name="ctr">El control y controles contenidos al que aplicar los colores.</param>
        /// <param name="invertir">Si se debe invertir la asignación de BackColor y ForeColor.</param>
        /// <remarks>Los botones tienen los colores invertidos.</remarks>
        private void AsignarColores(Control ctr, bool invertir = false)
        {
            if (invertir)
            {
                // aquí no llega nunca cuando es linkLabel
                if (ctr is LinkLabel)
                {
                    var lnk = ctr as LinkLabel;
                    lnk.LinkColor = Color.Blue;
                    lnk.VisitedLinkColor = Color.FromArgb(0, 0, 177);
                    lnk.ActiveLinkColor = Color.Red;

                }
                else
                {
                    ctr.ForeColor = notaUC1.BackColor;
                    ctr.BackColor = notaUC1.ForeColor;
                }
            }
            else
            {
                ctr.BackColor = notaUC1.BackColor;
                ctr.ForeColor = notaUC1.ForeColor;

                if (ctr is LinkLabel)
                {
                    var lnk = ctr as LinkLabel;
                    lnk.LinkColor = notaUC1.ForeColor;
                    lnk.VisitedLinkColor = lnk.LinkColor;
                    lnk.ActiveLinkColor = lnk.LinkColor;
                }
            }

            if (ctr.Controls is null) return;

            // no cambiar los colores del contenido de los flowLayout
            if (ctr.Name.IndexOf("FlowLayout") > -1)
                return;

            foreach (Control c in ctr.Controls)
            {
                // si es un botón, asignar los colores invertidos.
                AsignarColores(c, c is Button);
            }
        }

        /// <summary>
        /// Asigna los valores de la configuración.
        /// </summary>
        private void AsignarSettings()
        {
            OpcDeshacerCambios();

            if (MySettings.VistaNotasHorizontal)
            {
                HorizontalSize.Width = NotasFlowLayoutPanel.ClientSize.Width - 12;
                NotasSize = HorizontalSize;
            }
            else
            {
                NotasSize = NormalSize;
            }

            NotasFlowLayoutPanel.WrapContents = MySettings.VistaNotasHorizontal;

            OcultarPanelSuperior(MySettings.OcultarPanelSuperior);

            IniciarConWindows = MySettings.IniciarConWindows;

            // los valores a asignar a NotaUC
            notaUC1.Tema = MySettings.Tema == "Claro" ? Temas.Claro : Temas.Oscuro;
            //if (MySetting.Tema == "Claro")
            //    notaUC1.Tema = Temas.Claro;
            //else
            //    notaUC1.Tema = Temas.Oscuro;

            notaUC1.SiempreEncima = MySettings.SiempreEncima;

            notaUC1.txtEdit.WordWrap = MySettings.AjusteLineas;
            notaUC1.AutoGuardar = MySettings.Autoguardar;
            notaUC1.NoGuardarEnBlanco = true; // MySetting.NoGuardarEnBlanco;
            notaUC1.GuardarEnDrive = false; // MySetting.GuardarEnDrive;
            notaUC1.BorrarNotasAnterioresDeDrive = false; // MySetting.BorrarNotasAnterioresDeDrive;
            notaUC1.InvertirColores = MySettings.InvertirColores;

            TamApp = TamAppOriginal;
            if (MySettings.RecordarTam)
            {
                // Si Left tiene un valor pequeño y solo hay una pantalla, ponerlo a cero.
                if (MySettings.Left < 0 && Screen.AllScreens.Length < 2)
                {
                    MySettings.Left = 0;
                }

                TamApp = (MySettings.Left, MySettings.Top, MySettings.Width, MySettings.Height);
            }
            AsignarTamañoVentana(TamApp);

            MostrarNotas(notaUC1.Grupo, notaUC1.ComboNotas.SelectedIndex);
        }

        private void notaUC1_MenuCerrar(string mensaje)
        {
            MnuNotifyCerrar_Click(null, null);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Eliminar el grupo seleccionado
            var grupo = cboEdGrupos.Text;
            if (MessageBox.Show($"¿Quieres eliminar el grupo '{grupo}' y todo su contenido?", 
                                 "Eliminar grupo", 
                                 MessageBoxButtons.YesNo, 
                                 MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            iniciando = true;
            notaUC1.Notas.Remove(grupo);
            cboEdGrupos.Items.Clear();
            foreach (var g in notaUC1.Notas.Keys)
            {
                cboEdGrupos.Items.Add(g);
            }
            iniciando = false;

            notaUC1.AsignarGrupos(false);
            tabsConfig_SelectedIndexChanged(null, null);
        }

        private void btnCambiarNombre_Click(object sender, EventArgs e)
        {
            iniciando = true;

            // Cambiar el nombre del grupo
            var grupo = cboEdGrupos.Text;
            var nuevoGrupo = txtEdNombreGrupo.Text;
            // Si ese nombre existe, se copiarán los datos y se eliminará el seleccionado
            // si no existe, se crea el grupo y se copian los datos
            // después se eliminan las notas del grupo de origen
            if (!notaUC1.Notas.ContainsKey(nuevoGrupo))
            {
                notaUC1.Notas.Add(nuevoGrupo, new List<string>());
            }

            notaUC1.Notas[nuevoGrupo].AddRange(notaUC1.Notas[grupo]);
            notaUC1.Notas.Remove(grupo);

            cboEdGrupos.Items.Clear();
            foreach (var g in notaUC1.Notas.Keys)
            {
                cboEdGrupos.Items.Add(g);
            }
            iniciando = false;
            notaUC1.AsignarGrupos(grupo: nuevoGrupo);
            tabsConfig_SelectedIndexChanged(null, null);
        }

        private void btnMoverNota_Click(object sender, EventArgs e)
        {
            iniciando = true;
            var laNota = cboEdNotas.Text;
            var index = cboEdNotas.SelectedIndex;
            var grupoDestino = cboEdGrupoDestino.Text;
            var grupo = cboEdGrupoNotas.Text;
            notaUC1.Notas[grupoDestino].Add(laNota);
            notaUC1.Notas[grupo].RemoveAt(index);
            iniciando = false;
            notaUC1.AsignarGrupos(grupo: grupoDestino);

            tabsConfig_SelectedIndexChanged(null, null);
        }

        private void tabsConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Mostrar todo el formulario cuando se pulsa en una ficha
            if (OcultarPanelExpanded)
            {
                OcultarPanelSuperior(false);
            }

            if (tabsConfig.SelectedTab.Name == "tabColores")
            {
                OpcConfigurando = true;
                // Poner los botones de deshacer y guardar en este panel.
                panelColores.Controls.Add(OpcBtnDeshacer);
                panelColores.Controls.Add(OpcBtnGuardar);

                ColoresClaroCopia = Colores.CopiarColores(notaUC1.ColoresClaro);
                ColoresOscuroCopia = Colores.CopiarColores(notaUC1.ColoresOscuro);

                if (notaUC1.Tema == Temas.Claro)
                {
                    ColorCboTemas.SelectedIndex = 0;
                }
                else
                {
                    ColorCboTemas.SelectedIndex = 1;
                }
                // Mostrar los colores de los temas.
                ColorCboTemas_SelectedIndexChanged(null, null);

                // Mostrar los colores de los grupos.
                OpcCboColorGrupo_SelectedIndexChanged(null, null);

                OpcDeshacerCambios();
                return;
            }

            if (tabsConfig.SelectedTab.Name == "tabOpciones")
            {
                OpcConfigurando = true;
                // Poner los botones de deshacer y guardar en este panel.
                panelOpciones.Controls.Add(OpcBtnDeshacer);
                panelOpciones.Controls.Add(OpcBtnGuardar);

                // Mostrar los colores de los temas (aunque no se vean).
                ColorCboTemas_SelectedIndexChanged(null, null);

                // Mostrar los colores de los grupos (aunque no se vean).
                OpcCboColorGrupo_SelectedIndexChanged(null, null);

                OpcDeshacerCambios();
                return;
            }

            if (tabsConfig.SelectedTab.Name == "tabBuscarTexto")
            {
                lblGrupoBuscar.Text = $"Grupo actual: {notaUC1.ComboGrupos.Text}";
                lblBuscando.Text = "Indica lo que quieres buscar y pulsa en 'Buscar'";
                return;
            }

            if (tabsConfig.SelectedTab.Name == "tabAcercaDe")
            {
                System.Reflection.Assembly ensamblado = typeof(VersionUtilidades).Assembly;

                var versionWeb = "xx";
                string msgVersion;

                var cualVersion = VersionUtilidades.CompararVersionWeb(ensamblado, ref versionWeb);

                if (cualVersion == 1)
                    msgVersion = $"Existe una versión más reciente de '{Application.ProductName}': v{versionWeb}.";
                else //if (cualVersion == -1)
                    msgVersion = $"Esta versión de '{Application.ProductName}' es la más reciente.";

                var titulo = $"{Application.ProductName} v{Application.ProductVersion}";
                var msg = @$"Acerca de {titulo}

Utilidad para crear notas y grupos de notas.
Las notas se guardan en el fichero '{notaUC1.FicNotas}'.

Operaciones que puedes hacer:
    Añadir nuevos grupos y notas.
    Sustituir una nota con un nuevo texto.
    Eliminar una nota.
    Clasificar las notas del grupo seleccionado.
    Leer y guardar las notas en un fichero de texto.
    Buscar texto en las notas.

Al hacer doble-clic en una nota, se muestra en ventana independiente.

No se guardan los grupos y notas en blanco.

{msgVersion}";

                txtAcercaDe.Text = msg;
                return;
            }

            //if (tabOpciones.SelectedIndex != 2) return;
            if (tabsConfig.SelectedTab.Name != "tabEditarGrupos") return;

            if (!notaUC1.Notas.Keys.Any()) return;

            // Llenar los grupos
            iniciando = true;
            cboEdGrupoDestino.Items.Clear();
            cboEdGrupoNotas.Items.Clear();
            cboEdGrupos.Items.Clear();
            cboEdNotas.Items.Clear();
            foreach (var g in notaUC1.Notas.Keys)
            {
                cboEdGrupoDestino.Items.Add(g);
                cboEdGrupoNotas.Items.Add(g);
                cboEdGrupos.Items.Add(g);
            }
            iniciando = false;
            cboEdGrupoDestino.SelectedIndex = 0;
            cboEdGrupoNotas.SelectedIndex = 0;
            cboEdGrupos.SelectedIndex = 0;
        }

        // No estaba ligado el evento con el código. (27/oct/22 16.38)

        private void cboEdGrupoNotas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iniciando) return;

            iniciando = true;

            cboEdNotas.Items.Clear();
            var g = cboEdGrupoNotas.SelectedItem.ToString();
            for (var i = 0; i < notaUC1.Notas[g].Count; i++)
                cboEdNotas.Items.Add(notaUC1.Notas[g][i]);

            iniciando = false;

            if (cboEdNotas.Items.Count > 0)
                cboEdNotas.SelectedIndex = 0;
        }

        private void btnClasificarGrupos_Click(object sender, EventArgs e)
        {
            iniciando = true;

            var col = new List<string>();
            foreach (var g in notaUC1.Notas.Keys)
                col.Add(g);

            col.Sort();
            var dic = new Dictionary<string, List<string>>();
            for (var i = 0; i < col.Count; i++)
                dic.Add(col[i], notaUC1.Notas[col[i]]);

            notaUC1.Notas.Clear();
            notaUC1.Notas = dic;

            notaUC1.AsignarGrupos(false);
            iniciando = false;
            tabsConfig_SelectedIndexChanged(null, null);
        }

        private void MnuNotifyRestaurar_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                NotifyMenuRestaurar.Text = "Minimizar";
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
            }
            else
            {
                NotifyMenuRestaurar.Text = "Restaurar";
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void MnuNotifyCerrar_Click(object sender, EventArgs e)
        {
            // Indicar que se está cerrando desde las opciones de cerrar el programa
            // no desde la X del formulario.
            NoCerrar = false;
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lblBuscando.Text = "Buscando...";

            var buscar = txtBuscar.Text;
            if (string.IsNullOrWhiteSpace(buscar))
            {
                lblBuscando.Text = "Debes indicar un texto válido a buscar.";
                txtBuscar.Focus();
                return;
            }
            bool hallado = false;

            btnBuscar.Enabled = false;
            Application.DoEvents();

            var grupo = notaUC1.ComboGrupos.Text;
            lstResultadoBuscar.Items.Clear();
            if (chkBuscarEnGrupoActual.Checked)
            {
                for (var i = 0; i < notaUC1.Notas[grupo].Count; i++)
                {
                    if (notaUC1.Notas[grupo][i].IndexOf(buscar, StringComparison.InvariantCultureIgnoreCase) > -1)
                    {
                        var index = lstResultadoBuscar.Items.Add(notaUC1.Notas[grupo][i]);
                        lstResultadoBuscar.Items[index] = $"{grupo} @ {i}";
                        hallado = true;
                    }
                }
            }
            else
            {
                foreach (var g in notaUC1.Notas.Keys)
                {
                    for (var i = 0; i < notaUC1.Notas[g].Count; i++)
                    {
                        if (notaUC1.Notas[g][i].IndexOf(buscar, StringComparison.InvariantCultureIgnoreCase) > -1)
                        {
                            var index = lstResultadoBuscar.Items.Add(notaUC1.Notas[g][i]);
                            lstResultadoBuscar.Items[index] = $"{g} @ {i}";
                            hallado = true;
                        }
                    }
                }
            }
            if (!hallado)
            {
                lblBuscando.Text = "No se ha encontrado lo que se buscaba.";
            }
            else
            {
                lblBuscando.Text = $"Halladas {lstResultadoBuscar.Items.Count} coincidencias.";
            }

            btnBuscar.Enabled = true;
            Application.DoEvents();
        }

        private void lstResultadoBuscar_DoubleClick(object sender, EventArgs e)
        {
            // al hacer doble-click en el listbox, mostrar esa nota
            if (lstResultadoBuscar.Items.Count == 0)
                return;

            var s = lstResultadoBuscar.SelectedItem.ToString();
            // el formato es: grupo @ índice
            var i = s.IndexOf("@");
            if (i == -1) return;
            var grupo = s.Substring(0, i - 1);
            var index = -1;
            int.TryParse(s.Substring(i + 2), out index);
            if (index == -1) return;

            notaUC1.ComboGrupos.Text = grupo;
            notaUC1.Seleccionar(index, true);
        }

        //
        // Para las opciones de configuración (opciones y colores).
        //

        private void OpcBtnGuardar_Click(object sender, EventArgs e)
        {
            MySettings.ColorGrupo = OpcCboColorGrupo.SelectedIndex;
            ColorGrupo = MySettings.ColorGrupo;
            MySettings.Autoguardar = OpcChkAutoGuardar.Checked;
            MySettings.RecordarTam = OpcChkRecordarTam.Checked;
            MySettings.AjusteLineas = OpcChkAjusteLineas.Checked;
            // Esta opción siempre será true (para no guardar notas en blanco).
            MySettings.NoGuardarEnBlanco = true;
            //MySettings.NoGuardarEnBlanco = OpChkNoGuardarEnBlanco.Checked;
            MySettings.IniciarMinimizada = OpcChkIniciarMinimizada.Checked;
            MySettings.MinimizarAlCerrar = OpcChkMinimizarAlCerrar.Checked;
            MySettings.MostrarMismoGrupo = OpcChkMostrarMismoGrupo.Checked;
            var vistaAnt = MySettings.VistaNotasHorizontal;
            MySettings.VistaNotasHorizontal = OpcChkMostrarHorizontal.Checked;
            MySettings.OcultarPanelSuperior = OpcChkOcultarPanelSuperior.Checked;
            MySettings.IniciarConWindows = OpcChkIniciarConWindows.Checked;
            //MySettings.GuardarEnDrive = OpcChkGuardarEnDrive.Checked;
            //MySettings.BorrarNotasAnterioresDeDrive = OpcChkBorrarNotasAnterioresDrive.Checked;

            notaUC1.ColoresPredeterminados = ColorChkColoresPredeterminados.Checked;
            MySettings.ColorPredeterminadoTema = notaUC1.ColoresPredeterminados;

            // Guardar el último valor usado en el orden de los colores. (27/oct/22 14.53)
            MySettings.OrdenColores = (int)OrdenColores;

            // Asignar los colores de los temas (se hayan cambiado o no).
            notaUC1.ColoresClaro = Colores.CopiarColores(ColoresClaroConfig);
            notaUC1.ColoresOscuro = Colores.CopiarColores(ColoresOscuroConfig);
            ColoresClaroCopia = Colores.CopiarColores(ColoresClaroConfig);
            ColoresOscuroCopia = Colores.CopiarColores(ColoresOscuroConfig);

            // Guardar los colores de los temas en el fichero de colores.
            string grupoColor = "Tema-Claro";
            var colorsHex = Colores.ColoresToHex(notaUC1.ColoresClaro.ToList());
            if (Colores.ColoresGrupos.LosColores.ContainsKey(grupoColor))
            {
                Colores.ColoresGrupos.LosColores.Remove(grupoColor);
            }
            Colores.ColoresGrupos.LosColores.Add(grupoColor, colorsHex);
            
            grupoColor = "Tema-Oscuro";
            colorsHex = Colores.ColoresToHex(notaUC1.ColoresOscuro.ToList());
            if (Colores.ColoresGrupos.LosColores.ContainsKey(grupoColor))
            {
                Colores.ColoresGrupos.LosColores.Remove(grupoColor);
            }
            Colores.ColoresGrupos.LosColores.Add(grupoColor, colorsHex);

            OpcBtnDeshacer.Enabled = false;

            // Guardar los colores. (21/oct/22 08.37)
            //Colores.Guardar();
            Colores.Guardar(Colores.ColoresGrupos);
            MySettings.Save();
            OpcConfigurando = false;

            AsignarColoresGrupo();
            AsignarColores();
            // Colorear también los grupos.
            MostrarGrupos(ElGrupo, ElGrupoIndex);

            AsignarSettings();

            if (vistaAnt != MySettings.VistaNotasHorizontal)
                AplicarVista();

            // Aplicar el color del tema. (ponerlo antes de repintar los colores del tema y de los colores).
            AsignarColores();

            // Repintar los colores del tema. (29/oct/22 22.52)
            ColorCboTemas_SelectedIndexChanged(null, null);

            // Repintar los colores. (20/oct/22 16.28)
            OpcCboColorGrupo_SelectedIndexChanged(null, null);
        }

        private void OpcBtnDeshacer_Click(object sender, EventArgs e)
        {
            OpcConfigurando = false;
            OpcDeshacerCambios();
        }

        private bool OpcConfigurando = false;

        /// <summary>
        /// Deshace los cambios en el panel de configuración.
        /// Llamarla cuando se entra en la ficha de opciones (si no se está ya configurando).
        /// </summary>
        private void OpcDeshacerCambios()
        {
            if (OpcConfigurando) return;

            // Asignar el valor del color del grupo a usar (los valores van de 0 a 4)
            OpcCboColorGrupo.SelectedIndex = MySettings.ColorGrupo;
            OrdenColores = (WellPanel.OrderES)MySettings.OrdenColores;
            OpcChkAutoGuardar.Checked = MySettings.Autoguardar;
            OpcChkRecordarTam.Checked = MySettings.RecordarTam;
            OpcChkAjusteLineas.Checked = MySettings.AjusteLineas;
            // Este valor siempre será true (para no guardar notas en blanco).
            OpChkNoGuardarEnBlanco.Checked = true;
            //OpChkNoGuardarEnBlanco.Checked = MySetting.NoGuardarEnBlanco;
            OpcChkIniciarMinimizada.Checked = MySettings.IniciarMinimizada;
            OpcChkMinimizarAlCerrar.Checked = MySettings.MinimizarAlCerrar;
            OpcChkMostrarMismoGrupo.Checked = MySettings.MostrarMismoGrupo;
            OpcChkMostrarHorizontal.Checked = MySettings.VistaNotasHorizontal;
            OpcChkOcultarPanelSuperior.Checked = MySettings.OcultarPanelSuperior;
            OpcChkIniciarConWindows.Checked = MySettings.IniciarConWindows;

            // Los colores de los temas.
            ColorChkColoresPredeterminados.Checked = notaUC1.ColoresPredeterminados;
            ColoresClaroConfig = Colores.CopiarColores(ColoresClaroCopia);
            ColoresOscuroConfig = Colores.CopiarColores(ColoresOscuroCopia);

            ColorCboTemas.SelectedIndex = (int)notaUC1.Tema;
            ColorCboTemas_SelectedIndexChanged(null, null);

            OpcBtnDeshacer.Enabled = false;
        }

        /// <summary>
        /// Comprueba si se han modificado las opciones.
        /// </summary>
        /// <returns>True si se han modificado las opciones.</returns>
        private void OpcDatosModificados()
        {
            if (!OpcConfigurando) return;

            var modificado = false;

            if (OpcChkAutoGuardar.Checked != MySettings.Autoguardar)
                modificado = true;
            else if (OpcChkRecordarTam.Checked != MySettings.RecordarTam)
                modificado = true;
            else if (OpcChkAjusteLineas.Checked != MySettings.AjusteLineas)
                modificado = true;
            //else if (OpChkNoGuardarEnBlanco.Checked != MySetting.NoGuardarEnBlanco)
            //    modificado = true;
            else if (OpcChkIniciarMinimizada.Checked != MySettings.IniciarMinimizada)
                modificado = true;
            else if (OpcChkMinimizarAlCerrar.Checked != MySettings.MinimizarAlCerrar)
                modificado = true;
            else if (OpcChkMostrarMismoGrupo.Checked != MySettings.MostrarMismoGrupo)
                modificado = true;
            else if (OpcChkMostrarHorizontal.Checked != MySettings.VistaNotasHorizontal)
                modificado = true;
            else if (OpcChkOcultarPanelSuperior.Checked != MySettings.OcultarPanelSuperior)
                modificado = true;
            else if (OpcChkIniciarConWindows.Checked != MySettings.IniciarConWindows)
                modificado = true;
            else if (OpcCboColorGrupo.SelectedIndex != MySettings.ColorGrupo)
                modificado = true;
            else if (OrdenColores != (WellPanel.OrderES)MySettings.OrdenColores)
                modificado = true;

            // No tener en cuenta el tema seleccionado al cambiar los colores de los temas,
            // solo tener en cuenta los propios colores.
            //else if (ColorCboTemas.SelectedIndex != (int)notaUC1.Tema)
            //    modificado = true;

            if (ColorChkColoresPredeterminados.Checked != notaUC1.ColoresPredeterminados)
                modificado = true;

            // Por si se han cambiado los colores.
            // Solo si no se usan los predeterminados. (29/oct/22 19.58)
            if (ColorChkColoresPredeterminados.Checked == false) 
            {
                if (ColorCboTemas.SelectedIndex == 0)
                {
                    if (ColoresClaroConfig[0] != ColoresClaroCopia[0])
                        modificado = true;
                    else if (ColoresClaroConfig[1] != ColoresClaroCopia[1])
                        modificado = true;
                }
                else
                {
                    if (ColoresOscuroConfig[0] != ColoresOscuroCopia[0])
                        modificado = true;
                    if (ColoresOscuroConfig[1] != ColoresOscuroCopia[1])
                        modificado = true;
                }
            }

            // Estos valores no se guardan en la configuración.
            //else if (OpcChkGuardarEnDrive.Checked != MySetting.GuardarEnDrive)
            //    modificado = true;
            //else if (OpcChkBorrarNotasAnterioresDrive.Checked != MySetting.BorrarNotasAnterioresDeDrive)
            //    modificado = true;

            OpcBtnDeshacer.Enabled = modificado;
        }

        // Mostrar los colores de las etiquetas según el grupo seleccionado. (18/oct/22 21.30)

        private void OpcCboColorGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iniciando) return;

            flowLayoutPanel1.Controls.Clear();

            if (OpcCboColorGrupo.SelectedIndex < 0)
                OpcCboColorGrupo.SelectedIndex = 0;

            // Por si hay más grupos que los colores predeterminados.
            var colores = ElColorGrupo(OpcCboColorGrupo.SelectedIndex, notaUC1.Notas.Keys.Count);

            // Solo mostrar los colores con los grupos que hay actualmente
            for (int i = 0; i < colores.Count; i++)
            {
                var col = colores[i];
                var lbl = new Label();
                lbl.Margin = new Padding(0, 0, 3, 3);
                // El valor predeterminado de Width es 100.
                lbl.Width = 40;
                // El valor predeterminado de Height es 23.
                lbl.Height = 26;
                // Resaltar el grupo actual.
                if (i == ElGrupoIndex)
                {
                    lbl.Height = 36;
                    //lbl.BorderStyle = BorderStyle.FixedSingle;
                    lbl.BorderStyle = BorderStyle.Fixed3D;
                    lbl.Font = new Font(lbl.Font, FontStyle.Bold);
                }
                lbl.Text = i.ToString();
                lbl.BackColor = col;
                Colores.SetBackColor(lbl, col);
                // Poder cambiar el color. (25/oct/22 16.54)
                lbl.Click += Lbl_Click;
                lbl.Tag = OpcCboColorGrupo.SelectedIndex;
                flowLayoutPanel1.Controls.Add(lbl);
            }

            OpcDatosModificados();
        }

        // Para el orden de colores a mostrar. (27/oct/22 14.43)
        private WellPanel.OrderES OrdenColores = WellPanel.OrderES.Brillo;

        private void Lbl_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl == null) return;
            // Resaltar la etiqueta a la que se le cambia el color. (27/oct/22 14.56)
            var bordeAnt = lbl.BorderStyle;
            //lbl.BorderStyle= BorderStyle.Fixed3D;
            lbl.BorderStyle = BorderStyle.FixedSingle;
            var heightAnt = lbl.Height;
            lbl.Height = 36;

            var frmColor = new FormSeleccionarColor();
            frmColor.ElColor = lbl.BackColor;
            frmColor.OrdenColores = OrdenColores;
            if (frmColor.ShowDialog() == DialogResult.OK)
            {
                // Recordar el último valor usado. (27/oct/22 14.44)
                OrdenColores = frmColor.OrdenColores;

                lbl.BackColor = frmColor.ElColor;
                int grupo = (int)lbl.Tag;
                string grupoColor = $"Grupo-{grupo:00}";
                var colorsHex = Colores.ColoresGrupos.LosColores[grupoColor];
                int index = Convert.ToInt32(lbl.Text);
                string s = lbl.BackColor.ToArgb().ToString("x");
                colorsHex[index] = s;

                OpcDatosModificados();
            }
            // Reponer los valores que tenía antes.
            lbl.BorderStyle = bordeAnt;
            lbl.Height = heightAnt;
        }

        private void ColorChkColoresPredeterminados_CheckedChanged(object sender, EventArgs e)
        {
            ColorCboTemas.Enabled = !ColorChkColoresPredeterminados.Checked;
            if (ColorChkColoresPredeterminados.Checked)
            {
                lblTema.Font = new Font(lblTema.Font, FontStyle.Italic);
            }
            else
            {
                lblTema.Font = new Font(lblTema.Font, FontStyle.Regular);
            }
            ColorCboTemas_SelectedIndexChanged(null, null);

            OpcDatosModificados();
        }

        // Copia de los colores de los temas, por si se cambian en la configuración. (29/oct/22 20.30)
        private Color[] ColoresClaroCopia, ColoresClaroConfig;
        private Color[] ColoresOscuroCopia, ColoresOscuroConfig;

        // Cuando se cambia el tema en la configuración de los colores.

        private void ColorCboTemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iniciando) return;

            // El color del tema claro.
            if (ColorCboTemas.SelectedIndex == 0)
            {
                // Si se usan los colores predeterminados.
                if (ColorChkColoresPredeterminados.Checked)
                {
                    ColoresClaroConfig = Colores.CopiarColores(NotaUC.ColoresClaroPredeterminado);
                    ColoresClaroCopia = Colores.CopiarColores(ColoresClaroConfig);
                }
                lblColorFondo.BackColor = ColoresClaroConfig[0];
                lblColorTexto.BackColor = ColoresClaroConfig[1];
            }
            else
            {
                // Si se usan los colores predeterminados.
                if (ColorChkColoresPredeterminados.Checked)
                {
                    ColoresOscuroConfig = Colores.CopiarColores(NotaUC.ColoresOscuroPredeterminado);
                    ColoresOscuroCopia = Colores.CopiarColores(ColoresOscuroConfig);
                }
                lblColorFondo.BackColor = ColoresOscuroConfig[0];
                lblColorTexto.BackColor = ColoresOscuroConfig[1];
            }

            lblColorFondo.ForeColor = lblColorTexto.BackColor;
            lblColorTexto.ForeColor = lblColorFondo.BackColor;
            txtColorTema.BackColor = lblColorFondo.BackColor;
            txtColorTema.ForeColor = lblColorTexto.BackColor;
        }

        // Cambiar el color del tema seleccionado.

        private void LblTema_Click(object sender, EventArgs e)
        {
            // Nada que hacer si se usan los colores predeterminados. (29/oct/22 20.05)
            if (ColorChkColoresPredeterminados.Checked)
                return;

            Label lbl = sender as Label;
            if (lbl == null) return;
            // Resaltar la etiqueta a la que se le cambia el color. (27/oct/22 14.56)
            var bordeAnt = lbl.BorderStyle;
            lbl.BorderStyle = BorderStyle.FixedSingle;
            var heightAnt = lbl.Height;
            lbl.Height = 36;

            // Usar el cuadro de diálogo de colores del sistema. (30/oct/22 12.56)
            var colorDlg = new ColorDialog
            {
                // Permitir definir colores.
                AllowFullOpen = true,
                // Mostrar la parte de la definición personalizada de colores.
                FullOpen = true,
                // Mostrar todos los colores disponibles.
                AnyColor = true,
                // No solo los colores sólidos.
                SolidColorOnly = false,
                // El formato es en BGR
                // Añadir los colores personalizados de los temas.
                // Poner como primer color personalizado el que se modifica.
                CustomColors = new int[]
                {
                    ColorTranslator.ToOle(lbl.BackColor),
                    ColorTranslator.ToOle(Color.White),
                    ColorTranslator.ToOle(Color.FromArgb(0, 99, 177)),
                    ColorTranslator.ToOle(Color.FromArgb(30, 30, 30)),
                    ColorTranslator.ToOle(Color.FromArgb(87, 166, 58)),
                    ColorTranslator.ToOle(ColorTranslator.FromHtml("#2196F3")),
                    ColorTranslator.ToOle(ColorTranslator.FromHtml("#96d1ff")),
                    //ColorTranslator.ToOle(ColorTranslator.FromHtml("#0077df")),
                    //ColorTranslator.ToOle(ColorTranslator.FromHtml("#003391")),
                    //ColorTranslator.ToOle(ColorTranslator.FromHtml("#4A5C8C")),
                },

                Color = lbl.BackColor
            };
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                lbl.BackColor = colorDlg.Color;

                if (lbl == lblColorFondo)
                {
                    lblColorTexto.ForeColor = lbl.BackColor;
                }
                else if (lbl == lblColorTexto)
                {
                    lblColorFondo.ForeColor = lbl.BackColor;
                }
                txtColorTema.BackColor = lblColorFondo.BackColor;
                txtColorTema.ForeColor = lblColorTexto.BackColor;

                if (ColorCboTemas.SelectedIndex == 0)
                {
                    ColoresClaroConfig[0] = lblColorFondo.BackColor;
                    ColoresClaroConfig[1] = lblColorTexto.BackColor;
                }
                else
                {
                    ColoresOscuroConfig[0] = lblColorFondo.BackColor;
                    ColoresOscuroConfig[1] = lblColorTexto.BackColor;
                }

                OpcDatosModificados();
            }

            //var frmColor = new FormSeleccionarColor();
            //frmColor.ElColor = lbl.BackColor;
            //frmColor.OrdenColores = OrdenColores;
            //if (frmColor.ShowDialog() == DialogResult.OK)
            //{
            //    // Recordar el último valor usado. (27/oct/22 14.44)
            //    OrdenColores = frmColor.OrdenColores;

            //    lbl.BackColor = frmColor.ElColor;

            //    if (lbl == lblColorFondo)
            //    {
            //        lblColorTexto.ForeColor = lbl.BackColor;
            //    }
            //    else if (lbl == lblColorTexto)
            //    {
            //        lblColorFondo.ForeColor = lbl.BackColor;
            //    }
            //    txtColorTema.BackColor = lblColorFondo.BackColor;
            //    txtColorTema.ForeColor = lblColorTexto.BackColor;

            //    if (ColorCboTemas.SelectedIndex == 0)
            //    {
            //        ColoresClaroConfig[0] = lblColorFondo.BackColor;
            //        ColoresClaroConfig[1] = lblColorTexto.BackColor;
            //    }
            //    else
            //    {
            //        ColoresOscuroConfig[0] = lblColorFondo.BackColor;
            //        ColoresOscuroConfig[1] = lblColorTexto.BackColor;
            //    }

            //    OpcDatosModificados();
            //}

            // Reponer los valores que tenía antes.
            lbl.BorderStyle = bordeAnt;
            lbl.Height = heightAnt;
        }

        private void OpcBtnRestablecerTam_Click(object sender, EventArgs e)
        {
            // Restablecer el tamaño y posición de la ventana de la aplicación.
            AsignarTamañoVentana(TamAppOriginal);
        }

        /// <summary>
        /// Asignar el tamaño y posición a la ventana.
        /// </summary>
        /// <param name="nuevoTam"></param>
        private void AsignarTamañoVentana((int Left, int Top, int Width, int Height) nuevoTam)
        {
            var iniTmp = iniciando;
            iniciando = true;

            if (nuevoTam.Left == -2)
            {
                this.CenterToScreen();
                if (nuevoTam.Width != -1)
                    this.Width = nuevoTam.Width;
                if (nuevoTam.Height != -1)
                    this.Height = nuevoTam.Height;
            }
            else
            {
                if (nuevoTam.Left != -1)
                    this.Left = nuevoTam.Left;
                if (nuevoTam.Top != -1)
                    this.Top = nuevoTam.Top;
                if (nuevoTam.Width != -1)
                    this.Width = nuevoTam.Width;
                if (nuevoTam.Height != -1)
                    this.Height = nuevoTam.Height;
            }
            iniciando = iniTmp;
        }

        /// <summary>
        /// La posición y tamaño original de la ventana.
        /// </summary>
        private (int Left, int Top, int Width, int Height) TamAppOriginal = (0, 0, 1133, 1068);
        // El valor para .NET 6: (0, 0, 1133, 1068) (29/oct/22 17.48)
        // Este era el valor de la app para .NET Framework: (-2, -1, 823, 613)

        /// <summary>
        /// La posición y tamaño actual de la ventana.
        /// </summary>
        private (int Left, int Top, int Width, int Height) TamApp;

        private void Opciones_CheckedChanged(object sender, EventArgs e)
        {
            if (iniciando) return;

            //OpcConfigurando = true;
            // Esta opción siempre debe ser TRUE
            //OpChkNoGuardarEnBlanco.Checked = true;
            OpcDatosModificados();
        }

        private void NotasFlowLayoutPanel_Resize(object sender, EventArgs e)
        {
            if (OpcChkMostrarHorizontal.Checked)
            {
                HorizontalSize.Width = NotasFlowLayoutPanel.ClientSize.Width - 12;
            }
        }

        private void AplicarVista()
        {
            if (OpcChkMostrarHorizontal.Checked)
            {
                HorizontalSize.Width = NotasFlowLayoutPanel.ClientSize.Width - 12;
                NotasFlowLayoutPanel.WrapContents = false; // true;
            }
            else
                NotasFlowLayoutPanel.WrapContents = false;

            MostrarNotas(notaUC1.Grupo, notaUC1.ComboNotas.SelectedIndex);
        }

        private void OpcLinkSolicitarAutorización_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Esto da error 404. (19/oct/22 12.46)
            //System.Diagnostics.Process.Start("http://www.elguillemola.com/2020/12/te-gustaria-obtener-mas-prestaciones-de-gsnotasnet/#comments");

            // En .NET 6 da error al hacer esta llamada, mejor usar con UseShellExecute = true.
            //System.Diagnostics.Process.Start("https://www.elguillemola.com/te-gustaria-obtener-mas-prestaciones-de-gsnotasnet/#comments");
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "https://www.elguillemola.com/te-gustaria-obtener-mas-prestaciones-de-gsnotasnet/#comments";
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.WorkingDirectory = "";
            
            proc.Start();
        }

        /// <summary>
        /// Ocultar o mostrar el panel superior.
        /// </summary>
        /// <param name="ocultar">true para ocultarlo, false para mostrarlo.</param>
        private void OcultarPanelSuperior(bool ocultar)
        {
            OcultarPanelExpanded = ocultar;

            // No hay que hacer nada de tamaños, solo cambiar el porcentaje de la fila superior. (20/oct/22)
            
            if (ocultar)
            {
                picOcultarPanel1.Image = Properties.Resources.ExpanderDown;
                // Es en porcentaje.
                this.tableLayoutPanel1.RowStyles[0].Height = 3.2F;
                //this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
                //this.tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            }
            else
            {
                picOcultarPanel1.Image = Properties.Resources.ExpanderUp;
                this.tableLayoutPanel1.RowStyles[0].Height = 40;
                //this.tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            }
            //this.tableLayoutPanel1.RowStyles[1].Height = 100 - this.tableLayoutPanel1.RowStyles[0].Height;
            //this.tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
        }

        private bool OcultarPanelExpanded = false;

        private void picOcultarPanel1_Click(object sender, EventArgs e)
        {
            if (iniciando) return;

            OcultarPanelSuperior(!OcultarPanelExpanded);
        }

        private void OpcBtnGuardarEnDrive_Click(object sender, EventArgs e)
        {
            notaUC1.GuardarEnDrive = true;
            notaUC1.BorrarNotasAnterioresDeDrive = false;
            notaUC1.GuardarNotas();

            notaUC1.GuardarEnDrive = false;
        }

        /// <summary>
        /// Indicar si se inicia con Windows. Modificando el registro de Windows.
        /// </summary>
        /// <remarks>Debes ejecutar la aplicación con permisos de administrador.</remarks>
        private bool IniciarConWindows
        {
            get
            {
                return MySettings.IniciarConWindows;
            }
            set
            {
                //// Avisar si no se ejecuta como administrador            (12/Ago/07)
                //if (DatosConfiguracion.ComoAdministrador == false)
                //{
                //    MessageBox.Show("Sólo puedes modificar el registro de Windows " + "si ejecutas la aplicación como administrador.", "Iniciar con Windows", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    break;
                //}

                MySettings.IniciarConWindows = value;

                // Guardar la clave en el registro
                // HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
                try
                {
                    Microsoft.Win32.RegistryKey runK = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    if (value)
                    {
                        // El ensamblado actual
                        System.Reflection.Assembly ensamblado = System.Reflection.Assembly.GetExecutingAssembly(); //typeof(FormNotasUC).Assembly;
                        // añadirlo al registro
                        var appPath = System.IO.Path.GetFullPath(ensamblado.Location);

                        runK.SetValue("gsNotas", $"\"{appPath}\"");
                    }
                    else
                        // quitarlo del registo
                        runK.DeleteValue("gsNotas", false);
                }
                catch (Exception ex)
                {
                    if (mostrarAvisoReg)
                    {
                        MessageBox.Show("ERROR al guardar en el registro.\r\n" +
                            "Seguramente no tienes privilegios suficientes.\r\n" +
                            ex.Message + "\r\n---xxx---\r\n" +
                            ex.StackTrace,
                            "Iniciar automáticamente con Windows",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        notaUC1.StatusInfo = "Error al guardar en el registro.";
                    }
                }
            }
        }

        private void notaUC1_BuscarTexto(string mensaje)
        {
            // Se ha elegido buscar en el menú contextual
            // en mensaje estará el texto seleccionado 
            txtBuscar.Text = mensaje;
            tabsConfig.SelectedTab = tabBuscarTexto;
            tabBuscarTexto.Show();
        }

        private void btnCrearGrupo_Click(object sender, EventArgs e)
        {
            var nuevoGrupo = txtEdNuevoNombreGrupo.Text;
            // Si ese nombre existe no se hace nada, si no existe, se crea vacío.
            if (!notaUC1.Notas.ContainsKey(nuevoGrupo))
                notaUC1.Notas.Add(nuevoGrupo, new List<string>());

            notaUC1.AsignarGrupos(grupo: nuevoGrupo);
            tabsConfig_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// Asignar los anchors de los controles de las opciones, porque no siempre lo hace correctamente (al menos con .NET Framework).
        /// </summary>
        private void AsignarAnchors()
        {
            lblResultadoBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lstResultadoBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            lblBuscando.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            OpcBtnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            OpcBtnDeshacer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            OpcBtnRestablecerTam.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            OpcLinkSolicitarAutorización.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            OpcBtnGuardarEnDrive.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
            
            btnCambiarNombre.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtEdNombreGrupo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblEdCambiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblEdInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
            cboEdGrupoDestino.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMoverNota.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cboEdNotas.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
        }
    }
}
