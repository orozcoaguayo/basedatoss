namespace BaseDatos
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupNivel1 = new System.Windows.Forms.GroupBox();
            this.labelId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.labelNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.labelEdad = new System.Windows.Forms.Label();
            this.txtEdad = new System.Windows.Forms.TextBox();
            this.labelPos = new System.Windows.Forms.Label();
            this.numericPos = new System.Windows.Forms.NumericUpDown();
            this.btnGuardarBinario = new System.Windows.Forms.Button();
            this.labelBuscar = new System.Windows.Forms.Label();
            this.txtBuscarId = new System.Windows.Forms.TextBox();

            this.groupNivel2 = new System.Windows.Forms.GroupBox();
            this.btnGenerarIndice = new System.Windows.Forms.Button();
            this.btnCargarDesdeBinario = new System.Windows.Forms.Button();
            this.btnBuscarSecuencial = new System.Windows.Forms.Button();
            this.btnBuscarIndexado = new System.Windows.Forms.Button();
            this.labelTiempoSecuencial = new System.Windows.Forms.Label();
            this.labelTiempoIndexado = new System.Windows.Forms.Label();

            this.groupNivel3 = new System.Windows.Forms.GroupBox();
            this.labelCnx = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.btnMigrarSql = new System.Windows.Forms.Button();

            this.txtLog = new System.Windows.Forms.TextBox();

            // 
            // Form1
            // 
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Lab Datos - Práctica";

            // groupNivel1
            this.groupNivel1.Location = new System.Drawing.Point(12, 12);
            this.groupNivel1.Size = new System.Drawing.Size(380, 160);
            this.groupNivel1.Text = "Nivel 1 - El Escriba";
            // labelId
            this.labelId.Location = new System.Drawing.Point(12, 25);
            this.labelId.Size = new System.Drawing.Size(50, 20);
            this.labelId.Text = "Id:";
            // txtId
            this.txtId.Location = new System.Drawing.Point(70, 22);
            this.txtId.Size = new System.Drawing.Size(100, 20);
            // labelNombre
            this.labelNombre.Location = new System.Drawing.Point(12, 55);
            this.labelNombre.Size = new System.Drawing.Size(50, 20);
            this.labelNombre.Text = "Nombre:";
            // txtNombre
            this.txtNombre.Location = new System.Drawing.Point(70, 52);
            this.txtNombre.Size = new System.Drawing.Size(280, 20);
            // labelEdad
            this.labelEdad.Location = new System.Drawing.Point(12, 85);
            this.labelEdad.Size = new System.Drawing.Size(50, 20);
            this.labelEdad.Text = "Edad:";
            // txtEdad
            this.txtEdad.Location = new System.Drawing.Point(70, 82);
            this.txtEdad.Size = new System.Drawing.Size(60, 20);
            // labelPos
            this.labelPos.Location = new System.Drawing.Point(12, 115);
            this.labelPos.Size = new System.Drawing.Size(50, 20);
            this.labelPos.Text = "Posición:";
            // numericPos
            this.numericPos.Location = new System.Drawing.Point(70, 112);
            this.numericPos.Size = new System.Drawing.Size(80, 20);
            this.numericPos.Minimum = 0;
            this.numericPos.Maximum = 1000000;
            // btnGuardarBinario
            this.btnGuardarBinario.Location = new System.Drawing.Point(200, 108);
            this.btnGuardarBinario.Size = new System.Drawing.Size(150, 25);
            this.btnGuardarBinario.Text = "Guardar en Binario";
            this.btnGuardarBinario.Click += new System.EventHandler(this.btnGuardarBinario_Click);

            // labelBuscar
            this.labelBuscar.Location = new System.Drawing.Point(180, 22);
            this.labelBuscar.Size = new System.Drawing.Size(50, 20);
            this.labelBuscar.Text = "Buscar:";
            // txtBuscarId
            this.txtBuscarId.Location = new System.Drawing.Point(235, 22);
            this.txtBuscarId.Size = new System.Drawing.Size(115, 20);

            // add controls to groupNivel1
            this.groupNivel1.Controls.Add(this.labelId);
            this.groupNivel1.Controls.Add(this.txtId);
            this.groupNivel1.Controls.Add(this.labelNombre);
            this.groupNivel1.Controls.Add(this.txtNombre);
            this.groupNivel1.Controls.Add(this.labelEdad);
            this.groupNivel1.Controls.Add(this.txtEdad);
            this.groupNivel1.Controls.Add(this.labelPos);
            this.groupNivel1.Controls.Add(this.numericPos);
            this.groupNivel1.Controls.Add(this.btnGuardarBinario);
            this.groupNivel1.Controls.Add(this.labelBuscar);
            this.groupNivel1.Controls.Add(this.txtBuscarId);

            // groupNivel2
            this.groupNivel2.Location = new System.Drawing.Point(12, 180);
            this.groupNivel2.Size = new System.Drawing.Size(380, 140);
            this.groupNivel2.Text = "Nivel 2 - El Indexador";
            // btnGenerarIndice
            this.btnGenerarIndice.Location = new System.Drawing.Point(15, 20);
            this.btnGenerarIndice.Size = new System.Drawing.Size(170, 30);
            this.btnGenerarIndice.Text = "Generar Índice";
            this.btnGenerarIndice.Click += new System.EventHandler(this.btnGenerarIndice_Click);
            // btnCargarDesdeBinario
            this.btnCargarDesdeBinario.Location = new System.Drawing.Point(200, 20);
            this.btnCargarDesdeBinario.Size = new System.Drawing.Size(170, 30);
            this.btnCargarDesdeBinario.Text = "Cargar desde Binario";
            this.btnCargarDesdeBinario.Click += new System.EventHandler(this.btnCargarDesdeBinario_Click);

            // btnBuscarSecuencial
            this.btnBuscarSecuencial.Location = new System.Drawing.Point(15, 60);
            this.btnBuscarSecuencial.Size = new System.Drawing.Size(170, 30);
            this.btnBuscarSecuencial.Text = "Buscar (Secuencial)";
            this.btnBuscarSecuencial.Click += new System.EventHandler(this.btnBuscarSecuencial_Click);
            // btnBuscarIndexado
            this.btnBuscarIndexado.Location = new System.Drawing.Point(200, 60);
            this.btnBuscarIndexado.Size = new System.Drawing.Size(170, 30);
            this.btnBuscarIndexado.Text = "Buscar (Indexado)";
            this.btnBuscarIndexado.Click += new System.EventHandler(this.btnBuscarIndexado_Click);

            // labelTiempoSecuencial
            this.labelTiempoSecuencial.Location = new System.Drawing.Point(15, 100);
            this.labelTiempoSecuencial.Size = new System.Drawing.Size(170, 30);
            this.labelTiempoSecuencial.Text = "Tiempo secuencial: -";
            // labelTiempoIndexado
            this.labelTiempoIndexado.Location = new System.Drawing.Point(200, 100);
            this.labelTiempoIndexado.Size = new System.Drawing.Size(170, 30);
            this.labelTiempoIndexado.Text = "Tiempo indexado: -";

            this.groupNivel2.Controls.Add(this.btnGenerarIndice);
            this.groupNivel2.Controls.Add(this.btnCargarDesdeBinario);
            this.groupNivel2.Controls.Add(this.btnBuscarSecuencial);
            this.groupNivel2.Controls.Add(this.btnBuscarIndexado);
            this.groupNivel2.Controls.Add(this.labelTiempoSecuencial);
            this.groupNivel2.Controls.Add(this.labelTiempoIndexado);

            // groupNivel3
            this.groupNivel3.Location = new System.Drawing.Point(410, 12);
            this.groupNivel3.Size = new System.Drawing.Size(380, 130);
            this.groupNivel3.Text = "Nivel 3 - El Maestro de Datos";
            // labelCnx
            this.labelCnx.Location = new System.Drawing.Point(12, 25);
            this.labelCnx.Size = new System.Drawing.Size(100, 20);
            this.labelCnx.Text = "Connection String:";
            // txtConnectionString
            this.txtConnectionString.Location = new System.Drawing.Point(12, 45);
            this.txtConnectionString.Size = new System.Drawing.Size(360, 20);
            // btnMigrarSql
            this.btnMigrarSql.Location = new System.Drawing.Point(12, 75);
            this.btnMigrarSql.Size = new System.Drawing.Size(360, 35);
            this.btnMigrarSql.Text = "Migrar a SQL Server";
            this.btnMigrarSql.Click += new System.EventHandler(this.btnMigrarSql_Click);

            this.groupNivel3.Controls.Add(this.labelCnx);
            this.groupNivel3.Controls.Add(this.txtConnectionString);
            this.groupNivel3.Controls.Add(this.btnMigrarSql);

            // txtLog
            this.txtLog.Location = new System.Drawing.Point(410, 150);
            this.txtLog.Size = new System.Drawing.Size(380, 220);
            this.txtLog.Multiline = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.ReadOnly = true;

            // add top-level controls
            this.Controls.Add(this.groupNivel1);
            this.Controls.Add(this.groupNivel2);
            this.Controls.Add(this.groupNivel3);
            this.Controls.Add(this.txtLog);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupNivel1;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label labelEdad;
        private System.Windows.Forms.TextBox txtEdad;
        private System.Windows.Forms.Label labelPos;
        private System.Windows.Forms.NumericUpDown numericPos;
        private System.Windows.Forms.Button btnGuardarBinario;
        private System.Windows.Forms.Label labelBuscar;
        private System.Windows.Forms.TextBox txtBuscarId;

        private System.Windows.Forms.GroupBox groupNivel2;
        private System.Windows.Forms.Button btnGenerarIndice;
        private System.Windows.Forms.Button btnCargarDesdeBinario;
        private System.Windows.Forms.Button btnBuscarSecuencial;
        private System.Windows.Forms.Button btnBuscarIndexado;
        private System.Windows.Forms.Label labelTiempoSecuencial;
        private System.Windows.Forms.Label labelTiempoIndexado;

        private System.Windows.Forms.GroupBox groupNivel3;
        private System.Windows.Forms.Label labelCnx;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Button btnMigrarSql;

        private System.Windows.Forms.TextBox txtLog;
    }
}

