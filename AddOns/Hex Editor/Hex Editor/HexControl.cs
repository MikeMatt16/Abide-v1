using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;

namespace Hex_Editor
{
    /// <summary>
    /// Represents a hex data editor control.
    /// </summary>
    public partial class HexControl : UserControl
    {
        /// <summary>
        /// Gets or sets the group length.
        /// </summary>
        public int GroupLength
        {
            get { return groupLength; }
            set
            {
                if (value < 1) throw new InvalidOperationException("Cannot set group length to less than a byte.");
                bool changed = groupLength == value;
                groupLength = value;
                if (changed) OnGroupLengthChanged(new EventArgs());
            }
        }
        /// <summary>
        /// Gets or sets the hex control's data.
        /// </summary>
        public byte[] Data
        {
            get { return data; }
            set
            {
                bool changed = value != data;
                data = value;
                if (changed) OnDataChanged(new EventArgs());
            }
        }
        /// <summary>
        /// Gets or sets the offset length.
        /// </summary>
        public byte OffsetLength
        {
            get { return offsetLength; }
            set { offsetLength = value; OffsetView_CalculateWidth(); }
        }
        /// <summary>
        /// Gets the width of a character.
        /// </summary>
        private int CharacterWidth
        {
            get { return CharacterSize.Width; }
        }
        /// <summary>
        /// Gets the height of a character.
        /// </summary>
        private int CharacterHeight
        {
            get { return CharacterSize.Height; }
        }
        /// <summary>
        /// Gets the size of a character.
        /// </summary>
        private Size CharacterSize
        {
            get
            {
                //Prepare
                Size sz = Size.Empty;

                //Create Graphics
                using (Graphics g = CreateGraphics())
                    sz = g.MeasureString(0.ToString("D2"), Font).ToSize();
                sz = new Size(sz.Width / 2, sz.Height);

                //Return
                return sz;
            }
        }

