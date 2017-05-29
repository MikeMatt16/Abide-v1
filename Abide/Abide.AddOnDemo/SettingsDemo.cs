using Abide.AddOnApi;
using Abide.AddOnDemo.Properties;
using System;

namespace Abide.AddOnDemo
{
    public partial class SettingsDemo : SettingsPage
    {
        public SettingsDemo()
        {
            InitializeComponent();
            checkBox1.Checked = Settings.Default.demoSetting;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.demoSetting = checkBox1.Checked;
            Settings.Default.Save();
        }
    }
}
