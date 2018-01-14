namespace WindowsFormsApplication1
{
    partial class FBGForm
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartReflection = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTransmission = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_CountOfProbe = new System.Windows.Forms.TextBox();
            this.tb_MinimalPeriod = new System.Windows.Forms.TextBox();
            this.tb_maximalPeriod = new System.Windows.Forms.TextBox();
            this.tb_GratingLength = new System.Windows.Forms.TextBox();
            this.tbGratingPeriod = new System.Windows.Forms.TextBox();
            this.tb_GratingRIM = new System.Windows.Forms.TextBox();
            this.tb_Grating_NEff = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartReflection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTransmission)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartReflection
            // 
            chartArea1.Name = "ChartArea1";
            this.chartReflection.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartReflection.Legends.Add(legend1);
            this.chartReflection.Location = new System.Drawing.Point(47, 152);
            this.chartReflection.Name = "chartReflection";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Reflection";
            this.chartReflection.Series.Add(series1);
            this.chartReflection.Size = new System.Drawing.Size(434, 300);
            this.chartReflection.TabIndex = 0;
            this.chartReflection.Text = "Transmission";
            title1.Name = "Reflection";
            title1.Text = "Reflection";
            this.chartReflection.Titles.Add(title1);
            // 
            // chartTransmission
            // 
            chartArea2.Name = "ChartArea1";
            this.chartTransmission.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartTransmission.Legends.Add(legend2);
            this.chartTransmission.Location = new System.Drawing.Point(501, 152);
            this.chartTransmission.Name = "chartTransmission";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Transmission";
            this.chartTransmission.Series.Add(series2);
            this.chartTransmission.Size = new System.Drawing.Size(461, 300);
            this.chartTransmission.TabIndex = 1;
            this.chartTransmission.Text = "Transmission";
            title2.Name = "Transmission";
            title2.Text = "Transmission";
            this.chartTransmission.Titles.Add(title2);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(682, 449);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 2;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_maximalPeriod);
            this.groupBox1.Controls.Add(this.tb_MinimalPeriod);
            this.groupBox1.Controls.Add(this.tb_CountOfProbe);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(139, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 117);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Simulation data";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "MaximalPeriod";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "MinimalPeriod";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CountOfProbes";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_Grating_NEff);
            this.groupBox2.Controls.Add(this.tb_GratingRIM);
            this.groupBox2.Controls.Add(this.tbGratingPeriod);
            this.groupBox2.Controls.Add(this.tb_GratingLength);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(361, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 134);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grating data";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "length";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "period";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 2;
            this.label6.Tag = "refractive index modulation";
            this.label6.Text = "rim";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "neff";
            // 
            // tb_CountOfProbe
            // 
            this.tb_CountOfProbe.Location = new System.Drawing.Point(93, 20);
            this.tb_CountOfProbe.Name = "tb_CountOfProbe";
            this.tb_CountOfProbe.Size = new System.Drawing.Size(100, 20);
            this.tb_CountOfProbe.TabIndex = 3;
            // 
            // tb_MinimalPeriod
            // 
            this.tb_MinimalPeriod.Location = new System.Drawing.Point(93, 46);
            this.tb_MinimalPeriod.Name = "tb_MinimalPeriod";
            this.tb_MinimalPeriod.Size = new System.Drawing.Size(100, 20);
            this.tb_MinimalPeriod.TabIndex = 4;
            // 
            // tb_maximalPeriod
            // 
            this.tb_maximalPeriod.Location = new System.Drawing.Point(93, 68);
            this.tb_maximalPeriod.Name = "tb_maximalPeriod";
            this.tb_maximalPeriod.Size = new System.Drawing.Size(100, 20);
            this.tb_maximalPeriod.TabIndex = 5;
            // 
            // tb_GratingLength
            // 
            this.tb_GratingLength.Location = new System.Drawing.Point(59, 20);
            this.tb_GratingLength.Name = "tb_GratingLength";
            this.tb_GratingLength.Size = new System.Drawing.Size(100, 20);
            this.tb_GratingLength.TabIndex = 4;
            // 
            // tbGratingPeriod
            // 
            this.tbGratingPeriod.Location = new System.Drawing.Point(59, 46);
            this.tbGratingPeriod.Name = "tbGratingPeriod";
            this.tbGratingPeriod.Size = new System.Drawing.Size(100, 20);
            this.tbGratingPeriod.TabIndex = 5;
            // 
            // tb_GratingRIM
            // 
            this.tb_GratingRIM.Location = new System.Drawing.Point(59, 72);
            this.tb_GratingRIM.Name = "tb_GratingRIM";
            this.tb_GratingRIM.Size = new System.Drawing.Size(100, 20);
            this.tb_GratingRIM.TabIndex = 6;
            // 
            // tb_Grating_NEff
            // 
            this.tb_Grating_NEff.Location = new System.Drawing.Point(59, 98);
            this.tb_Grating_NEff.Name = "tb_Grating_NEff";
            this.tb_Grating_NEff.Size = new System.Drawing.Size(100, 20);
            this.tb_Grating_NEff.TabIndex = 7;
            // 
            // FBGForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 517);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.chartTransmission);
            this.Controls.Add(this.chartReflection);
            this.Name = "FBGForm";
            this.Text = "FBG";
            this.Load += new System.EventHandler(this.FBGForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartReflection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTransmission)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartReflection;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTransmission;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_maximalPeriod;
        private System.Windows.Forms.TextBox tb_MinimalPeriod;
        private System.Windows.Forms.TextBox tb_CountOfProbe;
        private System.Windows.Forms.TextBox tb_Grating_NEff;
        private System.Windows.Forms.TextBox tb_GratingRIM;
        private System.Windows.Forms.TextBox tbGratingPeriod;
        private System.Windows.Forms.TextBox tb_GratingLength;
    }
}

