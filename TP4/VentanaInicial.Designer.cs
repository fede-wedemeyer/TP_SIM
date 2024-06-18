namespace TP4
{
    partial class VentanaInicial
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
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            cantIteraciones = new TextBox();
            desdeLinea = new TextBox();
            groupBox1 = new GroupBox();
            label4 = new Label();
            prestamoTime = new TextBox();
            prestamoRate = new TextBox();
            label3 = new Label();
            groupBox2 = new GroupBox();
            label5 = new Label();
            devolucionTime = new TextBox();
            devolucionRate = new TextBox();
            label6 = new Label();
            groupBox3 = new GroupBox();
            label7 = new Label();
            pcTime = new TextBox();
            pcRate = new TextBox();
            label8 = new Label();
            groupBox4 = new GroupBox();
            label9 = new Label();
            infoTime = new TextBox();
            infoRate = new TextBox();
            label10 = new Label();
            label11 = new Label();
            groupBox5 = new GroupBox();
            groupBox11 = new GroupBox();
            label20 = new Label();
            consultaTime = new TextBox();
            consultaRate = new TextBox();
            label21 = new Label();
            groupBox6 = new GroupBox();
            groupBox12 = new GroupBox();
            label22 = new Label();
            consultaEstudianteTime = new TextBox();
            consultaEstudianteRate = new TextBox();
            label23 = new Label();
            groupBox7 = new GroupBox();
            label12 = new Label();
            prestamoEstudianteTime = new TextBox();
            prestamoEstudianteRate = new TextBox();
            label13 = new Label();
            groupBox8 = new GroupBox();
            label14 = new Label();
            devolucionEstudianteTime = new TextBox();
            devolucionEstudianteRate = new TextBox();
            label15 = new Label();
            groupBox9 = new GroupBox();
            label16 = new Label();
            infoEstudianteTime = new TextBox();
            infoEstudianteRate = new TextBox();
            label17 = new Label();
            groupBox10 = new GroupBox();
            label18 = new Label();
            pcEstudianteTime = new TextBox();
            pcEstudianteRate = new TextBox();
            label19 = new Label();
            label24 = new Label();
            probMemb = new TextBox();
            label25 = new Label();
            membTime = new TextBox();
            membRate = new TextBox();
            label26 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox11.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox12.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBox10.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(289, 610);
            button1.Name = "button1";
            button1.Size = new Size(129, 36);
            button1.TabIndex = 0;
            button1.Text = "Simular!";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 48);
            label1.Name = "label1";
            label1.Size = new Size(146, 15);
            label1.TabIndex = 1;
            label1.Text = "Cantidad de simulaciones:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(41, 76);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 2;
            label2.Text = "Desde línea:";
            // 
            // cantIteraciones
            // 
            cantIteraciones.Location = new Point(189, 45);
            cantIteraciones.Name = "cantIteraciones";
            cantIteraciones.Size = new Size(53, 23);
            cantIteraciones.TabIndex = 4;
            cantIteraciones.KeyPress += cantIteraciones_KeyPress;
            // 
            // desdeLinea
            // 
            desdeLinea.Location = new Point(190, 73);
            desdeLinea.Name = "desdeLinea";
            desdeLinea.Size = new Size(52, 23);
            desdeLinea.TabIndex = 5;
            desdeLinea.KeyPress += desdeLinea_KeyPress;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(prestamoTime);
            groupBox1.Controls.Add(prestamoRate);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(6, 32);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(300, 83);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Prestamo de libros:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(230, 39);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 9;
            label4.Text = "minutos";
            // 
            // prestamoTime
            // 
            prestamoTime.Location = new Point(172, 36);
            prestamoTime.Name = "prestamoTime";
            prestamoTime.Size = new Size(52, 23);
            prestamoTime.TabIndex = 8;
            prestamoTime.Text = "60";
            prestamoTime.KeyPress += prestamoTime_KeyPress;
            // 
            // prestamoRate
            // 
            prestamoRate.Location = new Point(13, 36);
            prestamoRate.Name = "prestamoRate";
            prestamoRate.Size = new Size(52, 23);
            prestamoRate.TabIndex = 7;
            prestamoRate.Text = "10";
            prestamoRate.KeyPress += prestamoRate_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(71, 39);
            label3.Name = "label3";
            label3.Size = new Size(95, 15);
            label3.TabIndex = 0;
            label3.Text = "Estudiantes cada";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(devolucionTime);
            groupBox2.Controls.Add(devolucionRate);
            groupBox2.Controls.Add(label6);
            groupBox2.Location = new Point(6, 121);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(300, 83);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "Devolución de libros";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(230, 39);
            label5.Name = "label5";
            label5.Size = new Size(51, 15);
            label5.TabIndex = 9;
            label5.Text = "minutos";
            // 
            // devolucionTime
            // 
            devolucionTime.Location = new Point(172, 36);
            devolucionTime.Name = "devolucionTime";
            devolucionTime.Size = new Size(52, 23);
            devolucionTime.TabIndex = 8;
            devolucionTime.Text = "60";
            devolucionTime.KeyPress += devolucionTime_KeyPress;
            // 
            // devolucionRate
            // 
            devolucionRate.Location = new Point(13, 36);
            devolucionRate.Name = "devolucionRate";
            devolucionRate.Size = new Size(52, 23);
            devolucionRate.TabIndex = 7;
            devolucionRate.Text = "12";
            devolucionRate.KeyPress += devolucionRate_KeyPress;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(71, 39);
            label6.Name = "label6";
            label6.Size = new Size(95, 15);
            label6.TabIndex = 0;
            label6.Text = "Estudiantes cada";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(pcTime);
            groupBox3.Controls.Add(pcRate);
            groupBox3.Controls.Add(label8);
            groupBox3.Location = new Point(6, 299);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(300, 83);
            groupBox3.TabIndex = 11;
            groupBox3.TabStop = false;
            groupBox3.Text = "Acceso a computadoras";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(230, 39);
            label7.Name = "label7";
            label7.Size = new Size(51, 15);
            label7.TabIndex = 9;
            label7.Text = "minutos";
            // 
            // pcTime
            // 
            pcTime.Location = new Point(172, 36);
            pcTime.Name = "pcTime";
            pcTime.Size = new Size(52, 23);
            pcTime.TabIndex = 8;
            pcTime.Text = "60";
            pcTime.KeyPress += pcTime_KeyPress;
            // 
            // pcRate
            // 
            pcRate.Location = new Point(13, 36);
            pcRate.Name = "pcRate";
            pcRate.Size = new Size(52, 23);
            pcRate.TabIndex = 7;
            pcRate.Text = "5";
            pcRate.KeyPress += pcRate_KeyPress;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(71, 39);
            label8.Name = "label8";
            label8.Size = new Size(95, 15);
            label8.TabIndex = 0;
            label8.Text = "Estudiantes cada";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(infoTime);
            groupBox4.Controls.Add(infoRate);
            groupBox4.Controls.Add(label10);
            groupBox4.Location = new Point(6, 388);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(300, 83);
            groupBox4.TabIndex = 12;
            groupBox4.TabStop = false;
            groupBox4.Text = "Información general";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(230, 39);
            label9.Name = "label9";
            label9.Size = new Size(51, 15);
            label9.TabIndex = 9;
            label9.Text = "minutos";
            // 
            // infoTime
            // 
            infoTime.Location = new Point(172, 36);
            infoTime.Name = "infoTime";
            infoTime.Size = new Size(52, 23);
            infoTime.TabIndex = 8;
            infoTime.Text = "60";
            infoTime.KeyPress += infoTime_KeyPress;
            // 
            // infoRate
            // 
            infoRate.Location = new Point(13, 36);
            infoRate.Name = "infoRate";
            infoRate.Size = new Size(52, 23);
            infoRate.TabIndex = 7;
            infoRate.Text = "15";
            infoRate.KeyPress += infoRate_KeyPress;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(71, 39);
            label10.Name = "label10";
            label10.Size = new Size(95, 15);
            label10.TabIndex = 0;
            label10.Text = "Estudiantes cada";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label11.Location = new Point(270, 9);
            label11.Name = "label11";
            label11.Size = new Size(175, 21);
            label11.TabIndex = 13;
            label11.Text = "TRABAJO PRÁCTICO 4";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(groupBox11);
            groupBox5.Controls.Add(groupBox1);
            groupBox5.Controls.Add(groupBox2);
            groupBox5.Controls.Add(groupBox4);
            groupBox5.Controls.Add(groupBox3);
            groupBox5.Location = new Point(374, 112);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(322, 492);
            groupBox5.TabIndex = 14;
            groupBox5.TabStop = false;
            groupBox5.Text = "Velocidad de bibliotecarios:";
            // 
            // groupBox11
            // 
            groupBox11.Controls.Add(label20);
            groupBox11.Controls.Add(consultaTime);
            groupBox11.Controls.Add(consultaRate);
            groupBox11.Controls.Add(label21);
            groupBox11.Location = new Point(6, 210);
            groupBox11.Name = "groupBox11";
            groupBox11.Size = new Size(300, 83);
            groupBox11.TabIndex = 12;
            groupBox11.TabStop = false;
            groupBox11.Text = "Consulta en sala";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(230, 39);
            label20.Name = "label20";
            label20.Size = new Size(51, 15);
            label20.TabIndex = 9;
            label20.Text = "minutos";
            // 
            // consultaTime
            // 
            consultaTime.Location = new Point(172, 36);
            consultaTime.Name = "consultaTime";
            consultaTime.Size = new Size(52, 23);
            consultaTime.TabIndex = 8;
            consultaTime.Text = "60";
            consultaTime.KeyPress += consultaTime_KeyPress;
            // 
            // consultaRate
            // 
            consultaRate.Location = new Point(13, 36);
            consultaRate.Name = "consultaRate";
            consultaRate.Size = new Size(52, 23);
            consultaRate.TabIndex = 7;
            consultaRate.Text = "8";
            consultaRate.KeyPress += consultaRate_KeyPress;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(71, 39);
            label21.Name = "label21";
            label21.Size = new Size(95, 15);
            label21.TabIndex = 0;
            label21.Text = "Estudiantes cada";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(groupBox12);
            groupBox6.Controls.Add(groupBox7);
            groupBox6.Controls.Add(groupBox8);
            groupBox6.Controls.Add(groupBox9);
            groupBox6.Controls.Add(groupBox10);
            groupBox6.Location = new Point(12, 113);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(322, 491);
            groupBox6.TabIndex = 15;
            groupBox6.TabStop = false;
            groupBox6.Text = "Velocidad de llegadas:";
            // 
            // groupBox12
            // 
            groupBox12.Controls.Add(label22);
            groupBox12.Controls.Add(consultaEstudianteTime);
            groupBox12.Controls.Add(consultaEstudianteRate);
            groupBox12.Controls.Add(label23);
            groupBox12.Location = new Point(6, 210);
            groupBox12.Name = "groupBox12";
            groupBox12.Size = new Size(300, 83);
            groupBox12.TabIndex = 13;
            groupBox12.TabStop = false;
            groupBox12.Text = "Consulta en sala";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(230, 39);
            label22.Name = "label22";
            label22.Size = new Size(51, 15);
            label22.TabIndex = 9;
            label22.Text = "minutos";
            // 
            // consultaEstudianteTime
            // 
            consultaEstudianteTime.Location = new Point(172, 36);
            consultaEstudianteTime.Name = "consultaEstudianteTime";
            consultaEstudianteTime.Size = new Size(52, 23);
            consultaEstudianteTime.TabIndex = 8;
            consultaEstudianteTime.Text = "60";
            consultaEstudianteTime.KeyPress += consultaEstudianteTime_KeyPress;
            // 
            // consultaEstudianteRate
            // 
            consultaEstudianteRate.Location = new Point(13, 36);
            consultaEstudianteRate.Name = "consultaEstudianteRate";
            consultaEstudianteRate.Size = new Size(52, 23);
            consultaEstudianteRate.TabIndex = 7;
            consultaEstudianteRate.Text = "10";
            consultaEstudianteRate.KeyPress += consultaEstudianteRate_KeyPress;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(71, 39);
            label23.Name = "label23";
            label23.Size = new Size(95, 15);
            label23.TabIndex = 0;
            label23.Text = "Estudiantes cada";
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(label12);
            groupBox7.Controls.Add(prestamoEstudianteTime);
            groupBox7.Controls.Add(prestamoEstudianteRate);
            groupBox7.Controls.Add(label13);
            groupBox7.Location = new Point(6, 32);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(300, 83);
            groupBox7.TabIndex = 6;
            groupBox7.TabStop = false;
            groupBox7.Text = "Prestamo de libros:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(230, 39);
            label12.Name = "label12";
            label12.Size = new Size(51, 15);
            label12.TabIndex = 9;
            label12.Text = "minutos";
            // 
            // prestamoEstudianteTime
            // 
            prestamoEstudianteTime.Location = new Point(172, 36);
            prestamoEstudianteTime.Name = "prestamoEstudianteTime";
            prestamoEstudianteTime.Size = new Size(52, 23);
            prestamoEstudianteTime.TabIndex = 8;
            prestamoEstudianteTime.Text = "60";
            prestamoEstudianteTime.KeyPress += prestamoEstudianteTime_KeyPress;
            // 
            // prestamoEstudianteRate
            // 
            prestamoEstudianteRate.Location = new Point(13, 36);
            prestamoEstudianteRate.Name = "prestamoEstudianteRate";
            prestamoEstudianteRate.Size = new Size(52, 23);
            prestamoEstudianteRate.TabIndex = 7;
            prestamoEstudianteRate.Text = "20";
            prestamoEstudianteRate.KeyPress += prestamoEstudianteRate_KeyPress;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(71, 39);
            label13.Name = "label13";
            label13.Size = new Size(95, 15);
            label13.TabIndex = 0;
            label13.Text = "Estudiantes cada";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(label14);
            groupBox8.Controls.Add(devolucionEstudianteTime);
            groupBox8.Controls.Add(devolucionEstudianteRate);
            groupBox8.Controls.Add(label15);
            groupBox8.Location = new Point(6, 121);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(300, 83);
            groupBox8.TabIndex = 10;
            groupBox8.TabStop = false;
            groupBox8.Text = "Devolución de libros";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(230, 39);
            label14.Name = "label14";
            label14.Size = new Size(51, 15);
            label14.TabIndex = 9;
            label14.Text = "minutos";
            // 
            // devolucionEstudianteTime
            // 
            devolucionEstudianteTime.Location = new Point(172, 36);
            devolucionEstudianteTime.Name = "devolucionEstudianteTime";
            devolucionEstudianteTime.Size = new Size(52, 23);
            devolucionEstudianteTime.TabIndex = 8;
            devolucionEstudianteTime.Text = "60";
            devolucionEstudianteTime.KeyPress += devolucionEstudianteTime_KeyPress;
            // 
            // devolucionEstudianteRate
            // 
            devolucionEstudianteRate.Location = new Point(13, 36);
            devolucionEstudianteRate.Name = "devolucionEstudianteRate";
            devolucionEstudianteRate.Size = new Size(52, 23);
            devolucionEstudianteRate.TabIndex = 7;
            devolucionEstudianteRate.Text = "15";
            devolucionEstudianteRate.KeyPress += devolucionEstudianteRate_KeyPress;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(71, 39);
            label15.Name = "label15";
            label15.Size = new Size(95, 15);
            label15.TabIndex = 0;
            label15.Text = "Estudiantes cada";
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(label16);
            groupBox9.Controls.Add(infoEstudianteTime);
            groupBox9.Controls.Add(infoEstudianteRate);
            groupBox9.Controls.Add(label17);
            groupBox9.Location = new Point(6, 387);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(300, 83);
            groupBox9.TabIndex = 12;
            groupBox9.TabStop = false;
            groupBox9.Text = "Información general";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(230, 39);
            label16.Name = "label16";
            label16.Size = new Size(51, 15);
            label16.TabIndex = 9;
            label16.Text = "minutos";
            // 
            // infoEstudianteTime
            // 
            infoEstudianteTime.Location = new Point(172, 36);
            infoEstudianteTime.Name = "infoEstudianteTime";
            infoEstudianteTime.Size = new Size(52, 23);
            infoEstudianteTime.TabIndex = 8;
            infoEstudianteTime.Text = "60";
            infoEstudianteTime.KeyPress += infoEstudianteTime_KeyPress;
            // 
            // infoEstudianteRate
            // 
            infoEstudianteRate.Location = new Point(13, 36);
            infoEstudianteRate.Name = "infoEstudianteRate";
            infoEstudianteRate.Size = new Size(52, 23);
            infoEstudianteRate.TabIndex = 7;
            infoEstudianteRate.Text = "25";
            infoEstudianteRate.KeyPress += infoEstudianteRate_KeyPress;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(71, 39);
            label17.Name = "label17";
            label17.Size = new Size(95, 15);
            label17.TabIndex = 0;
            label17.Text = "Estudiantes cada";
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(label18);
            groupBox10.Controls.Add(pcEstudianteTime);
            groupBox10.Controls.Add(pcEstudianteRate);
            groupBox10.Controls.Add(label19);
            groupBox10.Location = new Point(6, 298);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(300, 83);
            groupBox10.TabIndex = 11;
            groupBox10.TabStop = false;
            groupBox10.Text = "Acceso a computadoras";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(230, 39);
            label18.Name = "label18";
            label18.Size = new Size(51, 15);
            label18.TabIndex = 9;
            label18.Text = "minutos";
            // 
            // pcEstudianteTime
            // 
            pcEstudianteTime.Location = new Point(172, 36);
            pcEstudianteTime.Name = "pcEstudianteTime";
            pcEstudianteTime.Size = new Size(52, 23);
            pcEstudianteTime.TabIndex = 8;
            pcEstudianteTime.Text = "60";
            pcEstudianteTime.KeyPress += pcEstudianteTime_KeyPress;
            // 
            // pcEstudianteRate
            // 
            pcEstudianteRate.Location = new Point(13, 36);
            pcEstudianteRate.Name = "pcEstudianteRate";
            pcEstudianteRate.Size = new Size(52, 23);
            pcEstudianteRate.TabIndex = 7;
            pcEstudianteRate.Text = "8";
            pcEstudianteRate.KeyPress += pcEstudianteRate_KeyPress;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(71, 39);
            label19.Name = "label19";
            label19.Size = new Size(95, 15);
            label19.TabIndex = 0;
            label19.Text = "Estudiantes cada";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(270, 45);
            label24.Name = "label24";
            label24.Size = new Size(273, 15);
            label24.TabIndex = 16;
            label24.Text = "Probabilidad de que pasen a gestionar membresía:";
            // 
            // probMemb
            // 
            probMemb.Location = new Point(540, 42);
            probMemb.Name = "probMemb";
            probMemb.Size = new Size(32, 23);
            probMemb.TabIndex = 17;
            probMemb.Text = "35";
            probMemb.KeyPress += probMemb_KeyPress;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(488, 76);
            label25.Name = "label25";
            label25.Size = new Size(51, 15);
            label25.TabIndex = 13;
            label25.Text = "minutos";
            // 
            // membTime
            // 
            membTime.Location = new Point(430, 73);
            membTime.Name = "membTime";
            membTime.Size = new Size(52, 23);
            membTime.TabIndex = 12;
            membTime.Text = "60";
            membTime.KeyPress += membTime_KeyPress;
            // 
            // membRate
            // 
            membRate.Location = new Point(271, 73);
            membRate.Name = "membRate";
            membRate.Size = new Size(52, 23);
            membRate.TabIndex = 11;
            membRate.Text = "30";
            membRate.KeyPress += membRate_KeyPress;
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(329, 76);
            label26.Name = "label26";
            label26.Size = new Size(95, 15);
            label26.TabIndex = 10;
            label26.Text = "Estudiantes cada";
            // 
            // VentanaInicial
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(714, 658);
            Controls.Add(label25);
            Controls.Add(probMemb);
            Controls.Add(membTime);
            Controls.Add(label24);
            Controls.Add(membRate);
            Controls.Add(groupBox6);
            Controls.Add(label26);
            Controls.Add(groupBox5);
            Controls.Add(label11);
            Controls.Add(desdeLinea);
            Controls.Add(cantIteraciones);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "VentanaInicial";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox11.ResumeLayout(false);
            groupBox11.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox12.ResumeLayout(false);
            groupBox12.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Label label2;
        private TextBox cantIteraciones;
        private TextBox desdeLinea;
        private GroupBox groupBox1;
        private Label label4;
        private TextBox prestamoTime;
        private TextBox prestamoRate;
        private Label label3;
        private GroupBox groupBox2;
        private Label label5;
        private TextBox devolucionTime;
        private TextBox devolucionRate;
        private Label label6;
        private GroupBox groupBox3;
        private Label label7;
        private TextBox pcTime;
        private TextBox pcRate;
        private Label label8;
        private GroupBox groupBox4;
        private Label label9;
        private TextBox infoTime;
        private TextBox infoRate;
        private Label label10;
        private Label label11;
        private GroupBox groupBox5;
        private GroupBox groupBox11;
        private Label label20;
        private TextBox consultaTime;
        private TextBox consultaRate;
        private Label label21;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private Label label12;
        private TextBox prestamoEstudianteTime;
        private TextBox prestamoEstudianteRate;
        private Label label13;
        private GroupBox groupBox8;
        private Label label14;
        private TextBox devolucionEstudianteTime;
        private TextBox devolucionEstudianteRate;
        private Label label15;
        private GroupBox groupBox9;
        private Label label16;
        private TextBox infoEstudianteTime;
        private TextBox infoEstudianteRate;
        private Label label17;
        private GroupBox groupBox10;
        private Label label18;
        private TextBox pcEstudianteTime;
        private TextBox pcEstudianteRate;
        private Label label19;
        private GroupBox groupBox12;
        private Label label22;
        private TextBox consultaEstudianteTime;
        private TextBox consultaEstudianteRate;
        private Label label23;
        private Label label24;
        private TextBox probMemb;
        private Label label25;
        private TextBox membTime;
        private TextBox membRate;
        private Label label26;
    }
}
