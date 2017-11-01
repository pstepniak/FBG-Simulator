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
            ((System.ComponentModel.ISupportInitialize)(this.chartReflection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTransmission)).BeginInit();
            this.SuspendLayout();
            // 
            // chartReflection
            // 
            chartArea1.Name = "ChartArea1";
            this.chartReflection.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartReflection.Legends.Add(legend1);
            this.chartReflection.Location = new System.Drawing.Point(47, 85);
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
            this.chartTransmission.Location = new System.Drawing.Point(502, 85);
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
            // FBGForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 517);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.chartTransmission);
            this.Controls.Add(this.chartReflection);
            this.Name = "FBGForm";
            this.Text = "FBG";
            this.Load += new System.EventHandler(this.FBGForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartReflection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTransmission)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartReflection;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTransmission;
        private System.Windows.Forms.Button btnCalculate;
    }
}

