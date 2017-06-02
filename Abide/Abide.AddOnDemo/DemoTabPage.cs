using Abide.AddOnApi.Halo2;
using System.Windows.Forms;

namespace Abide.AddOnDemo
{
    public partial class DemoTabPage : AbideTabPage
    {
        public DemoTabPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            object test = Host.Request(this, "SelectedEntry");
            MessageBox.Show(SelectedEntry.ToString());
        }
    }
}
