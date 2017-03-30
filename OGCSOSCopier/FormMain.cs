using log4net.Appender;
using OGCSOSCopier.Models;
using OGCSOSCopier.RequestHandlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net.Core;
using log4net.Config;
using Netcad.SWE.Interop.OpenGIS.Swes_20;

namespace OGCSOSCopier
{
    public partial class Form_Main : Form, IAppender
    {
        public Form_Main()
        {
            InitializeComponent();

            var versions = Util.Common.SOSVerisonComboboxItemGenerator();
            comboBox_dest_version.Items.AddRange(versions);
            comboBox_source_version.Items.AddRange(versions);

            comboBox_dest_version.SelectedIndex = 1;
            comboBox_source_version.SelectedIndex = 1;

            richTextBox1.TabStop = false;
            richTextBox1.ReadOnly = true;
            richTextBox1.BackColor = Color.DimGray;
            richTextBox1.Cursor = Cursors.Arrow;
            richTextBox1.Enter += RichTextBox1_Enter;

    }

        private void RichTextBox1_Enter(object sender, EventArgs e)
        {
            label5.Focus();
        }

        public void DoAppend(LoggingEvent loggingEvent)
        {
            richTextBox1.AppendText(loggingEvent.RenderedMessage + "\n");
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
                MessageBox.Show("DESTINATION URL IS NOT VALID"); return;
            }

            Settings settings = new Settings();
            settings.OnlyProcedures = settings_procedure.Checked;
            settings.Observations = settings_observations.Checked;

            OGCSOSCopierConfig.SOURCE_SOS_URL = source_url;
            OGCSOSCopierConfig.SOURCE_SOS_VERSION = comboBox_source_version.SelectedItem.ToString();
            OGCSOSCopierConfig.DEST_SOS_URL = dest_url;
            OGCSOSCopierConfig.DEST_SOS_VERSION = comboBox_dest_version.SelectedItem.ToString();

            WriteLog("GetCapabilities Request....");
            GetCapabilitiesRequestHandler gcrh = new GetCapabilitiesRequestHandler(source_url, comboBox_source_version.SelectedItem.ToString());
            var allOfferings = gcrh.GetOfferings();
            WriteLog("GetCapabilities Request is succesfull");

            List<string> procedureList = new List<string>();
            foreach (var item in allOfferings)
            {
                if (item == null && item.AbstractOffering == null)
                    continue;
                procedureList.Add(item.AbstractOffering.procedure);
            }

            WriteLog("Select procedures");
            SelectProcedures sp = new SelectProcedures(procedureList);
            var dialogResult = sp.ShowDialog();

            if (sp.SeledtedProcedures != null && sp.SeledtedProcedures.Count == 0)
            {
                WriteLog("Select at least one procedure!");
                return;
            }
  

            List<DescribeSensorResponseType> describeSensorResponseList = new List<DescribeSensorResponseType>();
            WriteLog("Starting DescribeSensor operations");
            foreach (var item in sp.SeledtedProcedures)
            {
                DescribeSensorRequestHandler ds = new DescribeSensorRequestHandler(item);
                var describeSensorResponse = ds.Run();
                if (describeSensorResponse != null)
                {
                    describeSensorResponseList.Add(describeSensorResponse);
                    WriteLog("DescribeSensor OK : " + item);
                }
                else
                    WriteLog("DescribeSensor NOT OK : " + item);
            }





        }

        private void WriteLog(string message)
        {
            richTextBox1.AppendText(DateTime.Now.ToShortTimeString() + " >> " + message + "\n");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.ScrollToCaret();
        }
    }



    public class SOSVerisonComboboxItem
    {
        public SOSVerisonComboboxItem()
        {
        }

        public SOSVerisonComboboxItem(string text, string value)
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
