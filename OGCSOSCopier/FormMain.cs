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
using Netcad.SWE.Interop.OpenGIS.Sos_20;

namespace OGCSOSCopier
{
    public partial class Form_Main : Form
    {

        private static AbstractContentsTypeOffering[] AllOfferings = null;

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

        private void button_start_Click(object sender, EventArgs e)
        {
            string source_url = textBox_source_url.Text;
            string dest_url = textBox_dest_url.Text;

            if (!CheckUrls(source_url, dest_url)) return;

            Settings settings = new Settings();
            settings.OnlyProcedures = settings_procedure.Checked;
            settings.Observations = settings_observations.Checked;

            OGCSOSCopierConfig.SOURCE_SOS_URL = source_url;
            OGCSOSCopierConfig.SOURCE_SOS_VERSION = comboBox_source_version.SelectedItem.ToString();
            OGCSOSCopierConfig.DEST_SOS_URL = dest_url;
            OGCSOSCopierConfig.DEST_SOS_VERSION = comboBox_dest_version.SelectedItem.ToString();

            if (AllOfferings == null)
            {
                WriteLog("GetCapabilities Request....");
                try
                {
                    GetCapabilitiesRequestHandler gcrh = new GetCapabilitiesRequestHandler();
                    AllOfferings = gcrh.GetOfferings();
                    WriteLog("GetCapabilities Request is succesfull");
                }
                catch (Exception exp)
                {
                    WriteLog("Exception : " + exp.Message);
                    return;
                }
            }

            List<string> procedureList = new List<string>();
            foreach (var item in AllOfferings)
            {
                if (item == null && item.AbstractOffering == null) continue;
                procedureList.Add(item.AbstractOffering.procedure);
            }

            WriteLog("Select procedures");
            SelectProcedures sp = new SelectProcedures(procedureList);
            var dialogResult = sp.ShowDialog();

            if (sp.SeledtedProcedures != null && sp.SeledtedProcedures.Count == 0)
            {
                WriteLog("Select at least one procedure!"); return;
            }

            List<Tuple<DescribeSensorResponseType, AbstractContentsTypeOffering>> describeSensorResponseAndOfferings
                = new List<Tuple<DescribeSensorResponseType, AbstractContentsTypeOffering>>();

            if (settings.OnlyProcedures)
            {
                WriteLogLine(); WriteLog("Starting DescribeSensor operations");
                foreach (var item in AllOfferings)
                {
                    if (!sp.SeledtedProcedures.Contains(item.AbstractOffering.procedure)) continue;

                    DescribeSensorRequestHandler ds = new DescribeSensorRequestHandler(item.AbstractOffering.procedure);
                    try
                    {
                        var describeSensorResponse = ds.Run();
                        if (describeSensorResponse != null)
                        {
                            describeSensorResponseAndOfferings.Add(new Tuple<DescribeSensorResponseType, AbstractContentsTypeOffering>(describeSensorResponse, item));
                            WriteLog("DescribeSensor OK : " + item.AbstractOffering.procedure);
                        }
                        else
                            WriteLog("DescribeSensor NOT OK : " + item.AbstractOffering.procedure);
                    }
                    catch (Exception exp)
                    {
                        WriteLog("DescribeSensor NOT OK : " + exp.Message);
                    }
                }
                WriteLogLine(); WriteLog("Starting InsertSensor operations");
                foreach (var item in describeSensorResponseAndOfferings)
                {
                    InsertSensorRequestHandler isrh = new InsertSensorRequestHandler(item.Item1, (ObservationOfferingType)item.Item2.AbstractOffering);

                    try
                    {
                        Tuple<bool, string> res = isrh.Run();
                        if (res.Item1)
                            WriteLog("InsertSensor OK : " + res.Item2);
                        else
                            WriteLog("InsertSensor NOT OK : " + res.Item2);
                    }
                    catch (Exception exp)
                    {
                        WriteLog("InsertSensor NOT OK : " + exp.Message);
                    }
                }
            }

            if (settings.Observations)
            {

            }

            WriteLog("Completed!");
        }

        private void WriteLog(string message)
        {
            richTextBox1.AppendText(DateTime.Now.ToString("HH:mm:s") + " >> " + message + "\n");
        }

        private void WriteLogLine()
        {
            richTextBox1.AppendText("-----------------------------------------------------\n");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.ScrollToCaret();
        }

        private bool CheckUrls(string source_url, string dest_url)
        {
            if (string.IsNullOrEmpty(source_url) || string.IsNullOrEmpty(dest_url))
                return false;

            if (!Util.Common.IsValidUrl(source_url))
            {
                MessageBox.Show("SOURCE URL IS NOT VALID"); return false;
            }

            if (!Util.Common.IsValidUrl(dest_url))
            {
                MessageBox.Show("DESTINATION URL IS NOT VALID"); return false;
            }

            return true;
        }

        private void textBox_source_url_TextChanged(object sender, EventArgs e)
        {
            AllOfferings = null;
        }

        private void textBox_dest_url_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_source_version_SelectedValueChanged(object sender, EventArgs e)
        {
            AllOfferings = null;
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
