using Abide.AddOnApi;
using Abide.AddOnApi.Halo2;
using System;

namespace Abide.HaloLibrary.Builder.Test
{
    /// <summary>
    /// Represents our test new tag button.
    /// </summary>
    [AddOn]
    public class NewTagButton : AbideMenuButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewTagButton"/> class.
        /// </summary>
        public NewTagButton() : base()
        {
            Name = "Create New Tag";
            Author = "Click16";
            Description = $"Test button for {nameof(MapBuilder)}.";
            Click += NewTagButton_Click;
        }

        private void NewTagButton_Click(object sender, EventArgs e)
        {
            TagId id = TagId.Null;
            if(Map.CreateTag("bitm", @"dev\test_bitmap\test", 4, 108, out id))
            {
                System.Windows.Forms.MessageBox.Show(id.ToString());
                Host.Request(this, "ReloadMap");
            }
        }
    }
}
