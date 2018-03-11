using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Abide
{
    /// <summary>
    /// Handles the painting for Abide's Tool Strip.
    /// </summary>
    internal sealed class AbideToolStripRenderer : ToolStripProfessionalRenderer
    {
        private static readonly Color Abide1 = Color.FromArgb(213, 221, 229);
        private static readonly Color Abide2 = Color.FromArgb(186, 203, 219);
        
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using (Brush b = new LinearGradientBrush(e.AffectedBounds, Color.White, Abide1, LinearGradientMode.Vertical))
                e.Graphics.FillRectangle(b, e.AffectedBounds);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            TextRenderer.DrawText(e.Graphics, e.Text, e.TextFont, e.TextRectangle, e.Item.ForeColor, e.TextFormat);
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            using (Brush b = new LinearGradientBrush(e.AffectedBounds, Abide1, Abide2, LinearGradientMode.Vertical))
                e.Graphics.FillRectangle(b, e.AffectedBounds);
            e.Graphics.DrawRectangle(Pens.LightGray, e.AffectedBounds);
        }
    }
}
