namespace Seleccionar_Colores
{
    partial class FormSeleccionarColor
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
            this.wellPanel1 = new Seleccionar_Colores.WellPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.duSort = new System.Windows.Forms.DomainUpDown();
            this.lblBackColor = new System.Windows.Forms.Label();
            this.lblForeColor = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // wellPanel1
            // 
            this.wellPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wellPanel1.Color = System.Drawing.Color.Black;
            this.wellPanel1.ColorScheme = Seleccionar_Colores.WellPanel.Scheme.Web;
            this.wellPanel1.Columns = 0;
            this.wellPanel1.Location = new System.Drawing.Point(15, 57);
            this.wellPanel1.Margin = new System.Windows.Forms.Padding(6);
            this.wellPanel1.Name = "wellPanel1";
            this.wellPanel1.Size = new System.Drawing.Size(340, 116);
            this.wellPanel1.SortOrder = Seleccionar_Colores.WellPanel.Order.Hue;
            this.wellPanel1.TabIndex = 0;
            this.wellPanel1.WellSize = new System.Drawing.Size(16, 16);
            this.wellPanel1.ColorChanged += new Seleccionar_Colores.WellPanel.ColorChangedEventHandler(this.wellPanel1_ColorChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "Orden colores";
            // 
            // duSort
            // 
            this.duSort.Location = new System.Drawing.Point(168, 12);
            this.duSort.Name = "duSort";
            this.duSort.Size = new System.Drawing.Size(187, 31);
            this.duSort.TabIndex = 2;
            this.duSort.Text = "domainUpDown1";
            this.duSort.SelectedItemChanged += new System.EventHandler(this.duSort_SelectedItemChanged);
            // 
            // lblBackColor
            // 
            this.lblBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBackColor.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBackColor.Location = new System.Drawing.Point(15, 182);
            this.lblBackColor.Margin = new System.Windows.Forms.Padding(3);
            this.lblBackColor.Name = "lblBackColor";
            this.lblBackColor.Size = new System.Drawing.Size(167, 114);
            this.lblBackColor.TabIndex = 3;
            this.lblBackColor.Text = "Nombre color:\r\nvalor x\r\n";
            this.lblBackColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblForeColor
            // 
            this.lblForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblForeColor.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblForeColor.Location = new System.Drawing.Point(188, 182);
            this.lblForeColor.Margin = new System.Windows.Forms.Padding(3);
            this.lblForeColor.Name = "lblForeColor";
            this.lblForeColor.Size = new System.Drawing.Size(167, 114);
            this.lblForeColor.TabIndex = 4;
            this.lblForeColor.Text = "Nombre color:\r\nvalor x\r\n";
            this.lblForeColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(250, 350);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(112, 34);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(132, 350);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(112, 34);
            this.btnAceptar.TabIndex = 6;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // FormSeleccionarColor
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(374, 396);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblForeColor);
            this.Controls.Add(this.lblBackColor);
            this.Controls.Add(this.duSort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.wellPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSeleccionarColor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccionar Color";
            this.Load += new System.EventHandler(this.FormSeleccionarColor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private WellPanel wellPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DomainUpDown duSort;
        private System.Windows.Forms.Label lblBackColor;
        private System.Windows.Forms.Label lblForeColor;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
    }
}