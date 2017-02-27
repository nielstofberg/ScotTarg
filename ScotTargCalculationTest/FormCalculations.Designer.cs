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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dtShotsCalcBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsData = new ScotTargCalculationTest.DsData();
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
            this.label5 = new System.Windows.Forms.Label();
            this.txtRefAx = new System.Windows.Forms.TextBox();
            this.txtRefAy = new System.Windows.Forms.TextBox();
            this.txtRefBx = new System.Windows.Forms.TextBox();
            this.txtRefBy = new System.Windows.Forms.TextBox();
            this.txtRefCx = new System.Windows.Forms.TextBox();
            this.txtRefCy = new System.Windows.Forms.TextBox();
            this.txtRefDx = new System.Windows.Forms.TextBox();
            this.txtRefDy = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeBDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calcXDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calcYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.distDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DistA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DistB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DistC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DistD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalcXa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalcXb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalcXc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalcXd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalcYa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalcYb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalcYc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalcYd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBestConstX = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBestConstY = new System.Windows.Forms.TextBox();
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
            this.distDataGridViewTextBoxColumn,
            this.DistA,
            this.DistB,
            this.DistC,
            this.DistD,
            this.CalcXa,
            this.CalcXb,
            this.CalcXc,
            this.CalcXd,
            this.CalcYa,
            this.CalcYb,
            this.CalcYc,
            this.CalcYd});
            this.dataGridView1.DataSource = this.dtShotsCalcBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 104);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 20;
            this.dataGridView1.Size = new System.Drawing.Size(1218, 351);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
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
            // txtCalcConstant
            // 
            this.txtCalcConstant.Location = new System.Drawing.Point(122, 12);
            this.txtCalcConstant.Name = "txtCalcConstant";
            this.txtCalcConstant.Size = new System.Drawing.Size(83, 20);
            this.txtCalcConstant.TabIndex = 10;
            this.txtCalcConstant.Text = "3306";
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
            this.label4.Location = new System.Drawing.Point(215, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Dist Factor";
            // 
            // txtDistFactor
            // 
            this.txtDistFactor.Location = new System.Drawing.Point(279, 8);
            this.txtDistFactor.Name = "txtDistFactor";
            this.txtDistFactor.Size = new System.Drawing.Size(83, 20);
            this.txtDistFactor.TabIndex = 10;
            this.txtDistFactor.Text = "0.09";
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
            this.openFileDialog1.Filter = "STG Files (*.stg) | *.stg";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(801, 75);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 11;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(198, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "A";
            // 
            // txtRefAx
            // 
            this.txtRefAx.Location = new System.Drawing.Point(185, 51);
            this.txtRefAx.Name = "txtRefAx";
            this.txtRefAx.Size = new System.Drawing.Size(41, 20);
            this.txtRefAx.TabIndex = 10;
            this.txtRefAx.Text = "400";
            // 
            // txtRefAy
            // 
            this.txtRefAy.Location = new System.Drawing.Point(185, 77);
            this.txtRefAy.Name = "txtRefAy";
            this.txtRefAy.Size = new System.Drawing.Size(41, 20);
            this.txtRefAy.TabIndex = 10;
            this.txtRefAy.Text = "400";
            // 
            // txtRefBx
            // 
            this.txtRefBx.Location = new System.Drawing.Point(232, 51);
            this.txtRefBx.Name = "txtRefBx";
            this.txtRefBx.Size = new System.Drawing.Size(41, 20);
            this.txtRefBx.TabIndex = 10;
            this.txtRefBx.Text = "400";
            // 
            // txtRefBy
            // 
            this.txtRefBy.Location = new System.Drawing.Point(232, 77);
            this.txtRefBy.Name = "txtRefBy";
            this.txtRefBy.Size = new System.Drawing.Size(41, 20);
            this.txtRefBy.TabIndex = 10;
            this.txtRefBy.Text = "400";
            // 
            // txtRefCx
            // 
            this.txtRefCx.Location = new System.Drawing.Point(279, 51);
            this.txtRefCx.Name = "txtRefCx";
            this.txtRefCx.Size = new System.Drawing.Size(41, 20);
            this.txtRefCx.TabIndex = 10;
            this.txtRefCx.Text = "400";
            // 
            // txtRefCy
            // 
            this.txtRefCy.Location = new System.Drawing.Point(279, 77);
            this.txtRefCy.Name = "txtRefCy";
            this.txtRefCy.Size = new System.Drawing.Size(41, 20);
            this.txtRefCy.TabIndex = 10;
            this.txtRefCy.Text = "400";
            // 
            // txtRefDx
            // 
            this.txtRefDx.Location = new System.Drawing.Point(326, 51);
            this.txtRefDx.Name = "txtRefDx";
            this.txtRefDx.Size = new System.Drawing.Size(41, 20);
            this.txtRefDx.TabIndex = 10;
            this.txtRefDx.Text = "400";
            // 
            // txtRefDy
            // 
            this.txtRefDy.Location = new System.Drawing.Point(326, 77);
            this.txtRefDy.Name = "txtRefDy";
            this.txtRefDy.Size = new System.Drawing.Size(41, 20);
            this.txtRefDy.TabIndex = 10;
            this.txtRefDy.Text = "400";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(245, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "B";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(292, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "C";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(339, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "D";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(446, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.timeADataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.timeADataGridViewTextBoxColumn.HeaderText = "TimeA";
            this.timeADataGridViewTextBoxColumn.Name = "timeADataGridViewTextBoxColumn";
            this.timeADataGridViewTextBoxColumn.Width = 62;
            // 
            // timeBDataGridViewTextBoxColumn
            // 
            this.timeBDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.timeBDataGridViewTextBoxColumn.DataPropertyName = "TimeB";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.timeBDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.timeBDataGridViewTextBoxColumn.HeaderText = "TimeB";
            this.timeBDataGridViewTextBoxColumn.Name = "timeBDataGridViewTextBoxColumn";
            this.timeBDataGridViewTextBoxColumn.Width = 62;
            // 
            // timeCDataGridViewTextBoxColumn
            // 
            this.timeCDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.timeCDataGridViewTextBoxColumn.DataPropertyName = "TimeC";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.timeCDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.timeCDataGridViewTextBoxColumn.HeaderText = "TimeC";
            this.timeCDataGridViewTextBoxColumn.Name = "timeCDataGridViewTextBoxColumn";
            this.timeCDataGridViewTextBoxColumn.Width = 62;
            // 
            // timeDDataGridViewTextBoxColumn
            // 
            this.timeDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.timeDDataGridViewTextBoxColumn.DataPropertyName = "TimeD";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.timeDDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.timeDDataGridViewTextBoxColumn.HeaderText = "TimeD";
            this.timeDDataGridViewTextBoxColumn.Name = "timeDDataGridViewTextBoxColumn";
            this.timeDDataGridViewTextBoxColumn.Width = 63;
            // 
            // calcXDataGridViewTextBoxColumn
            // 
            this.calcXDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.calcXDataGridViewTextBoxColumn.DataPropertyName = "CalcX";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.calcXDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.calcXDataGridViewTextBoxColumn.HeaderText = "CalcX";
            this.calcXDataGridViewTextBoxColumn.Name = "calcXDataGridViewTextBoxColumn";
            this.calcXDataGridViewTextBoxColumn.Width = 60;
            // 
            // calcYDataGridViewTextBoxColumn
            // 
            this.calcYDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.calcYDataGridViewTextBoxColumn.DataPropertyName = "CalcY";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.calcYDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.calcYDataGridViewTextBoxColumn.HeaderText = "CalcY";
            this.calcYDataGridViewTextBoxColumn.Name = "calcYDataGridViewTextBoxColumn";
            this.calcYDataGridViewTextBoxColumn.Width = 60;
            // 
            // distDataGridViewTextBoxColumn
            // 
            this.distDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.distDataGridViewTextBoxColumn.DataPropertyName = "Dist";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.distDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.distDataGridViewTextBoxColumn.HeaderText = "Dist";
            this.distDataGridViewTextBoxColumn.Name = "distDataGridViewTextBoxColumn";
            this.distDataGridViewTextBoxColumn.Width = 50;
            // 
            // DistA
            // 
            this.DistA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DistA.DataPropertyName = "DistA";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DistA.DefaultCellStyle = dataGridViewCellStyle8;
            this.DistA.HeaderText = "DistA";
            this.DistA.Name = "DistA";
            this.DistA.ReadOnly = true;
            this.DistA.Width = 57;
            // 
            // DistB
            // 
            this.DistB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DistB.DataPropertyName = "DistB";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DistB.DefaultCellStyle = dataGridViewCellStyle9;
            this.DistB.HeaderText = "DistB";
            this.DistB.Name = "DistB";
            this.DistB.ReadOnly = true;
            this.DistB.Width = 57;
            // 
            // DistC
            // 
            this.DistC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DistC.DataPropertyName = "DistC";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DistC.DefaultCellStyle = dataGridViewCellStyle10;
            this.DistC.HeaderText = "DistC";
            this.DistC.Name = "DistC";
            this.DistC.ReadOnly = true;
            this.DistC.Width = 57;
            // 
            // DistD
            // 
            this.DistD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DistD.DataPropertyName = "DistD";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DistD.DefaultCellStyle = dataGridViewCellStyle11;
            this.DistD.HeaderText = "DistD";
            this.DistD.Name = "DistD";
            this.DistD.ReadOnly = true;
            this.DistD.Width = 58;
            // 
            // CalcXa
            // 
            this.CalcXa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CalcXa.DataPropertyName = "CalcXa";
            this.CalcXa.HeaderText = "CalcXa";
            this.CalcXa.Name = "CalcXa";
            this.CalcXa.ReadOnly = true;
            this.CalcXa.Width = 66;
            // 
            // CalcXb
            // 
            this.CalcXb.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CalcXb.DataPropertyName = "CalcXb";
            this.CalcXb.HeaderText = "CalcXb";
            this.CalcXb.Name = "CalcXb";
            this.CalcXb.ReadOnly = true;
            this.CalcXb.Width = 66;
            // 
            // CalcXc
            // 
            this.CalcXc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CalcXc.DataPropertyName = "CalcXc";
            this.CalcXc.HeaderText = "CalcXc";
            this.CalcXc.Name = "CalcXc";
            this.CalcXc.ReadOnly = true;
            this.CalcXc.Width = 66;
            // 
            // CalcXd
            // 
            this.CalcXd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CalcXd.DataPropertyName = "CalcXd";
            this.CalcXd.HeaderText = "CalcXd";
            this.CalcXd.Name = "CalcXd";
            this.CalcXd.ReadOnly = true;
            this.CalcXd.Width = 66;
            // 
            // CalcYa
            // 
            this.CalcYa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CalcYa.DataPropertyName = "CalcYa";
            this.CalcYa.HeaderText = "CalcYa";
            this.CalcYa.Name = "CalcYa";
            this.CalcYa.ReadOnly = true;
            this.CalcYa.Width = 66;
            // 
            // CalcYb
            // 
            this.CalcYb.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CalcYb.DataPropertyName = "CalcYb";
            this.CalcYb.HeaderText = "CalcYb";
            this.CalcYb.Name = "CalcYb";
            this.CalcYb.ReadOnly = true;
            this.CalcYb.Width = 66;
            // 
            // CalcYc
            // 
            this.CalcYc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CalcYc.DataPropertyName = "CalcYc";
            this.CalcYc.HeaderText = "CalcYc";
            this.CalcYc.Name = "CalcYc";
            this.CalcYc.ReadOnly = true;
            this.CalcYc.Width = 66;
            // 
            // CalcYd
            // 
            this.CalcYd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CalcYd.DataPropertyName = "CalcYd";
            this.CalcYd.HeaderText = "CalcYd";
            this.CalcYd.Name = "CalcYd";
            this.CalcYd.ReadOnly = true;
            this.CalcYd.Width = 66;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(527, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Bestconst X";
            // 
            // txtBestConstX
            // 
            this.txtBestConstX.Location = new System.Drawing.Point(597, 11);
            this.txtBestConstX.Name = "txtBestConstX";
            this.txtBestConstX.Size = new System.Drawing.Size(66, 20);
            this.txtBestConstX.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(527, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Bestconst Y";
            // 
            // txtBestConstY
            // 
            this.txtBestConstY.Location = new System.Drawing.Point(597, 37);
            this.txtBestConstY.Name = "txtBestConstY";
            this.txtBestConstY.Size = new System.Drawing.Size(66, 20);
            this.txtBestConstY.TabIndex = 14;
            // 
            // FormCalculations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 467);
            this.Controls.Add(this.txtBestConstY);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtBestConstX);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtRefDy);
            this.Controls.Add(this.txtRefCy);
            this.Controls.Add(this.txtRefBy);
            this.Controls.Add(this.txtRefAy);
            this.Controls.Add(this.txtRefY);
            this.Controls.Add(this.txtRefDx);
            this.Controls.Add(this.txtRefCx);
            this.Controls.Add(this.txtRefBx);
            this.Controls.Add(this.txtRefAx);
            this.Controls.Add(this.txtRefX);
            this.Controls.Add(this.txtDistFactor);
            this.Controls.Add(this.txtCalcConstant);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRefAx;
        private System.Windows.Forms.TextBox txtRefAy;
        private System.Windows.Forms.TextBox txtRefBx;
        private System.Windows.Forms.TextBox txtRefBy;
        private System.Windows.Forms.TextBox txtRefCx;
        private System.Windows.Forms.TextBox txtRefCy;
        private System.Windows.Forms.TextBox txtRefDx;
        private System.Windows.Forms.TextBox txtRefDy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeADataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeBDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn calcXDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn calcYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn distDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DistA;
        private System.Windows.Forms.DataGridViewTextBoxColumn DistB;
        private System.Windows.Forms.DataGridViewTextBoxColumn DistC;
        private System.Windows.Forms.DataGridViewTextBoxColumn DistD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CalcXa;
        private System.Windows.Forms.DataGridViewTextBoxColumn CalcXb;
        private System.Windows.Forms.DataGridViewTextBoxColumn CalcXc;
        private System.Windows.Forms.DataGridViewTextBoxColumn CalcXd;
        private System.Windows.Forms.DataGridViewTextBoxColumn CalcYa;
        private System.Windows.Forms.DataGridViewTextBoxColumn CalcYb;
        private System.Windows.Forms.DataGridViewTextBoxColumn CalcYc;
        private System.Windows.Forms.DataGridViewTextBoxColumn CalcYd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBestConstX;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBestConstY;
    }
}