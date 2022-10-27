//-----------------------------------------------------------------------------
// NotaUC                                                           (05/Dic/20)
// Editor de notas (y grupos)
// Unifico CabeceraNotaUC y NotaUC en este fichero                  (09/Dic/20)
//
// 18-oct-22:   Pruebo a lanzar los evento de MouseDown, MouseMove y MouseUp.
//              Nuevos directorios para las notas y copias de seguridad.
//              En lugar del directorio de documentos se usa el indicado en %LOCALAPPDATA%.
//              Usar colores predeterminados para los temas.
// 19-oct-22:   Importar notas (deben estar en el formato NotasUC).
//              Añado el evento TemaCambiado (como CambioDeTema).
//              Usar Event Properties para manejar los eventos.
//              ToolTip en importar notas.
//              Menú Siempre encima (TopMost), el control de usuario no tiene la propiedad TopMost, asignarlo al ParentForm.
//
// (c) Guillermo Som (elGuille), 2020-2022
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

using System.IO;
using System.Diagnostics;

namespace gsNotas
{
    /// <summary>
    /// Control para manejar los grupos y notas.
    /// </summary>
    public partial class NotaUC : UserControl
    {

        private bool iniciando = true;

        /// <summary>
        /// La versión del fichero no la de Application.ProductVersion
        /// </summary>
        private string FileVersion { get; init; }

        ///// <summary>
        ///// La carpeta de documentos.
        ///// </summary>
        //private string _dirDocumentos { get; set; }

        /// <summary>
        /// La carpeta local para los datos.
        /// </summary>
        private string _dirLocal { get; set; }

        /// <summary>
        /// El directorio de las notas. Será en Documentos\gsNotas.
        /// </summary>
        /// <remarks>Antes era el directorio del backup.</remarks>
        private string _dirNotas { get; set; }

        /// <summary>
        /// El fichero anterior de notas.
        /// </summary>
        private string _ficNotasAnt { get; set; }

        /// <summary>
        /// El directorio para el backup de las notas. Será en Documentos\gsNotas\Backup.
        /// </summary>
        /// <remarks>Estará dentro del directorio de Notas</remarks>
        private string _dirNotasBackup { get; set; }

        /// <summary>
        /// El nombre del fichero con las notas sin el path.
        /// </summary>
        private string _ficNotasSinPath { get; set; }

        private string NombreProducto { get; init; }
        private string VersionProducto { get; init; }

