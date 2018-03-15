using Abide.AddOnApi;
using Abide.AddOnApi.Halo2;
using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using YeloDebug;

namespace HUD_Editor.Halo2
{
    public partial class HudEditorForm : Form
    {
        public MapFile Map
        {
            get { return owner?.Map; }
        }
        public IndexEntry SelectedEntry
        {
            get { return owner?.SelectedEntry; }
        }
        public Xbox Xbox
        {
            get { return owner?.Xbox; }
        }
        
        private readonly HudEditorButton owner;
        private Point previousMouseLocation = Point.Empty;
        private readonly Bitmap fullscreenPreview = new Bitmap(640, 480);
        private Dictionary<HaloHud.BitmapWidgetProperties, Bitmap> hudBitmaps = new Dictionary<HaloHud.BitmapWidgetProperties, Bitmap>();
        private HaloHud hud;

        private HudEditorForm()
        {
            InitializeComponent();
        }
        public HudEditorForm (HudEditorButton owner) : this()
        {
            //
            // this
            //
            this.owner = owner;
        }

        private void HudEditorForm_Load(object sender, EventArgs e)
        {
            //Setup
            hudBox.Image = fullscreenPreview;

            //Load?
            if (SelectedEntry?.Root == "nhdt") Tag_Load(SelectedEntry);
        }

        private void WidgetPropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            //Redraw
            HudBox_DrawHud();
        }

        private void WidgetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Set
            widgetPropertyGrid.SelectedObject = widgetComboBox.SelectedItem;