        private byte[] data = new byte[0];
        private long selectionStart, selectionLength;
        private EditorPane caretPane = EditorPane.Hex;
        private EditorPane mousePane = EditorPane.Hex;
        private long caretPosition = 0;
        private long mousePosition = 0;
        private byte offsetLength = 5;
        private int bytesPerRow = 16;
        private int groupLength = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="HexControl"/> class.
        /// </summary>
        public HexControl()
        {
            //Initialize
            InitializeComponent();
            Font = new Font(FontFamily.GenericMonospace, 12f);

            //
            offsetPlaneSplitter.MinSize = 4 * CharacterWidth;

            //
            // offsetViewPictureBox
            //
            offsetViewPictureBox.Width = offsetLength * CharacterWidth;
        }
        protected virtual void OnGroupLengthChanged(EventArgs e)
        {
            //Draw
            DrawDataViewPane();
        }
        protected virtual void OnDataChanged(EventArgs e)
        {
            //Get Row Count
            int rowCount = (int)Math.Max(1, Math.Ceiling(data.Length / (float)bytesPerRow));

            //Setup
            offsetScrollBar.Visible = (rowCount * (CharacterHeight + 2)) > dataViewPictureBox.Height;
            offsetScrollBar.Maximum = rowCount;
            offsetScrollBar.SmallChange = 1;
            offsetScrollBar.LargeChange = dataViewPictureBox.Height / (CharacterHeight + 2);
            offsetScrollBar.Value = 0;

            //Draw
            DrawOffsetView();
            DrawDataViewPane();
        }
        protected override void OnLoad(EventArgs e)
        {
            //Base
            base.OnLoad(e);

            //Draw Offset View pane
            DrawOffsetView();

            //Draw Data View Pane
            DrawDataViewPane();
        }
        private void offsetScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            //Draw
            DrawOffsetView();
            DrawDataViewPane();
        }
        private void dataViewPictureBox_SizeChanged(object sender, EventArgs e)
        {
            //Get Row Count
            int rowCount = (int)Math.Max(1, Math.Ceiling(data.Length / (float)bytesPerRow));

            //Check
            offsetScrollBar.Visible = (rowCount * (CharacterHeight + 2)) > dataViewPictureBox.Height;
            offsetScrollBar.Maximum = rowCount;
            offsetScrollBar.SmallChange = 1;
            offsetScrollBar.LargeChange = dataViewPictureBox.Height / (CharacterHeight + 2);

            //Draw
            DrawOffsetView();
            DrawDataViewPane();
        }
        private void offsetPlaneSplitter_SplitterMoving(object sender, SplitterEventArgs e)
        {
            //Get offset length
            int offsetLength = (int)Math.Ceiling(e.SplitX / (float)CharacterWidth);

            //Check
            if (offsetLength != this.offsetLength)
            {
                //Set
                this.offsetLength = (byte)offsetLength;

                //Calculate
                OffsetView_CalculateWidth();
            }
        }
        private void offsetPlaneSplitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            //Redraw
            offsetPlaneSplitter.Refresh();
        }
        private void OffsetView_CalculateWidth()
        {
            //Get Width
            int oldWidth = offsetViewPictureBox.Width;
            int newWidth = CharacterWidth * offsetLength;

            //Check
            if (oldWidth != newWidth)
            {
                //Set Width
                offsetViewPictureBox.Width = newWidth;
                offsetLength = (byte)(newWidth / CharacterWidth);

                //Draw
                DrawOffsetView();
            }
        }
        private void DrawOffsetView()
        {
            //Prepare
            int offset = offsetScrollBar.Value * bytesPerRow;
            int rowOffset = offsetScrollBar.Value;
            int rowCount = (int)Math.Max(1, Math.Ceiling(data.Length / (float)bytesPerRow));
            int visibleRowCount = (int)Math.Ceiling(dataViewPictureBox.Height / (float)CharacterHeight);

            //Create Bitmap
            if (offsetViewPictureBox.Width > 0 && offsetViewPictureBox.Height > 0)
            {
                Bitmap offsetBitmap = new Bitmap(offsetViewPictureBox.Width, offsetViewPictureBox.Height);
                using (Graphics g = Graphics.FromImage(offsetBitmap))
                {
                    //Clear
                    g.Clear(SystemColors.Control);

                    //Setup
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    //Get Selection
                    int selectedRow = (int)(selectionStart / bytesPerRow);
                    int mouseRow = -1;

                    //Loop
                    int a = 0;
                    string offsetString = string.Empty;
                    Brush fontBrush = Brushes.DimGray;
                    for (int i = rowOffset; i < Math.Min(rowCount, rowOffset + visibleRowCount); i++)
                    {
                        //Get Start
                        a = i - rowOffset;

                        //Get Brush Color
                        if (i == mouseRow) fontBrush = Brushes.DarkBlue;
                        else if (i == selectedRow) fontBrush = Brushes.DarkViolet;
                        else fontBrush = Brushes.DimGray;
                        
                        //Draw
                        offsetString = (i * bytesPerRow).ToString($"D{offsetLength}");
                        g.DrawString(offsetString, Font, fontBrush, 2, (a * (CharacterHeight + 2)));
                    }
                }

                //Dispose
                if (offsetViewPictureBox.Image != null)
                    offsetViewPictureBox.Image.Dispose();

                //Set
                offsetViewPictureBox.Image = offsetBitmap;
                offsetViewPictureBox.Refresh();
            }
        }
        private void DrawDataViewPane()
        {
            //
            if (!Visible) return;

            //Prepare
            int a = 0;
            int offset = offsetScrollBar.Value * bytesPerRow;
            int rowCount = (int)Math.Max(1, Math.Ceiling((data.Length - offset) / (float)bytesPerRow));
            int visibleRowCount = (int)Math.Ceiling(dataViewPictureBox.Height / (float)CharacterHeight);
            string dataString = string.Empty;
            Brush activeBrush = null;
            int byteWidth = 0;

            //Create Bitmap
            if (dataViewPictureBox.Width > 0 && dataViewPictureBox.Height > 0)
            {
                Bitmap dataBitmap = new Bitmap(dataViewPictureBox.Width, dataViewPictureBox.Height);
                using (Graphics g = Graphics.FromImage(dataBitmap))
                {
                    //Clear
                    g.Clear(Color.White);
                    byteWidth = (int)Math.Ceiling(g.MeasureString("00", Font).Width);

                    //Setup
                    //g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    //g.SmoothingMode = SmoothingMode.HighQuality;

                    //Draw Slitter
                    int splitX = (byteWidth * bytesPerRow) + (CharacterWidth * (bytesPerRow / groupLength));
                    g.DrawLine(Pens.DimGray, splitX, 0, splitX, dataViewPictureBox.Height);
                    
                    //Draw
                    activeBrush = Brushes.DimGray; Rectangle textBox = Rectangle.Empty;
                    for (int i = offset; i < Math.Min(data.Length, offset + (visibleRowCount * bytesPerRow)); i++)
                    {
                        //Get start
                        a = i - offset;

                        //Get Color
                        if ((i / groupLength) % 2 == 0) activeBrush = Brushes.DimGray;
                        else activeBrush = Brushes.DarkBlue;

                        //Get start
                        int groupX = (a % bytesPerRow) / groupLength;
                        int x = a % bytesPerRow;
                        int y = a / bytesPerRow;

                        //Draw Hex
                        textBox = new Rectangle(x * byteWidth + (groupX * CharacterWidth), y * CharacterHeight, byteWidth, CharacterHeight);
                        g.DrawString(data[i].ToString("X2"), Font, activeBrush, textBox);

                        //Draw ASCII
                        textBox = new Rectangle(splitX + CharacterWidth + (x * CharacterWidth), y * CharacterHeight, CharacterWidth, CharacterHeight);
                        g.DrawString(GetAsciiChar(data[i]), Font, Brushes.Black, textBox);
                    }
                }

                //Dispose
                if (dataViewPictureBox.Image != null)
                    dataViewPictureBox.Image.Dispose();

                //Set
                dataViewPictureBox.Image = dataBitmap;
                dataViewPictureBox.Refresh();
            }
        }

        private string GetAsciiChar(params byte[] b)
        {
            //Prepare
            string asciiString = string.Empty;
            byte[] decoded = new byte[b.Length];

            //Loop
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i] < 127 && b[i] > 32) decoded[i] = b[i];
                else decoded[i] = (byte)'.';
            }

            //Return
            return Encoding.ASCII.GetString(decoded);
        }

        private enum EditorPane
        {
            Hex,
            Text
        };
    }
}