        public NotaUC()
        {
            InitializeComponent();

            // Interceptar los eventos del ratón. (18/oct/22)
            // No interceptar los del control, solo los de la etiqueta del título.
            //MouseDown += NotaUC_MouseDown;
            //MouseMove += NotaUC_MouseMove;
            //MouseUp += NotaUC_MouseUp;
            // Estos de la etiqueta LabelTitulo los quito del diseñador, para que quede claro que están definidos.
            this.LabelTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NotaUC_MouseDown);
            this.LabelTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.NotaUC_MouseMove);
            this.LabelTitulo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NotaUC_MouseUp);

            // El directorio de documentos (es posible que no siempre esté accesible)
            // Usado para acceder al fichero de versiones anteriores.
            var _dirDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Usar como directorio de datos uno compatible con la aplicación móvil. (18/oct/22 16.37)
            _dirLocal = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            // El directorio de las notas (para que no esté directamente en documentos). (18/oct/22)
            //_dirNotas = Path.Combine(DirDocumentos, "gsNotasNETF");
            _dirNotas = Path.Combine(_dirLocal, "gsNotas");
            if (!Directory.Exists(_dirNotas))
            {
                try
                {
                    Directory.CreateDirectory(_dirNotas);
                }
                catch
                {
                    statusInfo.Text = "Error al crear el directorio de las notas.";
                }
            }
            // El directorio para el backup de las notas. (18/oct/22)
            _dirNotasBackup = Path.Combine(_dirNotas, "Backup");
            if (!Directory.Exists(_dirNotasBackup))
            {
                try
                {
                    Directory.CreateDirectory(_dirNotasBackup);
                }
                catch
                {
                    statusInfo.Text = "Error al crear el directorio de Backup.";
                }
            }

            gsGoogleDriveDocsAPINET.ApisDriveDocs.IniciadoGuardarNotasEnDrive += ApisDriveDocs_IniciadoGuardarNotasEnDrive;
            gsGoogleDriveDocsAPINET.ApisDriveDocs.FinalizadoGuardarNotasEnDrive += ApisDriveDocs_FinalizadoGuardarNotasEnDrive;
            gsGoogleDriveDocsAPINET.ApisDriveDocs.GuardandoNotas += ApisDriveDocs_GuardandoNotas;

            CboGrupos.Text = "";
            CboNotas.Text = "";
            LabelTitulo.Text = "";
            BackColor = Color.White;
            ForeColor = Color.FromArgb(0, 99, 177);

            var ensamblado = System.Reflection.Assembly.GetCallingAssembly();
            var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(ensamblado.Location);
            FileVersion = fvi.FileVersion;
            NombreProducto = fvi.ProductName;
            VersionProducto = fvi.ProductVersion;

            _ficNotasSinPath = $"{fvi.ProductName}.notasUC.txt";

            // Las notas ahora se guardan en el directorio de notas. (18/oct/22 14.20)
            //FicNotas = Path.Combine(DirDocumentos, _ficNotasSinPath);
            FicNotas = Path.Combine(DirNotas, _ficNotasSinPath);
            // El fichero según la ruta usada hasta la versión 1.0.0.50. (18/oct/22 14.32)
            _ficNotasAnt = Path.Combine(_dirDocumentos, _ficNotasSinPath);

            LongitudTituloNota = LongitudTituloNotaDefault;

            // Esto estaba en el evento Load
            const int m_AñoActual = 2022;

            var elGuille = "©Guillermo Som (elGuille), 2020";
            if (DateTime.Now.Year > m_AñoActual)
            {
                elGuille += $"-{DateTime.Now.Year}";
            }
            else
            {
                elGuille += $"-{m_AñoActual}";
            }
            elGuille += $" - {NombreProducto} v{VersionProducto} ({FileVersion})";

            statusInfo.Text = elGuille;
            statusInfoTecla.Text = "Menú";
            statusInfoPos.Enabled = false;
            statusInfoPos.Text = "L: 0 , C: 0";
            txtEdit.Text = "";
            TextoModificado = false;
            // Esta propiedad no aparece en la ventana de propiedades
            // ni los eventos relacionados
            txtEdit.AllowDrop = true;
            txtEdit.DragEnter += NotaUC_DragEnter;
            txtEdit.DragDrop += NotaUC_DragDrop;

            // Llamar a Hacer la copia desde la aplicación que usa este control
            // si no, habrá dos copias... una sin notas y otra con notas
            //HacerCopia();

            iniciando = false;
        }

        // Los eventos para MouseDown, MouseMove y MouseUp. (18/oct/22 12.30)
        // No hace falta definirlos, solo usarlos.
        // Estos son para LabelTitulo.

        // Para solo lanzar el evento MouseMove cuando se pulse en el ratón ( sino, dará overflow)
        private bool ratonPulsado;

        private void NotaUC_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
            ratonPulsado = true;
        }

        private void NotaUC_MouseMove(object sender, MouseEventArgs e)
        {
            // Solo lanzarlo si está el botón pulsado.
            if (ratonPulsado)
            {
                this.OnMouseMove(e);
            }
        }

        private void NotaUC_MouseUp(object sender, MouseEventArgs e)
        {
            this.OnMouseUp(e);
            ratonPulsado = false;
        }

        // </Fin de los eventos del ratón.

        private void ApisDriveDocs_IniciadoGuardarNotasEnDrive()
        {
            statusInfo.BackColor = Color.Firebrick;
            statusInfo.ForeColor = Color.White;
        }

        private void ApisDriveDocs_GuardandoNotas(string mensaje)
        {
            statusInfo.Text = mensaje;
            Application.DoEvents();
        }

        private void ApisDriveDocs_FinalizadoGuardarNotasEnDrive()
        {
            statusInfo.Text = "Finalizada la operación de guardar las notas en Google Drive.";
            if (Tema == Temas.Claro)
            {
                statusInfo.BackColor = ColoresClaro[0];
                statusInfo.ForeColor = ColoresClaro[1];
            }
            else
            {
                statusInfo.BackColor = ColoresOscuro[0];
                statusInfo.ForeColor = ColoresOscuro[1];
            }
        }

        //
        // Los eventos y métodos On asociados.
        //

        // Usar Event Properties para manejar los eventos. (19/oct/22)

        // Define the delegate collection.
        protected EventHandlerList listEventDelegates = new EventHandlerList();

        // Define a unique key for each event.
        static readonly object BuscarTextoEventKey = new object();
        static readonly object NotasGuardadasEventKey = new object();
        static readonly object NotasLeidasEventKey = new object();
        static readonly object CambioDeTemaEventKey = new object();
        static readonly object TemaCambiadoEventKey = new object();
        static readonly object MenuCerrarEventKey = new object();
        static readonly object NotaReemplazadaEventKey = new object();
        static readonly object ErrorEnNotasUCEventKey = new object();
        static readonly object DatosModificadosEventKey = new object();
        static readonly object NotaCambiadaEventKey = new object();
        static readonly object GrupoCambiadoEventKey = new object();

        [Browsable(true)]
        [Description("Este evento se produce cuando se pulsa en buscar.")]
        [Category("NotasUC")]
        public event MensajeDelegate BuscarTexto
        {
            // Add the input delegate to the collection.
            add
            {
                listEventDelegates.AddHandler(BuscarTextoEventKey, value);
            }
            // Remove the input delegate from the collection.
            remove
            {
                listEventDelegates.RemoveHandler(BuscarTextoEventKey, value);
            }
        }

        protected virtual void OnBuscarTexto(string mensaje)
        {
            //BuscarTexto?.Invoke(mensaje);
            ((MensajeDelegate)listEventDelegates[BuscarTextoEventKey])?.Invoke(mensaje);
        }

        [Browsable(true)]
        [Description("Este evento se produce cuando se guardan las notas.")]
        [Category("NotasUC")]
        public event MensajeDelegate NotasGuardadas
        {
            add
            {
                listEventDelegates.AddHandler(NotasGuardadasEventKey, value);
            }
            remove
            {
                listEventDelegates.RemoveHandler(NotasGuardadasEventKey, value);
            }
        }

        protected virtual void OnNotasGuardadas(string mensaje)
        {
            //NotasGuardadas?.Invoke(mensaje);
            ((MensajeDelegate)listEventDelegates[NotasGuardadasEventKey])?.Invoke(mensaje);
        }

        [Browsable(true)]
        [Description("Este evento se produce cuando se leen las notas.")]
        [Category("NotasUC")]
        public event MensajeDelegate NotasLeidas
        {
            add
            {
                listEventDelegates.AddHandler(NotasLeidasEventKey, value);
            }
            remove
            {
                listEventDelegates.RemoveHandler(NotasLeidasEventKey, value);
            }
        }

        protected virtual void OnNotasLeidas(string mensaje)
        {
            //NotasLeidas?.Invoke(mensaje);
            ((MensajeDelegate)listEventDelegates[NotasLeidasEventKey])?.Invoke(mensaje);
        }

        // Lo marco como obsoleto. (19/oct/22 11.43)

        [Browsable(true)]
        [Description("Este evento se produce cuando se cambia el tema.")]
        [Category("NotasUC")]
        [Obsolete("Usar el evento TemaCambiado.")]
        public event TemaCambiado CambioDeTema
        {
            add
            {
                listEventDelegates.AddHandler(CambioDeTemaEventKey, value);
            }
            remove
            {
                listEventDelegates.RemoveHandler(CambioDeTemaEventKey, value);
            }
        }

        // Otro nombre para el evento de Cambio de tema. (19/oct/22 11.22)
        [Browsable(true)]
        [Description("Este evento se produce cuando se cambia el tema.")]
        [Category("NotasUC")]
        public event TemaCambiado TemaCambiado
        {
            add
            {
                listEventDelegates.AddHandler(TemaCambiadoEventKey, value);
            }
            remove
            {
                listEventDelegates.RemoveHandler(TemaCambiadoEventKey, value);
            }
        }

        protected virtual void OnCambioDeTema(Temas tema)
        {
            //CambioDeTema?.Invoke(tema);
            ((TemaCambiado)listEventDelegates[CambioDeTemaEventKey])?.Invoke(tema);

            //TemaCambiado?.Invoke(tema);
            ((TemaCambiado)listEventDelegates[TemaCambiadoEventKey])?.Invoke(tema);
        }

        [Browsable(true)]
        [Description("Este evento se produce cuando se ha elegido el menú de cerrar.")]
        [Category("NotasUC")]
        public event MensajeDelegate MenuCerrar
        {
            add
            {
                listEventDelegates.AddHandler(MenuCerrarEventKey, value);
            }
            remove
            {
                listEventDelegates.RemoveHandler(MenuCerrarEventKey, value);
            }
        }

        protected virtual void OnMenuCerrar(string mensaje)
        {
            //MenuCerrar?.Invoke(mensaje);
            ((MensajeDelegate)listEventDelegates[MenuCerrarEventKey])?.Invoke(mensaje);
        }

        /// <summary>
        /// Este evento se usará para avisar de que se ha reemplazado una nota.
        /// </summary>
        [Browsable(true)]
        [Description("Este evento se usará para avisar de que se ha reemplazado una nota.")]
        [Category("NotasUC")]
        public event ReemplazarNota NotaReemplazada
        {
            add
            {
                listEventDelegates.AddHandler(NotaReemplazadaEventKey, value);
            }
            remove
            {
                listEventDelegates.RemoveHandler(NotaReemplazadaEventKey, value);
            }
        }

        protected virtual void OnNotaReemplazada(string grupo, string texto, int index)
        {
            //NotaReemplazada?.Invoke(grupo, texto, index);
            ((ReemplazarNota)listEventDelegates[NotaReemplazadaEventKey])?.Invoke(grupo, texto, index);
        }

        /// <summary>
        /// Este evento se lanzará cuando se produzca un error o
        /// un aviso para alertar de que algo no va del todo bien.
        /// </summary>
        [Browsable(true)]
        [Description("Este evento se lanzará cuando se produzca un error o un aviso para alertar de que algo no va del todo bien.")]
        [Category("NotasUC")]
        public event MensajeDelegate ErrorEnNotasUC
        {
            add
            {
                listEventDelegates.AddHandler(ErrorEnNotasUCEventKey, value);
            }
            remove
            {
                listEventDelegates.RemoveHandler(ErrorEnNotasUCEventKey, value);
            }
        }

        protected virtual void OnDatosErrorEnNotasUC(string mensaje)
        {
            //ErrorEnNotasUC?.Invoke(mensaje);
            ((MensajeDelegate)listEventDelegates[ErrorEnNotasUCEventKey])?.Invoke(mensaje);
        }

        /// <summary>
        /// Este evento se produce cuando se han cambiado los datos y no se han guardado.
        /// </summary>
        [Browsable(true)]
        [Description("Este evento se produce cuando se han cambiado los datos y no se han guardado.")]
        [Category("NotasUC")]
        public event MensajeDelegate DatosModificados
        {
            add
            {
                listEventDelegates.AddHandler(DatosModificadosEventKey, value);
            }
            remove
            {
                listEventDelegates.RemoveHandler(DatosModificadosEventKey, value);
            }
        }

        protected virtual void OnDatosModificados(string mensaje)
        {
            //DatosModificados?.Invoke(mensaje);
            ((MensajeDelegate)listEventDelegates[DatosModificadosEventKey])?.Invoke(mensaje);
        }

        /// <summary>
        /// Para avisar de cuando cambia el texto del editor.
        /// </summary>
        /// <param name="mensaje"></param>
        protected virtual void OnTextoModificado(string mensaje)
        {
            //DatosModificados?.Invoke(mensaje);
            ((MensajeDelegate)listEventDelegates[DatosModificadosEventKey])?.Invoke(mensaje);
        }

        /// <summary>
        /// Se produce cuando se selecciona una nota.
        /// </summary>
        [Browsable(true)]
        [Description("Se produce cuando se selecciona una nota.")]
        [Category("NotasUC")]
        public event TextoModificado NotaCambiada
        {
            add
            {
                listEventDelegates.AddHandler(NotaCambiadaEventKey, value);
            }
            remove
            {
                listEventDelegates.RemoveHandler(NotaCambiadaEventKey, value);
            }
        }

        protected virtual void OnNotaCambiada(string texto, int index)
        {
            //NotaCambiada?.Invoke(texto, index);
            ((TextoModificado)listEventDelegates[NotaCambiadaEventKey])?.Invoke(texto, index);
        }

        /// <summary>
        /// Se produce cuando se selecciona un grupo.
        /// </summary>
        [Browsable(true)]
        [Description("Se produce cuando se selecciona un grupo.")]
        [Category("NotasUC")]
        public event TextoModificado GrupoCambiado
        {
            add
            {
                listEventDelegates.AddHandler(GrupoCambiadoEventKey, value);
            }
            remove
            {
                listEventDelegates.RemoveHandler(GrupoCambiadoEventKey, value);
            }
        }

        protected virtual void OnGrupoCambiado(string texto, int index)
        {
            //GrupoCambiado?.Invoke(texto, index);
            ((TextoModificado)listEventDelegates[GrupoCambiadoEventKey])?.Invoke(texto, index);
        }

        //
        // Las propiedades públicas.
        //

        /// <summary>
        /// Para acceder al control statusInfo.
        /// </summary>
        [Browsable(false)]
        [Description("Para acceder al control statusInfo.")]
        [Category("NotasUC")]
        public string StatusInfo
        {
            get { return statusInfo.Text; }
            set { statusInfo.Text = value; }
        }

        /// <summary>
        /// Si se deben guardar las notas como documentos en Google Drive.
        /// Para hacerlo debes tener tu correo de GMail autorizado.
        /// Los docuemntos se crean con el tipo de letra 'Roboto Mono'.
        /// </summary>
        /// <remarks>
        /// Para solicitar autorización visita esta página:
        /// http://www.elguillemola.com/2020/12/te-gustaria-obtener-mas-prestaciones-de-gsnotasnet/#comments
        /// </remarks>
        [Browsable(true)]
        [Description("Si se deben guardar las notas como documentos en Google Drive.")]
        [DefaultValue(false)]
        [Category("NotasUC")]
        public bool GuardarEnDrive { get; set; } = false;

        /// <summary>
        /// Si al guardar los documentos en Drive se borran definitivamente las notas anteriores.
        /// </summary>
        /// <remarks>
        /// Si asignas un valor true los documentos anteriores se eliminan DEFINITIVAMENTE y solo se dejan las notas actuales.
        /// No te preocupes por tener documentos repetidos, siempre puedes acceder  a ellos por fecha de modificación.
        /// </remarks>
        [Browsable(true)]
        [Description("Si se deben guardar las notas como documentos en Google Drive.")]
        [DefaultValue(false)]
        [Category("NotasUC")]
        public bool BorrarNotasAnterioresDeDrive { get; set; } = false;


        /// <summary>
        /// Si el valor es true guardar automáticamente al cambiar la selección de la nota o el grupo.
        /// </summary>
        [Browsable(true)]
        [Description("Si el valor es true guardar automáticamente al cambiar la selección de la nota o el grupo.")]
        [DefaultValue(false)]
        [Category("NotasUC")]
        public bool AutoGuardar { get; set; } = false;

        /// <summary>
        /// No guardar notas que estén en blanco.
        /// </summary>
        [Browsable(true)]
        [Description("No guardar notas que estén en blanco.")]
        [DefaultValue(true)]
        [Category("NotasUC")]
        public bool NoGuardarEnBlanco { get; set; } = true;

        private bool _ModoEdicionNota = false;

        /// <summary>
        /// Poner o quitar el Modo de edición.
        /// Si está activo (true) no se pueden añadir notas ni grupos,
        /// solo se permite actualizar la nota que actualmente se está editando.
        /// </summary>
        [Browsable(true)]
        [Description("Poner o quitar el Modo de edición. Si está activo (true) no se pueden añadir notas ni grupos, solo se permite actualizar la nota que actualmente se está editando.")]
        [DefaultValue(false)]
        [Category("NotasUC")]
        public bool ModoEdicionNota
        {
            get { return _ModoEdicionNota; }
            set
            {
                _ModoEdicionNota = value;

                CboGrupos.Enabled = !value;
                CboGrupos.BackColor = this.BackColor;
                CboNotas.Visible = !value;

                if (_ModoEdicionNota)
                {
                    foreach (ToolStripItem c in statusInfoTecla.DropDownItems)
                    {
                        c.Enabled = false;
                        c.Visible = false;
                    }
                    MnuTopMost.Enabled = true;
                    MnuTopMost.Visible = true;
                    MnuTopMostSeparator.Visible = true;
                    MnuSustituirNota.Enabled = true;
                    MnuSustituirNota.Visible = true;
                    MnuCerrar.Enabled = true;
                    MnuCerrar.Visible = true;
                    MnuCerrarSeparator.Enabled = true;
                    MnuCerrarSeparator.Visible = true;
                }
                else
                {
                    foreach (ToolStripItem c in statusInfoTecla.DropDownItems)
                    {
                        c.Enabled = true;
                        c.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Para asignar al menú SiempreEncima (TopMost) el valor.
        /// </summary>
        [Browsable(true)]
        [Description("Si la ventana que contiene el control debe estar siempre encima o no.")]
        [DefaultValue(typeof(bool), "false")]
        [Category("NotasUC")]
        public bool SiempreEncima
        {
            get => MnuTopMost.Checked;
            set => MnuTopMost.Checked = value;
        }

        private bool _ColoresPredeterminados = true;

        /// <summary>
        /// Si se deben usar los colores predeterminados de los temas.
        /// </summary>
        [Browsable(true)]
        [Description("Usar los colores predeterminados de los temas (true sobre-escribe los definidos en diseño, false permite definir colores propios).")]
        [DefaultValue(typeof(bool), "true")]
        [Category("Colores")]
        public bool ColoresPredeterminados
        {
            get { return _ColoresPredeterminados; }
            set
            {
                _ColoresPredeterminados = value;
                if (value)
                {
                    // Asignar los colores.
                    ColoresOscuro = new Color[] { Color.FromArgb(30, 30, 30), Color.FromArgb(87, 166, 58) };
                    ColoresClaro = new Color[] { Color.White, Color.FromArgb(0, 99, 177) };
                }
            }
        }

        /// <summary>
        /// Colores a usar en el tema Oscuro. Fondo negro, letras verdes.
        /// </summary>
        /// <remarks>Si ColoresPredeterminados es true, no se asignan los nuevos colores.</remarks>
        [Browsable(true)]
        [Description("Colores a usar en el tema Oscuro. Fondo negro, letras verde.")]
        [DefaultValue(typeof(Color[]), "(30, 30, 30), (87, 166, 58)")]
        [Category("Colores")]
        public Color[] ColoresOscuro { get; set; } = new Color[] { Color.FromArgb(30, 30, 30), Color.FromArgb(87, 166, 58) };

        //private Color[] _ColoresOscuro = new Color[] { Color.FromArgb(30, 30, 30), Color.FromArgb(87, 166, 58) };
        //public Color[] ColoresOscuro 
        //{ 
        //    get { return _ColoresOscuro; }
        //    set
        //    {
        //        if (ColoresPredeterminados)
        //        {
        //            value = new Color[] { Color.FromArgb(30, 30, 30), Color.FromArgb(87, 166, 58) };
        //        }
        //        _ColoresOscuro = value;
        //    }
        //}

        //public Color[] ColoresOscuro { get; set; } = new Color[] { Color.FromArgb(30, 30, 30), Color.FromArgb(87, 166, 58) };
        //public Color[] ColoresOscuro { get; set; } = new Color[] { Color.Black, Color.Green};
        //public Color[] ColoresOscuro { get; set; } = new Color[] { Color.DimGray, Color.Lime};
        //public Color[] ColoresOscuro { get; set; } = new Color[] { Color.DimGray, Color.Gold};

        /// <summary>
        /// Colores a usar en el tema Claro.
        /// Fondo blanco, letras en color azul 0,99,177.
        /// </summary>
        [Browsable(true)]
        [Description("Colores a usar en el tema Claro. Fondo blanco, letras en color azul (0,99,177).")]
        [DefaultValue(typeof(Color[]), "White, (0,99,177)")]
        [Category("Colores")]
        public Color[] ColoresClaro { get; set; } = new Color[] { Color.White, Color.FromArgb(0, 99, 177) };

        private Temas _Tema = Temas.Claro;

        /// <summary>
        /// El color de los temas. Light/Claro o Dark/Oscuro.
        /// </summary>
        [Browsable(true)]
        [Description("El color de los temas. Light/Claro o Dark/Oscuro.")]
        [DefaultValue(typeof(Temas), "Claro")]
        [Category("Colores")]
        public Temas Tema
        {
            get { return _Tema; }
            set
            {
                _Tema = value;
                if (_Tema == Temas.Claro)
                {
                    if (_invertirColores)
                    {
                        BackColor = ColoresClaro[1];
                        ForeColor = ColoresClaro[0];
                    }
                    else
                    {
                        BackColor = ColoresClaro[0];
                        ForeColor = ColoresClaro[1];
                    }
                }
                else
                {
                    if (_invertirColores)
                    {
                        BackColor = ColoresOscuro[1];
                        ForeColor = ColoresOscuro[0];
                    }
                    else
                    {
                        BackColor = ColoresOscuro[0];
                        ForeColor = ColoresOscuro[1];
                    }
                }
            }
        }

        /// <summary>
        /// Si se invierten los colores del tema actual.
        /// </summary>
        [Browsable(true)]
        [Description("Si se invierten los colores del tema actual.")]
        [DefaultValue("false")]
        [Category("Colores")]
        public bool InvertirColores
        {
            get { return _invertirColores; }
            set
            {
                _invertirColores = value;
                MnuTemaInvertir.Checked = value;
                Tema = _Tema;
            }
        }

        /// <summary>
        /// El título a mostrar.
        /// </summary>
        [Browsable(true)]
        [Description("El título a mostrar.")]
        [DefaultValue("NotasUC")]
        [Category("NotasUC")]
        public string Titulo
        {
            get { return LabelTitulo.Text; }
            set { LabelTitulo.Text = value; }
        }

        /// <summary>
        /// El ComboBox con los grupos de notas.
        /// </summary>
        [Browsable(true)]
        [Description("El ComboBox con los grupos.")]
        [Category("NotasUC")]
        public ComboBox ComboGrupos
        {
            get { return CboGrupos; }
        }

        /// <summary>
        /// El ComboBox con las notas.
        /// </summary>
        /// <remarks>Hay que rellenarla con las notas de cada elemento del Grupo</remarks>
        [Browsable(true)]
        [Description("El ComboBox con las notas.")]
        [Category("NotasUC")]
        public ComboBox ComboNotas
        {
            get { return CboNotas; }
        }

        private Dictionary<string, List<string>> _notas = new Dictionary<string, List<string>>();

        /// <summary>
        /// Colección con los grupos y las notas asociadas a cada grupo.
        /// </summary>
        [Browsable(false)]
        [Description("Los grupos y notas (no asignar en diseño).")]
        [DefaultValue(typeof(Dictionary<string, List<string>>), "")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Category("NotasUC")]
        public Dictionary<string, List<string>> Notas
        {
            get { return _notas; }
            set
            {
                _notas = value;
                AsignarGrupos(false);
            }
        }

        /// <summary>
        /// Devuelve la nota que se ha seleccionado (el texto del combo de notas).
        /// Cuando se asigna, se asigna al editor.
        /// </summary>
        [Browsable(true)]
        [Description("Devuelve la nota que se ha seleccionado (el texto del combo de notas). Cuando se asigna, se asigna al editor.")]
        [Category("NotasUC")]
        public string Nota
        {
            get { return CboNotas.Text; }
            set { txtEdit.Text = value; TextoModificado = false; }
        }

        /// <summary>
        /// El nombre anterior del grupo antes de cambiarlo.
        /// </summary>
        private string NombreGrupoAnterior = "";

        /// <summary>
        /// Valor separado de Grupo para poder saber qué valor asignar a NombreGrupoAnterior.
        /// </summary>
        private string NombreGrupo = "";

        /// <summary>
        /// El índice del grupo seleccionado.
        /// </summary>
        [Browsable(false)]
        [Description("El índice del grupo actual.")]
        [Category("NotasUC")]
        public int GrupoIndex
        {
            get => CboGrupos.SelectedIndex;
            private set => CboGrupos.SelectedIndex = value;
        }

        /// <summary>
        /// Lee o asigna el nombre del grupo seleccionado (del texto del combo de grupos).
        /// </summary>
        [Browsable(true)]
        [Description("Lee o asigna el nombre del grupo seleccionado (del texto del combo de grupos).")]
        [Category("NotasUC")]
        public string Grupo
        {
            get
            {
                return CboGrupos.Text;
            }
            set
            {
                if (string.IsNullOrEmpty(NombreGrupo))
                    NombreGrupo = CboGrupos.Text;
                NombreGrupoAnterior = NombreGrupo;
                CboGrupos.Text = value;
                NombreGrupo = value;
            }
        }

        /// <summary>
        /// El valor de la longitud del texto a mostrar en el título.
        /// El título será el nombre del grupo y la nota seleccionada.
        /// </summary>
        /// <remarks>18-oct-22: Antes 70, ahora 79.</remarks>
        [Browsable(false)]
        [Description("El valor predeterminado de la longitud máxima del título")]
        [DefaultValue(79)]
        [Category("NotasUC")]
        public const int LongitudTituloNotaDefault = 79;

        /// <summary>
        /// Longitud del título de la nota.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(LongitudTituloNotaDefault)]
        [Description("La longitud máxima del título")]
        [Category("NotasUC")]
        public int LongitudTituloNota { get; set; } = LongitudTituloNotaDefault;

        /// <summary>
        /// El título de la nota (los <see cref="LongitudTituloNota"/> primeros caracteres)
        /// </summary>
        [Browsable(false)]
        public string TituloNota
        {
            get
            {
                if (string.IsNullOrEmpty(Nota))
                    return "";

                var s = Nota.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                if (s.Length == 0)
                    return Nota;

                if (s[0].Length < LongitudTituloNota)
                    return s[0];

                return s[0].Substring(0, LongitudTituloNota);
            }
        }

        /// <summary>
        /// Array con los caracteres especiales que se sustituirán al guardar.
        /// </summary>
        [Browsable(true)]
        [Description("Array con los caracteres especiales que se sustituirán al guardar.")]
        [DefaultValue(typeof(string[]), "\n\r, \n, \r, \", <, >, &")]
        [Category("NotasUC")]
        public string[] EspCaracteres { get; set; } = { "\n\r", "\n", "\r", "\"", "<", ">", "&" };

        /// <summary>
        /// Array con las marcas a reemplazar según el caracter especial.
        /// </summary>
        [Browsable(true)]
        [Description("Array con las marcas a reemplazar según el caracter especial.")]
        [DefaultValue(typeof(string[]), "|NL|, |CR|, |LF|, |quot|, |lt|, |gt|, |A|")]
        [Category("NotasUC")]
        public string[] EspMarcas { get; set; } = { "|NL|", "|CR|", "|LF|", "|quot|", "|lt|", "|gt|", "|A|" };

        /// <summary>
        /// Guarda las notas en el fichero indicado.
        /// </summary>
        /// <param name="path">El path completo donde se guardarán las notas.</param>
        /// <returns>true si se guardaron correctamente las notas, en otro caso, false.</returns>
        [Browsable(false)]
        private bool GuardarNotasEnFichero(string path = "")
        {
            // Guardar siempre en el nuevo path. (18/oct/22)
            if (string.IsNullOrWhiteSpace(path))
                path = FicNotas;

            try
            {
                using (var sw = new StreamWriter(path, false, Encoding.UTF8))
                {
                    sw.WriteLine("#Formato NotasUC - v1.0.0.0 - 05-dic-2020");
                    sw.WriteLine("#");
                    sw.WriteLine("# Fichero de Notas con formato NotasUC");
                    sw.WriteLine("#");
                    sw.WriteLine($"# Contenido de {Path.GetFileName(path)}");
                    sw.WriteLine("#");
                    sw.WriteLine("# Formato:");
                    sw.WriteLine("#    G:Nombre del grupo");
                    sw.WriteLine("#       Lista de notas del grupo 1, una en cada línea");
                    sw.WriteLine("#    GFin: # Fin de las notas del grupo");
                    sw.WriteLine("#");
                    sw.WriteLine("# Notas:");
                    sw.WriteLine("#     No se guardan los grupos y notas que estén en blanco.");
                    sw.WriteLine("#     Las líneas que empiecen por # no se procesan.");
                    sw.WriteLine("#         No usar # después de la nota o el grupo, ya que se asignarán como si fuesen parte del texto.");
                    sw.WriteLine("#     Si la nota tiene estos caracteres, se hará un cambio al guardar el texto:");
                    sw.WriteLine("#         Las comillas dobles se guardan como |quot|.");
                    sw.WriteLine("#         El signo mayor > se guarda como |gt|.");
                    sw.WriteLine("#         El signo mayor < se guarda como |lt|.");
                    sw.WriteLine("#         El ampersand & se guarda como |A|.");
                    sw.WriteLine("#         Si la cadena empieza con un espacio, se sustituye por |sp|.");
                    sw.WriteLine(@"#         Los cambios de línea  '\n\r' (CrLf) se guardan como |NL|.");
                    sw.WriteLine(@"#         Los cambios de línea  '\n'   (Cr)   se guardan como |CR|.");
                    sw.WriteLine(@"#         Los retornos de carro '\r'   (Lf)   se guardan como |LF|.");
                    sw.WriteLine("#             La comprobación se hace en este orden: CrLf, Cr, Lf");
                    sw.WriteLine("#             Es para los casos que haya distintos cambios de línea (según el formato del fichero)");
                    sw.WriteLine("#");

                    foreach (var g in _notas.Keys)
                    {
                        if (g.Any())
                        {
                            sw.WriteLine($"G:{g}");
                            foreach (var n in _notas[g])
                                if (n.Any())
                                    sw.WriteLine(espQuitar(n));
                            sw.WriteLine($"GFin:{g}");
                        }
                    }
                    sw.Flush();
                    sw.Close();
                }
                return true;
            }
            catch { return false; };
        }

        /// <summary>
        /// Lee las notas del fichero indicado y las asigna a la colección <see cref="Notas"/>.
        /// </summary>
        /// <param name="path">El path completo donde están las notas guardadas.</param>
        [Browsable(false)]
        public void LeerNotas(string path = "")
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = FicNotas;

                // Si no existe en el nuevo path, usar el anterior. (18/oct/22)
                // Esto solo ocurrirá la primera vez, siempre que se guarden los datos.
                if (File.Exists(path) == false)
                {
                    path = _ficNotasAnt;
                }
            }

            var colNotas = new Dictionary<string, List<string>>();

            try
            {
                if (!File.Exists(path))
                {
                    _notas = colNotas;
                    return;
                }

                using var sr = new StreamReader(path, Encoding.UTF8, true);
                while (!sr.EndOfStream)
                {
                    var s = sr.ReadLine();
                    if (!string.IsNullOrEmpty(s))
                    {
                        var s1 = s.TrimStart();
                        // Ignorar los comentarios #
                        if (s1.StartsWith("#"))
                            continue;

                        // Si es un grupo
                        if (s1.StartsWith("g:", StringComparison.OrdinalIgnoreCase))
                        {
                            var i = s.IndexOf("g:", StringComparison.OrdinalIgnoreCase);
                            if (i == -1) continue;

                            var g = s.Substring(i + 2).Trim();
                            // Es un grupo
                            // si ya existe, se ignora, pero se leen las notas y se continúa
                            var existe = false;
                            if (colNotas.ContainsKey(g))
                                existe = true;
                            else
                                colNotas.Add(g, new List<string>());

                            // Leer las notas
                            while (!sr.EndOfStream)
                            {
                                s = sr.ReadLine();
                                if (string.IsNullOrEmpty(s))
                                    continue;

                                s1 = s.TrimStart();
                                if (!s1.StartsWith("gfin:", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (!existe)
                                        colNotas[g].Add(espPoner(s));
                                }
                                else
                                    break;
                            }
                        }
                    }
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            };

            _notas = colNotas;
            AsignarGrupos(false);
        }

        private bool _Modificado;

        /// <summary>
        /// Indica si los datos se han modificado.
        /// </summary>
        [Browsable(true)]
        [Description("Indica si los datos se han modificado.")]
        [DefaultValue(false)]
        [Category("NotasUC")]
        public bool Modificado
        {
            get { return _Modificado; }
            private set
            {
                _Modificado = value;
                if (_Modificado)
                    OnDatosModificados("Los datos se han modificado.");
            }
        }

        /// <summary>
        /// El fichero donde se guardarán o se leerán las notas.
        /// </summary>
        [Browsable(true)]
        [Description("El fichero con las notas.")]
        [DefaultValue("NotasNET.notasUC.txt")]
        [Category("NotasUC")]
        public string FicNotas { get; private set; }

        /// <summary>
        /// El Directorio donde se guardarán las notas y el directorio con las copias de seguridad de las notas.
        /// </summary>
        [Browsable(true)]
        [Description("El Directorio donde se guardarán las notas y el directorio con las copias de seguridad de las notas.")]
        [Category("Directorios - solo lectura")]
        public string DirNotas
        {
            get { return _dirNotas; }
        }

        /// <summary>
        /// El Directorio donde se guardarán las notas y el directorio con las copias de seguridad de las notas.
        /// </summary>
        [Browsable(true)]
        [Description("El Directorio donde se guardarán las copias de seguridad de las notas.")]
        [Category("Directorios - solo lectura")]
        public string DirNotasBackup
        {
            get { return _dirNotasBackup; }
        }

        /// <summary>
        /// El path al directorio de documentos.
        /// </summary>
        [Browsable(true)]
        [Description("El directorio local para los datos (antes el de documentos).")]
        [Category("Directorios - solo lectura")]
        public string DirDocumentos
        {
            get { return _dirLocal; }
        }

        /// <summary>
        /// Asigna el título de la cabecera.
        /// Al asignar se asigna el texto a las notas.
        /// </summary>
        [Browsable(false)]
        [Description("Asigna el título de la cabecera. Al asignar se asigna el texto a las notas.")]
        [Category("NotasUC")]
        public string TituloCabecera
        {
            get { return Titulo; }
            set
            {
                CboNotas.Items.Add(value);
                CboNotas.Text = value;
                Titulo = TituloNota;
            }
        }

        /// <summary>
        /// El color de fondo del control, del control de las notas y del color del texto del título.
        /// </summary>
        [Browsable(true)]
        [Description("El color de fondo del control, del control de las notas y del color del texto del título.")]
        [DefaultValue(typeof(Color), "White")]
        [Category("Colores")]
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                LabelTitulo.ForeColor = value;
                CboGrupos.BackColor = value;
                CboNotas.BackColor = value;
                panelCabecera.BackColor = value;
                panelEditor.BackColor = value;
                txtEdit.BackColor = value;
                statusStrip1.BackColor = value;
                statusInfo.BackColor = value;
                statusInfoPos.BackColor = value;
                statusInfoTecla.BackColor = value;
            }
        }

        /// <summary>
        /// El color del texto y del fondo del título.
        /// </summary>
        [Browsable(true)]
        [Description("El color del texto y del fondo del título.")]
        [DefaultValue(typeof(Color), "0,99,177")]
        [Category("Colores")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;
                LabelTitulo.BackColor = value;
                CboGrupos.ForeColor = value;
                CboNotas.ForeColor = value;
                panelCabecera.ForeColor = value;
                panelEditor.ForeColor = value;
                txtEdit.ForeColor = value;
                statusStrip1.ForeColor = value;
                statusInfo.ForeColor = value;
                statusInfoPos.ForeColor = value;
                statusInfoTecla.ForeColor = value;
            }
        }

        /// <summary>
        /// El texto del editor.
        /// </summary>
        [Browsable(true)]
        [Description("El texto del editor.")]
        [DefaultValue("")]
        [Category("NotasUC")]
        public string EditorText
        {
            get { return txtEdit.Text; }
            set { txtEdit.Text = value; }
        }

        /// <summary>
        /// El texto enriquecido (Rtf) del editor.
        /// </summary>
        [Browsable(true)]
        [Description("El texto enriquecido (Rtf) del editor.")]
        [DefaultValue("")]
        [Category("NotasUC")]
        public string EditorRtf
        {
            get { return txtEdit.Rtf; }
            set { txtEdit.Rtf = value; }
        }

        //
        // Fin de las propiedades públicas.
        //

        //
        // Los métodos públicos.
        //


        /// <summary>
        /// Asignar los grupos.
        /// No asigna las notas hasta que se seleccione un grupo.
        /// </summary>
        /// <param name="mostrarUltimo">True para mostrar el último grupo, false para mostrar el primero.</param>
        /// <param name="grupo">Mostrar el grupo con este nombre (se ignora el valor de mostrarUltimo).</param>
        [Browsable(false)]
        public void AsignarGrupos(bool mostrarUltimo = false, string grupo = "")
        {
            iniciando = true;

            CboGrupos.Items.Clear();
            CboNotas.Items.Clear();
            CboGrupos.Text = "";
            CboNotas.Text = "";

            if (ComprobarNotasEsNulo())
            {
                iniciando = false;
                return;
            }

            var indexGrupo = -1;

            foreach (var k in _notas.Keys)
            {
                var j = CboGrupos.Items.Add(k);
                if (grupo.Any() && grupo.Equals(k))
                    indexGrupo = j;
            }

            iniciando = false;

            if (CboGrupos.Items.Count > 0)
            {
                if (grupo.Any() && indexGrupo > -1)
                    CboGrupos.SelectedIndex = indexGrupo;
                else if (mostrarUltimo)
                    CboGrupos.SelectedIndex = CboGrupos.Items.Count - 1;
                else
                    CboGrupos.SelectedIndex = 0;
            }
            TextoModificado = false;
        }

        /// <summary>
        /// Resetea la longitud del título, el tema y los colores.
        /// </summary>
        public void Reset()
        {
            LongitudTituloNota = LongitudTituloNotaDefault;
            Tema = Temas.Claro;
            BackColor = ColoresClaro[0];
            ForeColor = ColoresClaro[1];
            TextoModificado = false;
        }

        /// <summary>
        /// Añadir un nuevo grupo de notas vacío.
        /// Si el grupo existe, se ignora esta creación, 
        /// ya que los grupos no pueden estar repetidos.
        /// </summary>
        /// <param name="grupo">El nombre del grupo</param>
        [Browsable(false)]
        public void GruposAdd(string grupo)
        {
            // no asignar grupos en blanco
            if (string.IsNullOrWhiteSpace(grupo))
                return;

            _ = ComprobarNotasEsNulo();

            if (!_notas.ContainsKey(grupo))
                _notas.Add(grupo, new List<string>());

            AsignarGrupos(grupo: grupo);
            TextoModificado = false;
        }

        /// <summary>
        /// Añadir notas al grupo indicado.
        /// Si el grupo existe, se agregan las notas indicadas.
        /// </summary>
        /// <param name="grupo">El nombre grupo.</param>
        /// <param name="notas">Colección con las notas a asignar al grupo.</param>
        [Browsable(false)]
        public void GruposAdd(string grupo, List<string> notas)
        {
            // no asignar grupos en blanco
            if (string.IsNullOrWhiteSpace(grupo))
                return;

            _ = ComprobarNotasEsNulo();

            // No añadir las notas vacías
            if (NoGuardarEnBlanco)
            {
                var sinVacias = notas.TakeWhile((s) => s.Any());
                notas = sinVacias.ToList<string>();
            }

            if (!_notas.ContainsKey(grupo))
                _notas.Add(grupo, notas);
            else
                _notas[grupo].AddRange(notas);

            AsignarGrupos(grupo: grupo);
            TextoModificado = false;
        }

        /// <summary>
        /// Añadir una nota al grupo indicado.
        /// No se comprueba si ya existe esa nota, simplemente se añade.
        /// si el grupo no existe, se crea uno nuevo.
        /// </summary>
        /// <param name="grupo">El nombre del grupo.</param>
        /// <param name="nota">La nota a añadir.</param>
        [Browsable(false)]
        public void GruposAdd(string grupo, string nota)
        {
            // no asignar grupos en blanco
            if (string.IsNullOrWhiteSpace(grupo))
                return;

            // No añadir notas en blanco, si así se indica
            if (NoGuardarEnBlanco && string.IsNullOrWhiteSpace(nota))
                return;

            _ = ComprobarNotasEsNulo();


            if (_notas.ContainsKey(grupo))
            {
                _notas.TryGetValue(grupo, out List<string> colNotas);
                colNotas.Add(nota);
            }
            else
                _notas.Add(grupo, new List<string> { nota });

            AsignarGrupos(true);
            TextoModificado = false;
        }

        /// <summary>
        /// Añadir una nota al grupo indicado.
        /// Si el grupo no existe, se crea.
        /// </summary>
        /// <param name="grupo">El nombre del grupo.</param>
        /// <param name="nota">La nota a añadir.</param>
        [Browsable(false)]
        public void NotasAdd(string grupo, string nota)
        {
            // no asignar grupos en blanco
            if (string.IsNullOrWhiteSpace(grupo))
                return;

            // No añadir notas en blanco, si así se indica
            if (NoGuardarEnBlanco && string.IsNullOrWhiteSpace(nota))
                return;

            _ = ComprobarNotasEsNulo();

            if (_notas.ContainsKey(grupo))
            {
                _notas.TryGetValue(grupo, out List<string> colNotas);
                colNotas.Add(nota);
            }
            else
            {
                _notas.Add(grupo, new List<string> { nota });
            }

            CboNotas.Items.Add(nota);
            // Seleccionar la última nota añadida. (27/oct/22 17.05)
            // Si no se hace esto al reemplazar, puede reemplazar la que hubiera antes seleccionada.
            CboNotas.SelectedIndex = CboNotas.Items.Count - 1;

            statusInfo.Text = "Se ha añadido una nota.";
            TextoModificado = false;
        }

        /// <summary>
        /// Añadir notas al grupo indicado.
        /// Si el grupo no existe, se crea.
        /// </summary>
        /// <param name="grupo">El nombre grupo.</param>
        /// <param name="notas">Colección con las notas a asignar al grupo.</param>
        [Browsable(false)]
        public void NotasAdd(string grupo, List<string> notas)
        {
            //GruposAdd(grupo, notas);

            // no asignar grupos en blanco
            if (string.IsNullOrWhiteSpace(grupo))
                return;

            _ = ComprobarNotasEsNulo();

            // No añadir las notas vacías
            if (NoGuardarEnBlanco)
            {
                var sinVacias = notas.TakeWhile((s) => s.Any());
                notas = sinVacias.ToList<string>();
            }

            if (!_notas.ContainsKey(grupo))
                _notas.Add(grupo, notas);
            else
                _notas[grupo].AddRange(notas);

            AsignarNotas(grupo);
            statusInfo.Text = $"Se han añadido notas al grupo {grupo}.";
            TextoModificado = false;
        }

        /// <summary>
        /// Reemplazar la nota actualmente seleccionada en el CboNotas por la nueva.
        /// </summary>
        /// <param name="grupo">El nombre del grupo.</param>
        /// <param name="nota">La nueva nota a poner.</param>
        [Browsable(false)]
        public void NotaReplace(string grupo, string nota)
        {
            var i = CboNotas.SelectedIndex;
            if (i == -1)
                return;

            iniciando = true;

            if (!_ModoEdicionNota)
            {
                _notas[grupo][CboNotas.SelectedIndex] = nota;
                CboNotas.Items[CboNotas.SelectedIndex] = nota;

                OnNotaCambiada(nota, CboNotas.SelectedIndex);
            }

            OnNotaReemplazada(grupo, nota, CboNotas.SelectedIndex);

            // Mostrar el número de nota en base 1 (humanamente es más lógico). (27/oct/22 17.10)
            statusInfo.Text = $"Se ha reemplazado la nota con índice {CboNotas.SelectedIndex + 1} en el grupo {grupo}.";

            iniciando = false;
            TextoModificado = false;
        }

        /// <summary>
        /// Asignar una nota existente a un grupo y en el índice indicado.
        /// Se usará cuando está en modo <see cref="ModoEdicionNota"./>
        /// </summary>
        /// <param name="grupo">El grupo.</param>
        /// <param name="nota">El texto de la nota.</param>
        /// <param name="index">El índice de la nota.</param>
        /// <remarks>Usar esto solo para asignar notas existentes.</remarks>
        public void AsignarNota(string grupo, string nota, int index)
        {
            if (string.IsNullOrWhiteSpace(grupo))
                return;

            if (NoGuardarEnBlanco && string.IsNullOrWhiteSpace(nota))
                return;

            if (index < 0)
            {
                OnDatosErrorEnNotasUC("El índice no puede ser menor de cero. En: 'AsignarNota'.");
                statusInfo.Text = "El índice no puede ser menor de cero. En: 'AsignarNota'.";
                return;
            }

            // Si está en modo edición no asignarlo a la colección ???
            //if(!_ModoEdicionNota)
            //{
            //}
            if (!_notas.ContainsKey(grupo))
            {
                OnDatosErrorEnNotasUC($"El grupo '{grupo}' no existe. En: 'AsignarNota'.");
                statusInfo.Text = $"El grupo '{grupo}' no existe. En: 'AsignarNota'.";
                return;
            }

            if (index >= _notas[grupo].Count)
                index = _notas[grupo].Count - 1;

            _notas[grupo][index] = nota;

            iniciando = true;

            if ((string)CboNotas.Items[index] != nota)
                Modificado = true;

            CboNotas.Items[index] = nota;

            OnNotaCambiada(nota, index);
            OnNotaReemplazada(grupo, nota, index);

            statusInfo.Text = $"Se han asignado la nota con índice {index} del grupo {grupo}.";
            iniciando = false;
            TextoModificado = false;
        }

        /// <summary>
        /// Asignar las notas del grupo indicado.
        /// </summary>
        /// <param name="grupo"></param>
        [Browsable(false)]
        public void AsignarNotas(string grupo)
        {
            if (string.IsNullOrEmpty(grupo) || !_notas.ContainsKey(grupo))
                return;

            //iniciando = true;

            CboNotas.Items.Clear();

            if (_notas.TryGetValue(grupo, out List<string> colNotas))
            {
                iniciando = true;

                // Rellenar el grupo de notas
                for (var i = 0; i < colNotas.Count; i++)
                {
                    CboNotas.Items.Add(colNotas[i]);
                }

                iniciando = false;

                if (CboNotas.Items.Count > 0)
                    CboNotas.SelectedIndex = 0;
            }
            //iniciando = false;

            OnGrupoCambiado(CboGrupos.Text, CboGrupos.SelectedIndex);
            statusInfo.Text = $"Se han asignado las notas al grupo {grupo}.";
            TextoModificado = false;
        }

        /// <summary>
        /// Seleccionar el elemento del grupo con el índice indicado.
        /// </summary>
        /// <param name="index">El índice del combo de los grupos a seleccionar.</param>
        /// <param name="esNota">
        /// True si se debe seleccionar una nota, 
        /// false si se debe seleccioanr un grupo</param>
        [Browsable(false)]
        public void Seleccionar(int index, bool esNota)
        {
            if (index > -1)
            {
                if (esNota)
                {
                    if (CboNotas.Items.Count > 0 && index < CboNotas.Items.Count)
                    {
                        CboNotas.SelectedIndex = index;
                        statusInfo.Text = $"Se ha seleccionado la nota {index + 1} del grupo {Grupo}.";
                    }
                }
                else
                {
                    if (CboGrupos.Items.Count > 0 && index < CboGrupos.Items.Count)
                    {
                        CboGrupos.SelectedIndex = index;
                        statusInfo.Text = $"Se ha seleccionado el grupo con índice {index + 1}.";
                    }
                }
            }
        }

        //
        // Fin de los métodos públicos.
        //

        //
        // Las propiedades privadas.
        //

        /// <summary>
        /// Cambia los caracteres normales en marcas para guardar.
        /// </summary>
        /// <param name="s">La cadena donde se harán los cambios.</param>
        /// <returns>Una nueva cadena con los caracteres sustituidos por las marcas.</returns>
        /// <remarks>Usar esta función para guardarlos en el fichero.</remarks>
        private string espQuitar(string s)
        {
            // sustituir los caracteres especiales
            // usar esta función para guardarlo en la colección
            if (s.StartsWith(" "))
                s = "|sp|" + s;
            for (int j = 0; j < EspCaracteres.Length; j++)
                s = s.Replace(EspCaracteres[j], EspMarcas[j]);
            return s;
        }

        /// <summary>
        /// Cambia las marcas especiales por los caracteres normales.
        /// </summary>
        /// <param name="s">La cadena donde se harán los cambios.</param>
        /// <returns>Una nueva cadena con las marcas sustituidas por los caracteres.</returns>
        /// <remarks>Usar esta función al leer del fichero y mostrarlos correctamente.</remarks>
        private string espPoner(string s)
        {
            // restablecer los caracteres especiales
            // usar esta función para mostrarlos correctamente
            if (s.StartsWith("|sp|"))
                s = s.Substring(4);
            for (int j = 0; j < EspCaracteres.Length; j++)
                s = s.Replace(EspMarcas[j], EspCaracteres[j]);
            return s;
        }

        /// <summary>
        /// Comprueba si existe el grupo indicado.
        /// </summary>
        /// <param name="g">El nombre del grupo.</param>
        /// <returns>True si el grupo existe, false si no existe.</returns>
        private bool ExisteGrupo(string g)
        {
            if (!Notas.ContainsKey(g))
            {
                statusInfo.Text = $"No existe el grupo {g}.";
                return false;
            }
            return true;
        }

        //
        // Fin de las propiedades privadas.
        //

        //
        // Los métodos privados.
        //

        /// <summary>
        /// Comprueba si _notas es nulo o las claves del dictionary es nulo
        /// en ese caso crea un nuevo objeto en _notas y devuelve true.
        /// </summary>
        /// <returns>True si _notas es nulo o _notas.Keys es nulo (pero crea la colección _notas),
        /// False si _notas no era nulo.</returns>
        private bool ComprobarNotasEsNulo()
        {
            if (_notas is null || _notas.Keys is null)
            {
                _notas = new Dictionary<string, List<string>>();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Muestra la posición en la que se está en el editor.
        /// </summary>
        /// <param name="e">
        /// Un objeto de tipo <see cref="KeyEventArgs"/> o nulo.
        /// Si se llama desde KeyUp o KeyDown pasar el valor de e.
        /// </param>
        private void mostrarPosicion(KeyEventArgs e)
        {
            // Saber la línea y columna de la posición del cursor
            int pos = txtEdit.SelectionStart + 1;
            int lin = txtEdit.GetLineFromCharIndex(pos) + 1;
            int col = pos - txtEdit.GetFirstCharIndexOfCurrentLine();
            if (e != null)
            {
                if (e.KeyCode == Keys.Tab && e.Modifiers == Keys.Shift)
                    col = 1;
                else if (e.KeyCode == Keys.Home)
                    col = 1;
            }
            statusInfoPos.Text = $"L: {lin} , C: {col}";
        }

        //
        // Fin de los métodos privados.
        //

        //
        // Los métodos de evento.
        //

        private void CboGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iniciando) return;

            if (string.IsNullOrEmpty(NombreGrupo))
            {
                NombreGrupo = CboGrupos.Text;
            }
            NombreGrupoAnterior = NombreGrupo;
            NombreGrupo = CboGrupos.Text;
            GrupoIndex = CboGrupos.SelectedIndex;

            Titulo = $"Notas de '{Grupo}'";

            if (CboGrupos.Items.Count > 0 && CboGrupos.SelectedIndex != -1)
                OnGrupoCambiado(Grupo, CboGrupos.SelectedIndex);

            // Si es autoguardar y se cambia de grupo
            // asignar el texto actual a una nueva nota del grupo anterior
            // si el texto ha cambiado
            //
            // Ahora sustituye la nota anterior con el nuevo texto. (27/oct/22 18.56)

            // esto no debería darse
            if (string.IsNullOrEmpty(NombreGrupoAnterior))
                NombreGrupoAnterior = Grupo;

            if (AutoGuardar && NombreGrupoAnterior != Grupo && TextoModificado)
            {
                if (_notas.Keys.Contains(NombreGrupoAnterior))
                {
                    if (CboNotasIndiceAnterior > -1)
                    {
                        _notas[NombreGrupoAnterior][CboNotasIndiceAnterior] = txtEdit.Text;
                    }
                    else
                    {
                        _notas[NombreGrupoAnterior].Add(txtEdit.Text);
                    }
                }
            }
            TextoModificado = false;

            AsignarNotas(Grupo);

            statusInfo.Text = $"Grupo: '{Grupo}' con {CboNotas.Items.Count} nota{(CboNotas.Items.Count == 1 ? "" : "s")}";
        }

        private void CboGrupos_Validating(object sender, CancelEventArgs e)
        {
            if (iniciando) return;

            Titulo = $"'{Grupo} - {TituloNota}'";

            if (CboGrupos.Items.Count > 0 && CboGrupos.SelectedIndex != -1)
                OnGrupoCambiado(Grupo, CboGrupos.SelectedIndex);

            if (!_notas.ContainsKey(Grupo))
            {
                GruposAdd(Grupo, "nota-en-blanco");
                statusInfo.Text = $"Se ha añadido el grupo {Grupo}.";
            }
            else
            {
                AsignarNotas(Grupo);
                statusInfo.Text = $"Se han asignado las notas al grupo {Grupo}.";
            }

            TextoModificado = false;
        }

        private int CboNotasIndiceAnterior = -1;
        private void CboNotas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iniciando) return;

            if (string.IsNullOrEmpty(NombreGrupo))
            {
                NombreGrupo = CboGrupos.Text;
            }

            NombreGrupoAnterior = NombreGrupo;
            NombreGrupo = CboGrupos.Text;

            Titulo = $"'{Grupo} - {TituloNota}'";

            if (AutoGuardar && TextoModificado)
            {
                if (_notas.Keys.Contains(NombreGrupo))
                {
                    // Reemplazar la nota actualmente en edición. (27/oct/22 17.39)
                    // Este sería el nuevo índice seleccionado.
                    //int index = CboNotas.SelectedIndex;
                    if (CboNotasIndiceAnterior > -1)
                    {
                        _notas[NombreGrupoAnterior][CboNotasIndiceAnterior] = txtEdit.Text;
                        Modificado = true;
                        // También hay que sustituir el contenido de CboNotas.
                        iniciando = true;
                        CboNotas.Items[CboNotasIndiceAnterior] = txtEdit.Text;
                        iniciando = false;
                    }
                    else
                    {
                        var notaAnt = CboNotas.Text;
                        _notas[NombreGrupo].Add(txtEdit.Text);
                        iniciando = true;
                        CboNotas.Items.Add(txtEdit.Text);
                        CboNotas.Text = notaAnt;
                        iniciando = false;
                    }
                }
            }
            txtEdit.Text = Nota;
            TextoModificado = false;

            // El índice de la nota seleccionada. (27/oct/22 17.46)
            CboNotasIndiceAnterior = CboNotas.SelectedIndex;

            OnNotaCambiada(Nota, CboNotas.SelectedIndex);
            statusInfo.Text = $"Grupo: '{Grupo}' con {CboNotas.Items.Count} nota{(CboNotas.Items.Count == 1 ? "" : "s")}";
        }

        private void CboNotas_Validating(object sender, CancelEventArgs e)
        {
            if (iniciando) return;

            Titulo = $"'{Grupo} - {TituloNota}'";

            if (CboNotas.Items.Count > 0 && CboNotas.SelectedIndex != -1)
                OnNotaCambiada(Nota, CboNotas.SelectedIndex);

            if (_notas.ContainsKey(Grupo) && !_notas[Grupo].Contains(Nota))
                NotasAdd(Grupo, Nota);

            TextoModificado = false;
        }

        private void CboGrupos_KeyUp(object sender, KeyEventArgs e)
        {
            if (iniciando) return;

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");

                OnGrupoCambiado(Grupo, CboGrupos.SelectedIndex);

                statusInfo.Text = $"Grupo: '{Grupo}' con {CboNotas.Items.Count} nota{(CboNotas.Items.Count == 1 ? "" : "s")}";
            }
        }

        private void CboNotas_KeyUp(object sender, KeyEventArgs e)
        {
            if (iniciando) return;

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void CboGrupos_KeyPress(object sender, KeyPressEventArgs e)
        {
            // El ENTER / INTRO no llega aquí
            //if (iniciando) return;

            //if (e.KeyChar == '\n')
            //{
            //    e.Handled = true;
            //    SendKeys.Send("{TAB}");
            //}
        }

        private void CboNotas_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (iniciando) return;

            //if (e.KeyChar == '\n')
            //{
            //    e.Handled = true;
            //    SendKeys.Send("{TAB}");
            //}
        }

        private void NotaUC_Load(object sender, EventArgs e)
        {
            // No poner nada aquí
            return;
        }

        private void txtEdit_SelectionChanged(object sender, EventArgs e)
        {
            mostrarPosicion(null);
        }

        private void txtEdit_KeyDown(object sender, KeyEventArgs e)
        {
            mostrarPosicion(e);
        }

        private void txtEdit_Enter(object sender, EventArgs e)
        {
            statusInfoPos.Enabled = true;
        }

        private void txtEdit_Leave(object sender, EventArgs e)
        {
            statusInfoPos.Enabled = false;
        }

        private void MnuAñadirNota_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "F8";

            var g = CboGrupos.Text;
            if (string.IsNullOrWhiteSpace(g))
            {
                CboGrupos.Focus();
                return;
            }

            if (!ExisteGrupo(g))
            {
                // Añadir el grupo
                _notas.Add(g, new List<string>() { txtEdit.Text });
                GruposAdd(g);
                OnGrupoCambiado(g, CboGrupos.SelectedIndex);
            }
            else
            {
                NotasAdd(g, txtEdit.Text);
                TextoModificado = false;
                OnNotaCambiada(txtEdit.Text, CboNotas.SelectedIndex);
            }
            statusInfo.Text = "Se ha añadido una nota.";
            Modificado = true;
        }

        private void MnuSustituirNota_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "Shift+F8";

            var g = CboGrupos.Text;
            if (string.IsNullOrWhiteSpace(g))
            {
                CboGrupos.Focus();
                return;
            }

            if (!ModoEdicionNota && !ExisteGrupo(g))
                return;

            if (!ModoEdicionNota)
                NotaReplace(g, txtEdit.Text);

            OnNotaReemplazada(g, txtEdit.Text, CboNotas.SelectedIndex);
            statusInfo.Text = "Se ha reemplazado la nota.";
            TextoModificado = false;
            Modificado = true;
        }

        private void MnuClasificar_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "F5";

            var g = CboGrupos.Text;
            if (string.IsNullOrWhiteSpace(g))
            {
                CboGrupos.Focus();
                return;
            }

            if (!ExisteGrupo(g))
                return;

            Notas[g].Sort();
            AsignarNotas(g);
            statusInfo.Text = $"Se han clasificado las notas del grupo {g}.";
            Modificado = true;
        }

        private void MnuGuardar_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "F9";

            GuardarNotas();

            //MostrarInfoNotas();
        }

        /// <summary>
        /// Guardar las notas en el fichero de texto y si así se ha indicado en el Google Drive.
        /// </summary>
        public void GuardarNotas()
        {
            if (string.IsNullOrEmpty(FicNotas))
            {
                statusInfo.Text = "No se ha indicado un nombre fichero para el fichero de notas.";
                OnDatosErrorEnNotasUC("No se ha indicado un nombre fichero para el fichero de notas.");
                return;
            }
            GuardarNotasEnFichero();
            OnNotasGuardadas($"Se han guardado las notas en el fichero de notas '{FicNotas}'.");
            statusInfo.Text = $"Se han guardado las notas en el fichero de notas '{FicNotas}'.";
            if (GuardarEnDrive)
            {
                try
                {
                    var sBorrar = BorrarNotasAnterioresDeDrive ? "SI" : "NO";
                    var total = gsGoogleDriveDocsAPINET.ApisDriveDocs.GuardarNotasDrive(FicNotas, sBorrar);
                    OnNotasGuardadas($"Se han guardado {total} notas en Google Drive y {sBorrar} se han eliminado las anteriores.");
                    statusInfo.Text = $"Se han guardado {total} notas en Google Drive y {sBorrar} se han eliminado las anteriores.";
                }
                catch (Exception ex)
                {
                    var crlf = "\r\n";
                    statusInfo.Text = $"Error: {ex.Message}.";
                    ApisDriveDocs_FinalizadoGuardarNotasEnDrive();
                    MessageBox.Show($"Error:{crlf}{crlf}{ex.Message}.",
                                    "Error al guardar las notas en Drive.",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }
            }
            TextoModificado = false;
            Modificado = false;
        }

        private void MnuLeer_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "Ctrl+L";

            if (string.IsNullOrEmpty(FicNotas))
            {
                statusInfo.Text = "No se ha indicado un nombre fichero para el fichero de notas.";
                OnDatosErrorEnNotasUC("No se ha indicado un nombre fichero para el fichero de notas.");
                return;
            }
            LeerNotas();
            OnNotasLeidas($"Se han leído las notas del fichero '{FicNotas}'.");
            statusInfo.Text = $"Se han leído las notas del fichero '{FicNotas}'.";
            TextoModificado = false;
            Modificado = false;

            MostrarInfoNotas();
        }

        /// <summary>
        /// Mostrar la información de cuantas notas hay.
        /// </summary>
        private void MostrarInfoNotas()
        {
            var grupos = Notas.Keys.Count;
            var notas = 0;
            foreach (var g in Notas.Keys)
            {
                notas += Notas[g].Count;
            }
            if (grupos == 1 && notas == 1)
                statusInfo.Text = "Hay 1 grupo y 1 nota en total.";
            else if (grupos == 1)
                statusInfo.Text = $"Hay 1 grupo y {notas} notas en total.";
            else if (notas == 1)
                statusInfo.Text = $"Hay {grupos} grupos y 1 nota en total. (Esto no es posible)";
            else
                statusInfo.Text = $"Hay {grupos} grupos y {notas} notas en total.";
        }

        private void MnuNuevoGrupo_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "Ctrl+G";

            CboNotas.Text = "";
            CboGrupos.Text = "Nuevo-grupo";
            CboGrupos.Focus();

            statusInfo.Text = "Nuevo grupo, indica el nombre y pulsa ENTER para crearlo.";
        }

        private void MnuEliminarNota_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "F6";

            var i = CboNotas.SelectedIndex;
            if (i == -1)
                return;

            Notas[CboGrupos.Text].Remove(CboNotas.Text);
            CboNotas.Items.RemoveAt(i);

            statusInfo.Text = "Se ha eliminado la nota, el texto se deja en el editor.";
            TextoModificado = false;
            Modificado = true;
        }

        private void MnuAcercaDe_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "F1";

            System.Reflection.Assembly ensamblado = typeof(VersionUtilidades).Assembly;

            var versionWeb = "xx";
            string msgVersion;

            var cualVersion = VersionUtilidades.CompararVersionWeb(ensamblado, ref versionWeb);

            if (cualVersion == 1)
                msgVersion = $"Existe una versión más reciente de '{Application.ProductName}': v{versionWeb}.";
            else //if (cualVersion == -1)
                msgVersion = $"Esta versión de '{Application.ProductName}' es la más reciente.";

            // Mostrar acerca de
            var titulo = $"{Application.ProductName} v{Application.ProductVersion}";
            MessageBox.Show(@$"Acerca de {titulo}

Utilidad para crear notas y grupos de notas.
Las notas se guardan en el fichero '{FicNotas}'.

Operaciones que puedes hacer:
    Añadir nuevos grupos y notas.
    Sustituir una nota con un nuevo texto.
    Eliminar una nota.
    Clasificar las notas del grupo seleccionado.
    Leer y guardar las notas en un fichero de texto.

El formato del fichero de notas es:
    G: Nombre del grupo 1
        Lista de notas del grupo 1, una en cada línea
        GFin: Fin de las notas del grupo 1

Al guardar se sustituyen caracteres especiales por marcadores:
    Las comillas dobles se guardan como |quot|.
    El signo mayor > se guarda como | gt |.
    El signo mayor < se guarda como | lt |.
    El ampersand &se guarda como | A |.
    Si la cadena empieza con un espacio, se sustituye por | sp |.
    Los cambios de línea  '\n\r'(CrLf) se guardan como | NL |.
    Los cambios de línea  '\n'(Cr)   se guardan como | CR |.
    Los retornos de carro '\r'(Lf)   se guardan como | LF |.
    La comprobación se hace en este orden: CrLf, Cr, Lf.

No se guardan los grupos y notas en blanco.

{msgVersion}",
                $"Acerca de {titulo}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MnuTemaClaro_Click(object sender, EventArgs e)
        {
            Tema = Temas.Claro;
            OnCambioDeTema(Tema);
        }

        private void MnuTemaOscuro_Click(object sender, EventArgs e)
        {
            Tema = Temas.Oscuro;
            OnCambioDeTema(Tema);
        }

        private void MnuTemas_DropDownOpening(object sender, EventArgs e)
        {
            MnuTemaClaro.Checked = (Tema == Temas.Claro);
            MnuTemaOscuro.Checked = (Tema == Temas.Oscuro);
            MnuTemaColoresPredeterminados.Checked = ColoresPredeterminados;
        }

        private void MnuCerrar_Click(object sender, EventArgs e)
        {
            OnMenuCerrar("Cerrar la aplicación.");
        }

        private void NotaUC_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) ||
                    e.Data.GetDataPresent(DataFormats.StringFormat) ||
                    e.Data.GetDataPresent(DataFormats.CommaSeparatedValue) ||
                    e.Data.GetDataPresent(DataFormats.Html) ||
                    e.Data.GetDataPresent(DataFormats.OemText) ||
                    e.Data.GetDataPresent(DataFormats.Rtf) ||
                    e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
        }

        private void NotaUC_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var fics = (string[])(e.Data.GetData(DataFormats.FileDrop));
                if (fics is null || fics.Length == 0)
                    return;

                var fic = fics[0];
                var s = "";
                using (var sr = new StreamReader(fic, Encoding.UTF8, true))
                {
                    s = sr.ReadToEnd();
                    sr.Close();
                }

                // abrir el fichero y asignar el contenido
                txtEdit.Text = s;
                TextoModificado = false;
            }
            // Creo que comparando con Text sería suficiente...
            else if (e.Data.GetDataPresent(DataFormats.StringFormat) ||
                     e.Data.GetDataPresent(DataFormats.CommaSeparatedValue) ||
                     e.Data.GetDataPresent(DataFormats.Html) ||
                     e.Data.GetDataPresent(DataFormats.OemText) ||
                     e.Data.GetDataPresent(DataFormats.Rtf) ||
                     e.Data.GetDataPresent(DataFormats.Text))
            {
                int i;
                String s;

                // Get start position to drop the text.  
                i = txtEdit.SelectionStart;
                s = txtEdit.Text.Substring(i);
                txtEdit.Text = txtEdit.Text.Substring(0, i);

                // Drop the text on to the RichTextBox.  
                txtEdit.Text = txtEdit.Text + e.Data.GetData(DataFormats.Text).ToString();
                txtEdit.Text = txtEdit.Text + s;

                TextoModificado = false;
            }
        }

        private void NotaUC_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileDrop"))
                e.Effect = DragDropEffects.Copy;
        }

        private bool TextoModificado = false;

        private void txtEdit_TextChanged(object sender, EventArgs e)
        {
            TextoModificado = true;
            OnTextoModificado("El texto se ha modificado.");
        }

        private bool _invertirColores = false;

        private void MnuTemaInvertir_Click(object sender, EventArgs e)
        {
            // invertir los colores del tema
            InvertirColores = !InvertirColores;
            MnuTemaInvertir.Checked = _invertirColores;

            OnCambioDeTema(Tema);
        }

        /// <summary>
        /// Hacer una copia del fichero de notas.
        /// </summary>
        public void HacerCopia()
        {
            var sDateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm");
            var nombreBak = $"Backup_{sDateTime}_{_ficNotasSinPath}";
            var fic = Path.Combine(_dirNotasBackup, nombreBak);
            try
            {
                File.Copy(FicNotas, fic, true);
            }
            catch { }
        }

        private void statusInfoTecla_ButtonClick(object sender, EventArgs e)
        {
            // mostrar el menú
            statusInfoTecla.DropDown.Left = statusStrip1.Right - statusInfoTecla.Width;
            statusInfoTecla.DropDown.Show();
        }

        private void mnuSeleccionarTodo_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "Ctrl+A";
            txtEdit.SelectAll();
        }

        private void mnuPegar_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "Ctrl+V";
            txtEdit.Paste();
        }

        // Pegar con el formato del texto guardado. (27/oct/22 16.49)
        // Por si se copia el texto del fichero de notas y se pega.

        private void mnuPegarFormato_Click(object sender, EventArgs e)
        {
            // Convertir el texto en el portapales a texto normal.
            statusInfoTecla.Text = "Ctrl+Alt+V";
            string s = Clipboard.GetText();
            txtEdit.Text = espPoner(s);
        }

        private void mnuCopiar_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "Ctrl+C";
            txtEdit.Copy();
        }

        private void mnuCortar_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "Ctrl+X";
            txtEdit.Paste();
        }

        private void mnuBuscar_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "F3";
            var texto = txtEdit.SelectedText;
            OnBuscarTexto(texto);
        }

        private void mnuDeshacer_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "Ctrl+Z";
            txtEdit.Undo();
        }

        private void mnuRehacer_Click(object sender, EventArgs e)
        {
            statusInfoTecla.Text = "Ctrl+Y";
            txtEdit.Redo();
        }

        private void contextEditor_Opening(object sender, CancelEventArgs e)
        {
            mnuPegar.Enabled = txtEdit.CanPaste(DataFormats.GetFormat("Text"));
            var sLen = txtEdit.SelectionLength;
            mnuCopiar.Enabled = sLen > 0;
            mnuCortar.Enabled = mnuCopiar.Enabled;
            mnuDeshacer.Enabled = txtEdit.CanUndo;
            mnuRehacer.Enabled = txtEdit.CanRedo;
            mnuSeleccionarTodo.Enabled = txtEdit.CanSelect;
        }

        private void MnuTemaColoresPredeterminados_Click(object sender, EventArgs e)
        {
            ColoresPredeterminados = !ColoresPredeterminados;
            // Forzar el cambio de color.
            Tema = Tema;
            OnCambioDeTema(Tema);
        }

        private void MnuImportar_Click(object sender, EventArgs e)
        {
            // Importar notas (usando el formato de NotasUC) (19/oct/22 07.29)
            var oFD = new OpenFileDialog();
            oFD.Title = "Selecciona el fichero para importar en formato gsNotas";
            oFD.InitialDirectory = DirNotas;
            oFD.FileName = "gsNotas.notasUC.Exportar.txt";
            oFD.Filter = "Textos (.*.txt)|*.txt|Todos (*.*)|*.*";
            if (oFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var colNotas = _notas;

            using var sr = new StreamReader(oFD.FileName, System.Text.Encoding.Default, true);
            while (!sr.EndOfStream)
            {
                var s = sr.ReadLine();
                if (!string.IsNullOrEmpty(s))
                {
                    var s1 = s.TrimStart();
                    // Ignorar los comentarios #
                    if (s1.StartsWith("#"))
                        continue;

                    // Si es un grupo
                    if (s1.StartsWith("g:", StringComparison.OrdinalIgnoreCase))
                    {
                        var i = s.IndexOf("g:", StringComparison.OrdinalIgnoreCase);
                        if (i == -1) continue;

                        var g = s.Substring(i + 2).Trim();
                        // Es un grupo
                        // si ya existe, se agregan las notas a ese grupo.
                        // Aunque puede que haya repetidas. Pero...
                        if (colNotas.ContainsKey(g) == false)
                            colNotas.Add(g, new List<string>());

                        // Leer las notas
                        while (!sr.EndOfStream)
                        {
                            s = sr.ReadLine();
                            if (string.IsNullOrEmpty(s))
                                continue;

                            s1 = s.TrimStart();
                            if (!s1.StartsWith("gfin:", StringComparison.OrdinalIgnoreCase))
                            {
                                colNotas[g].Add(espPoner(s));
                            }
                            else
                                break;
                        }
                    }
                }
            }
            sr.Close();

            // Actualizar el contenido de los grupos.
            AsignarGrupos(false);
            Modificado = true;
        }

        private void MnuTopMost_Click(object sender, EventArgs e)
        {
            // El control de usuario no tiene la propiedad TopMost, asignarlo al ParentForm.
            //this.TopMost = MnuTopMost.Checked;
            this.ParentForm.TopMost = MnuTopMost.Checked;
        }
    }
}
