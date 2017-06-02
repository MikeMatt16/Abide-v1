using Abide.AddOnApi.Halo2;
using System;
using System.Windows.Forms;

namespace Abide.AddOnDemo
{
    public partial class DemoTool : AbideTool
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
