namespace TP2_WF
{
    partial class PantallaPrincipalGenerador
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
            this.cbo_selectInterval = new System.Windows.Forms.ComboBox();
            this.txt_tamM = new System.Windows.Forms.TextBox();
            this.txt_param1 = new System.Windows.Forms.TextBox();
            this.txt_param2 = new System.Windows.Forms.TextBox();
            this.lbl_tamM = new System.Windows.Forms.Label();
            this.lbl_param1 = new System.Windows.Forms.Label();
            this.lbl_param2 = new System.Windows.Forms.Label();
            this.cbo_selectDist = new System.Windows.Forms.ComboBox();
            this.lbl_selectDist = new System.Windows.Forms.Label();
            this.lbl_selectInterval = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.rbt_lambda = new System.Windows.Forms.RadioButton();
            this.gbx_radioButtons = new System.Windows.Forms.GroupBox();
            this.gbx_radioButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbo_selectInterval
            // 
            this.cbo_selectInterval.FormattingEnabled = true;
            this.cbo_selectInterval.Items.AddRange(new object[] {
            "10",
            "12",
            "16",
            "23"});
            this.cbo_selectInterval.Location = new System.Drawing.Point(257, 28);
            this.cbo_selectInterval.Name = "cbo_selectInterval";
            this.cbo_selectInterval.Size = new System.Drawing.Size(158, 21);
            this.cbo_selectInterval.TabIndex = 0;
            this.cbo_selectInterval.SelectedIndexChanged += new System.EventHandler(this.cbo_selectInterval_SelectedIndexChanged);
            // 
            // txt_tamM
            // 
            this.txt_tamM.Location = new System.Drawing.Point(12, 77);
            this.txt_tamM.Name = "txt_tamM";
            this.txt_tamM.Size = new System.Drawing.Size(192, 20);
            this.txt_tamM.TabIndex = 1;
            this.txt_tamM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_tamM_KeyPress);
            // 
            // txt_param1
            // 
            this.txt_param1.Location = new System.Drawing.Point(12, 117);
            this.txt_param1.Name = "txt_param1";
            this.txt_param1.Size = new System.Drawing.Size(192, 20);
            this.txt_param1.TabIndex = 2;
            this.txt_param1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_param1_KeyPress);
            // 
            // txt_param2
            // 
            this.txt_param2.Location = new System.Drawing.Point(12, 160);
            this.txt_param2.Name = "txt_param2";
            this.txt_param2.Size = new System.Drawing.Size(192, 20);
            this.txt_param2.TabIndex = 3;
            this.txt_param2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_param2_KeyPress);
            // 
            // lbl_tamM
            // 
            this.lbl_tamM.AutoSize = true;
            this.lbl_tamM.Location = new System.Drawing.Point(12, 58);
            this.lbl_tamM.Name = "lbl_tamM";
            this.lbl_tamM.Size = new System.Drawing.Size(102, 13);
            this.lbl_tamM.TabIndex = 4;
            this.lbl_tamM.Text = "Tamaño de Muestra";
            // 
            // lbl_param1
            // 
            this.lbl_param1.AutoSize = true;
            this.lbl_param1.Location = new System.Drawing.Point(11, 100);
            this.lbl_param1.Name = "lbl_param1";
            this.lbl_param1.Size = new System.Drawing.Size(29, 13);
            this.lbl_param1.TabIndex = 5;
            this.lbl_param1.Text = "label";
            // 
            // lbl_param2
            // 
            this.lbl_param2.AutoSize = true;
            this.lbl_param2.Location = new System.Drawing.Point(12, 144);
            this.lbl_param2.Name = "lbl_param2";
            this.lbl_param2.Size = new System.Drawing.Size(35, 13);
            this.lbl_param2.TabIndex = 6;
            this.lbl_param2.Text = "label3";
            // 
            // cbo_selectDist
            // 
            this.cbo_selectDist.FormattingEnabled = true;
            this.cbo_selectDist.Items.AddRange(new object[] {
            "Uniforme",
            "Normal",
            "Exponencial Negativa"});
            this.cbo_selectDist.Location = new System.Drawing.Point(12, 28);
            this.cbo_selectDist.Name = "cbo_selectDist";
            this.cbo_selectDist.Size = new System.Drawing.Size(192, 21);
            this.cbo_selectDist.TabIndex = 7;
            this.cbo_selectDist.SelectedIndexChanged += new System.EventHandler(this.cbo_selectDist_SelectedIndexChanged);
            // 
            // lbl_selectDist
            // 
            this.lbl_selectDist.AutoSize = true;
            this.lbl_selectDist.Location = new System.Drawing.Point(12, 9);
            this.lbl_selectDist.Name = "lbl_selectDist";
            this.lbl_selectDist.Size = new System.Drawing.Size(173, 13);
            this.lbl_selectDist.TabIndex = 8;
            this.lbl_selectDist.Text = "Seleccione la Distribución deseada";
            // 
            // lbl_selectInterval
            // 
            this.lbl_selectInterval.AutoSize = true;
            this.lbl_selectInterval.Location = new System.Drawing.Point(254, 9);
            this.lbl_selectInterval.Name = "lbl_selectInterval";
            this.lbl_selectInterval.Size = new System.Drawing.Size(169, 13);
            this.lbl_selectInterval.TabIndex = 9;
            this.lbl_selectInterval.Text = "Seleccione Cantidad de Intervalos";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(257, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 103);
            this.button1.TabIndex = 10;
            this.button1.Text = "Generar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rbt_lambda
            // 
            this.rbt_lambda.AutoSize = true;
            this.rbt_lambda.Location = new System.Drawing.Point(6, 43);
            this.rbt_lambda.Name = "rbt_lambda";
            this.rbt_lambda.Size = new System.Drawing.Size(14, 13);
            this.rbt_lambda.TabIndex = 0;
            this.rbt_lambda.TabStop = true;
            this.rbt_lambda.UseVisualStyleBackColor = true;
            this.rbt_lambda.CheckedChanged += new System.EventHandler(this.rbt_lambda_CheckedChanged);
            // 
            // gbx_radioButtons
            // 
            this.gbx_radioButtons.Controls.Add(this.rbt_lambda);
            this.gbx_radioButtons.Location = new System.Drawing.Point(204, 77);
            this.gbx_radioButtons.Name = "gbx_radioButtons";
            this.gbx_radioButtons.Size = new System.Drawing.Size(54, 103);
            this.gbx_radioButtons.TabIndex = 11;
            this.gbx_radioButtons.TabStop = false;
            // 
            // PantallaPrincipalGenerador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 201);
            this.Controls.Add(this.gbx_radioButtons);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_selectInterval);
            this.Controls.Add(this.lbl_selectDist);
            this.Controls.Add(this.cbo_selectDist);
            this.Controls.Add(this.lbl_param2);
            this.Controls.Add(this.lbl_param1);
            this.Controls.Add(this.lbl_tamM);
            this.Controls.Add(this.txt_param2);
            this.Controls.Add(this.txt_param1);
            this.Controls.Add(this.txt_tamM);
            this.Controls.Add(this.cbo_selectInterval);
            this.Name = "PantallaPrincipalGenerador";
            this.Text = "Generador de Números Aleatorios";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbx_radioButtons.ResumeLayout(false);
            this.gbx_radioButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbo_selectInterval;
        private System.Windows.Forms.TextBox txt_tamM;
        private System.Windows.Forms.TextBox txt_param1;
        private System.Windows.Forms.TextBox txt_param2;
        private System.Windows.Forms.Label lbl_tamM;
        private System.Windows.Forms.Label lbl_param1;
        private System.Windows.Forms.Label lbl_param2;
        private System.Windows.Forms.ComboBox cbo_selectDist;
        private System.Windows.Forms.Label lbl_selectDist;
        private System.Windows.Forms.Label lbl_selectInterval;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rbt_lambda;
        private System.Windows.Forms.GroupBox gbx_radioButtons;
    }
}

