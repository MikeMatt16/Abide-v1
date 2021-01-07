using Abide.HaloLibrary.Halo2;
using Abide.Wpf.Modules.ViewModel;

namespace Abide.Wpf.Modules.Tools.Halo2.Retail.TextureEditor
{
    public sealed class TextureEditorViewModel : BaseAddOnViewModel
    {
        private BitmapContainer selectedBitmap;
        private HaloBitmap bitmap = null;

        public HaloBitmap CurrentBitmap
        {
            get => bitmap;
            private set
            {
                if (bitmap != value)
                {
                    bitmap = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public BitmapContainer SelectedBitmap
        {
            get => selectedBitmap;
            set
            {
                if (selectedBitmap != value)
                {
                    selectedBitmap = value;
                    NotifyPropertyChanged();
                }
            }
        }

        protected override void OnSelectedTagChanged()
        {
            if (SelectedTag.GroupTag == HaloTags.bitm)
            {
                CurrentBitmap = new HaloBitmap(SelectedTag);
                CurrentBitmap.Load();
                CurrentBitmap.Process();

                if (CurrentBitmap.BitmapContainers.Count > 0)
                {
                    SelectedBitmap = CurrentBitmap.BitmapContainers[0];
                }
            }
        }
    }
}
