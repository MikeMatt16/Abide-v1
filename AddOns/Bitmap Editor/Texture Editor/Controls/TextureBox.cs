using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Texture_Editor.Controls
{
    public class TextureBox : Panel
    {
        /// <summary>
        /// Occurs when the <see cref="Texture"/> property is changed.
        /// </summary>
        [Category("Property Changed"), Description("Event raised when the texture propery is changed.")]
        public event EventHandler ImageChanged
        {
            add { imageChanged += value; }
            remove { imageChanged -= value; }
        }
        /// <summary>
        /// The image texture to draw on the texture box.
        /// </summary>
        [Category("Appearance"), Description("The texture to show on the control.")]
        public Image Texture
        {
            get { return texture; }
            set
            {
                bool changed = texture != value;
                texture = value;
                if (changed) OnImageChanged(new EventArgs());
            }
        }

        private ContextMenuStrip contextMenu;
        private Point previousPoint = Point.Empty;
        private Point originTranslation = Point.Empty;
        private float zoomLevel = 1f;
        private float zoomFactor = 1f / 120f;
        private event EventHandler imageChanged;
        private Image texture;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextureBox"/> class.
        /// </summary>
        public TextureBox() : base()
        {
            //
            // contextMenu
            //
            contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("&Reset", null, ResetToolStripMenuItem_Click);
            ToolStripMenuItem scaleToolStripItem = (ToolStripMenuItem)contextMenu.Items.Add("Scale");
            scaleToolStripItem.DropDownItems.Add("100%", null, Scale100ToolStripItem_Click);
            scaleToolStripItem.DropDownItems.Add("200%", null, Scale200ToolStripItem_Click);
            scaleToolStripItem.DropDownItems.Add("300%", null, Scale300ToolStripItem_Click);
            ToolStripMenuItem translateToolStripItem = (ToolStripMenuItem)contextMenu.Items.Add("Translation");
            translateToolStripItem.DropDownItems.Add("Fix to Top Left", null, TopLeftToolStripItem_Click);
            //
            // this
            //
            ContextMenuStrip = contextMenu;
            DoubleBuffered = true;
            BorderStyle = BorderStyle.FixedSingle;
            MinimumSize = new Size(1, 1);
            Width = 150;
            Height = 150;
        }

        private void TopLeftToolStripItem_Click(object sender, EventArgs e)
        {
            originTranslation = Point.Empty;
            Refresh();
        }
        private void Scale300ToolStripItem_Click(object sender, EventArgs e)
        {
            zoomLevel = 3f;
            Refresh();
        }
        private void Scale200ToolStripItem_Click(object sender, EventArgs e)
        {
            zoomLevel = 2f;
            Refresh();
        }
        private void Scale100ToolStripItem_Click(object sender, EventArgs e)
        {
            zoomLevel = 1f;
            Refresh();
        }
        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            originTranslation = Point.Empty;
            zoomLevel = 1f;
            Refresh();
        }

        /// <summary>
        /// Raises the <see cref="ImageChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnImageChanged(EventArgs e)
        {
            //Invoke
            imageChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the <see cref="Control.MouseWheel"/> event.
        /// </summary>
        /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //Float zoom Amnt
            zoomLevel += e.Delta / 1200f;

            //Zoom
            if(float.IsInfinity(zoomLevel) || zoomLevel <= 0) zoomLevel = 0.1f;
            Refresh();

            //Invoke MouseWheel event...
            base.OnMouseWheel(e);
        }
        /// <summary>
        /// Raises the <see cref="Control.MouseMove"/> event.
        /// </summary>
        /// <param name="e">A <see cref="MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //Check...
            if (e.Button == MouseButtons.Left)
            {
                originTranslation.X += (e.X - previousPoint.X);
                originTranslation.Y += (e.Y - previousPoint.Y);
                Refresh();
            }

            //Set
            previousPoint = e.Location;
            
            //Invoke MouseMove event...
            base.OnMouseMove(e);
        }
        /// <summary>
        /// Raises the <see cref="Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="PaintEventArgs"/> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //Check...
            if (texture != null)
            {
                //Setup graphics...
                e.Graphics.TranslateTransform(originTranslation.X, originTranslation.Y);
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                //Draw Image
                e.Graphics.DrawImage(texture, new RectangleF(0, 0, zoomLevel * texture.Width, zoomLevel * texture.Height));

                //Reset
                e.Graphics.ResetTransform();
            }

            //Draw Information
            TextRenderer.DrawText(e.Graphics, $"{Math.Round(zoomLevel * 100f)}%", Font, e.ClipRectangle, ForeColor, (TextFormatFlags.Bottom | TextFormatFlags.Left));
            TextRenderer.DrawText(e.Graphics, $"({originTranslation.X}, {originTranslation.Y})", Font, e.ClipRectangle, ForeColor, (TextFormatFlags.Bottom | TextFormatFlags.Right));

            //Invoke Paint event...
            base.OnPaint(e);
        }
    }
}
