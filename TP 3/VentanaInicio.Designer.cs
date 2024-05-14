namespace TP_3
{
    partial class VentanaInicio
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TrabajoPractico3 = new Label();
            Simulación = new Label();
            label1 = new Label();
            groupBox1 = new GroupBox();
            ausenteCinco = new TextBox();
            ausenteCuatro = new TextBox();
            ausenteTres = new TextBox();
            ausenteDos = new TextBox();
            ausenteUno = new TextBox();
            ausentesCero = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            groupBox2 = new GroupBox();
            costoVariable = new TextBox();
            costoVariableLbl = new Label();
            costoObrero = new TextBox();
            costoObrerosLbl = new Label();
            cantidadObreroslbl = new Label();
            cantObreros = new TextBox();
            cantSemanas = new TextBox();
            simularBtn = new Button();
            precioVentaLbl = new Label();
            precioVenta = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // TrabajoPractico3
            // 
            TrabajoPractico3.AutoSize = true;
            TrabajoPractico3.Font = new Font("Segoe UI", 10F);
            TrabajoPractico3.Location = new Point(136, 38);
            TrabajoPractico3.Name = "TrabajoPractico3";
            TrabajoPractico3.Size = new Size(117, 19);
            TrabajoPractico3.TabIndex = 0;
            TrabajoPractico3.Text = "Trabajo Práctico 3";
            // 
            // Simulación
            // 
            Simulación.AutoSize = true;
            Simulación.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Simulación.Location = new Point(142, 9);
            Simulación.Name = "Simulación";
            Simulación.Size = new Size(110, 25);
            Simulación.TabIndex = 1;
            Simulación.Text = "Simulación";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 83);
            label1.Name = "label1";
            label1.Size = new Size(174, 15);
            label1.TabIndex = 2;
            label1.Text = "Cantidad de semanas a simular:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ausenteCinco);
            groupBox1.Controls.Add(ausenteCuatro);
            groupBox1.Controls.Add(ausenteTres);
            groupBox1.Controls.Add(ausenteDos);
            groupBox1.Controls.Add(ausenteUno);
            groupBox1.Controls.Add(ausentesCero);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(12, 125);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(149, 188);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Probabilidades (en %)";
            // 
            // ausenteCinco
            // 
            ausenteCinco.Location = new Point(74, 164);
            ausenteCinco.Name = "ausenteCinco";
            ausenteCinco.Size = new Size(45, 23);
            ausenteCinco.TabIndex = 15;
            ausenteCinco.Text = "0";
            ausenteCinco.KeyPress += ausenteCinco_KeyPress;
            // 
            // ausenteCuatro
            // 
            ausenteCuatro.Location = new Point(74, 135);
            ausenteCuatro.Name = "ausenteCuatro";
            ausenteCuatro.Size = new Size(45, 23);
            ausenteCuatro.TabIndex = 14;
            ausenteCuatro.Text = "1";
            ausenteCuatro.KeyPress += ausenteCuatro_KeyPress;
            // 
            // ausenteTres
            // 
            ausenteTres.Location = new Point(74, 106);
            ausenteTres.Name = "ausenteTres";
            ausenteTres.Size = new Size(45, 23);
            ausenteTres.TabIndex = 13;
            ausenteTres.Text = "6";
            ausenteTres.KeyPress += ausenteTres_KeyPress;
            // 
            // ausenteDos
            // 
            ausenteDos.Location = new Point(74, 77);
            ausenteDos.Name = "ausenteDos";
            ausenteDos.Size = new Size(45, 23);
            ausenteDos.TabIndex = 12;
            ausenteDos.Text = "19";
            ausenteDos.KeyPress += ausenteDos_KeyPress;
            // 
            // ausenteUno
            // 
            ausenteUno.Location = new Point(74, 48);
            ausenteUno.Name = "ausenteUno";
            ausenteUno.Size = new Size(45, 23);
            ausenteUno.TabIndex = 11;
            ausenteUno.Text = "38";
            ausenteUno.KeyPress += ausenteUno_KeyPress;
            // 
            // ausentesCero
            // 
            ausentesCero.Location = new Point(74, 21);
            ausentesCero.Name = "ausentesCero";
            ausentesCero.Size = new Size(45, 23);
            ausentesCero.TabIndex = 5;
            ausentesCero.Text = "36";
            ausentesCero.KeyPress += ausentesCero_KeyPress;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 167);
            label7.Name = "label7";
            label7.Size = new Size(48, 15);
            label7.TabIndex = 10;
            label7.Text = "5 o mas";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 139);
            label6.Name = "label6";
            label6.Size = new Size(62, 15);
            label6.TabIndex = 9;
            label6.Text = "4 ausentes";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 109);
            label5.Name = "label5";
            label5.Size = new Size(62, 15);
            label5.TabIndex = 8;
            label5.Text = "3 ausentes";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 80);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 7;
            label4.Text = "2 ausentes";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 51);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 6;
            label3.Text = "1 ausente";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 24);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 5;
            label2.Text = "0 ausentes";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(precioVenta);
            groupBox2.Controls.Add(precioVentaLbl);
            groupBox2.Controls.Add(costoVariable);
            groupBox2.Controls.Add(costoVariableLbl);
            groupBox2.Controls.Add(costoObrero);
            groupBox2.Controls.Add(costoObrerosLbl);
            groupBox2.Controls.Add(cantidadObreroslbl);
            groupBox2.Controls.Add(cantObreros);
            groupBox2.Location = new Point(189, 125);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(195, 141);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Parámetros";
            // 
            // costoVariable
            // 
            costoVariable.Location = new Point(129, 77);
            costoVariable.Name = "costoVariable";
            costoVariable.PlaceholderText = "$";
            costoVariable.Size = new Size(52, 23);
            costoVariable.TabIndex = 5;
            costoVariable.Text = "2400";
            costoVariable.KeyPress += costoVariable_KeyPress;
            // 
            // costoVariableLbl
            // 
            costoVariableLbl.AutoSize = true;
            costoVariableLbl.Location = new Point(6, 80);
            costoVariableLbl.Name = "costoVariableLbl";
            costoVariableLbl.Size = new Size(95, 15);
            costoVariableLbl.TabIndex = 4;
            costoVariableLbl.Text = "Costos variables:";
            // 
            // costoObrero
            // 
            costoObrero.Location = new Point(129, 48);
            costoObrero.Name = "costoObrero";
            costoObrero.PlaceholderText = "$";
            costoObrero.Size = new Size(52, 23);
            costoObrero.TabIndex = 3;
            costoObrero.Text = "40";
            costoObrero.KeyPress += costoObrero_KeyPress;
            // 
            // costoObrerosLbl
            // 
            costoObrerosLbl.AutoSize = true;
            costoObrerosLbl.Location = new Point(6, 51);
            costoObrerosLbl.Name = "costoObrerosLbl";
            costoObrerosLbl.Size = new Size(112, 15);
            costoObrerosLbl.TabIndex = 2;
            costoObrerosLbl.Text = "Costo de un obrero:";
            // 
            // cantidadObreroslbl
            // 
            cantidadObreroslbl.AutoSize = true;
            cantidadObreroslbl.Location = new Point(6, 24);
            cantidadObreroslbl.Name = "cantidadObreroslbl";
            cantidadObreroslbl.Size = new Size(117, 15);
            cantidadObreroslbl.TabIndex = 1;
            cantidadObreroslbl.Text = "Cantidad de obreros:";
            // 
            // cantObreros
            // 
            cantObreros.Location = new Point(129, 21);
            cantObreros.Name = "cantObreros";
            cantObreros.Size = new Size(52, 23);
            cantObreros.TabIndex = 0;
            cantObreros.Text = "24";
            cantObreros.KeyPress += cantObreros_KeyPress;
            // 
            // cantSemanas
            // 
            cantSemanas.Location = new Point(195, 80);
            cantSemanas.Name = "cantSemanas";
            cantSemanas.Size = new Size(175, 23);
            cantSemanas.TabIndex = 16;
            cantSemanas.Text = "16";
            cantSemanas.KeyPress += cantSemanas_KeyPress;
            // 
            // simularBtn
            // 
            simularBtn.Location = new Point(189, 272);
            simularBtn.Name = "simularBtn";
            simularBtn.Size = new Size(195, 40);
            simularBtn.TabIndex = 17;
            simularBtn.Text = "SIMULAR";
            simularBtn.UseVisualStyleBackColor = true;
            simularBtn.Click += simularBtn_Click;
            // 
            // precioVentaLbl
            // 
            precioVentaLbl.AutoSize = true;
            precioVentaLbl.Location = new Point(6, 109);
            precioVentaLbl.Name = "precioVentaLbl";
            precioVentaLbl.Size = new Size(91, 15);
            precioVentaLbl.TabIndex = 6;
            precioVentaLbl.Text = "Precio de Venta:";
            // 
            // precioVenta
            // 
            precioVenta.Location = new Point(129, 106);
            precioVenta.Name = "precioVenta";
            precioVenta.PlaceholderText = "$";
            precioVenta.Size = new Size(52, 23);
            precioVenta.TabIndex = 7;
            precioVenta.Text = "4000";
            precioVenta.KeyPress += precioVenta_KeyPress;
            // 
            // VentanaInicio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(407, 323);
            Controls.Add(simularBtn);
            Controls.Add(cantSemanas);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(Simulación);
            Controls.Add(TrabajoPractico3);
            Name = "VentanaInicio";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TP Simulación";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label TrabajoPractico3;
        private Label Simulación;
        private Label label1;
        private GroupBox groupBox1;
        private Label label3;
        private Label label2;
        private GroupBox groupBox2;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private TextBox ausentesCero;
        private TextBox ausenteCuatro;
        private TextBox ausenteTres;
        private TextBox ausenteDos;
        private TextBox ausenteUno;
        private TextBox ausenteCinco;
        private Label cantidadObreroslbl;
        private TextBox cantObreros;
        private TextBox cantSemanas;
        private Label costoObrerosLbl;
        private TextBox costoVariable;
        private Label costoVariableLbl;
        private TextBox costoObrero;
        private Button simularBtn;
        private TextBox precioVenta;
        private Label precioVentaLbl;
    }
}
