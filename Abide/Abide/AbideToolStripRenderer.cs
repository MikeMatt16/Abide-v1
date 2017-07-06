using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Abide
{
    /// <summary>
    /// Handles the painting for Abide's Tool Strip.
    /// </summary>
    internal sealed class AbideToolStripRenderer : ToolStripRenderer
    {
        private static readonly Color Abide1 = Color.FromArgb(213, 221, 229);
        private static readonly Color Abide2 = Color.FromArgb(186, 203, 219);

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);

        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            TextRenderer.DrawText(e.Graphics, e.Text, e.TextFont, e.TextRectangle, e.TextColor, TextFormatFlags.Left);
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderMenuItemBackground(e);
            
        }
    }
}
