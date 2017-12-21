namespace ScotTarg.SingleTarget
{
    partial class FormSession
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.radOpen = new System.Windows.Forms.RadioButton();
            this.radNew = new System.Windows.Forms.RadioButton();
            this.gbNew = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmboShooter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboTarget = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudDistance = new System.Windows.Forms.NumericUpDown();
            this.txtSessionName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmboCalibre = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.gbNew.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistance)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(339, 333);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(258, 333);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // radOpen
            // 
            this.radOpen.AutoSize = true;
            this.radOpen.Location = new System.Drawing.Point(12, 12);
            this.radOpen.Name = "radOpen";
            this.radOpen.Size = new System.Drawing.Size(141, 17);
            this.radOpen.TabIndex = 1;
            this.radOpen.Text = "Open a previous session";
            this.radOpen.UseVisualStyleBackColor = true;
            // 
            // radNew
            // 
            this.radNew.AutoSize = true;
            this.radNew.Checked = true;
            this.radNew.Location = new System.Drawing.Point(12, 121);
            this.radNew.Name = "radNew";
            this.radNew.Size = new System.Drawing.Size(117, 17);
            this.radNew.TabIndex = 1;
            this.radNew.TabStop = true;
            this.radNew.Text = "Start a new session";
            this.radNew.UseVisualStyleBackColor = true;
            // 
            // gbNew
            // 
            this.gbNew.Controls.Add(this.nudDistance);
            this.gbNew.Controls.Add(this.txtSessionName);
            this.gbNew.Controls.Add(this.cmboCalibre);
            this.gbNew.Controls.Add(this.cmboTarget);
            this.gbNew.Controls.Add(this.label7);
            this.gbNew.Controls.Add(this.label5);
            this.gbNew.Controls.Add(this.label6);
            this.gbNew.Controls.Add(this.label3);
            this.gbNew.Controls.Add(this.label2);
            this.gbNew.Controls.Add(this.cmboShooter);
            this.gbNew.Controls.Add(this.label4);
            this.gbNew.Controls.Add(this.label1);
            this.gbNew.Location = new System.Drawing.Point(12, 144);
            this.gbNew.Name = "gbNew";
            this.gbNew.Size = new System.Drawing.Size(402, 166);
            this.gbNew.TabIndex = 3;
            this.gbNew.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(12, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 53);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(84, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(312, 20);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shooter Name";
            // 
            // cmboShooter
            // 
            this.cmboShooter.FormattingEnabled = true;
            this.cmboShooter.Location = new System.Drawing.Point(87, 50);
            this.cmboShooter.Name = "cmboShooter";
            this.cmboShooter.Size = new System.Drawing.Size(295, 21);
            this.cmboShooter.TabIndex = 1;
            this.cmboShooter.TextUpdate += new System.EventHandler(this.cmboShooter_TextUpdate);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Target";
            // 
            // cmboTarget
            // 
            this.cmboTarget.FormattingEnabled = true;
            this.cmboTarget.Items.AddRange(new object[] {
            "10m Air Rifle (ISSF)",
            "25yrd Rifle (NSRA)",
            "50m Rifle (ISSF)"});
            this.cmboTarget.Location = new System.Drawing.Point(87, 77);
            this.cmboTarget.Name = "cmboTarget";
            this.cmboTarget.Size = new System.Drawing.Size(295, 21);
            this.cmboTarget.TabIndex = 2;
            this.cmboTarget.SelectedIndexChanged += new System.EventHandler(this.cmboTarget_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Distance";
            // 
            // nudDistance
            // 
            this.nudDistance.DecimalPlaces = 2;
            this.nudDistance.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudDistance.Location = new System.Drawing.Point(87, 131);
            this.nudDistance.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDistance.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudDistance.Name = "nudDistance";
            this.nudDistance.Size = new System.Drawing.Size(120, 20);
            this.nudDistance.TabIndex = 4;
            this.nudDistance.Value = new decimal(new int[] {
            2286,
            0,
            0,
            131072});
            // 
            // txtSessionName
            // 
            this.txtSessionName.Location = new System.Drawing.Point(87, 19);
            this.txtSessionName.Name = "txtSessionName";
            this.txtSessionName.ReadOnly = true;
            this.txtSessionName.Size = new System.Drawing.Size(295, 20);
            this.txtSessionName.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Session Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(209, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "m";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Calibre";
            // 
            // cmboCalibre
            // 
            this.cmboCalibre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboCalibre.FormattingEnabled = true;
            this.cmboCalibre.Items.AddRange(new object[] {
            "4.5",
            "5.5"});
            this.cmboCalibre.Location = new System.Drawing.Point(87, 104);
            this.cmboCalibre.Name = "cmboCalibre";
            this.cmboCalibre.Size = new System.Drawing.Size(87, 21);
            this.cmboCalibre.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(173, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "mm";
            // 
            // FormSession
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(426, 368);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbNew);
            this.Controls.Add(this.radNew);
            this.Controls.Add(this.radOpen);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "FormSession";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Session";
            this.gbNew.ResumeLayout(false);
            this.gbNew.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton radOpen;
        private System.Windows.Forms.RadioButton radNew;
        private System.Windows.Forms.GroupBox gbNew;
        private System.Windows.Forms.NumericUpDown nudDistance;
        private System.Windows.Forms.ComboBox cmboTarget;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboShooter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSessionName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmboCalibre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}