namespace OGCSOSCopier
{
	partial class Form_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_source_version = new System.Windows.Forms.ComboBox();
            this.textBox_source_url = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_dest_version = new System.Windows.Forms.ComboBox();
            this.textBox_dest_url = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.settings_observations = new System.Windows.Forms.CheckBox();
            this.button_start = new System.Windows.Forms.Button();
            this.settings_procedure = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox_source_version);
            this.panel1.Controls.Add(this.textBox_source_url);
            this.panel1.Location = new System.Drawing.Point(12, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(765, 70);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Url";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(555, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Version";
            // 
            // comboBox_source_version
            // 
            this.comboBox_source_version.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_source_version.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox_source_version.FormattingEnabled = true;
            this.comboBox_source_version.Location = new System.Drawing.Point(617, 17);
            this.comboBox_source_version.Name = "comboBox_source_version";
            this.comboBox_source_version.Size = new System.Drawing.Size(121, 21);
            this.comboBox_source_version.TabIndex = 1;
            this.comboBox_source_version.SelectedValueChanged += new System.EventHandler(this.comboBox_source_version_SelectedValueChanged);
            // 
            // textBox_source_url
            // 
            this.textBox_source_url.Location = new System.Drawing.Point(53, 17);
            this.textBox_source_url.Name = "textBox_source_url";
            this.textBox_source_url.Size = new System.Drawing.Size(479, 20);
            this.textBox_source_url.TabIndex = 0;
            this.textBox_source_url.Text = "http://netigma.netcad.com.tr/SOS/Swe.svc/";
            this.textBox_source_url.TextChanged += new System.EventHandler(this.textBox_source_url_TextChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.comboBox_dest_version);
            this.panel2.Controls.Add(this.textBox_dest_url);
            this.panel2.Location = new System.Drawing.Point(11, 129);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(766, 70);
            this.panel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(12, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Url";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(555, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Version";
            // 
            // comboBox_dest_version
            // 
            this.comboBox_dest_version.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_dest_version.FormattingEnabled = true;
            this.comboBox_dest_version.Location = new System.Drawing.Point(617, 17);
            this.comboBox_dest_version.Name = "comboBox_dest_version";
            this.comboBox_dest_version.Size = new System.Drawing.Size(121, 21);
            this.comboBox_dest_version.TabIndex = 1;
            // 
            // textBox_dest_url
            // 
            this.textBox_dest_url.Location = new System.Drawing.Point(53, 17);
            this.textBox_dest_url.Name = "textBox_dest_url";
            this.textBox_dest_url.Size = new System.Drawing.Size(479, 20);
            this.textBox_dest_url.TabIndex = 0;
            this.textBox_dest_url.Text = "http://netigma.netcad.com.tr/SOS_DEV/Swe.svc/";
            this.textBox_dest_url.TextChanged += new System.EventHandler(this.textBox_dest_url_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "SOURCE";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(12, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "DESTINATION";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(12, 214);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "SETTINGS";
            // 
            // settings_observations
            // 
            this.settings_observations.AutoSize = true;
            this.settings_observations.Location = new System.Drawing.Point(101, 244);
            this.settings_observations.Name = "settings_observations";
            this.settings_observations.Size = new System.Drawing.Size(88, 17);
            this.settings_observations.TabIndex = 8;
            this.settings_observations.Text = "Observations";
            this.settings_observations.UseVisualStyleBackColor = true;
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(704, 240);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 9;
            this.button_start.Text = "START";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // settings_procedure
            // 
            this.settings_procedure.AutoSize = true;
            this.settings_procedure.Checked = true;
            this.settings_procedure.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_procedure.Location = new System.Drawing.Point(15, 244);
            this.settings_procedure.Name = "settings_procedure";
            this.settings_procedure.Size = new System.Drawing.Size(80, 17);
            this.settings_procedure.TabIndex = 7;
            this.settings_procedure.Text = "Procedures";
            this.settings_procedure.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.LightGray;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.richTextBox1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.richTextBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.richTextBox1.Location = new System.Drawing.Point(11, 281);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(768, 254);
            this.richTextBox1.TabIndex = 10;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(791, 547);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.settings_observations);
            this.Controls.Add(this.settings_procedure);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OGC SOS Copier";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox textBox_source_url;
		private System.Windows.Forms.ComboBox comboBox_source_version;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboBox_dest_version;
		private System.Windows.Forms.TextBox textBox_dest_url;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox settings_observations;
		private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.CheckBox settings_procedure;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

