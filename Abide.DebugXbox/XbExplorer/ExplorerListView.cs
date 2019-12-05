using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace XbExplorer
{
    /// <summary>
    /// Represents an Explorer-inspired Windows list view control.
    /// </summary>
    internal class ExplorerListView : ListView
    {
        private readonly ImageList smallImageList;
        private readonly ImageList largeImageList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExplorerListView"/> class.
        /// </summary>
        public ExplorerListView() : base()
        {
            SmallImageList = smallImageList = new ImageList() { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new Size(16, 16) };
            LargeImageList = largeImageList = new ImageList() { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new Size(48, 48) };
            PrepareImageLists();
        }
        /// <summary>
        /// Raises the <see cref="Control.HandleCreated"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            //Base procedure
            base.OnHandleCreated(e);

            //Set explorer theme
            int result = SetWindowTheme(Handle, "Explorer", null);
            if (result != 0) SetWindowTheme(Handle, null, null);
        }
        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="ExplorerListView"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            //Base procedures
            base.Dispose(disposing);

            //Dispose
            smallImageList.Dispose();
            largeImageList.Dispose();
        }
        /// <summary>
        /// Clears and rebuilds the image lists.
        /// </summary>
        public void RefreshImageLists()
        {
            //Prepare
            PrepareImageLists();
        }
        
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);
        
        private void PrepareImageLists()
        {
            //Dispose
            foreach (Image image in smallImageList.Images) image.Dispose();
            foreach (Image image in largeImageList.Images) image.Dispose();

            //Clear
            smallImageList.Images.Clear();
            largeImageList.Images.Clear();

            //Get sizes
            int sWidth = SmallImageList.ImageSize.Width;
            int sHeight = SmallImageList.ImageSize.Height;
            int lWidth = LargeImageList.ImageSize.Width;
            int lHeight = LargeImageList.ImageSize.Height;

            //Check
            if (sWidth > 255) sWidth = -1;
            if (sHeight > 255) sHeight = -1;
            if (lWidth > 255) lWidth = -1;
            if (lHeight > 255) lHeight = -1;


            //Add
            smallImageList.Images.Add(new Icon(Properties.Resources.folder_icon, sWidth, sHeight));
            largeImageList.Images.Add(new Icon(Properties.Resources.folder_icon, lWidth, lHeight));

            smallImageList.Images.Add(new Icon(Properties.Resources.new_xbox_icon, sWidth, sHeight));
            largeImageList.Images.Add(new Icon(Properties.Resources.new_xbox_icon, lWidth, lHeight));

            smallImageList.Images.Add(new Icon(Properties.Resources.xbox_icon, sWidth, sHeight));
            largeImageList.Images.Add(new Icon(Properties.Resources.xbox_icon, lWidth, lHeight));

            smallImageList.Images.Add(new Icon(Properties.Resources.xbox_online_icon, sWidth, sHeight));
            largeImageList.Images.Add(new Icon(Properties.Resources.xbox_online_icon, lWidth, lHeight));

            smallImageList.Images.Add(new Icon(Properties.Resources.xbox_offline_icon, sWidth, sHeight));
            largeImageList.Images.Add(new Icon(Properties.Resources.xbox_offline_icon, lWidth, lHeight));

            smallImageList.Images.Add(new Icon(Properties.Resources.drive_icon, sWidth, sHeight));
            largeImageList.Images.Add(new Icon(Properties.Resources.drive_icon, lWidth, lHeight));

            smallImageList.Images.Add(new Icon(Properties.Resources.xbe_icon, sWidth, sHeight));
            largeImageList.Images.Add(new Icon(Properties.Resources.xbe_icon, lWidth, lHeight));

            smallImageList.Images.Add(new Icon(Properties.Resources.application_icon, sWidth, sHeight));
            largeImageList.Images.Add(new Icon(Properties.Resources.application_icon, lWidth, lHeight));

            smallImageList.Images.Add(new Icon(Properties.Resources.file_icon, sWidth, sHeight));
            largeImageList.Images.Add(new Icon(Properties.Resources.file_icon, lWidth, lHeight));
        }
    }
}