            //Redraw
            HudBox_DrawHud();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            //Save
            if (hud != null) hud.Write();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            //Reload
            Tag_Reload();
        }

        private void HudBox_MouseMove(object sender, MouseEventArgs e)
        {
            //Check
            if (widgetComboBox.SelectedItem != null && widgetComboBox.SelectedItem is HaloHud.BitmapWidgetProperties)
            {
                //Get Bitmap Widget
                HaloHud.BitmapWidgetProperties widget = (HaloHud.BitmapWidgetProperties)widgetComboBox.SelectedItem;

                //Check
                Point widgetLocation = Widget_GetFullscreenLocation(hudBitmaps[widget], widget);
                Size widgetSize = Widget_GetSize(hudBitmaps[widget], widget);

                //Check
                if (e.X >= widgetLocation.X && e.X < widgetLocation.X + widgetSize.Width && e.Y >= widgetLocation.Y && e.Y < widgetLocation.Y + widgetSize.Height)
                {
                    //Set Cursor
                    hudBox.Cursor = Cursors.SizeAll;

                    //Check
                    if (e.Button == MouseButtons.Left)
                    {
                        Point newPoint = new Point(e.X - previousMouseLocation.X, e.Y - previousMouseLocation.Y);
                        Point widgetPoint = new Point(widget.FullscreenPositionOffset.X + newPoint.X, widget.FullscreenPositionOffset.Y + newPoint.Y);
                        widget.FullscreenPositionOffset = widgetPoint;
                        HudBox_DrawHud();
                    }
                }
                else hudBox.Cursor = Cursors.Default;
            }

            //Set
            previousMouseLocation = e.Location;
        }

        private void Tag_Reload()
        {
            //Check
            if (hud != null) hud.Dispose();
            hud = new HaloHud(SelectedEntry, Map);
            foreach (var bitmap in hudBitmaps)
                bitmap.Value.Dispose();
            hudBitmaps.Clear();

            //Load Bitmaps
            foreach (var widget in hud.BitmapWidgets)
                BitmapWidget_LoadBitmap(widget);

            //Add
            widgetComboBox.BeginUpdate();
            widgetComboBox.Items.Clear();

            //Loop
            foreach (var widget in hud.BitmapWidgets)
                widgetComboBox.Items.Add(widget);

            //End
            widgetComboBox.EndUpdate();

            //Select
            if (widgetComboBox.Items.Count > 0) widgetComboBox.SelectedIndex = 0;

            //Draw
            HudBox_DrawHud();
        }

        private void Tag_Load(IndexEntry entry)
        {
            //Check
            if (entry == null) return;
            widgetPropertyGrid.SelectedObject = null;

            //Setup
            Text = $"HUD Editor - {entry.Filename}.{entry.Root}";

            //Check
            if (hud != null) hud.Dispose();
            hud = new HaloHud(entry, Map);
            foreach (var bitmap in hudBitmaps)
                bitmap.Value.Dispose();
            hudBitmaps.Clear();

            //Load Bitmaps
            foreach (var widget in hud.BitmapWidgets)
                BitmapWidget_LoadBitmap(widget);

            //Add
            widgetComboBox.BeginUpdate();
            widgetComboBox.Items.Clear();

            //Loop
            foreach (var widget in hud.BitmapWidgets)
                widgetComboBox.Items.Add(widget);

            //End
            widgetComboBox.EndUpdate();

            //Select
            if (widgetComboBox.Items.Count > 0) widgetComboBox.SelectedIndex = 0;

            //Draw
            HudBox_DrawHud();
        }

        private void BitmapWidget_LoadBitmap(HaloHud.BitmapWidgetProperties widget)
        {
            //Prepare
            Bitmap map = null;

            //Get Tag
            if (widget.BitmapTag != null && Map.IndexEntries[widget.BitmapTag.ID].Root == HaloTags.bitm)
                if (widget.FullscreenSequenceIndex != byte.MaxValue)
                    using (HaloBitmap bitmap = new HaloBitmap(Map.IndexEntries[widget.BitmapTag.ID]))
                    {
                        //Get Widget Bitmap
                        if (widget.FullscreenSequenceIndex < bitmap.BitmapCount)
                            map = bitmap[widget.FullscreenSequenceIndex, 0, 0];
                        if (widget.FullscreenSequenceIndex < bitmap.SequenceCount)
                            map = bitmap[bitmap.Sequences[widget.FullscreenSequenceIndex].Index, 0, 0];

                        //Clone
                        map = (Bitmap)map.Clone();
                    }

            //Add
            hudBitmaps.Add(widget, map);
        }

        private void HudBox_DrawHud()
        {
            //Create Graphics
            if (fullscreenPreview == null) return;
            using (Graphics g = Graphics.FromImage(fullscreenPreview))
            {
                //Clear
                g.Clear(Color.Transparent);

                //Loop
                foreach (HaloHud.BitmapWidgetProperties widgetProperties in hud.BitmapWidgets)
                    if (hudBitmaps.ContainsKey(widgetProperties) && hudBitmaps[widgetProperties] != null)
                        using (Bitmap bitmap = Widget_CreateBitmap(hudBitmaps[widgetProperties], widgetProperties.Flags))
                        {
                            //Get Point
                            Point location = Widget_GetFullscreenLocation(hudBitmaps[widgetProperties], widgetProperties);

                            //Draw
                            g.DrawImage(bitmap, location);

                            //Check
                            if (widgetComboBox.SelectedItem == widgetProperties)
                            {
                                using (Pen selected = new Pen(Color.Black))
                                {
                                    selected.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                                    g.DrawRectangle(selected, new Rectangle(location.X, location.Y, bitmap.Width - 1, bitmap.Height - 1));
                                }
                            }
                        }
            }

            //Refresh
            hudBox.Refresh();
        }

        private Bitmap Widget_CreateBitmap(Bitmap widget, DrawFlags flags)
        {
            //Check
            Bitmap map = (Bitmap)widget.Clone();

            //Prepare bitmap
            if (flags.HasFlag(DrawFlags.MirrorX))
                map = Widget_MirrorX(map);
            if (flags.HasFlag(DrawFlags.MirrorY))
                map = Widget_MirrorY(map);
            if (flags.HasFlag(DrawFlags.FlipX))
                map = Widget_FlipX(map);
            if (flags.HasFlag(DrawFlags.FlipY))
                map = Widget_FlipY(map);

            //Return
            return map;
        }

        private Bitmap Widget_FlipY(Bitmap widget)
        {
            //Create Bitmap
            Bitmap map = new Bitmap(widget.Width, widget.Height);

            //Create Graphics
            using (Graphics g = Graphics.FromImage(map))
            {
                g.TranslateTransform(0, widget.Height);
                g.ScaleTransform(1, -1);
                g.DrawImage(widget, Point.Empty);
                g.ResetTransform();
            }

            //Dispse source map
            widget.Dispose();

            //Return
            return map;
        }

        private Bitmap Widget_FlipX(Bitmap widget)
        {
            //Create Bitmap
            Bitmap map = new Bitmap(widget.Width, widget.Height);

            //Create Graphics
            using (Graphics g = Graphics.FromImage(map))
            {
                g.TranslateTransform(widget.Width, 0);
                g.ScaleTransform(-1, 1);
                g.DrawImage(widget, Point.Empty);
                g.ResetTransform();
            }

            //Dispse source map
            widget.Dispose();

            //Return
            return map;
        }

        private Bitmap Widget_MirrorY(Bitmap widget)
        {
            //Create Bitmap
            Bitmap map = new Bitmap(widget.Width, widget.Height * 2);

            //Create Graphics
            using (Graphics g = Graphics.FromImage(map))
            {
                g.DrawImage(widget, Point.Empty);
                g.TranslateTransform(0, widget.Height * 2);
                g.ScaleTransform(1, -1);
                g.DrawImage(widget, Point.Empty);
                g.ResetTransform();
            }

            //Dispse source map
            widget.Dispose();

            //Return
            return map;
        }

        private Bitmap Widget_MirrorX(Bitmap widget)
        {
            //Create Bitmap
            Bitmap map = new Bitmap(widget.Width * 2, widget.Height);

            //Create Graphics
            using (Graphics g = Graphics.FromImage(map))
            {
                g.DrawImage(widget, Point.Empty);
                g.TranslateTransform(widget.Width * 2, 0);
                g.ScaleTransform(-1, 1);
                g.DrawImage(widget, Point.Empty);
                g.ResetTransform();
            }

            //Dispse source map
            widget.Dispose();

            //Return
            return map;
        }

        private Size Widget_GetSize(Bitmap bitmap, HaloHud.BitmapWidgetProperties widgetProperties)
        {
            //Get width and height
            int cx = bitmap.Width, cy = bitmap.Height;

            //Check Flags
            if (widgetProperties.Flags.HasFlag(DrawFlags.MirrorX)) cx *= 2;
            if (widgetProperties.Flags.HasFlag(DrawFlags.MirrorY)) cy *= 2;

            //Return
            return new Size(cx, cy);
        }

        private Point Widget_GetFullscreenLocation(Bitmap bitmap, HaloHud.BitmapWidgetProperties widgetProperties)
        {
            //Initialize
            int x = widgetProperties.FullscreenPositionOffset.X, y = widgetProperties.FullscreenPositionOffset.Y;

            //Adjust based on anchor
            switch (widgetProperties.Anchor)
            {
                case Halo2.Anchor.HealthAndShield:
                    x += 592;
                    y += 36;
                    break;
                case Halo2.Anchor.WeaponHud:
                    x += 48;
                    y += 36;
                    break;
                case Halo2.Anchor.MotionSensor:
                    x += 48;
                    y += 444;
                    break;
                case Halo2.Anchor.Scoreboard:
                    x += 592;
                    y += 444;
                    break;
                case Halo2.Anchor.Crosshair:
                    x += 320;
                    y += 284;
                    break;
                case Halo2.Anchor.LockOnTarget:
                    x += 320;
                    y += 60;
                    break;
            }

            //Registration Point
            x -= (int)(bitmap.Width * widgetProperties.FullscreenRegistrationPoint.X);
            y -= (int)(bitmap.Height * widgetProperties.FullscreenRegistrationPoint.Y);

            //Return
            return new Point(x, y);
        }

        /// <summary>
        /// Represents the HUD Editor menu button.
        /// </summary>
        [AddOn]
        public sealed class HudEditorButton : AbideMenuButton
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="HudEditorButton"/> class.
            /// </summary>
            public HudEditorButton() : base()
            {
                //
                // this
                //
                MapVersion = Abide.HaloLibrary.MapVersion.Halo2;
                Author = "Click16";
                Name = "HUD Editor";
                Icon = Properties.Resources.HUD_Editor;
                TagFilter.Add("nhdt");
                ApplyTagFilter = true;
                Click += HudEditorButton_Click;
            }
            /// <summary>
            /// Occurs when the button is clicked.
            /// </summary>
            /// <param name="sender">The object that sent the event.</param>
            /// <param name="e">The event args.</param>
            private void HudEditorButton_Click(object sender, EventArgs e)
            {
                //Create
                HudEditorForm editor = new HudEditorForm(this);

                //Closed
                editor.FormClosed += Editor_FormClosed;

                //Show
                editor.Show();
            }
            /// <summary>
            /// Occurs when the HUD editor form is closed.
            /// </summary>
            /// <param name="sender">The editor form.</param>
            /// <param name="e">The form closed event args.</param>
            private void Editor_FormClosed(object sender, FormClosedEventArgs e)
            {
                //Dispose
                if (sender is IDisposable) ((IDisposable)sender).Dispose();
            }
        }
    }
}
