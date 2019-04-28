using Abide.Guerilla.Tool.Bitmap;
using System;
using System.Windows.Forms;

namespace Abide.Guerilla.Tool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void bitmapToolButton_Click(object sender, EventArgs e)
        {
            //Prepare
            BitmapTool bitmapTool = new BitmapTool();
            bitmapTool.FormClosed += BitmapTool_FormClosed;

            //Hide self, show bitmap tool
            Hide();
            bitmapTool.Show();
        }

        private void BitmapTool_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Close or show
            if (e.CloseReason == CloseReason.UserClosing) Show();
            else Close();

            //Dispose
            if (sender is IDisposable disposable) disposable.Dispose();
        }
    }
}
