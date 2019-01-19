using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XbExplorer
{
    /// <summary>
    /// Represents a breadcrumb navigation bar control.
    /// </summary>
    [ToolboxItem(true), ToolboxBitmap("XbExplorer.Resources.breadcrumb-nav-bar-toolbox-icon.bmp")]
    public class BreadcrumbBar : Control
    {
        private const int C_MinHeight = 22;
        private const int C_MinWidth = 44;

        /// <summary>
        /// Occurs when the <see cref="BorderColor"/> property is changed.
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the BorderColor property is changed.")]
        public event EventHandler BorderColorChanged;
        /// <summary>
        /// Occurs when the Path property is changed.
        /// </summary>
        [Category("Property changed"), Description("Occurs when the Path property is changed.")]
        public event EventHandler PathChanged;

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        [Browsable(false)]
        public override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }
        [Category("Behavior")]
        public BreadcrumbBarItemCollection Items { get; }
        /// <summary>
        /// Gets or sets the border color of the control.
        /// </summary>
        [Category("Appearance"), Description("The border color of the control.")]
        public Color BorderColor
        {
            get { return m_BorderColor; }
            set { bool changed = m_BorderColor == value; m_BorderColor = value; if (changed) OnBorderColorChanged(new EventArgs()); }
        }
        /// <summary>
        /// 
        /// </summary>
        [Category(), Description()]
        public string Path
        {
            get { return m_Path; }
            set { bool changed = m_Path == value; m_Path = value; if (changed) OnPathChanged(new EventArgs()); }
        }
        /// <summary>
        /// 
        /// </summary>
        [Category(), Description()]
        public ImageList ImageList { get; set; }

        private Color m_BorderColor = SystemColors.ScrollBar;
        private string m_Path = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbBar"/> control.
        /// </summary>
        public BreadcrumbBar()
        {
            Size = new Size(300, 22);
            MinimumSize = new Size(C_MinWidth, C_MinHeight);
            BackColor = SystemColors.Window;
            Items = new BreadcrumbBarItemCollection(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //Draw items
            int currentLeft = 21;
            Rectangle itemRectangle = new Rectangle();
            foreach (BreadcrumbBarItem item in Items)
            {
                //Get rectangle for item
                itemRectangle = new Rectangle(currentLeft, 1, 14 + TextRenderer.MeasureText(item.Text, Font).Width, e.ClipRectangle.Height - 2);
                currentLeft = itemRectangle.Right;

                //Paint
                e.Graphics.IntersectClip(itemRectangle);
                using (PaintBreadcrumbItemEventArgs pbe = new PaintBreadcrumbItemEventArgs(item, e.Graphics, itemRectangle))
                    OnPaintItemBackground(pbe);
            }
        }
        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="pevent">A <see cref="PaintEventArgs"/> that contains information about the control to paint.</param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Base
            base.OnPaintBackground(pevent);

            //Clear
            pevent.Graphics.Clear(BackColor);

            //Draw rectangle
            Rectangle borderRect = new Rectangle(pevent.ClipRectangle.X, pevent.ClipRectangle.Y,
                pevent.ClipRectangle.Width - 1, pevent.ClipRectangle.Height - 1);
            using (Pen borderBrush = new Pen(m_BorderColor, 1))
                pevent.Graphics.DrawRectangle(borderBrush, borderRect);

            //Draw item backgrounds
            int currentLeft = 21;
            Rectangle itemRectangle = new Rectangle();
            foreach (BreadcrumbBarItem item in Items)
            {
                //Get rectangle for item
                itemRectangle = new Rectangle(currentLeft, 1, 14 + TextRenderer.MeasureText(item.Text, Font).Width, pevent.ClipRectangle.Height - 2);
                currentLeft = itemRectangle.Right;

                //Paint
                pevent.Graphics.IntersectClip(itemRectangle);
                using (PaintBreadcrumbItemEventArgs e = new PaintBreadcrumbItemEventArgs(item, pevent.Graphics, itemRectangle))
                    OnPaintItemBackground(e);
            }
        }
        /// <summary>
        /// Performs the work of setting the specified bounds of this control.
        /// </summary>
        /// <param name="x">The new <see cref="Control.Left"/> property value of the control.</param>
        /// <param name="y">The new <see cref="Control.Top"/> property value of the control.</param>
        /// <param name="width">The new <see cref="Control.Width"/> property value of the control.</param>
        /// <param name="height">The new <see cref="Control.Height"/> property value of the control.</param>
        /// <param name="specified">A bitwise combination of the <see cref="BoundsSpecified"/> values.</param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            //Adjust width or height
            if (width < C_MinWidth) width = C_MinWidth;
            if (height < C_MinHeight) height = C_MinHeight;
            base.SetBoundsCore(x, y, width, height, specified); //Set bounds core
        }
        /// <summary>
        /// Raises the <see cref="BorderColorChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnBorderColorChanged(EventArgs e)
        {
            //Redraw
            Refresh();

            //Invoke event
            BorderColorChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Raises the <see cref="PathChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected virtual void OnPathChanged(EventArgs e)
        {
            //Raise event
            PathChanged?.Invoke(this, e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPaintItem(PaintBreadcrumbItemEventArgs e)
        {
            //Draw
            TextRenderer.DrawText(e.Graphics, e.Item.Text, Font,
                new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 14, e.ClipRectangle.Height),
                ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPaintItemBackground(PaintBreadcrumbItemEventArgs e)
        {
            //Do nothing
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ListBindable(BindableSupport.Yes)]
    public sealed class BreadcrumbBarItemCollection : ICollection, IList, IEnumerable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public BreadcrumbBarItem this[string key]
        {
            get
            {
                foreach (object obj in innerList)
                    if (obj is BreadcrumbBarItem item)
                        if (item.Name == key) return item;
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BreadcrumbBarItem this[int index]
        {
            get { return innerList[index] as BreadcrumbBarItem; }
            set { innerList[index] = value; }
        }

        private readonly ArrayList innerList = new ArrayList();
        private readonly BreadcrumbBar m_Owner = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="items"></param>
        public BreadcrumbBarItemCollection(BreadcrumbBar owner, params BreadcrumbBarItem[] items)
        {
            m_Owner = owner;
        }
        public void Add(BreadcrumbBarItem item)
        {
            //Check
            if (item == null) throw new ArgumentNullException(nameof(item));

            //Set owner
            item.Owner = m_Owner;

            //Add
            innerList.Add(item);

            //Refresh
            m_Owner.Refresh();
        }
        public BreadcrumbBarItem Add(string text)
        {
            //Default
            if (text == null) text = string.Empty;
            BreadcrumbBarItem item = new BreadcrumbBarItem(text);

            //Set owner
            item.Owner = m_Owner;

            //Add
            innerList.Add(item);

            //Refresh
            m_Owner.Refresh();

            //Return
            return item;
        }

        object IList.this[int index] { get => innerList[index]; set => innerList[index] = value; }
        int ICollection.Count => innerList.Count;
        object ICollection.SyncRoot => false;
        bool ICollection.IsSynchronized => false;
        bool IList.IsReadOnly => false;
        bool IList.IsFixedSize => false;
        int IList.Add(object value)
        {
            return innerList.Add(value);
        }
        void IList.Clear()
        {
            innerList.Clear();
        }
        bool IList.Contains(object value)
        {
            return innerList.Contains(value);
        }
        void ICollection.CopyTo(Array array, int index)
        {
            innerList.CopyTo(array, index);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerList.GetEnumerator();
        }
        int IList.IndexOf(object value)
        {
            return innerList.IndexOf(value);
        }
        void IList.Insert(int index, object value)
        {
            innerList.Insert(index, value);
        }
        void IList.Remove(object value)
        {
            innerList.Remove(value);
        }
        void IList.RemoveAt(int index)
        {
            innerList.RemoveAt(index);
        }
    }

    /// <summary>
    /// Represents an item in a breadcrumb navigation bar control.
    /// </summary>
    [ToolboxItem(false), Serializable, DesignTimeVisible(false)]
    public class BreadcrumbBarItem : Component, IComponent, ICloneable, ISerializable
    {
        /// <summary>
        /// The name of the <see cref="BreadcrumbBarItem"/>. The default is null.
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get
            {
                if (Site != null)
                {
                    m_Name = Site.Name;
                    return Site.Name;
                }
                else return m_Name;
            }
            set
            {
                m_Name = value;
                if (Site != null) Site.Name = m_Name;
            }
        }
        /// <summary>
        /// The text to display for the item.
        /// </summary>
        public string Text { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string ImageKey { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public int ImageIndex { get; set; } = -1;
        /// <summary>
        /// 
        /// </summary>
        public BreadcrumbBar Owner { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BreadcrumbBarItem OwnerItem { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public BreadcrumbBarItemCollection Items { get; }

        private string m_Name = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbBarItem"/> class.
        /// </summary>
        public BreadcrumbBarItem()
        {
            Items = new BreadcrumbBarItemCollection(Owner);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbBarItem"/> class using the specified text.
        /// </summary>
        /// <param name="text">The item's text.</param>
        public BreadcrumbBarItem(string text)
        {
            Text = text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }
        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }

    public delegate void PaintBreadcrumbItemEventHandler(object sender, PaintBreadcrumbItemEventArgs e);

    public sealed class PaintBreadcrumbItemEventArgs : PaintEventArgs
    {
        public BreadcrumbBarItem Item { get; }

        public PaintBreadcrumbItemEventArgs(BreadcrumbBarItem item, Graphics graphics, Rectangle clipRect) : base(graphics, clipRect)
        {
            Item = item;
        }
    }
}
