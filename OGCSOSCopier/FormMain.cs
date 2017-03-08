using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OGCSOSCopier
{
	public partial class Form_Main : Form
	{
		public Form_Main()
		{
			InitializeComponent();

			var versions = Util.Common.SOSVerisonComboboxItemGenerator();
			comboBox_dest_version.Items.AddRange(versions);
			comboBox_source_version.Items.AddRange(versions);

			comboBox_dest_version.SelectedIndex = 1;
			comboBox_source_version.SelectedIndex = 1;

		}

		private void button_start_Click(object sender, EventArgs e)
		{
			string source_url = textBox_source_url.Text;
			string dest_url = textBox_dest_url.Text;

			if (string.IsNullOrEmpty(source_url))
				return;

			if (string.IsNullOrEmpty(dest_url))
				return;

			if (!Util.Common.IsValidUrl(source_url))
			{
				MessageBox.Show("SOURCE URL IS NOT VALID"); return;
			}

			if (!Util.Common.IsValidUrl(dest_url))
			{
				MessageBox.Show("DESTINATION URL IS NOT VALID");return;
			}
				

			MessageBox.Show("OK");
		}
	}


	public class SOSVerisonComboboxItem
	{
		public SOSVerisonComboboxItem()
		{
		}

		public SOSVerisonComboboxItem(string text,string value)
		{
			this.Text = text;
			this.Value = value;
		}

		public string Text { get; set; }
		public string Value { get; set; }

		public override string ToString()
		{
			return Text;
		}
	}


}
