using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XbExplorer
{
    public sealed class XbExplorerToolstripRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            //Base procedures
            base.OnRenderToolStripBackground(e);

            //Simple
            e.Graphics.Clear(e.BackColor);
        }
    }
}
