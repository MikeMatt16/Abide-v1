using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Abide.Tag.Ui.Controls
{
    public class BlockView : Control
    {
        /// <summary>
        /// Gets and returns the block collection of this block view.
        /// </summary>
        [Browsable(true), Category("Behavior"), Description(""), Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
        public BlockCollection Blocks
        {
            get { return blocks; }
        }
        /// <summary>
        /// Gets or sets the block height.
        /// </summary>
        [Browsable(true), Category("Appearance"), Description("Determines the block height in the data visualization.")]
        public int BlockHeight
        {
            get { return blockHeight; }
            set { blockHeight = value; UpdateBlocks(); }
        }
        /// <summary>
        /// Gets or sets the block width.
        /// </summary>
        [Browsable(true), Category("Appearance"), Description("Determines the number of bytes that represent a 1-pixel wide block.")]
        public int BlockWidth
        {
            get { return blockWidth; }
            set { blockWidth = value; UpdateBlocks(); }
        }
        /// <summary>
        /// Gets or sets the length of the data.
        /// </summary>
        [Browsable(true), Category("Appearance"), Description("Determines the total length of the data.")]
        public int DataLength
        {
            get { return dataLength; }
            set { dataLength = value; UpdateBlocks(); }
        }
        /// <summary>
        /// Gets or sets the text for this control.
        /// </summary>
        [Browsable(false)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
        /// <summary>
        /// Gets or sets the visibilty of the vertical scroll bar.
        /// </summary>
        [Browsable(true), Category("Appearance"), Description("Determines the visibility of the scroll bar.")]
        public bool ScrollBarVisible
        {
            get { return verticalScroll.Visible; }
            set { verticalScroll.Visible = value; UpdateBlocks(); }
        }
        /// <summary>
        /// Gets or sets the offset of the block view.
        /// </summary>
        [Browsable(true), Category("Appearance"), Description("Determines the starting offset of the block view.")]
        public int Offset
        {
            get { return verticalScroll.Value; }
            set { verticalScroll.Value = value; }
        }
        /// <summary>
        /// Gets or sets whether to draw the border on blocks.
        /// </summary>
        [Browsable(true), Category("Appearance"), Description("Determines whether or not to draw the block border.")]
        public bool DrawBorder
        {
            get { return drawBorder; }
            set { drawBorder = value; }
        }
        
        private const int HeadPri = unchecked((int)0xFF1699f7);
        private const int HeadSec = unchecked((int)0xFF002381);

        private readonly Brush paddingBrush = new TextureBrush(Properties.Resources.padding);
        private bool isUpdating = false;
        private bool drawBorder = true;
        private Bitmap bitmap;
        private int dataLength = 0;
        private int blockWidth = 8;
        private int blockHeight = 24;
        private BlockCollection blocks;
        private VScrollBar verticalScroll;
        private Block hover = null;
        private int scrollValue = 0;

        public BlockView()
        {
            //Initialize
            blocks = new BlockCollection(this);
            verticalScroll = new VScrollBar();

            //
            // verticalScroll
            //
            verticalScroll.Scroll += VerticalScroll_Scroll;
            verticalScroll.Dock = DockStyle.Right;
            verticalScroll.Visible = false;
            verticalScroll.LargeChange = 1;
            verticalScroll.SmallChange = 1;
            verticalScroll.Value = 0;
            verticalScroll.Maximum = 1;

            //
            // this
            //
            DoubleBuffered = true;
            Size = new Size(150, 150);
            Controls.Add(verticalScroll);
            BackColor = Color.Gainsboro;
        }
        public void BeginUpdate()
        {
            isUpdating = true;
        }
        public void EndUpdate()
        {
            isUpdating = false;
            UpdateBlocks();
        }
        public void Clear()
        {
            //Set
            blocks.Clear();
            UpdateBlocks();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateBlocks();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //Base Paint
            base.OnPaint(e);

            //Clear
            e.Graphics.Clear(BackColor);

            //Fill Blocks
            if (bitmap != null)
                e.Graphics.DrawImage(bitmap, Point.Empty);

            //Draw
            if (hover != null)
                e.Graphics.DrawString(string.Format("{0} 0x{1:X}", hover.Name, hover.Offset), Font, Brushes.Black, new Point(5, 5));

            //Draw Border
            Size Workspace = GetWorkingSize();
            e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, Workspace.Width - 1, Workspace.Height - 1));
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //Base Mouse Move
            base.OnMouseMove(e);

            //Get Offset
            int baseOffset = verticalScroll.Visible ? verticalScroll.Value : 0;
            int offset = (e.X * BlockWidth) + ((e.Y / blockHeight) * GetDataWidth()) + baseOffset;

            //Find
            Block previous = hover; hover = null;
            foreach (Block b in blocks)
                if (b.Offset <= offset && (b.Offset + b.Length) >= offset)
                { hover = b; break; }

            //Check
            if (previous != hover)
                Refresh();
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //Base
            base.OnMouseWheel(e);

            //Check
            if (verticalScroll.Visible)
            {
                //Offset Modifier
                int offsetOffset = e.Delta > 0 ? -GetDataWidth() : GetDataWidth();
                int value = scrollValue + offsetOffset;
                if (value < verticalScroll.Minimum)
                    value = verticalScroll.Minimum;
                else if (value > (verticalScroll.Maximum - GetDataWidth()))
                    value = verticalScroll.Maximum;

                //Check
                scrollValue = value;
                verticalScroll.Value = value;
                UpdateBlocks();
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            //Base
            base.OnMouseDown(e);

            //Check
            if (hover != null)
            {
                //Set offset or length depending on mouse button
                if (e.Button == MouseButtons.Left)
                    Clipboard.SetText(hover.Offset.ToString());
                else if(e.Button == MouseButtons.Right)
                    Clipboard.SetText(hover.Length.ToString());
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            //Base
            base.OnMouseLeave(e);

            //Refresh
            hover = null;
            Refresh();
        }
        private void VerticalScroll_Scroll(object sender, ScrollEventArgs e)
        {
            //Prepare
            int newScrollValue = 0;
            int dataWidth = GetDataWidth();

            //Check
            if (e.NewValue == 0)
            {
                verticalScroll.Value = newScrollValue;
                scrollValue = newScrollValue;
                UpdateBlocks();
            }
            else
            {
                //Pad Scroll Value
                if(e.NewValue % dataWidth != 0)
                {
                    int num = (int)Math.Ceiling(e.NewValue / (decimal)dataWidth) * dataWidth;
                    e.NewValue = num;
                }

                //Adjust
                newScrollValue = (e.NewValue / dataWidth) * dataWidth;

                //Update Blocks
                if (newScrollValue != scrollValue)
                {
                    verticalScroll.Value = newScrollValue;
                    scrollValue = newScrollValue;
                    UpdateBlocks();
                }
            }
        }
        public void UpdateBlocks()
        {
            //Check
            if ((blocks.Count == 0 && dataLength == 0) || isUpdating)
                return;

            //Get Working Area
            Size Workspace = GetWorkingSize();
            Rectangle block = Rectangle.Empty;

            //Get potential visible data.
            int visibleLength = (Workspace.Width * blockWidth) * (Workspace.Height / blockHeight);
            if (dataLength > visibleLength && visibleLength > 0)
            {
                //Setup Scroll
                verticalScroll.Visible = true;
                verticalScroll.Minimum = 0;
                verticalScroll.Maximum = dataLength + (Workspace.Height % blockHeight > 0 ? GetDataWidth() : 0);
                verticalScroll.LargeChange = visibleLength;
                verticalScroll.SmallChange = GetDataWidth();

                //Get New Size
                Workspace = GetWorkingSize();
                visibleLength = (Workspace.Width * blockWidth) * (Workspace.Height / blockHeight);
            }
            else if (verticalScroll.Visible)
            {
                //Setup Scroll
                verticalScroll.Visible = false;
                verticalScroll.Value = 0;
                verticalScroll.Maximum = 1;
                verticalScroll.SmallChange = 1;
                verticalScroll.LargeChange = 1;

                //Get New Size
                Workspace = GetWorkingSize();
                visibleLength = (Workspace.Width * blockWidth) * (Workspace.Height / blockHeight);
            }

            //Check
            if (bitmap != null) bitmap.Dispose();
            if (Workspace.Width > 1 && Workspace.Height > 1)
            {
                bitmap = new Bitmap(Workspace.Width, Workspace.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    //Get Length
                    int blockLengths = 0;
                    foreach (Block b in blocks)
                        blockLengths += b.Length;

                    //Get Blocks per row
                    int row = GetDataWidth();
                    int offset = verticalScroll.Value;
                    int start = 0;
                    int remainder = 0;

                    //Draw Padding
                    int width = 0;
                    int x = 0, y = 0;
                    while (offset < dataLength)
                    {
                        //Get X and Y
                        y = start / row;
                        x = start % row;

                        //Get Width
                        width = dataLength - offset;
                        if (x + width < row)
                            block = new Rectangle(x / blockWidth, y * blockHeight, width / blockWidth, blockHeight);
                        else
                            block = new Rectangle(0, y * blockHeight, Workspace.Width, blockHeight);

                        //Increment
                        offset += row;
                        start += row;

                        //Draw
                        if (!block.IsEmpty)
                            g.FillRectangle(paddingBrush, block);
                    }

                    //Draw Blocks
                    foreach (Block dataBlock in blocks)
                    {
                        //Check
                        if (dataBlock.Offset + dataBlock.Length < verticalScroll.Value)
                            continue;

                        //Prepare
                        offset = dataBlock.Offset - verticalScroll.Value;
                        start = 0;
                        while (start < dataBlock.Length)
                        {
                            //Get X and Y
                            y = offset / row;
                            x = offset % row;

                            //Get Block
                            width = dataBlock.Length - start;
                            remainder = width - (row - x);
                            if (width <= (row - x))
                                block = new Rectangle(x / blockWidth, y * blockHeight, width / blockWidth, blockHeight);
                            else
                                block = new Rectangle(x / blockWidth, y * blockHeight, (width - remainder) / blockWidth, blockHeight);

                            //Increment
                            offset += x + width <= (row - x) ? dataBlock.Length : width - remainder;
                            start += x + width <= (row - x) ? dataBlock.Length : width - remainder;

                            //Draw
                            if (!block.IsEmpty && block.Width > 0 && block.Height > 0)
                            {
                                using (Brush b = new LinearGradientBrush(block, dataBlock.Primary, dataBlock.Secondary, LinearGradientMode.Vertical))
                                    g.FillRectangle(b, block);
                                if (drawBorder)
                                    using (Pen p = new Pen(dataBlock.Secondary))
                                        g.DrawRectangle(p, new Rectangle(block.X, block.Y, block.Width, block.Height));
                            }
                        }
                    }
                }
            }

            //Redraw
            Refresh();
        }
        private int GetDataWidth()
        {
            int width = GetWorkingSize().Width * blockWidth;
            if (width < 1) width = 1;
            return width;
        }
        private Size GetWorkingSize()
        {
            //Get
            int width = verticalScroll.Visible ? (Width - verticalScroll.Width) : Width;
            int height = Height;

            //Return
            return new Size(width, height);
        }

        /// <summary>
        /// Represnts a collection of blocks.
        /// </summary>
        public class BlockCollection : ICollection<Block>, IEnumerable<Block>
        {
            public Block this[int index]
            {
                get { return blocks[index]; }
            }
            public int Count
            {
                get { return blocks.Count; }
            }

            private readonly List<Block> blocks;
            private readonly BlockView owner;

            public BlockCollection(BlockView owner)
            {
                //Set
                this.owner = owner;
                blocks = new List<Block>();
            }
            public void Add(Block item)
            {
                blocks.Add(item);
                owner.UpdateBlocks();
            }
            public void Add(int offset, int length, string name)
            {
                blocks.Add(new Block(offset, length, name));
                owner.UpdateBlocks();
            }
            public void Clear()
            {
                blocks.Clear();
                owner.UpdateBlocks();
            }
            public bool Contains(Block item)
            {
                return blocks.Contains(item);
            }
            public void CopyTo(Block[] array, int arrayIndex)
            {
                blocks.CopyTo(array, arrayIndex);
            }
            public IEnumerator<Block> GetEnumerator()
            {
                return blocks.GetEnumerator();
            }
            public int IndexOf(Block item)
            {
                return blocks.IndexOf(item);
            }
            public void Insert(int index, Block item)
            {
                blocks.Insert(index, item);
                owner.UpdateBlocks();
            }
            public bool Remove(Block item)
            {
                if (blocks.Remove(item))
                {
                    owner.UpdateBlocks();
                    return true;
                }
                return false;
            }
            public void RemoveAt(int index)
            {
                blocks.RemoveAt(index);
                owner.UpdateBlocks();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return blocks.GetEnumerator();
            }
            bool ICollection<Block>.IsReadOnly
            {
                get { return false; }
            }
        }
    }

    /// <summary>
    /// Represents a block component for a <see cref="BlockView"/> control.
    /// </summary>
    public class Block : Component
    {
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        public Color Primary
        {
            get { return gradient[0]; }
            set { gradient[0] = value; }
        }
        public Color Secondary
        {
            get { return gradient[1]; }
            set { gradient[1] = value; }
        }

        private static readonly Random blockRandom = new Random(1234);
        private readonly Color[] gradient;
        private int offset;
        private int length;
        private string name;

        public Block(int offset, int length, string name)
        {
            this.offset = offset;
            this.length = length;
            this.name = name;

            int channelIdx = blockRandom.Next(3);
            byte[] rgb = new byte[] { (byte)blockRandom.Next(256), (byte)blockRandom.Next(256), (byte)blockRandom.Next(256) };
            rgb[channelIdx] = 0xFF;

            Color Primary = Color.FromArgb(0xFF, rgb[0], rgb[1], rgb[2]);
            Color Secondary = Color.FromArgb(0xFF, Math.Max(rgb[0] / 2, 0), Math.Max(rgb[1] / 2, 0), Math.Max(rgb[2] / 2, 0));
            gradient = new Color[] { Primary, Secondary };
        }
        public Block(int offset, int length, Block inheritFrom)
        {
            this.offset = offset;
            this.length = length;
            name = inheritFrom.name;
            gradient = inheritFrom.gradient;
        }
        public Block()
        {
            int channelIdx = blockRandom.Next(3);
            byte[] rgb = new byte[] { (byte)blockRandom.Next(256), (byte)blockRandom.Next(256), (byte)blockRandom.Next(256) };
            rgb[channelIdx] = 0xFF;

            Color Primary = Color.FromArgb(0xFF, rgb[0], rgb[1], rgb[2]);
            Color Secondary = Color.FromArgb(0xFF, Math.Max(rgb[0] / 2, 0), Math.Max(rgb[1] / 2, 0), Math.Max(rgb[2] / 2, 0));
            gradient = new Color[] { Primary, Secondary };
        }
    }
}
