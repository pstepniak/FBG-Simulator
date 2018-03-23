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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartReflection = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTransmission = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_maximalWavelength = new System.Windows.Forms.TextBox();
            this.tb_MinimalWavelength = new System.Windows.Forms.TextBox();
            this.tb_CountOfProbe = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_GratingParts = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_Grating_NEff = new System.Windows.Forms.TextBox();
            this.tb_GratingRIM = new System.Windows.Forms.TextBox();
            this.tbGratingPeriod = new System.Windows.Forms.TextBox();
            this.tb_GratingLength = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_gratingApodProfile = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chartApod = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tb_grtingApodParam = new System.Windows.Forms.TextBox();
            this.l_grtingApodParam = new System.Windows.Forms.Label();
            this.cb_gratingApodReverse = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartReflection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTransmission)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartApod)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartReflection
            // 
            chartArea1.Name = "ChartArea1";
            this.chartReflection.ChartAreas.Add(chartArea1);
            this.chartReflection.Location = new System.Drawing.Point(52, 316);
            this.chartReflection.Name = "chartReflection";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Reflection";
            this.chartReflection.Series.Add(series1);
            this.chartReflection.Size = new System.Drawing.Size(444, 300);
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
            this.chartTransmission.Location = new System.Drawing.Point(506, 316);
            this.chartTransmission.Name = "chartTransmission";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Name = "Transmission";
            this.chartTransmission.Series.Add(series2);
            this.chartTransmission.Size = new System.Drawing.Size(454, 300);
            this.chartTransmission.TabIndex = 1;
            this.chartTransmission.Text = "Transmission";
            title2.Name = "Transmission";
            title2.Text = "Transmission";
            this.chartTransmission.Titles.Add(title2);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(885, 287);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 2;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_maximalWavelength);
            this.groupBox1.Controls.Add(this.tb_MinimalWavelength);
            this.groupBox1.Controls.Add(this.tb_CountOfProbe);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(52, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(235, 298);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Simulation data";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // tb_maximalWavelength
            // 
            this.tb_maximalWavelength.Location = new System.Drawing.Point(115, 72);
            this.tb_maximalWavelength.Name = "tb_maximalWavelength";
            this.tb_maximalWavelength.Size = new System.Drawing.Size(100, 20);
            this.tb_maximalWavelength.TabIndex = 5;
            // 
            // tb_MinimalWavelength
            // 
            this.tb_MinimalWavelength.Location = new System.Drawing.Point(115, 46);
            this.tb_MinimalWavelength.Name = "tb_MinimalWavelength";
            this.tb_MinimalWavelength.Size = new System.Drawing.Size(100, 20);
            this.tb_MinimalWavelength.TabIndex = 4;
            // 
            // tb_CountOfProbe
            // 
            this.tb_CountOfProbe.Location = new System.Drawing.Point(116, 20);
            this.tb_CountOfProbe.Name = "tb_CountOfProbe";
            this.tb_CountOfProbe.Size = new System.Drawing.Size(100, 20);
            this.tb_CountOfProbe.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Maximal wavelength";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Minimal Wavelength";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Count of probes";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Location = new System.Drawing.Point(293, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(586, 298);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grating data";
            // 
            // tb_GratingParts
            // 
            this.tb_GratingParts.Location = new System.Drawing.Point(71, 126);
            this.tb_GratingParts.Name = "tb_GratingParts";
            this.tb_GratingParts.Size = new System.Drawing.Size(100, 20);
            this.tb_GratingParts.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "parts";
            // 
            // tb_Grating_NEff
            // 
            this.tb_Grating_NEff.Location = new System.Drawing.Point(71, 100);
            this.tb_Grating_NEff.Name = "tb_Grating_NEff";
            this.tb_Grating_NEff.Size = new System.Drawing.Size(100, 20);
            this.tb_Grating_NEff.TabIndex = 7;
            // 
            // tb_GratingRIM
            // 
            this.tb_GratingRIM.Location = new System.Drawing.Point(71, 74);
            this.tb_GratingRIM.Name = "tb_GratingRIM";
            this.tb_GratingRIM.Size = new System.Drawing.Size(100, 20);
            this.tb_GratingRIM.TabIndex = 6;
            // 
            // tbGratingPeriod
            // 
            this.tbGratingPeriod.Location = new System.Drawing.Point(71, 48);
            this.tbGratingPeriod.Name = "tbGratingPeriod";
            this.tbGratingPeriod.Size = new System.Drawing.Size(100, 20);
            this.tbGratingPeriod.TabIndex = 5;
            // 
            // tb_GratingLength
            // 
            this.tb_GratingLength.Location = new System.Drawing.Point(71, 22);
            this.tb_GratingLength.Name = "tb_GratingLength";
            this.tb_GratingLength.Size = new System.Drawing.Size(100, 20);
            this.tb_GratingLength.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "neff";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 2;
            this.label6.Tag = "refractive index modulation";
            this.label6.Text = "rim";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "period [nm]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "length [um]";
            // 
            // cb_gratingApodProfile
            // 
            this.cb_gratingApodProfile.FormattingEnabled = true;
            this.cb_gratingApodProfile.Location = new System.Drawing.Point(60, 21);
            this.cb_gratingApodProfile.Name = "cb_gratingApodProfile";
            this.cb_gratingApodProfile.Size = new System.Drawing.Size(99, 21);
            this.cb_gratingApodProfile.TabIndex = 10;
            this.cb_gratingApodProfile.SelectedValueChanged += new System.EventHandler(this.cb_gratingApodProfile_SelectedValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "profile";
            // 
            // chartApod
            // 
            chartArea3.Name = "ChartArea1";
            this.chartApod.ChartAreas.Add(chartArea3);
            this.chartApod.Location = new System.Drawing.Point(6, 100);
            this.chartApod.Name = "chartApod";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend2";
            series3.Name = "Transmission";
            this.chartApod.Series.Add(series3);
            this.chartApod.Size = new System.Drawing.Size(336, 169);
            this.chartApod.TabIndex = 12;
            this.chartApod.Text = "Apodisation Profile";
            title3.IsDockedInsideChartArea = false;
            title3.Name = "Apodisation Profile";
            title3.Text = "Apodisation Profile";
            this.chartApod.Titles.Add(title3);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cb_gratingApodReverse);
            this.groupBox3.Controls.Add(this.l_grtingApodParam);
            this.groupBox3.Controls.Add(this.tb_grtingApodParam);
            this.groupBox3.Controls.Add(this.cb_gratingApodProfile);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.chartApod);
            this.groupBox3.Location = new System.Drawing.Point(232, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(348, 269);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Apodisation";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.tb_GratingParts);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.tb_GratingLength);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.tbGratingPeriod);
            this.groupBox4.Controls.Add(this.tb_Grating_NEff);
            this.groupBox4.Controls.Add(this.tb_GratingRIM);
            this.groupBox4.Location = new System.Drawing.Point(14, 23);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(199, 269);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Grating properties";
            // 
            // tb_grtingApodParam
            // 
            this.tb_grtingApodParam.Location = new System.Drawing.Point(60, 48);
            this.tb_grtingApodParam.Name = "tb_grtingApodParam";
            this.tb_grtingApodParam.Size = new System.Drawing.Size(99, 20);
            this.tb_grtingApodParam.TabIndex = 10;
            // 
            // l_grtingApodParam
            // 
            this.l_grtingApodParam.AutoSize = true;
            this.l_grtingApodParam.Location = new System.Drawing.Point(19, 49);
            this.l_grtingApodParam.Name = "l_grtingApodParam";
            this.l_grtingApodParam.Size = new System.Drawing.Size(36, 13);
            this.l_grtingApodParam.TabIndex = 13;
            this.l_grtingApodParam.Text = "param";
            // 
            // cb_gratingApodReverse
            // 
            this.cb_gratingApodReverse.AutoSize = true;
            this.cb_gratingApodReverse.Location = new System.Drawing.Point(17, 74);
            this.cb_gratingApodReverse.Name = "cb_gratingApodReverse";
            this.cb_gratingApodReverse.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_gratingApodReverse.Size = new System.Drawing.Size(142, 17);
            this.cb_gratingApodReverse.TabIndex = 14;
            this.cb_gratingApodReverse.Text = "        reverse apodisation";
            this.cb_gratingApodReverse.UseVisualStyleBackColor = true;
            // 
            // FBGForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 628);
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
            ((System.ComponentModel.ISupportInitialize)(this.chartApod)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private System.Windows.Forms.TextBox tb_maximalWavelength;
        private System.Windows.Forms.TextBox tb_MinimalWavelength;
        private System.Windows.Forms.TextBox tb_CountOfProbe;
        private System.Windows.Forms.TextBox tb_Grating_NEff;
        private System.Windows.Forms.TextBox tb_GratingRIM;
        private System.Windows.Forms.TextBox tbGratingPeriod;
        private System.Windows.Forms.TextBox tb_GratingLength;
        private System.Windows.Forms.TextBox tb_GratingParts;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartApod;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_gratingApodProfile;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cb_gratingApodReverse;
        private System.Windows.Forms.Label l_grtingApodParam;
        private System.Windows.Forms.TextBox tb_grtingApodParam;
    }
}

