namespace TP2_WF.Presentacion
{
    partial class PantallaVisualizacion
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.gdw_dataSet = new System.Windows.Forms.DataGridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gdw_frecObs = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gdw_dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdw_frecObs)).BeginInit();
            this.SuspendLayout();
            // 
            // gdw_dataSet
            // 
            this.gdw_dataSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdw_dataSet.Location = new System.Drawing.Point(9, 38);
            this.gdw_dataSet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gdw_dataSet.Name = "gdw_dataSet";
            this.gdw_dataSet.RowHeadersWidth = 51;
            this.gdw_dataSet.RowTemplate.Height = 24;
            this.gdw_dataSet.Size = new System.Drawing.Size(144, 459);
            this.gdw_dataSet.TabIndex = 0;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(172, 38);
            this.chart1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(669, 459);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // gdw_frecObs
            // 
            this.gdw_frecObs.AllowUserToAddRows = false;
            this.gdw_frecObs.AllowUserToDeleteRows = false;
            this.gdw_frecObs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdw_frecObs.Location = new System.Drawing.Point(858, 38);
            this.gdw_frecObs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gdw_frecObs.Name = "gdw_frecObs";
            this.gdw_frecObs.ReadOnly = true;
            this.gdw_frecObs.RowHeadersWidth = 51;
            this.gdw_frecObs.RowTemplate.Height = 24;
            this.gdw_frecObs.Size = new System.Drawing.Size(305, 459);
            this.gdw_frecObs.TabIndex = 2;
            // 
            // PantallaVisualizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 553);
            this.Controls.Add(this.gdw_frecObs);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.gdw_dataSet);
            this.Name = "PantallaVisualizacion";
            this.Text = "Visualización de Datos Generados";
            this.Load += new System.EventHandler(this.PantallaVisualizacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gdw_dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdw_frecObs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gdw_dataSet;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridView gdw_frecObs;
    }
}