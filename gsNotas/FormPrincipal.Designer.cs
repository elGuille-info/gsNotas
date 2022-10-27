namespace gsNotas
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabsConfig = new System.Windows.Forms.TabControl();
            this.tabNotas = new System.Windows.Forms.TabPage();
            this.NotasFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.LblNota = new System.Windows.Forms.Label();
            this.tabGrupos = new System.Windows.Forms.TabPage();
            this.GruposFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.LblGrupo = new System.Windows.Forms.Label();
            this.tabEditarGrupos = new System.Windows.Forms.TabPage();
            this.panelEditarGrupos = new System.Windows.Forms.Panel();
            this.btnCrearGrupo = new System.Windows.Forms.Button();
            this.txtEdNuevoNombreGrupo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClasificarGrupos = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboEdGrupoDestino = new System.Windows.Forms.ComboBox();
            this.btnMoverNota = new System.Windows.Forms.Button();
            this.cboEdNotas = new System.Windows.Forms.ComboBox();
            this.cboEdGrupoNotas = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEdInfo = new System.Windows.Forms.Label();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnCambiarNombre = new System.Windows.Forms.Button();
            this.txtEdNombreGrupo = new System.Windows.Forms.TextBox();
            this.lblEdCambiar = new System.Windows.Forms.Label();
            this.cboEdGrupos = new System.Windows.Forms.ComboBox();
            this.lblEdSeleccionar = new System.Windows.Forms.Label();
            this.tabBuscarTexto = new System.Windows.Forms.TabPage();
            this.panelBuscarTexto = new System.Windows.Forms.Panel();
            this.lblResultadoBuscar = new System.Windows.Forms.Label();
            this.lblBuscando = new System.Windows.Forms.Label();
            this.lstResultadoBuscar = new System.Windows.Forms.ListBox();
            this.chkBuscarEnGrupoActual = new System.Windows.Forms.CheckBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.tabOpciones = new System.Windows.Forms.TabPage();
            this.panelOpciones = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.OpcCboColorGrupo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.OpcChkIniciarConWindows = new System.Windows.Forms.CheckBox();
            this.OpcBtnGuardarEnDrive = new System.Windows.Forms.Button();
            this.OpcLinkSolicitarAutorización = new System.Windows.Forms.LinkLabel();
            this.OpcChkMostrarHorizontal = new System.Windows.Forms.CheckBox();
            this.OpcChkOcultarPanelSuperior = new System.Windows.Forms.CheckBox();
            this.OpcChkMostrarMismoGrupo = new System.Windows.Forms.CheckBox();
            this.OpcChkIniciarMinimizada = new System.Windows.Forms.CheckBox();
            this.OpcBtnDeshacer = new System.Windows.Forms.Button();
            this.OpcBtnGuardar = new System.Windows.Forms.Button();
            this.OpcChkMinimizarAlCerrar = new System.Windows.Forms.CheckBox();
            this.OpcChkAjusteLineas = new System.Windows.Forms.CheckBox();
            this.OpcBtnRestablecerTam = new System.Windows.Forms.Button();
            this.OpcChkRecordarTam = new System.Windows.Forms.CheckBox();
            this.OpChkNoGuardarEnBlanco = new System.Windows.Forms.CheckBox();
            this.OpcChkAutoGuardar = new System.Windows.Forms.CheckBox();
            this.tabAcercaDe = new System.Windows.Forms.TabPage();
            this.panelAcercaDe = new System.Windows.Forms.Panel();
            this.txtAcercaDe = new System.Windows.Forms.TextBox();
            this.notaUC1 = new gsNotas.NotaUC();
            this.picOcultarPanel1 = new System.Windows.Forms.PictureBox();
            this.timerInicio = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NotifyMenuRestaurar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.NotifyMenuCerrar = new System.Windows.Forms.ToolStripMenuItem();
            this.contextEditarNota = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuEditarEnVentanaSeparada = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabsConfig.SuspendLayout();
            this.tabNotas.SuspendLayout();
            this.NotasFlowLayoutPanel.SuspendLayout();
            this.tabGrupos.SuspendLayout();
            this.GruposFlowLayoutPanel.SuspendLayout();
            this.tabEditarGrupos.SuspendLayout();
            this.panelEditarGrupos.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabBuscarTexto.SuspendLayout();
            this.panelBuscarTexto.SuspendLayout();
            this.tabOpciones.SuspendLayout();
            this.panelOpciones.SuspendLayout();
            this.tabAcercaDe.SuspendLayout();
            this.panelAcercaDe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOcultarPanel1)).BeginInit();
            this.contextNotify.SuspendLayout();
            this.contextEditarNota.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabsConfig, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.notaUC1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1111, 1012);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tabsConfig
            // 
            this.tabsConfig.Controls.Add(this.tabNotas);
            this.tabsConfig.Controls.Add(this.tabGrupos);
            this.tabsConfig.Controls.Add(this.tabEditarGrupos);
            this.tabsConfig.Controls.Add(this.tabBuscarTexto);
            this.tabsConfig.Controls.Add(this.tabOpciones);
            this.tabsConfig.Controls.Add(this.tabAcercaDe);
            this.tabsConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabsConfig.Location = new System.Drawing.Point(3, 3);
            this.tabsConfig.Name = "tabsConfig";
            this.tabsConfig.SelectedIndex = 0;
            this.tabsConfig.Size = new System.Drawing.Size(1105, 398);
            this.tabsConfig.TabIndex = 1;
            this.tabsConfig.SelectedIndexChanged += new System.EventHandler(this.tabsConfig_SelectedIndexChanged);
            // 
            // tabNotas
            // 
            this.tabNotas.Controls.Add(this.NotasFlowLayoutPanel);
            this.tabNotas.Location = new System.Drawing.Point(4, 34);
            this.tabNotas.Name = "tabNotas";
            this.tabNotas.Padding = new System.Windows.Forms.Padding(3);
            this.tabNotas.Size = new System.Drawing.Size(1097, 360);
            this.tabNotas.TabIndex = 0;
            this.tabNotas.Text = "Notas";
            this.tabNotas.UseVisualStyleBackColor = true;
            // 
            // NotasFlowLayoutPanel
            // 
            this.NotasFlowLayoutPanel.AutoScroll = true;
            this.NotasFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.NotasFlowLayoutPanel.Controls.Add(this.LblNota);
            this.NotasFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotasFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.NotasFlowLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.NotasFlowLayoutPanel.Name = "NotasFlowLayoutPanel";
            this.NotasFlowLayoutPanel.Size = new System.Drawing.Size(1091, 354);
            this.NotasFlowLayoutPanel.TabIndex = 2;
            this.NotasFlowLayoutPanel.WrapContents = false;
            this.NotasFlowLayoutPanel.Resize += new System.EventHandler(this.NotasFlowLayoutPanel_Resize);
            // 
            // LblNota
            // 
            this.LblNota.BackColor = System.Drawing.Color.MistyRose;
            this.LblNota.Location = new System.Drawing.Point(3, 0);
            this.LblNota.Name = "LblNota";
            this.LblNota.Size = new System.Drawing.Size(237, 32);
            this.LblNota.TabIndex = 0;
            this.LblNota.Text = "LabelNota";
            // 
            // tabGrupos
            // 
            this.tabGrupos.Controls.Add(this.GruposFlowLayoutPanel);
            this.tabGrupos.Location = new System.Drawing.Point(4, 34);
            this.tabGrupos.Name = "tabGrupos";
            this.tabGrupos.Padding = new System.Windows.Forms.Padding(3);
            this.tabGrupos.Size = new System.Drawing.Size(1097, 360);
            this.tabGrupos.TabIndex = 1;
            this.tabGrupos.Text = "Grupos";
            this.tabGrupos.UseVisualStyleBackColor = true;
            // 
            // GruposFlowLayoutPanel
            // 
            this.GruposFlowLayoutPanel.AutoScroll = true;
            this.GruposFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GruposFlowLayoutPanel.Controls.Add(this.LblGrupo);
            this.GruposFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GruposFlowLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.GruposFlowLayoutPanel.Name = "GruposFlowLayoutPanel";
            this.GruposFlowLayoutPanel.Size = new System.Drawing.Size(1091, 354);
            this.GruposFlowLayoutPanel.TabIndex = 0;
            // 
            // LblGrupo
            // 
            this.LblGrupo.BackColor = System.Drawing.Color.OldLace;
            this.LblGrupo.Location = new System.Drawing.Point(3, 0);
            this.LblGrupo.Name = "LblGrupo";
            this.LblGrupo.Size = new System.Drawing.Size(237, 92);
            this.LblGrupo.TabIndex = 1;
            this.LblGrupo.Text = "LblGrupo";
            // 
            // tabEditarGrupos
            // 
            this.tabEditarGrupos.Controls.Add(this.panelEditarGrupos);
            this.tabEditarGrupos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.tabEditarGrupos.Location = new System.Drawing.Point(4, 34);
            this.tabEditarGrupos.Name = "tabEditarGrupos";
            this.tabEditarGrupos.Padding = new System.Windows.Forms.Padding(3);
            this.tabEditarGrupos.Size = new System.Drawing.Size(1097, 360);
            this.tabEditarGrupos.TabIndex = 2;
            this.tabEditarGrupos.Text = "Editar grupos y notas";
            this.tabEditarGrupos.UseVisualStyleBackColor = true;
            // 
            // panelEditarGrupos
            // 
            this.panelEditarGrupos.AutoScroll = true;
            this.panelEditarGrupos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelEditarGrupos.Controls.Add(this.btnCrearGrupo);
            this.panelEditarGrupos.Controls.Add(this.txtEdNuevoNombreGrupo);
            this.panelEditarGrupos.Controls.Add(this.label3);
            this.panelEditarGrupos.Controls.Add(this.btnClasificarGrupos);
            this.panelEditarGrupos.Controls.Add(this.groupBox2);
            this.panelEditarGrupos.Controls.Add(this.lblEdInfo);
            this.panelEditarGrupos.Controls.Add(this.btnBorrar);
            this.panelEditarGrupos.Controls.Add(this.btnCambiarNombre);
            this.panelEditarGrupos.Controls.Add(this.txtEdNombreGrupo);
            this.panelEditarGrupos.Controls.Add(this.lblEdCambiar);
            this.panelEditarGrupos.Controls.Add(this.cboEdGrupos);
            this.panelEditarGrupos.Controls.Add(this.lblEdSeleccionar);
            this.panelEditarGrupos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEditarGrupos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.panelEditarGrupos.Location = new System.Drawing.Point(3, 3);
            this.panelEditarGrupos.Name = "panelEditarGrupos";
            this.panelEditarGrupos.Padding = new System.Windows.Forms.Padding(3);
            this.panelEditarGrupos.Size = new System.Drawing.Size(1091, 354);
            this.panelEditarGrupos.TabIndex = 0;
            this.panelEditarGrupos.Text = "Editar los grupos";
            // 
            // btnCrearGrupo
            // 
            this.btnCrearGrupo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.btnCrearGrupo.ForeColor = System.Drawing.Color.White;
            this.btnCrearGrupo.Location = new System.Drawing.Point(415, 141);
            this.btnCrearGrupo.Name = "btnCrearGrupo";
            this.btnCrearGrupo.Size = new System.Drawing.Size(112, 40);
            this.btnCrearGrupo.TabIndex = 10;
            this.btnCrearGrupo.Text = "Crear";
            this.btnCrearGrupo.UseVisualStyleBackColor = false;
            this.btnCrearGrupo.Click += new System.EventHandler(this.btnCrearGrupo_Click);
            // 
            // txtEdNuevoNombreGrupo
            // 
            this.txtEdNuevoNombreGrupo.Location = new System.Drawing.Point(203, 144);
            this.txtEdNuevoNombreGrupo.Name = "txtEdNuevoNombreGrupo";
            this.txtEdNuevoNombreGrupo.Size = new System.Drawing.Size(206, 31);
            this.txtEdNuevoNombreGrupo.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 149);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 29);
            this.label3.TabIndex = 8;
            this.label3.Text = "Crear grupo:";
            // 
            // btnClasificarGrupos
            // 
            this.btnClasificarGrupos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.btnClasificarGrupos.ForeColor = System.Drawing.Color.White;
            this.btnClasificarGrupos.Location = new System.Drawing.Point(203, 80);
            this.btnClasificarGrupos.Name = "btnClasificarGrupos";
            this.btnClasificarGrupos.Size = new System.Drawing.Size(193, 40);
            this.btnClasificarGrupos.TabIndex = 7;
            this.btnClasificarGrupos.Text = "Clasificar los grupos";
            this.btnClasificarGrupos.UseVisualStyleBackColor = false;
            this.btnClasificarGrupos.Click += new System.EventHandler(this.btnClasificarGrupos_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboEdGrupoDestino);
            this.groupBox2.Controls.Add(this.btnMoverNota);
            this.groupBox2.Controls.Add(this.cboEdNotas);
            this.groupBox2.Controls.Add(this.cboEdGrupoNotas);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 214);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1079, 134);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Editar las notas";
            // 
            // cboEdGrupoDestino
            // 
            this.cboEdGrupoDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEdGrupoDestino.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboEdGrupoDestino.FormattingEnabled = true;
            this.cboEdGrupoDestino.Location = new System.Drawing.Point(868, 27);
            this.cboEdGrupoDestino.Name = "cboEdGrupoDestino";
            this.cboEdGrupoDestino.Size = new System.Drawing.Size(200, 33);
            this.cboEdGrupoDestino.TabIndex = 4;
            // 
            // btnMoverNota
            // 
            this.btnMoverNota.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.btnMoverNota.ForeColor = System.Drawing.Color.White;
            this.btnMoverNota.Location = new System.Drawing.Point(750, 22);
            this.btnMoverNota.Name = "btnMoverNota";
            this.btnMoverNota.Size = new System.Drawing.Size(112, 40);
            this.btnMoverNota.TabIndex = 3;
            this.btnMoverNota.Text = "Mover a";
            this.btnMoverNota.UseVisualStyleBackColor = false;
            this.btnMoverNota.Click += new System.EventHandler(this.btnMoverNota_Click);
            // 
            // cboEdNotas
            // 
            this.cboEdNotas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEdNotas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboEdNotas.FormattingEnabled = true;
            this.cboEdNotas.Location = new System.Drawing.Point(474, 27);
            this.cboEdNotas.Name = "cboEdNotas";
            this.cboEdNotas.Size = new System.Drawing.Size(270, 33);
            this.cboEdNotas.TabIndex = 2;
            // 
            // cboEdGrupoNotas
            // 
            this.cboEdGrupoNotas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEdGrupoNotas.FormattingEnabled = true;
            this.cboEdGrupoNotas.Location = new System.Drawing.Point(262, 27);
            this.cboEdGrupoNotas.Name = "cboEdGrupoNotas";
            this.cboEdGrupoNotas.Size = new System.Drawing.Size(206, 33);
            this.cboEdGrupoNotas.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Selecciona la nota a mover:";
            // 
            // lblEdInfo
            // 
            this.lblEdInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.lblEdInfo.ForeColor = System.Drawing.Color.White;
            this.lblEdInfo.Location = new System.Drawing.Point(553, 67);
            this.lblEdInfo.Margin = new System.Windows.Forms.Padding(3);
            this.lblEdInfo.Name = "lblEdInfo";
            this.lblEdInfo.Size = new System.Drawing.Size(527, 113);
            this.lblEdInfo.TabIndex = 6;
            this.lblEdInfo.Text = "Si indicas un nombre existente, las notas se moverán a ese grupo.\r\nEn los nombres" +
    " de los grupos se distinguen mayúsculas de minúsculas.\r\n";
            // 
            // btnBorrar
            // 
            this.btnBorrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.btnBorrar.ForeColor = System.Drawing.Color.White;
            this.btnBorrar.Location = new System.Drawing.Point(415, 16);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(112, 40);
            this.btnBorrar.TabIndex = 2;
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.UseVisualStyleBackColor = false;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // btnCambiarNombre
            // 
            this.btnCambiarNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.btnCambiarNombre.ForeColor = System.Drawing.Color.White;
            this.btnCambiarNombre.Location = new System.Drawing.Point(968, 15);
            this.btnCambiarNombre.Name = "btnCambiarNombre";
            this.btnCambiarNombre.Size = new System.Drawing.Size(112, 40);
            this.btnCambiarNombre.TabIndex = 5;
            this.btnCambiarNombre.Text = "Cambiar";
            this.btnCambiarNombre.UseVisualStyleBackColor = false;
            this.btnCambiarNombre.Click += new System.EventHandler(this.btnCambiarNombre_Click);
            // 
            // txtEdNombreGrupo
            // 
            this.txtEdNombreGrupo.Location = new System.Drawing.Point(734, 20);
            this.txtEdNombreGrupo.Name = "txtEdNombreGrupo";
            this.txtEdNombreGrupo.Size = new System.Drawing.Size(228, 31);
            this.txtEdNombreGrupo.TabIndex = 4;
            // 
            // lblEdCambiar
            // 
            this.lblEdCambiar.Location = new System.Drawing.Point(553, 23);
            this.lblEdCambiar.Margin = new System.Windows.Forms.Padding(3);
            this.lblEdCambiar.Name = "lblEdCambiar";
            this.lblEdCambiar.Size = new System.Drawing.Size(175, 29);
            this.lblEdCambiar.TabIndex = 3;
            this.lblEdCambiar.Text = "Cambiar nombre:";
            // 
            // cboEdGrupos
            // 
            this.cboEdGrupos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEdGrupos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboEdGrupos.FormattingEnabled = true;
            this.cboEdGrupos.Location = new System.Drawing.Point(203, 21);
            this.cboEdGrupos.Name = "cboEdGrupos";
            this.cboEdGrupos.Size = new System.Drawing.Size(206, 33);
            this.cboEdGrupos.TabIndex = 1;
            // 
            // lblEdSeleccionar
            // 
            this.lblEdSeleccionar.Location = new System.Drawing.Point(6, 24);
            this.lblEdSeleccionar.Margin = new System.Windows.Forms.Padding(3);
            this.lblEdSeleccionar.Name = "lblEdSeleccionar";
            this.lblEdSeleccionar.Size = new System.Drawing.Size(191, 29);
            this.lblEdSeleccionar.TabIndex = 0;
            this.lblEdSeleccionar.Text = "Selecciona el grupo:";
            // 
            // tabBuscarTexto
            // 
            this.tabBuscarTexto.BackColor = System.Drawing.Color.White;
            this.tabBuscarTexto.Controls.Add(this.panelBuscarTexto);
            this.tabBuscarTexto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.tabBuscarTexto.Location = new System.Drawing.Point(4, 34);
            this.tabBuscarTexto.Name = "tabBuscarTexto";
            this.tabBuscarTexto.Padding = new System.Windows.Forms.Padding(3);
            this.tabBuscarTexto.Size = new System.Drawing.Size(1097, 360);
            this.tabBuscarTexto.TabIndex = 3;
            this.tabBuscarTexto.Text = "Buscar texto";
            this.tabBuscarTexto.UseVisualStyleBackColor = true;
            // 
            // panelBuscarTexto
            // 
            this.panelBuscarTexto.AutoScroll = true;
            this.panelBuscarTexto.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelBuscarTexto.Controls.Add(this.lblResultadoBuscar);
            this.panelBuscarTexto.Controls.Add(this.lblBuscando);
            this.panelBuscarTexto.Controls.Add(this.lstResultadoBuscar);
            this.panelBuscarTexto.Controls.Add(this.chkBuscarEnGrupoActual);
            this.panelBuscarTexto.Controls.Add(this.btnBuscar);
            this.panelBuscarTexto.Controls.Add(this.txtBuscar);
            this.panelBuscarTexto.Controls.Add(this.lblBuscar);
            this.panelBuscarTexto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBuscarTexto.Location = new System.Drawing.Point(3, 3);
            this.panelBuscarTexto.Name = "panelBuscarTexto";
            this.panelBuscarTexto.Padding = new System.Windows.Forms.Padding(3);
            this.panelBuscarTexto.Size = new System.Drawing.Size(1091, 354);
            this.panelBuscarTexto.TabIndex = 0;
            // 
            // lblResultadoBuscar
            // 
            this.lblResultadoBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.lblResultadoBuscar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblResultadoBuscar.ForeColor = System.Drawing.Color.White;
            this.lblResultadoBuscar.Location = new System.Drawing.Point(627, 21);
            this.lblResultadoBuscar.Margin = new System.Windows.Forms.Padding(3);
            this.lblResultadoBuscar.Name = "lblResultadoBuscar";
            this.lblResultadoBuscar.Size = new System.Drawing.Size(458, 35);
            this.lblResultadoBuscar.TabIndex = 4;
            this.lblResultadoBuscar.Text = "Resultado de la búsqueda:";
            // 
            // lblBuscando
            // 
            this.lblBuscando.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.lblBuscando.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBuscando.ForeColor = System.Drawing.Color.White;
            this.lblBuscando.Location = new System.Drawing.Point(8, 313);
            this.lblBuscando.Margin = new System.Windows.Forms.Padding(3);
            this.lblBuscando.Name = "lblBuscando";
            this.lblBuscando.Size = new System.Drawing.Size(525, 35);
            this.lblBuscando.TabIndex = 6;
            this.lblBuscando.Text = "Buscando...";
            // 
            // lstResultadoBuscar
            // 
            this.lstResultadoBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.lstResultadoBuscar.FormattingEnabled = true;
            this.lstResultadoBuscar.ItemHeight = 25;
            this.lstResultadoBuscar.Location = new System.Drawing.Point(627, 66);
            this.lstResultadoBuscar.Name = "lstResultadoBuscar";
            this.lstResultadoBuscar.Size = new System.Drawing.Size(458, 254);
            this.lstResultadoBuscar.Sorted = true;
            this.lstResultadoBuscar.TabIndex = 5;
            this.lstResultadoBuscar.DoubleClick += new System.EventHandler(this.lstResultadoBuscar_DoubleClick);
            // 
            // chkBuscarEnGrupoActual
            // 
            this.chkBuscarEnGrupoActual.AutoSize = true;
            this.chkBuscarEnGrupoActual.Location = new System.Drawing.Point(190, 69);
            this.chkBuscarEnGrupoActual.Name = "chkBuscarEnGrupoActual";
            this.chkBuscarEnGrupoActual.Size = new System.Drawing.Size(361, 29);
            this.chkBuscarEnGrupoActual.TabIndex = 3;
            this.chkBuscarEnGrupoActual.Text = "Buscar solo en las notas del grupo actual";
            this.chkBuscarEnGrupoActual.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(421, 16);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(112, 40);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(187, 21);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(228, 31);
            this.txtBuscar.TabIndex = 1;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Location = new System.Drawing.Point(6, 24);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(3);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(175, 29);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Cambiar nombre:";
            // 
            // tabOpciones
            // 
            this.tabOpciones.AutoScroll = true;
            this.tabOpciones.BackColor = System.Drawing.Color.White;
            this.tabOpciones.Controls.Add(this.panelOpciones);
            this.tabOpciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.tabOpciones.Location = new System.Drawing.Point(4, 34);
            this.tabOpciones.Name = "tabOpciones";
            this.tabOpciones.Padding = new System.Windows.Forms.Padding(3);
            this.tabOpciones.Size = new System.Drawing.Size(1097, 360);
            this.tabOpciones.TabIndex = 4;
            this.tabOpciones.Text = "Opciones";
            this.tabOpciones.UseVisualStyleBackColor = true;
            // 
            // panelOpciones
            // 
            this.panelOpciones.AutoScroll = true;
            this.panelOpciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelOpciones.Controls.Add(this.flowLayoutPanel1);
            this.panelOpciones.Controls.Add(this.OpcCboColorGrupo);
            this.panelOpciones.Controls.Add(this.label4);
            this.panelOpciones.Controls.Add(this.OpcChkIniciarConWindows);
            this.panelOpciones.Controls.Add(this.OpcBtnGuardarEnDrive);
            this.panelOpciones.Controls.Add(this.OpcLinkSolicitarAutorización);
            this.panelOpciones.Controls.Add(this.OpcChkMostrarHorizontal);
            this.panelOpciones.Controls.Add(this.OpcChkOcultarPanelSuperior);
            this.panelOpciones.Controls.Add(this.OpcChkMostrarMismoGrupo);
            this.panelOpciones.Controls.Add(this.OpcChkIniciarMinimizada);
            this.panelOpciones.Controls.Add(this.OpcBtnDeshacer);
            this.panelOpciones.Controls.Add(this.OpcBtnGuardar);
            this.panelOpciones.Controls.Add(this.OpcChkMinimizarAlCerrar);
            this.panelOpciones.Controls.Add(this.OpcChkAjusteLineas);
            this.panelOpciones.Controls.Add(this.OpcBtnRestablecerTam);
            this.panelOpciones.Controls.Add(this.OpcChkRecordarTam);
            this.panelOpciones.Controls.Add(this.OpChkNoGuardarEnBlanco);
            this.panelOpciones.Controls.Add(this.OpcChkAutoGuardar);
            this.panelOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.panelOpciones.Location = new System.Drawing.Point(3, 3);
            this.panelOpciones.Name = "panelOpciones";
            this.panelOpciones.Padding = new System.Windows.Forms.Padding(3);
            this.panelOpciones.Size = new System.Drawing.Size(1091, 354);
            this.panelOpciones.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(597, 224);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(479, 124);
            this.flowLayoutPanel1.TabIndex = 15;
            // 
            // OpcCboColorGrupo
            // 
            this.OpcCboColorGrupo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OpcCboColorGrupo.FormattingEnabled = true;
            this.OpcCboColorGrupo.Items.AddRange(new object[] {
            "Aleatorio",
            "Predeterminado",
            "Colores 1",
            "Colores 2",
            "Colores 3"});
            this.OpcCboColorGrupo.Location = new System.Drawing.Point(794, 185);
            this.OpcCboColorGrupo.Name = "OpcCboColorGrupo";
            this.OpcCboColorGrupo.Size = new System.Drawing.Size(206, 33);
            this.OpcCboColorGrupo.TabIndex = 14;
            this.OpcCboColorGrupo.SelectedIndexChanged += new System.EventHandler(this.OpcCboColorGrupo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(597, 189);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 29);
            this.label4.TabIndex = 13;
            this.label4.Text = "Colores grupo:";
            this.toolTip1.SetToolTip(this.label4, "Pulsa en un color para cambiarlo");
            // 
            // OpcChkIniciarConWindows
            // 
            this.OpcChkIniciarConWindows.AutoSize = true;
            this.OpcChkIniciarConWindows.Location = new System.Drawing.Point(9, 324);
            this.OpcChkIniciarConWindows.Name = "OpcChkIniciarConWindows";
            this.OpcChkIniciarConWindows.Size = new System.Drawing.Size(197, 29);
            this.OpcChkIniciarConWindows.TabIndex = 9;
            this.OpcChkIniciarConWindows.Text = "Iniciar con Windows";
            this.toolTip1.SetToolTip(this.OpcChkIniciarConWindows, "Debes ejecutar la aplicación con provilegios de administrador para que surta efec" +
        "to.");
            this.OpcChkIniciarConWindows.UseVisualStyleBackColor = true;
            // 
            // OpcBtnGuardarEnDrive
            // 
            this.OpcBtnGuardarEnDrive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.OpcBtnGuardarEnDrive.ForeColor = System.Drawing.Color.White;
            this.OpcBtnGuardarEnDrive.Location = new System.Drawing.Point(677, 139);
            this.OpcBtnGuardarEnDrive.Name = "OpcBtnGuardarEnDrive";
            this.OpcBtnGuardarEnDrive.Size = new System.Drawing.Size(346, 40);
            this.OpcBtnGuardarEnDrive.TabIndex = 12;
            this.OpcBtnGuardarEnDrive.Text = "Guardar las notas en Google Drive";
            this.OpcBtnGuardarEnDrive.UseVisualStyleBackColor = false;
            this.OpcBtnGuardarEnDrive.Click += new System.EventHandler(this.OpcBtnGuardarEnDrive_Click);
            // 
            // OpcLinkSolicitarAutorización
            // 
            this.OpcLinkSolicitarAutorización.AutoSize = true;
            this.OpcLinkSolicitarAutorización.Location = new System.Drawing.Point(677, 108);
            this.OpcLinkSolicitarAutorización.Margin = new System.Windows.Forms.Padding(3);
            this.OpcLinkSolicitarAutorización.Name = "OpcLinkSolicitarAutorización";
            this.OpcLinkSolicitarAutorización.Size = new System.Drawing.Size(175, 25);
            this.OpcLinkSolicitarAutorización.TabIndex = 11;
            this.OpcLinkSolicitarAutorización.TabStop = true;
            this.OpcLinkSolicitarAutorización.Text = "Solicitar autorización";
            this.toolTip1.SetToolTip(this.OpcLinkSolicitarAutorización, "Pide al Guille que te incluya en las cuentas autorizadas");
            this.OpcLinkSolicitarAutorización.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpcLinkSolicitarAutorización_LinkClicked);
            // 
            // OpcChkMostrarHorizontal
            // 
            this.OpcChkMostrarHorizontal.AutoSize = true;
            this.OpcChkMostrarHorizontal.Location = new System.Drawing.Point(9, 254);
            this.OpcChkMostrarHorizontal.Name = "OpcChkMostrarHorizontal";
            this.OpcChkMostrarHorizontal.Size = new System.Drawing.Size(309, 29);
            this.OpcChkMostrarHorizontal.TabIndex = 7;
            this.OpcChkMostrarHorizontal.Text = "Mostrar las notas horizontalmente";
            this.OpcChkMostrarHorizontal.UseVisualStyleBackColor = true;
            // 
            // OpcChkOcultarPanelSuperior
            // 
            this.OpcChkOcultarPanelSuperior.AutoSize = true;
            this.OpcChkOcultarPanelSuperior.Location = new System.Drawing.Point(9, 184);
            this.OpcChkOcultarPanelSuperior.Name = "OpcChkOcultarPanelSuperior";
            this.OpcChkOcultarPanelSuperior.Size = new System.Drawing.Size(400, 29);
            this.OpcChkOcultarPanelSuperior.TabIndex = 5;
            this.OpcChkOcultarPanelSuperior.Text = "Al iniciar la aplicación ocultar el panel superior";
            this.OpcChkOcultarPanelSuperior.UseVisualStyleBackColor = true;
            // 
            // OpcChkMostrarMismoGrupo
            // 
            this.OpcChkMostrarMismoGrupo.AutoSize = true;
            this.OpcChkMostrarMismoGrupo.Location = new System.Drawing.Point(9, 114);
            this.OpcChkMostrarMismoGrupo.Name = "OpcChkMostrarMismoGrupo";
            this.OpcChkMostrarMismoGrupo.Size = new System.Drawing.Size(487, 29);
            this.OpcChkMostrarMismoGrupo.TabIndex = 3;
            this.OpcChkMostrarMismoGrupo.Text = "Al iniciar la aplicación mostrar el mismo grupo que había";
            this.OpcChkMostrarMismoGrupo.UseVisualStyleBackColor = true;
            // 
            // OpcChkIniciarMinimizada
            // 
            this.OpcChkIniciarMinimizada.AutoSize = true;
            this.OpcChkIniciarMinimizada.Location = new System.Drawing.Point(9, 149);
            this.OpcChkIniciarMinimizada.Name = "OpcChkIniciarMinimizada";
            this.OpcChkIniciarMinimizada.Size = new System.Drawing.Size(361, 29);
            this.OpcChkIniciarMinimizada.TabIndex = 4;
            this.OpcChkIniciarMinimizada.Text = "Al inicar la aplicación hacerlo minimizado";
            this.OpcChkIniciarMinimizada.UseVisualStyleBackColor = true;
            // 
            // OpcBtnDeshacer
            // 
            this.OpcBtnDeshacer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.OpcBtnDeshacer.ForeColor = System.Drawing.Color.White;
            this.OpcBtnDeshacer.Location = new System.Drawing.Point(846, 6);
            this.OpcBtnDeshacer.Name = "OpcBtnDeshacer";
            this.OpcBtnDeshacer.Size = new System.Drawing.Size(112, 50);
            this.OpcBtnDeshacer.TabIndex = 16;
            this.OpcBtnDeshacer.Text = "Deshacer";
            this.OpcBtnDeshacer.UseVisualStyleBackColor = false;
            this.OpcBtnDeshacer.Click += new System.EventHandler(this.OpcBtnDeshacer_Click);
            // 
            // OpcBtnGuardar
            // 
            this.OpcBtnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.OpcBtnGuardar.ForeColor = System.Drawing.Color.White;
            this.OpcBtnGuardar.Location = new System.Drawing.Point(964, 6);
            this.OpcBtnGuardar.Name = "OpcBtnGuardar";
            this.OpcBtnGuardar.Size = new System.Drawing.Size(112, 50);
            this.OpcBtnGuardar.TabIndex = 17;
            this.OpcBtnGuardar.Text = "Guardar";
            this.OpcBtnGuardar.UseVisualStyleBackColor = false;
            this.OpcBtnGuardar.Click += new System.EventHandler(this.OpcBtnGuardar_Click);
            // 
            // OpcChkMinimizarAlCerrar
            // 
            this.OpcChkMinimizarAlCerrar.AutoSize = true;
            this.OpcChkMinimizarAlCerrar.Location = new System.Drawing.Point(9, 289);
            this.OpcChkMinimizarAlCerrar.Name = "OpcChkMinimizarAlCerrar";
            this.OpcChkMinimizarAlCerrar.Size = new System.Drawing.Size(531, 29);
            this.OpcChkMinimizarAlCerrar.TabIndex = 8;
            this.OpcChkMinimizarAlCerrar.Text = "Al cerrar (desde X de la ventana) minimizar en el área de tareas";
            this.OpcChkMinimizarAlCerrar.UseVisualStyleBackColor = true;
            // 
            // OpcChkAjusteLineas
            // 
            this.OpcChkAjusteLineas.AutoSize = true;
            this.OpcChkAjusteLineas.Location = new System.Drawing.Point(9, 219);
            this.OpcChkAjusteLineas.Name = "OpcChkAjusteLineas";
            this.OpcChkAjusteLineas.Size = new System.Drawing.Size(263, 29);
            this.OpcChkAjusteLineas.TabIndex = 6;
            this.OpcChkAjusteLineas.Text = "Ajuste de líneas (WordWrap)";
            this.OpcChkAjusteLineas.UseVisualStyleBackColor = true;
            // 
            // OpcBtnRestablecerTam
            // 
            this.OpcBtnRestablecerTam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.OpcBtnRestablecerTam.ForeColor = System.Drawing.Color.White;
            this.OpcBtnRestablecerTam.Location = new System.Drawing.Point(677, 62);
            this.OpcBtnRestablecerTam.Name = "OpcBtnRestablecerTam";
            this.OpcBtnRestablecerTam.Size = new System.Drawing.Size(346, 40);
            this.OpcBtnRestablecerTam.TabIndex = 10;
            this.OpcBtnRestablecerTam.Text = "Restablecer tamaño y posición";
            this.toolTip1.SetToolTip(this.OpcBtnRestablecerTam, "Restablece (ahora) el tamaño y posición de la ventana");
            this.OpcBtnRestablecerTam.UseVisualStyleBackColor = false;
            this.OpcBtnRestablecerTam.Click += new System.EventHandler(this.OpcBtnRestablecerTam_Click);
            // 
            // OpcChkRecordarTam
            // 
            this.OpcChkRecordarTam.AutoSize = true;
            this.OpcChkRecordarTam.Location = new System.Drawing.Point(9, 79);
            this.OpcChkRecordarTam.Name = "OpcChkRecordarTam";
            this.OpcChkRecordarTam.Size = new System.Drawing.Size(538, 29);
            this.OpcChkRecordarTam.TabIndex = 2;
            this.OpcChkRecordarTam.Text = "Al iniciar la aplicación recordar posición y tamaño de la ventana";
            this.OpcChkRecordarTam.UseVisualStyleBackColor = true;
            // 
            // OpChkNoGuardarEnBlanco
            // 
            this.OpChkNoGuardarEnBlanco.AutoSize = true;
            this.OpChkNoGuardarEnBlanco.Checked = true;
            this.OpChkNoGuardarEnBlanco.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OpChkNoGuardarEnBlanco.Enabled = false;
            this.OpChkNoGuardarEnBlanco.Location = new System.Drawing.Point(9, 44);
            this.OpChkNoGuardarEnBlanco.Name = "OpChkNoGuardarEnBlanco";
            this.OpChkNoGuardarEnBlanco.Size = new System.Drawing.Size(260, 29);
            this.OpChkNoGuardarEnBlanco.TabIndex = 1;
            this.OpChkNoGuardarEnBlanco.Text = "No guardar notas en blanco";
            this.OpChkNoGuardarEnBlanco.UseVisualStyleBackColor = true;
            // 
            // OpcChkAutoGuardar
            // 
            this.OpcChkAutoGuardar.AutoSize = true;
            this.OpcChkAutoGuardar.Location = new System.Drawing.Point(9, 9);
            this.OpcChkAutoGuardar.Name = "OpcChkAutoGuardar";
            this.OpcChkAutoGuardar.Size = new System.Drawing.Size(545, 29);
            this.OpcChkAutoGuardar.TabIndex = 0;
            this.OpcChkAutoGuardar.Text = "Auto guardar las notas (se actualiza al cambiar de nota o grupo)";
            this.toolTip1.SetToolTip(this.OpcChkAutoGuardar, "Por ahora se crea una nueva nota en el grupo actual \r\n(cuando se cambia de grupo)" +
        " si el texto está modificado.\r\n");
            this.OpcChkAutoGuardar.UseVisualStyleBackColor = true;
            // 
            // tabAcercaDe
            // 
            this.tabAcercaDe.Controls.Add(this.panelAcercaDe);
            this.tabAcercaDe.Location = new System.Drawing.Point(4, 34);
            this.tabAcercaDe.Name = "tabAcercaDe";
            this.tabAcercaDe.Padding = new System.Windows.Forms.Padding(3);
            this.tabAcercaDe.Size = new System.Drawing.Size(1097, 360);
            this.tabAcercaDe.TabIndex = 5;
            this.tabAcercaDe.Text = "Acerca de";
            this.tabAcercaDe.UseVisualStyleBackColor = true;
            // 
            // panelAcercaDe
            // 
            this.panelAcercaDe.Controls.Add(this.txtAcercaDe);
            this.panelAcercaDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAcercaDe.Location = new System.Drawing.Point(3, 3);
            this.panelAcercaDe.Name = "panelAcercaDe";
            this.panelAcercaDe.Size = new System.Drawing.Size(1091, 354);
            this.panelAcercaDe.TabIndex = 0;
            // 
            // txtAcercaDe
            // 
            this.txtAcercaDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAcercaDe.Location = new System.Drawing.Point(0, 0);
            this.txtAcercaDe.Multiline = true;
            this.txtAcercaDe.Name = "txtAcercaDe";
            this.txtAcercaDe.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAcercaDe.Size = new System.Drawing.Size(1091, 354);
            this.txtAcercaDe.TabIndex = 0;
            // 
            // notaUC1
            // 
            this.notaUC1.AllowDrop = true;
            this.notaUC1.ColoresClaro = new System.Drawing.Color[] {
        System.Drawing.Color.White,
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))))};
            this.notaUC1.ColoresOscuro = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(166)))), ((int)(((byte)(58)))))};
            this.notaUC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notaUC1.EditorRtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Consolas;}" +
    "}\r\n{\\colortbl ;\\red0\\green99\\blue177;}\r\n{\\*\\generator Riched20 10.0.22000}\\viewk" +
    "ind4\\uc1 \r\n\\pard\\cf1\\f0\\fs20\\par\r\n}\r\n";
            this.notaUC1.EspCaracteres = new string[] {
        "\n\r",
        "\n",
        "\r",
        "\"",
        "<",
        ">",
        "&"};
            this.notaUC1.EspMarcas = new string[] {
        "|NL|",
        "|CR|",
        "|LF|",
        "|quot|",
        "|lt|",
        "|gt|",
        "|A|"};
            this.notaUC1.Grupo = "";
            this.notaUC1.InvertirColores = false;
            this.notaUC1.Location = new System.Drawing.Point(5, 410);
            this.notaUC1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.notaUC1.MinimumSize = new System.Drawing.Size(667, 385);
            this.notaUC1.Name = "notaUC1";
            this.notaUC1.Nota = "";
            this.notaUC1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.notaUC1.Size = new System.Drawing.Size(1101, 596);
            this.notaUC1.StatusInfo = "Grupo: \'\' con 1 nota";
            this.notaUC1.TabIndex = 2;
            this.notaUC1.Titulo = "";
            this.notaUC1.TituloCabecera = "";
            this.notaUC1.BuscarTexto += new gsNotas.MensajeDelegate(this.notaUC1_BuscarTexto);
            this.notaUC1.TemaCambiado += new gsNotas.TemaCambiado(this.notaUC1_TemaCambiado);
            this.notaUC1.MenuCerrar += new gsNotas.MensajeDelegate(this.notaUC1_MenuCerrar);
            this.notaUC1.NotaReemplazada += new gsNotas.ReemplazarNota(this.notaUC1_NotaReemplazada);
            this.notaUC1.NotaCambiada += new gsNotas.TextoModificado(this.notaUC1_NotaCambiada);
            this.notaUC1.GrupoCambiado += new gsNotas.TextoModificado(this.notaUC1_GrupoCambiado);
            // 
            // picOcultarPanel1
            // 
            this.picOcultarPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picOcultarPanel1.Image = global::gsNotas.Properties.Resources.ExpanderDown;
            this.picOcultarPanel1.Location = new System.Drawing.Point(1082, 0);
            this.picOcultarPanel1.Name = "picOcultarPanel1";
            this.picOcultarPanel1.Size = new System.Drawing.Size(24, 24);
            this.picOcultarPanel1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picOcultarPanel1.TabIndex = 0;
            this.picOcultarPanel1.TabStop = false;
            this.picOcultarPanel1.Click += new System.EventHandler(this.picOcultarPanel1_Click);
            // 
            // timerInicio
            // 
            this.timerInicio.Tick += new System.EventHandler(this.timerInicio_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // contextNotify
            // 
            this.contextNotify.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NotifyMenuRestaurar,
            this.toolStripSeparator1,
            this.NotifyMenuCerrar});
            this.contextNotify.Name = "contextNotify";
            this.contextNotify.Size = new System.Drawing.Size(166, 74);
            // 
            // NotifyMenuRestaurar
            // 
            this.NotifyMenuRestaurar.Image = ((System.Drawing.Image)(resources.GetObject("NotifyMenuRestaurar.Image")));
            this.NotifyMenuRestaurar.Name = "NotifyMenuRestaurar";
            this.NotifyMenuRestaurar.Size = new System.Drawing.Size(165, 32);
            this.NotifyMenuRestaurar.Text = "Restaurar";
            this.NotifyMenuRestaurar.Click += new System.EventHandler(this.MnuNotifyRestaurar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // NotifyMenuCerrar
            // 
            this.NotifyMenuCerrar.Image = ((System.Drawing.Image)(resources.GetObject("NotifyMenuCerrar.Image")));
            this.NotifyMenuCerrar.Name = "NotifyMenuCerrar";
            this.NotifyMenuCerrar.Size = new System.Drawing.Size(165, 32);
            this.NotifyMenuCerrar.Text = "Cerrar";
            this.NotifyMenuCerrar.Click += new System.EventHandler(this.MnuNotifyCerrar_Click);
            // 
            // contextEditarNota
            // 
            this.contextEditarNota.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextEditarNota.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditarEnVentanaSeparada});
            this.contextEditarNota.Name = "contextEditarNota";
            this.contextEditarNota.Size = new System.Drawing.Size(342, 36);
            this.contextEditarNota.Opening += new System.ComponentModel.CancelEventHandler(this.contextEditarNota_Opening);
            // 
            // mnuEditarEnVentanaSeparada
            // 
            this.mnuEditarEnVentanaSeparada.Name = "mnuEditarEnVentanaSeparada";
            this.mnuEditarEnVentanaSeparada.Size = new System.Drawing.Size(341, 32);
            this.mnuEditarEnVentanaSeparada.Text = "Editar en ventana de tamaño fijo";
            this.mnuEditarEnVentanaSeparada.Click += new System.EventHandler(this.mnu_EditarEnVentana);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 1012);
            this.Controls.Add(this.picOcultarPanel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPrincipal";
            this.Text = "gsNotas - Gestionar notas y grupos de notas usando NotaUC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNotasUC_FormClosing);
            this.Load += new System.EventHandler(this.FormNotasUC_Load);
            this.LocationChanged += new System.EventHandler(this.FormNotasUC_LocationChanged);
            this.Resize += new System.EventHandler(this.FormNotasUC_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabsConfig.ResumeLayout(false);
            this.tabNotas.ResumeLayout(false);
            this.NotasFlowLayoutPanel.ResumeLayout(false);
            this.tabGrupos.ResumeLayout(false);
            this.GruposFlowLayoutPanel.ResumeLayout(false);
            this.tabEditarGrupos.ResumeLayout(false);
            this.panelEditarGrupos.ResumeLayout(false);
            this.panelEditarGrupos.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabBuscarTexto.ResumeLayout(false);
            this.panelBuscarTexto.ResumeLayout(false);
            this.panelBuscarTexto.PerformLayout();
            this.tabOpciones.ResumeLayout(false);
            this.panelOpciones.ResumeLayout(false);
            this.panelOpciones.PerformLayout();
            this.tabAcercaDe.ResumeLayout(false);
            this.panelAcercaDe.ResumeLayout(false);
            this.panelAcercaDe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOcultarPanel1)).EndInit();
            this.contextNotify.ResumeLayout(false);
            this.contextEditarNota.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabsConfig;
        private System.Windows.Forms.TabPage tabNotas;
        private System.Windows.Forms.PictureBox picOcultarPanel1;
        private System.Windows.Forms.TabPage tabGrupos;
        private System.Windows.Forms.TabPage tabEditarGrupos;
        private System.Windows.Forms.TabPage tabBuscarTexto;
        private System.Windows.Forms.TabPage tabOpciones;
        private System.Windows.Forms.TabPage tabAcercaDe;
        private System.Windows.Forms.TextBox txtAcercaDe;
        private System.Windows.Forms.FlowLayoutPanel GruposFlowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel NotasFlowLayoutPanel;
        private System.Windows.Forms.Timer timerInicio;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextNotify;
        private System.Windows.Forms.ToolStripMenuItem NotifyMenuRestaurar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem NotifyMenuCerrar;
        private System.Windows.Forms.Button btnCambiarNombre;
        private System.Windows.Forms.TextBox txtEdNombreGrupo;
        private System.Windows.Forms.Label lblEdCambiar;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.ComboBox cboEdGrupos;
        private System.Windows.Forms.Label lblEdSeleccionar;
        private System.Windows.Forms.Button btnCrearGrupo;
        private System.Windows.Forms.TextBox txtEdNuevoNombreGrupo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClasificarGrupos;
        private System.Windows.Forms.Label lblEdInfo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboEdGrupoDestino;
        private System.Windows.Forms.Button btnMoverNota;
        private System.Windows.Forms.ComboBox cboEdNotas;
        private System.Windows.Forms.ComboBox cboEdGrupoNotas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.CheckBox chkBuscarEnGrupoActual;
        private System.Windows.Forms.Label lblBuscando;
        private System.Windows.Forms.Label lblResultadoBuscar;
        private System.Windows.Forms.ListBox lstResultadoBuscar;
        private System.Windows.Forms.Button OpcBtnDeshacer;
        private System.Windows.Forms.CheckBox OpcChkAutoGuardar;
        private System.Windows.Forms.Button OpcBtnGuardar;
        private System.Windows.Forms.Button OpcBtnRestablecerTam;
        private System.Windows.Forms.Button OpcBtnGuardarEnDrive;
        private System.Windows.Forms.LinkLabel OpcLinkSolicitarAutorización;
        private System.Windows.Forms.CheckBox OpcChkRecordarTam;
        private System.Windows.Forms.CheckBox OpChkNoGuardarEnBlanco;
        private System.Windows.Forms.CheckBox OpcChkOcultarPanelSuperior;
        private System.Windows.Forms.CheckBox OpcChkMostrarMismoGrupo;
        private System.Windows.Forms.CheckBox OpcChkAjusteLineas;
        private System.Windows.Forms.CheckBox OpcChkMostrarHorizontal;
        private System.Windows.Forms.CheckBox OpcChkMinimizarAlCerrar;
        private System.Windows.Forms.CheckBox OpcChkIniciarConWindows;
        private System.Windows.Forms.ComboBox OpcCboColorGrupo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label LblNota;
        private System.Windows.Forms.Label LblGrupo;
        private System.Windows.Forms.CheckBox OpcChkIniciarMinimizada;
        private System.Windows.Forms.ContextMenuStrip contextEditarNota;
        private System.Windows.Forms.ToolStripMenuItem mnuEditarEnVentanaSeparada;
        private System.Windows.Forms.Panel panelAcercaDe;
        private System.Windows.Forms.Panel panelBuscarTexto;
        private System.Windows.Forms.Panel panelEditarGrupos;
        private System.Windows.Forms.Panel panelOpciones;
        private NotaUC notaUC1;
    }
}