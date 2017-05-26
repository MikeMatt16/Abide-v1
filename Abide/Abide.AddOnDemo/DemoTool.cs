using Abide.AddOnApi;
using Abide.HaloLibrary.Halo2Map;
using System;
using System.Windows.Forms;
using YeloDebug;

namespace Abide.AddOnDemo
{
    public partial class DemoTool : Tool<MapFile, IndexEntry, Xbox>
    {
        public DemoTool()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Host.InvokeRequired)
                Host.Invoke(new MethodInvoker(test));
            else test();
        }

        private void test()
        {
            MessageBox.Show(Map.Name);
        }
    }
}
