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

            GetCapabilitiesRequestHandler gcrh = new GetCapabilitiesRequestHandler(source_url, comboBox_source_version.SelectedItem.ToString());
            var capabilitiesResponse = gcrh.Run(settings);


            if (capabilitiesResponse == null)
                return;

            if (capabilitiesResponse.contents == null ||
                capabilitiesResponse.contents.Contents == null ||
                capabilitiesResponse.contents.Contents.offering == null ||
                capabilitiesResponse.contents.Contents.offering.Length == 0)
                throw new Exception("Offering not found!");

            foreach (var item in capabilitiesResponse.contents.Contents.offering)
            {
                if (item == null && item.AbstractOffering == null)
                    continue;

                DescribeSensorRequestHandler ds = new DescribeSensorRequestHandler(item.AbstractOffering.procedure);
                var describeSensorResponse = ds.Run();
            }


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
