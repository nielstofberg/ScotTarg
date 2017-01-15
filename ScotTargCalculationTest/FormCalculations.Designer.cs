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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCalcConstant = new System.Windows.Forms.TextBox();
            this.txtRefX = new System.Windows.Forms.TextBox();
            this.txtRefY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDistFactor = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeBDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calcXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calcYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.distDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtShotsCalcBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsData = new ScotTargCalculationTest.DsData();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.distDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.dtShotsCalcBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 104);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 20;
            this.dataGridView1.Size = new System.Drawing.Size(864, 351);
            this.dataGridView1.TabIndex = 8;
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
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Reference X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Reference Y";
            // 
            // txtCalcConstant
            // 
            this.txtCalcConstant.Location = new System.Drawing.Point(122, 12);
            this.txtCalcConstant.Name = "txtCalcConstant";
            this.txtCalcConstant.Size = new System.Drawing.Size(83, 20);
            this.txtCalcConstant.TabIndex = 10;
            this.txtCalcConstant.Text = "900";
            // 
            // txtRefX
            // 
            this.txtRefX.Location = new System.Drawing.Point(122, 38);
            this.txtRefX.Name = "txtRefX";
            this.txtRefX.Size = new System.Drawing.Size(83, 20);
            this.txtRefX.TabIndex = 10;
            this.txtRefX.Text = "400";
            // 
            // txtRefY
            // 
            this.txtRefY.Location = new System.Drawing.Point(122, 64);
            this.txtRefY.Name = "txtRefY";
            this.txtRefY.Size = new System.Drawing.Size(83, 20);
            this.txtRefY.TabIndex = 10;
            this.txtRefY.Text = "400";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(280, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Dist Factor";
            // 
            // txtDistFactor
            // 
            this.txtDistFactor.Location = new System.Drawing.Point(344, 9);
            this.txtDistFactor.Name = "txtDistFactor";
            this.txtDistFactor.Size = new System.Drawing.Size(83, 20);
            this.txtDistFactor.TabIndex = 10;
            this.txtDistFactor.Text = "4.29";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(801, 6);
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
            this.openFileDialog1.Filter = "Text File|*.txt";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(344, 75);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 11;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // timeADataGridViewTextBoxColumn
            // 
            this.timeADataGridViewTextBoxColumn.DataPropertyName = "TimeA";
            this.timeADataGridViewTextBoxColumn.HeaderText = "TimeA";
            this.timeADataGridViewTextBoxColumn.Name = "timeADataGridViewTextBoxColumn";
            // 
            // timeBDataGridViewTextBoxColumn
            // 
            this.timeBDataGridViewTextBoxColumn.DataPropertyName = "TimeB";
            this.timeBDataGridViewTextBoxColumn.HeaderText = "TimeB";
            this.timeBDataGridViewTextBoxColumn.Name = "timeBDataGridViewTextBoxColumn";
            // 
            // timeCDataGridViewTextBoxColumn
            // 
            this.timeCDataGridViewTextBoxColumn.DataPropertyName = "TimeC";
            this.timeCDataGridViewTextBoxColumn.HeaderText = "TimeC";
            this.timeCDataGridViewTextBoxColumn.Name = "timeCDataGridViewTextBoxColumn";
            // 
            // timeDDataGridViewTextBoxColumn
            // 
            this.timeDDataGridViewTextBoxColumn.DataPropertyName = "TimeD";
            this.timeDDataGridViewTextBoxColumn.HeaderText = "TimeD";
            this.timeDDataGridViewTextBoxColumn.Name = "timeDDataGridViewTextBoxColumn";
            // 
            // calcXDataGridViewTextBoxColumn
            // 
            this.calcXDataGridViewTextBoxColumn.DataPropertyName = "CalcX";
            this.calcXDataGridViewTextBoxColumn.HeaderText = "CalcX";
            this.calcXDataGridViewTextBoxColumn.Name = "calcXDataGridViewTextBoxColumn";
            // 
            // calcYDataGridViewTextBoxColumn
            // 
            this.calcYDataGridViewTextBoxColumn.DataPropertyName = "CalcY";
            this.calcYDataGridViewTextBoxColumn.HeaderText = "CalcY";
            this.calcYDataGridViewTextBoxColumn.Name = "calcYDataGridViewTextBoxColumn";
            // 
            // distDataGridViewTextBoxColumn
            // 
            this.distDataGridViewTextBoxColumn.DataPropertyName = "Dist";
            this.distDataGridViewTextBoxColumn.HeaderText = "Dist";
            this.distDataGridViewTextBoxColumn.Name = "distDataGridViewTextBoxColumn";
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
            // FormCalculations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 467);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtRefY);
            this.Controls.Add(this.txtRefX);
            this.Controls.Add(this.txtDistFactor);
            this.Controls.Add(this.txtCalcConstant);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormCalculations";
            this.Text = "ReCalculate";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtShotsCalcBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeADataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeBDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn calcXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn calcYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn distDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource dtShotsCalcBindingSource;
        private DsData dsData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCalcConstant;
        private System.Windows.Forms.TextBox txtRefX;
        private System.Windows.Forms.TextBox txtRefY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDistFactor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnCalculate;
    }
}