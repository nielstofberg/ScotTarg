namespace ScotTargCalculationTest
{
    partial class FormCalculations
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRefX = new System.Windows.Forms.TextBox();
            this.txtRefY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDistFactor = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.nudCorrection = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.dtShotsCalcBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsData = new ScotTargCalculationTest.DsData();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeBDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calcXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calcYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.distDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCorrection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtShotsCalcBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsData)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.timeADataGridViewTextBoxColumn,
            this.timeBDataGridViewTextBoxColumn,
            this.timeCDataGridViewTextBoxColumn,
            this.timeDDataGridViewTextBoxColumn,
            this.calcXDataGridViewTextBoxColumn,
            this.calcYDataGridViewTextBoxColumn,
            this.distDataGridViewTextBoxColumn,
            this.tlX,
            this.tlY,
            this.brX,
            this.brY});
            this.dataGridView1.DataSource = this.dtShotsCalcBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 104);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 20;
            this.dataGridView1.Size = new System.Drawing.Size(771, 351);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Calculation Constant";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Reference X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Reference Y";
            // 
            // txtRefX
            // 
            this.txtRefX.Location = new System.Drawing.Point(122, 51);
            this.txtRefX.Name = "txtRefX";
            this.txtRefX.Size = new System.Drawing.Size(41, 20);
            this.txtRefX.TabIndex = 10;
            this.txtRefX.Text = "400";
            // 
            // txtRefY
            // 
            this.txtRefY.Location = new System.Drawing.Point(122, 77);
            this.txtRefY.Name = "txtRefY";
            this.txtRefY.Size = new System.Drawing.Size(41, 20);
            this.txtRefY.TabIndex = 10;
            this.txtRefY.Text = "400";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(215, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Dist Factor";
            // 
            // txtDistFactor
            // 
            this.txtDistFactor.Location = new System.Drawing.Point(279, 12);
            this.txtDistFactor.Name = "txtDistFactor";
            this.txtDistFactor.Size = new System.Drawing.Size(83, 20);
            this.txtDistFactor.TabIndex = 10;
            this.txtDistFactor.Text = "0.0225";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(390, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Import";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "STG Files (*.stg) | *.stg";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(471, 12);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 11;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // nudWidth
            // 
            this.nudWidth.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudWidth.Location = new System.Drawing.Point(122, 12);
            this.nudWidth.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(87, 20);
            this.nudWidth.TabIndex = 12;
            this.nudWidth.Value = new decimal(new int[] {
            13120,
            0,
            0,
            0});
            this.nudWidth.ValueChanged += new System.EventHandler(this.btnCalculate_Click);
            // 
            // nudCorrection
            // 
            this.nudCorrection.Location = new System.Drawing.Point(279, 52);
            this.nudCorrection.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudCorrection.Name = "nudCorrection";
            this.nudCorrection.Size = new System.Drawing.Size(60, 20);
            this.nudCorrection.TabIndex = 12;
            this.nudCorrection.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.nudCorrection.ValueChanged += new System.EventHandler(this.btnCalculate_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(215, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Correction";
            // 
            // dtShotsCalcBindingSource
            // 
            this.dtShotsCalcBindingSource.DataMember = "DtShotsCalc";
            this.dtShotsCalcBindingSource.DataSource = this.dsData;
            // 
            // dsData
            // 
            this.dsData.DataSetName = "DsData";
            this.dsData.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.Frozen = true;
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Width = 41;
            // 
            // timeADataGridViewTextBoxColumn
            // 
            this.timeADataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.timeADataGridViewTextBoxColumn.DataPropertyName = "TimeA";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.timeADataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.timeADataGridViewTextBoxColumn.HeaderText = "TimeA";
            this.timeADataGridViewTextBoxColumn.Name = "timeADataGridViewTextBoxColumn";
            this.timeADataGridViewTextBoxColumn.Width = 62;
            // 
            // timeBDataGridViewTextBoxColumn
            // 
            this.timeBDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.timeBDataGridViewTextBoxColumn.DataPropertyName = "TimeB";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.timeBDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.timeBDataGridViewTextBoxColumn.HeaderText = "TimeB";
            this.timeBDataGridViewTextBoxColumn.Name = "timeBDataGridViewTextBoxColumn";
            this.timeBDataGridViewTextBoxColumn.Width = 62;
            // 
            // timeCDataGridViewTextBoxColumn
            // 
            this.timeCDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.timeCDataGridViewTextBoxColumn.DataPropertyName = "TimeC";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.timeCDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.timeCDataGridViewTextBoxColumn.HeaderText = "TimeC";
            this.timeCDataGridViewTextBoxColumn.Name = "timeCDataGridViewTextBoxColumn";
            this.timeCDataGridViewTextBoxColumn.Width = 62;
            // 
            // timeDDataGridViewTextBoxColumn
            // 
            this.timeDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.timeDDataGridViewTextBoxColumn.DataPropertyName = "TimeD";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.timeDDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.timeDDataGridViewTextBoxColumn.HeaderText = "TimeD";
            this.timeDDataGridViewTextBoxColumn.Name = "timeDDataGridViewTextBoxColumn";
            this.timeDDataGridViewTextBoxColumn.Width = 63;
            // 
            // calcXDataGridViewTextBoxColumn
            // 
            this.calcXDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.calcXDataGridViewTextBoxColumn.DataPropertyName = "CalcX";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.calcXDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.calcXDataGridViewTextBoxColumn.HeaderText = "CalcX";
            this.calcXDataGridViewTextBoxColumn.Name = "calcXDataGridViewTextBoxColumn";
            this.calcXDataGridViewTextBoxColumn.Width = 60;
            // 
            // calcYDataGridViewTextBoxColumn
            // 
            this.calcYDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.calcYDataGridViewTextBoxColumn.DataPropertyName = "CalcY";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.calcYDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle13;
            this.calcYDataGridViewTextBoxColumn.HeaderText = "CalcY";
            this.calcYDataGridViewTextBoxColumn.Name = "calcYDataGridViewTextBoxColumn";
            this.calcYDataGridViewTextBoxColumn.Width = 60;
            // 
            // distDataGridViewTextBoxColumn
            // 
            this.distDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.distDataGridViewTextBoxColumn.DataPropertyName = "Dist";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.distDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle14;
            this.distDataGridViewTextBoxColumn.HeaderText = "Dist";
            this.distDataGridViewTextBoxColumn.Name = "distDataGridViewTextBoxColumn";
            this.distDataGridViewTextBoxColumn.Width = 50;
            // 
            // tlX
            // 
            this.tlX.DataPropertyName = "tlX";
            this.tlX.HeaderText = "TL X";
            this.tlX.Name = "tlX";
            this.tlX.ReadOnly = true;
            this.tlX.Width = 60;
            // 
            // tlY
            // 
            this.tlY.DataPropertyName = "tlY";
            this.tlY.HeaderText = "TL Y";
            this.tlY.Name = "tlY";
            this.tlY.ReadOnly = true;
            this.tlY.Width = 60;
            // 
            // brX
            // 
            this.brX.DataPropertyName = "brX";
            this.brX.HeaderText = "BR X";
            this.brX.Name = "brX";
            this.brX.ReadOnly = true;
            this.brX.Width = 60;
            // 
            // brY
            // 
            this.brY.DataPropertyName = "brY";
            this.brY.HeaderText = "BR Y";
            this.brY.Name = "brY";
            this.brY.ReadOnly = true;
            this.brY.Width = 60;
            // 
            // FormCalculations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 467);
            this.Controls.Add(this.nudCorrection);
            this.Controls.Add(this.nudWidth);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtRefY);
            this.Controls.Add(this.txtRefX);
            this.Controls.Add(this.txtDistFactor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormCalculations";
            this.Text = "ReCalculate";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCorrection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtShotsCalcBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource dtShotsCalcBindingSource;
        private DsData dsData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRefX;
        private System.Windows.Forms.TextBox txtRefY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDistFactor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.NumericUpDown nudCorrection;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeADataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeBDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn calcXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn calcYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn distDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tlX;
        private System.Windows.Forms.DataGridViewTextBoxColumn tlY;
        private System.Windows.Forms.DataGridViewTextBoxColumn brX;
        private System.Windows.Forms.DataGridViewTextBoxColumn brY;
    }
}