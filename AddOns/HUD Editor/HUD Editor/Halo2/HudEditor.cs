using Abide.AddOnApi.Halo2;
using Abide.HaloLibrary;
using Abide.HaloLibrary.Halo2Map;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HUD_Editor.Halo2
{
    public partial class HudEditor : AbideTool
    {
        private Point previousMouseLocation = Point.Empty;
        private readonly Bitmap fullscreenPreview = new Bitmap(640, 480);
        private Dictionary<HaloHud.BitmapWidgetProperties, Bitmap> hudBitmaps = new Dictionary<HaloHud.BitmapWidgetProperties, Bitmap>();
        private HaloHud hud;

        public HudEditor()
        {
            InitializeComponent();
        }

        private void HudEditor_Load(object sender, EventArgs e)
        {
            //Setup
            hudBox.Image = fullscreenPreview;
        }

        private void HudEditor_TagSelected(object sender, EventArgs e)
        {
            //Setup
            if (SelectedEntry != null && SelectedEntry.Root == "nhdt")
                tag_Load(SelectedEntry);
        }

        private void widgetPropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            //Redraw
            hudBox_DrawHud();
        }

        private void widgetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Set
            widgetPropertyGrid.SelectedObject = widgetComboBox.SelectedItem;

            //Redraw
            hudBox_DrawHud();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //Save
            if (hud != null) hud.Write();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            //Reload
            tag_Reload();
        }

        private void hudBox_MouseMove(object sender, MouseEventArgs e)
        {
            //Check
            if (widgetComboBox.SelectedItem != null && widgetComboBox.SelectedItem is HaloHud.BitmapWidgetProperties)
            {
                //Get Bitmap Widget
                HaloHud.BitmapWidgetProperties widget = (HaloHud.BitmapWidgetProperties)widgetComboBox.SelectedItem;

                //Check
                Point widgetLocation = widget_GetFullscreenLocation(hudBitmaps[widget], widget);
                Size widgetSize = widget_GetSize(hudBitmaps[widget], widget);

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
                        hudBox_DrawHud();
                    }
                }
                else hudBox.Cursor = Cursors.Default;
            }

            //Set
            previousMouseLocation = e.Location;
        }

        private void tag_Reload()
        {
            //Check
            if (hud != null) hud.Dispose();
            hud = new HaloHud(SelectedEntry, Map);
            foreach (var bitmap in hudBitmaps)
                bitmap.Value.Dispose();
            hudBitmaps.Clear();

            //Load Bitmaps
            foreach (var widget in hud.BitmapWidgets)
                bitmapWidget_LoadBitmap(widget);

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
            hudBox_DrawHud();
        }

        private void tag_Load(IndexEntry entry)
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
                bitmapWidget_LoadBitmap(widget);

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
            hudBox_DrawHud();
        }

        private void bitmapWidget_LoadBitmap(HaloHud.BitmapWidgetProperties widget)
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

        private void hudBox_DrawHud()
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
                        using (Bitmap bitmap = widget_CreateBitmap(hudBitmaps[widgetProperties], widgetProperties.Flags))
                        {
                            //Get Point
                            Point location = widget_GetFullscreenLocation(hudBitmaps[widgetProperties], widgetProperties);

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

        private Bitmap widget_CreateBitmap(Bitmap widget, DrawFlags flags)
        {
            //Check
            Bitmap map = (Bitmap)widget.Clone();

            //Prepare bitmap
            if (flags.HasFlag(DrawFlags.MirrorX))
                map = widget_MirrorX(map);
            if (flags.HasFlag(DrawFlags.MirrorY))
                map = widget_MirrorY(map);
            if (flags.HasFlag(DrawFlags.FlipX))
                map = widget_FlipX(map);
            if (flags.HasFlag(DrawFlags.FlipY))
                map = widget_FlipY(map);

            //Return
            return map;
        }

        private Bitmap widget_FlipY(Bitmap widget)
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

        private Bitmap widget_FlipX(Bitmap widget)
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

        private Bitmap widget_MirrorY(Bitmap widget)
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

        private Bitmap widget_MirrorX(Bitmap widget)
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

        private Size widget_GetSize(Bitmap bitmap, HaloHud.BitmapWidgetProperties widgetProperties)
        {
            //Get width and height
            int cx = bitmap.Width, cy = bitmap.Height;

            //Check Flags
            if (widgetProperties.Flags.HasFlag(DrawFlags.MirrorX)) cx *= 2;
            if (widgetProperties.Flags.HasFlag(DrawFlags.MirrorY)) cy *= 2;

            //Return
            return new Size(cx, cy);
        }

        private Point widget_GetFullscreenLocation(Bitmap bitmap, HaloHud.BitmapWidgetProperties widgetProperties)
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
    }
}
