using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Abide
{
    /// <summary>
    /// Handles the painting for Abide's Aero Tool Strip.
    /// </summary>
    internal sealed class AeroToolStripRenderer : ToolStripRenderer
    {
        private static readonly Color Abide1 = Color.FromArgb(213, 221, 229);
        private static readonly Color Abide2 = Color.FromArgb(186, 203, 219);

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            //Setup
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            //Draw Text
            TextRenderer.DrawText(e.Graphics, e.Text, e.TextFont, e.TextRectangle, Color.Red, e.TextFormat);
            //e.Graphics.DrawString(e.Text, e.TextFont, Brushes.Blue, e.TextRectangle);
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            //Fill
            e.Graphics.FillRectangle(Brushes.Transparent, e.AffectedBounds);
        }
    }
}
